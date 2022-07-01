using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace VRDR.HTTP
{
    public class Program
    {
        public VRDRListener Listener;

        public Program()
        {
            Listener = new VRDRListener(SendResponse, "http://*:8080/");
        }

        public void Start()
        {
            Listen();
            ManualResetEvent _quitEvent = new ManualResetEvent(false);
            _quitEvent.WaitOne();
            Stop();
        }

        public void Listen()
        {
            Listener.Run();
        }

        public void Stop()
        {
            Listener.Stop();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            string requestBody = GetBodyContent(request);
            DeathRecord deathRecord = null;

            Console.WriteLine(
                $"Request from: {request.UserHostAddress}, type: {request.ContentType}, url: {request.RawUrl}.");

            // Look at content type to determine input format; be permissive in what we accept as format specification
            try
            {
                switch (request.ContentType)
                {
                    case string ijeType when new Regex(@"ije").IsMatch(ijeType): // application/ije
                        IJEMortality ije = new IJEMortality(requestBody);
                        deathRecord = ije.ToDeathRecord();
                        break;
                    case string nightingaleType when new Regex(@"nightingale").IsMatch(nightingaleType):
                        deathRecord = Nightingale.FromNightingale(requestBody);
                        break;
                    case string jsonType when new Regex(@"json").IsMatch(jsonType): // application/fhir+json
                    case string xmlType when new Regex(@"xml").IsMatch(xmlType): // application/fhir+xml
                    default:
                        deathRecord = new DeathRecord(requestBody);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            // Look at URL extension to determine output format; be permissive in what we accept as format specification
            string result = "";
            try
            {
                switch (request.RawUrl)
                {
                    case string url when new Regex(@"(ije|mor)$").IsMatch(url): // .mor or .ije
                        IJEMortality ije = new IJEMortality(deathRecord);
                        result = ije.ToString();
                        break;
                    case string url when new Regex(@"json$").IsMatch(url): // .json
                        result = deathRecord.ToJSON();
                        break;
                    case string url when new Regex(@"xml$").IsMatch(url): // .xml
                        result = deathRecord.ToXML();
                        break;
                    case string url when new Regex(@"nightingale$").IsMatch(url): // .nightingale
                        result = Nightingale.ToNightingale(deathRecord);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public static string GetBodyContent(HttpListenerRequest request)
        {
            using (Stream body = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static String GenerateJsonResponse(string type, string data)
        {
            var response = "{" +
                               "\"type\": \"" + type + "\"," +
                               "\"detail\": \"" + data + "\"" +
                               "}";
            return response;
        }
    }
}