using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using FhirDeathRecord;

namespace FhirDeathRecord.HTTP
{
    class Program
    {
        static void Main(string[] args)
        {
            FhirDeathRecordListener IJEXMLListener = new FhirDeathRecordListener(SendXMLIJEResponse, "http://localhost:8080/ije-xml/");
            FhirDeathRecordListener IJEJSONListener = new FhirDeathRecordListener(SendJSONIJEResponse, "http://localhost:8080/ije-json/");
            FhirDeathRecordListener XMLListener = new FhirDeathRecordListener(SendFHIRXMLResponse, "http://localhost:8080/xml/");
            FhirDeathRecordListener JSONListener = new FhirDeathRecordListener(SendFHIRJSONResponse, "http://localhost:8080/json/");
            IJEXMLListener.Run();
            IJEJSONListener.Run();
            XMLListener.Run();
            JSONListener.Run();

            Console.WriteLine("Example FHIR Death Record Translation Service.");

            IJEXMLListener.Stop();
            IJEJSONListener.Stop();
            XMLListener.Stop();
            JSONListener.Stop();
        }

    
        public static string SendXMLIJEResponse(HttpListenerRequest request)
        {
            string xmlText = GetBodyContent(request);
            DeathRecord deathRecord = new DeathRecord(xmlText);
            IJEMortality ije = new IJEMortality(deathRecord);
            return ije.ToString();
        }
        
        public static string SendJSONIJEResponse(HttpListenerRequest request)
        {
            string jsonText = GetBodyContent(request);
            DeathRecord deathRecord = new DeathRecord(jsonText);
            IJEMortality ije = new IJEMortality(deathRecord);
            return ije.ToString();
        }
    
        public static string SendFHIRXMLResponse(HttpListenerRequest request)
        {
            string IJEText = GetBodyContent(request);
            IJEMortality ije = new IJEMortality(IJEText);
            return ije.ToDeathRecord().ToXML();   
        }

        public static string SendFHIRJSONResponse(HttpListenerRequest request)
        {
            string IJEText = GetBodyContent(request);
            IJEMortality ije = new IJEMortality(IJEText);
            return ije.ToDeathRecord().ToJSON();
        }

        public static string GetBodyContent(HttpListenerRequest request)
        {
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        
    }
}
