using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using FhirDeathRecord;

namespace FhirDeathRecord.HTTP
{
    class Program
    {
        static void Main(string[] args)
        {
            FhirDeathRecordListener XMLIJEListener = new FhirDeathRecordListener(SendXMLIJEResponse, "http://*:8080/xml-ije/");
            FhirDeathRecordListener JSONLIJEistener = new FhirDeathRecordListener(SendJSONIJEResponse, "http://*:8080/json-ije/");
            FhirDeathRecordListener IJEXMLListener = new FhirDeathRecordListener(SendFHIRXMLResponse, "http://*:8080/ije-xml/");
            FhirDeathRecordListener IJEJSONListener = new FhirDeathRecordListener(SendFHIRJSONResponse, "http://*:8080/ije-json/");
            XMLIJEListener.Run();
            JSONLIJEistener.Run();
            IJEXMLListener.Run();
            IJEJSONListener.Run();

            Console.WriteLine("Example FHIR Death Record Translation Service.");

            ManualResetEvent _quitEvent = new ManualResetEvent(false);
            _quitEvent.WaitOne();

            XMLIJEListener.Stop();
            JSONLIJEistener.Stop();
            IJEXMLListener.Stop();
            IJEJSONListener.Stop();
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
