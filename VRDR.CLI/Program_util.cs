using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using System.Net.Http;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VRDR;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode; 
using JsonDiffPatchDotNet;


namespace VRDR.CLI
{
    partial class Program
    {

            // CompareTwo: Perform a field by field comparison of d1 to d2
            private static int CompareTwo(DeathRecord d1, DeathRecord d2)
            {
                int good = 0;
                int bad = 0;
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach (PropertyInfo property in properties)
                {
                    // Console.WriteLine($"Property: Name: {property.Name.ToString()} Type: {property.PropertyType.ToString()}");
                    string one;
                    string two;
                    if (property.PropertyType.ToString() == "System.Collections.Generic.Dictionary`2[System.String,System.String]")
                    {
                        Dictionary<string, string> oneDict = (Dictionary<string, string>)property.GetValue(d1);
                        Dictionary<string, string> twoDict = (Dictionary<string, string>)property.GetValue(d2);
                        // Ignore empty entries in the dictionary so they don't throw off comparisons.
                        one = String.Join(", ", oneDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                        //two = String.Join(", ", twoDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                        two = String.Join(", ", twoDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                    }
                    else if (property.PropertyType.ToString() == "System.String[]")
                    {
                        one = String.Join(", ", (string[])property.GetValue(d1));
                        two = String.Join(", ", (string[])property.GetValue(d2));
                    }
                    else
                    {
                        one = Convert.ToString(property.GetValue(d1));
                        //two = Convert.ToString(property.GetValue(d2));
                        two = Convert.ToString(property.GetValue(d2));
                    }
                    if (one.ToLower() != two.ToLower())
                    {
                        Console.WriteLine("[***** MISMATCH *****]\t" + $"\"{one}\" (property: {property.Name}) does not equal \"{two}\"" + $"      1:\"{one}\"  3:\"{two}\"");
                        bad++;
                    }
                    else
                    {
                        // We don't actually need to see all the matches and it makes it hard to see the mismatches
                        // Console.WriteLine("[MATCH]\t" + $"\"{one}\" (property: {property.Name}) equals \"{three}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\"");
                        good++;
                    }
                }
                Console.WriteLine($"\n{bad} mismatches out of {good + bad} total properties checked.");
                if (bad > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            // CompareThree: Perform afield by field comparison of d1 to d2 to d3 and highlight differences
            private static int CompareThree(DeathRecord d1, DeathRecord d2, DeathRecord d3)
            {
                int good = 0;
                int bad = 0;
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                HashSet<string> skipPropertyNames = new HashSet<string>() { "UsualOccupationCoded", "UsualIndustryCoded", "DeathRecordIdentifier", "CausesOfDeath", "AgeAtDeathYears", "AgeAtDeathMonths", "AgeAtDeathDays", "AgeAtDeathHours", "AgeAtDeathMinutes" };
                foreach (PropertyInfo property in properties)
                {
                    // Console.WriteLine($"Property: Name: {property.Name.ToString()} Type: {property.PropertyType.ToString()}");
                    string one;
                    string two;
                    string three;
                    if (skipPropertyNames.Contains(property.Name))
                    {
                        continue;
                    }
                    if (property.PropertyType.ToString() == "System.Collections.Generic.Dictionary`2[System.String,System.String]")
                    {
                        Dictionary<string, string> oneDict = (Dictionary<string, string>)property.GetValue(d1);
                        Dictionary<string, string> twoDict = (Dictionary<string, string>)property.GetValue(d2);
                        Dictionary<string, string> threeDict = (Dictionary<string, string>)property.GetValue(d3);
                        // Ignore empty entries in the dictionary so they don't throw off comparisons.
                        one = String.Join(", ", oneDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                        two = String.Join(", ", twoDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                        three = String.Join(", ", threeDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                    }
                    else if (property.PropertyType.ToString() == "System.String[]")
                    {
                        one = String.Join(", ", (string[])property.GetValue(d1));
                        two = String.Join(", ", (string[])property.GetValue(d2));
                        three = String.Join(", ", (string[])property.GetValue(d3));
                    }
                    else
                    {
                        one = Convert.ToString(property.GetValue(d1));
                        two = Convert.ToString(property.GetValue(d2));
                        three = Convert.ToString(property.GetValue(d3));
                    }
                    if (one.ToLower() != three.ToLower())
                    {
                        Console.WriteLine("[***** MISMATCH *****]\t" + $"\"{one}\" (property: {property.Name}) does not equal \"{three}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\"");
                        bad++;
                    }
                    else
                    {
                        // We don't actually need to see all the matches and it makes it hard to see the mismatches
                        // Console.WriteLine("[MATCH]\t" + $"\"{one}\" (property: {property.Name}) equals \"{three}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\"");
                        good++;
                    }
                }
                Console.WriteLine($"\n{bad} mismatches out of {good + bad} total properties checked.");
                if (bad > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        // VRDR STU2.2 and STU3 are not compatible.  Content that transitioned from VRDR to VRCL as part of the harmonization changed identifiers.
        // There will be jurisdictions using these different versions for a while, so converting between the versions is likely required.

        // CreateSTU2toSTU3Mapping:  Reverse the sense of the STU3 to STU2 mapping.
        static Dictionary<string, string> CreateSTU2toSTU3Mapping(Dictionary<string, string> urisSTU3toSTU2)
        {
            var revUrisSTU3toSTU2 = new Dictionary<string, string>();
            foreach (var kvp in urisSTU3toSTU2)
            {
                revUrisSTU3toSTU2[kvp.Value] = kvp.Key;
            }
            return revUrisSTU3toSTU2;
        }

        // ConvertVersionJSON:  The boolean STU3toSTU2 should be true when used in this library that supports STU3.  
        // The same code could be used in the vrdr-dotnet library that supports VRDR STU2.2, with STU3toSTU2 set to false.
        static void ConvertVersion(string pOutputFile, string pInputFile, bool STU3toSTU2, bool jsonConversion)
        {
            var uris = UrisSTU3toSTU2;
            var dateTimeUris = dateTimeComponentsSTU3toSTU2;
            Bundle bundle; 
            string newContent;

            if (!STU3toSTU2)
            { // The mapping is bidirectional.  Depending on which direction, we flip the map.
                uris = CreateSTU2toSTU3Mapping(UrisSTU3toSTU2);
                dateTimeUris = CreateSTU2toSTU3Mapping(dateTimeComponentsSTU3toSTU2);
            }
            string content = File.ReadAllText(pInputFile);
            // Iterate through the mapped codesystem strings, and replace them one by one
            foreach (var kvp in uris)
            {
                content = content.Replace(kvp.Key, kvp.Value);
            }
            // Fix an observation's code and CodeSystem.  This can't be done using string replace.
            if(jsonConversion){ // JSON Conversion
                ParserSettings parserSettings = new ParserSettings
                {
                    AcceptUnknownMembers = true,
                    AllowUnrecognizedEnums = true,
                    PermissiveParsing = true
                };
                FhirJsonParser parser = new FhirJsonParser(parserSettings);
                bundle = parser.Parse<Bundle>(content);
            }else{
                //XML Conversion
                // Parse the FHIR XML into a Resource object
                    var parser = new FhirXmlParser();
                    bundle = parser.Parse<Bundle>(content);
            }
            // Scan through all Observations to make sure they all have codes!
            foreach (var entry  in bundle.Entry)
            {
                if (entry.Resource is Observation){
                    Observation obs = (Observation)entry.Resource;
                    if (obs.Code == null || obs.Code.Coding == null || obs.Code.Coding.FirstOrDefault() == null || obs.Code.Coding.First().Code == null)
                    {
                        continue;
                    }
                    if (!STU3toSTU2)
                    {
                        switch (obs.Code.Coding.First().Code)
                        {
                            case "BR":
                             obs.Code = new CodeableConcept("http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-observations-cs", "childbirthrecordidentifier", "Birth Record Identifier of Child", null);
                                break;
                        }
                    }
                    else
                    {
                        switch (obs.Code.Coding.First().Code)
                        {
                            case "childbirthrecordidentifier":
                                obs.Code = new CodeableConcept(CodeSystems.HL7_identifier_type, "BR", "Birth registry number", null);
                                break;
                        }
                    }
                }  
            }

            // Recursively update all extensions in the entire bundle
            UpdateExtensionsRecursively(bundle, dateTimeUris);

            
            if (jsonConversion){ // Serialize the bundle as JSON
                newContent = bundle.ToJson(new FhirJsonSerializationSettings { Pretty = true, AppendNewLine = true });
            }else{
                var serializer = new FhirXmlSerializer();   
                newContent = serializer.SerializeToString(bundle);
            }
            File.WriteAllText(pOutputFile, newContent);
    }

    // UpdateExtensionsRecursively:    
    // Utility routine to fill the gap in Firely FHIRPath capabilities.
    static void UpdateExtensionsRecursively(Base fhirElement, Dictionary<string, string> replacements)
    {
        if (fhirElement == null) return;

        // Check if this element has extensions (any FHIR element can!)
        if (fhirElement is IExtendable extendable)
        {
            if (extendable.Extension != null)
            {
                foreach (var ext in extendable.Extension)
                {
                    // If the URL matches, update it
                    if (replacements.ContainsKey(ext.Url))
                    {
                        ext.Url = replacements[ext.Url];
                    }

                    // Recursively process nested extensions
                    if (ext.Extension != null && ext.Extension.Any())
                    {
                        UpdateExtensionsRecursively(ext, replacements);
                    }
                }
            }
        }

        // Recursively process all properties of the current resource
        foreach (var property in fhirElement.GetType().GetProperties())
        {
            if (typeof(Base).IsAssignableFrom(property.PropertyType))
            {
                // Single FHIR element (e.g., Patient.name)
                var value = property.GetValue(fhirElement) as Base;
                if (value != null) UpdateExtensionsRecursively(value, replacements);
            }
            else if (typeof(IEnumerable<Base>).IsAssignableFrom(property.PropertyType))
            {
                // List of FHIR elements (e.g., Bundle.entry, Patient.name)
                var values = property.GetValue(fhirElement) as IEnumerable<Base>;
                if (values != null)
                {
                    foreach (var item in values)
                    {
                        UpdateExtensionsRecursively(item, replacements);
                    }
                }
            }
        }
    }


    // CompareJsonIgnoringOrderAndSpacing:    
    // Compare the content of two json strings ignoring differences due to node ordering or white space.
    static bool CompareJsonIgnoringOrderAndSpacing(string json1, string json2)
    {
        var jdp = new JsonDiffPatchDotNet.JsonDiffPatch();
        var left = JToken.Parse(json1);
        var right = JToken.Parse(json2);
        
        // Sort both JSON objects
        var sortedLeft = SortJson(left);
        var sortedRight = SortJson(right);
        // bool same = (String.Compare(sortedLeft.ToString(), sortedRight.ToString()) == 0);
        // string samestring = (same?"same":"different");
        // Console.WriteLine("Two Json strings are " + samestring);
        // File.WriteAllText("sortedLeft.json", sortedLeft.ToString());
        // File.WriteAllText("sortedRight.json", sortedRight.ToString());
        
        // Compare the sorted JSON objects
        var diff = jdp.Diff(sortedLeft, sortedRight);
        string samestring = (diff == null?"same":"different");
        // Print the diff to the console
        if (diff != null)
        {
            Console.WriteLine("Differences:");
            Console.WriteLine(diff.ToString());
        }
        return diff == null;
    }

    static JToken SortJson(JToken token)
    {
        if (token is JObject jObj)
        {
            var properties = jObj.Properties().ToList();
            var sortedObj = new JObject();
            foreach (var prop in properties.OrderBy(p => p.Name))
            {
                sortedObj.Add(prop.Name, SortJson(prop.Value));
            }
            return sortedObj;
        }
        else if (token is JArray jArr)
        {
            return new JArray(jArr.Select(SortJson).OrderBy(t => t.ToString()));
        }
        else
        {
            return token;
        }
    }

    }
}