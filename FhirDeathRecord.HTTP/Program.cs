using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ServiceProcess;
using FhirDeathRecord;

namespace FhirDeathRecord.HTTP
{
    class Program
    {
        static void Main(string[] args)
        {
            FhirDeathRecordListener Listener = new FhirDeathRecordListener(SendResponse, "http://*:8080/");
            Listener.Run();
            ManualResetEvent _quitEvent = new ManualResetEvent(false);
            _quitEvent.WaitOne();
            Listener.Stop();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            string requestBody = GetBodyContent(request);
            DeathRecord deathRecord = null;

            // Look at content type to determine input format; be permissive in what we accept as format specification
            switch (request.ContentType)
            {
                case string ijeType when new Regex(@"ije").IsMatch(ijeType): // application/ije
                    IJEMortality ije = new IJEMortality(requestBody);
                    deathRecord = ije.ToDeathRecord();
                    break;
                case string jsonType when new Regex(@"json").IsMatch(jsonType): // application/fhir+json
                case string xmlType when new Regex(@"xml").IsMatch(xmlType): // application/fhir+xml
                default:
                    deathRecord = new DeathRecord(requestBody);
                    break;
            }

            // Look at URL extension to determine output format; be permissive in what we accept as format specification
            switch (request.RawUrl)
            {
                case string url when new Regex(@"(ije|mor)$").IsMatch(url): // .mor or .ije
                    IJEMortality ije = new IJEMortality(deathRecord);
                    return ije.ToString();
                case string url when new Regex(@"json$").IsMatch(url): // .json
                    return deathRecord.ToJSON();
                case string url when new Regex(@"xml$").IsMatch(url): // .xml
                    return deathRecord.ToXML();
            }

            return "";
        }

        public static string GetBodyContent(HttpListenerRequest request)
        {
            using (System.IO.Stream body = request.InputStream)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

    }
}
