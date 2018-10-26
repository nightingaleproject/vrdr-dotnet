using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;

namespace FhirDeathRecord.HTTP
{
    class Program
    {
    static void Main(string[] args)
    {
        FhirDeathRecordServer fhirDeathRecordServer = new FhirDeathRecordServer(SendIJEResponse, "http://localhost:8080/ije/");
        fhirDeathRecordServer.Run();
        Console.WriteLine("Example FHIR webserver. Press a key to quit.");
        Console.ReadKey();
        fhirDeathRecordServer.Stop();
    }

 
    public static string SendIJEResponse(HttpListenerRequest request)
    {
        return string.Format("IJE Response Here", DateTime.Now);    
    }
    
  
 
    public static string SendFHIRResponse(HttpListenerRequest request)
    {
        return string.Format("FHIR Response Here", DateTime.Now);    
    }
}
