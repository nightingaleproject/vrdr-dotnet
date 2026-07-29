using System;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Newtonsoft.Json;
using Hl7.Fhir.Utility;
using VRDR;

namespace VRDR
{
    /// <summary>Helper class to extract Replace Status Flag value based on the MessageHeader Resource</summary>
    public static class ReplaceStatusFlag
    {
        
        /// <summary>This variable stores the messageheader destination endpoint information so to compute the Replace Status Flag value </summary>
        public static List<string> destinationEndpoint;
       
       /// <summary>This variable stores the messageheader event type information</summary>
        public static string eventUri;
        /// <summary>This variable stores the computed replaceStatusFlagCode value </summary>
        public static string replaceStatusFlagCode;

          /// <summary>This class stores the messageheader destination endpoint and the event type information so to compute the Replace Status Flag value </summary>
        /// <param name="destinationEndpts"> Death Record Destination Endpoints specified in the MessageHeader Resource</param>
        /// <param name="eventUri"> Death Record Event Type specified in the MessageHeader Resource</param>
        public static void GetReplaceStatusFlagCode(List<string> destinationEndpts, String eventUri)
        {
  
             //Extract StatusFlagCode value which should be one of these values:
             // 0 -- For original Submission
             // 1-- For updated Submission
             // 2 -- For submission not to send to NCHS
            
             string StatusFlagCode = DeathRecord.DestinationFoundusingStringParameters(destinationEndpts, eventUri);
             replaceStatusFlagCode = StatusFlagCode;
       
        }
    }
}