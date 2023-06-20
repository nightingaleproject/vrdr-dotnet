using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace VRDR;

public class FilterService
{
    
    /// <summary>The filter array.</summary>
    private string[] nchsIjeFilterArray;

    /// <summary>The FHIR fields that should be passed through.</summary>
    private string[] passthroughDeathRecordProperties;

    /// <summary>The mapping between IJE fields and FHIR fields.</summary>
    private Dictionary<string, string[]> ijeToFhirMappingDictionary;

    /// <summary>A class that takes a Death Record and removes the fields that are not present in the filter array.</summary>
    public FilterService(string nchsIjeFilterFileLocation, string ijeToFhirMappingFileLocation, bool filterIsFile = true)
    {
        // The passthrough fields
        string nchsIjeFilterText;
        if (filterIsFile)
        {
            nchsIjeFilterText = File.ReadAllText(nchsIjeFilterFileLocation);
            nchsIjeFilterArray = JsonConvert.DeserializeObject<string[]>(nchsIjeFilterText);
        }
        else
        {
            nchsIjeFilterArray = JsonConvert.DeserializeObject<string[]>(nchsIjeFilterFileLocation);
        }
        
        nchsIjeFilterArray = nchsIjeFilterArray?.Select(e => e.ToUpper()).ToArray();

        // The mappings
        string ijeToFhirMappingText = File.ReadAllText(ijeToFhirMappingFileLocation);
        ijeToFhirMappingDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(ijeToFhirMappingText);

        // Get distinct Death Record properties
        var deathRecordProperties = new List<string>();
        foreach (var ijeField in nchsIjeFilterArray)
        {
            string[] drpList;
            if (ijeToFhirMappingDictionary.TryGetValue(ijeField.ToUpper(), out drpList))
            {
                deathRecordProperties.AddRange(drpList);
            }
        }

        deathRecordProperties.RemoveAll(e => e == "NOTFOUND");
        passthroughDeathRecordProperties = deathRecordProperties.Distinct().ToArray();
    }

    /// <summary>
    /// The method that performs that actual filtering. This is called after an instance of this class is made
    /// </summary>
    /// <param name="message">base message to be filtered</param>
    /// <returns>The filtered base message</returns>
    public BaseMessage filterMessage(BaseMessage message)
    {
        Type messageType = message.GetType();

        // Get the original deathRecord from 
        // DeathRecordSubmissionMessage or DeathRecordUpdateMessage

        DeathRecord dr;
        if (messageType == typeof(DeathRecordSubmissionMessage))
        {
            var drsm = message as DeathRecordSubmissionMessage;
            dr = drsm?.DeathRecord;
        }
        else if (messageType == typeof(DeathRecordUpdateMessage))
        {
            var drum = message as DeathRecordUpdateMessage;
            dr = drum?.DeathRecord;
        }
        else
        {
            return message;
        }

        DeathRecord outputRecord = new DeathRecord();

        foreach (var property in passthroughDeathRecordProperties)
        {
            // Console.WriteLine($"Copying passthrough property, {property}");

            // if property has a subproperty
            if (property.Contains("."))
            {
                string[] keypath = property.Split(".");
                PropertyInfo p = typeof(DeathRecord).GetProperty(keypath[0]);
                Dictionary<string, string> dictionary = p?.GetValue(dr) as Dictionary<string, string>;
                string value;
                if (dictionary.TryGetValue(keypath[1], out value))
                {
                    Dictionary<string, string> updatedDictionary =
                        (Dictionary<string, string>)p?.GetValue(outputRecord);
                    updatedDictionary[keypath[1]] = value;
                    if (updatedDictionary.Count > 0)
                    {
                        p?.SetValue(outputRecord, updatedDictionary);
                    }
                }
                else
                {
                    // Need to think about whether this should be an error or if there are situations where a
                    // dictionary might not always have a value for what is actually a valid key
                    Console.WriteLine($"Warning: dictionary {keypath[0]} does not have key {keypath[1]}");
                }
            }
            // copy the entire array property
            else if (property.Contains("[]"))
            {
                string propertyName = property.Split("[]")[0];
                PropertyInfo p = typeof(DeathRecord).GetProperty(propertyName);
                string x = p?.GetValue(dr)?.ToString();
                if (!String.IsNullOrEmpty(x))
                {
                    p.SetValue(outputRecord, p.GetValue(dr));
                }
            }
            // copy the entire property
            else
            {
                PropertyInfo p = typeof(DeathRecord).GetProperty(property);
                string x = p?.GetValue(dr)?.ToString();
                if (!String.IsNullOrEmpty(x))
                {
                    p?.SetValue(outputRecord, p.GetValue(dr));
                }
            }
        }

        // Done copying properties to the new DeathRecord outputRecord
        // apply the outputRecord to the original message based on its type

        if (messageType == typeof(DeathRecordSubmissionMessage))
        {
            var drsm = message as DeathRecordSubmissionMessage;
            drsm.DeathRecord = outputRecord;
            return drsm;
        }
        else if (messageType == typeof(DeathRecordUpdateMessage))
        {
            var drum = message as DeathRecordUpdateMessage;
            drum.DeathRecord = outputRecord;
            return drum;
        }
        else
        {
            return message;
        }
    }
}