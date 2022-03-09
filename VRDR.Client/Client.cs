using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using VRDR;
using RestSharp;

namespace nvssclient.lib;
public class Client
{
    /// <summary>The API url</summary>
    public String Url { get; }
    /// <summary>Whether the client is running locally</summary>
    public bool LocalTesting { get; }
    /// <summary>The credentials to access the API server</summary>
    public Credentials Credentials { get; }
    /// <summary>The token to access the API server</summary>
    public string Token { get; set; }
    /// <summary>Constructor</summary>
    public Client() { }
    /// <summary>Constructor</summary>
    private HttpClient client = new HttpClient();
    public Client(String url, bool local, Credentials credentials)
    {
        this.Url = url;
        this.LocalTesting = local;
        this.Credentials = credentials;
    }
    // GetMessageResponsesAsync makes a GET request to the NVSS FHIR API server for new messages
    // responses since the provided timestamp
    public String GetMessageResponsesAsync(String lastUpdated)
    {
        var address = this.Url;
        Console.WriteLine($">>> Get messages since: {lastUpdated}");

        // if testing against the NVSS FHIR API server, add the authentication token
        if (!this.LocalTesting){
            if (String.IsNullOrEmpty(this.Token))
            {
                this.Token = this.Credentials.GetAuthorizeToken();
            }
            string authorization = this.Token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
        }

        if (lastUpdated != null){
            address = this.Url + "?lastUpdated=" + lastUpdated;
        }
        var response = client.GetAsync(address).Result;
        
        if (response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            return content;
        }
        else
        {
            Console.WriteLine(response.StatusCode);
            return "";
        }

    }

    // PostMessageAsync POSTS a single message to the NVSS FHIR API server for processing
    public Boolean PostMessageAsync(BaseMessage message)
    {

        var json = message.ToJSON();
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        //using var client = new HttpClient();

        // if testing against the NVSS FHIR API server, add the authentication token
        if (!this.LocalTesting){
            if (String.IsNullOrEmpty(this.Token))
            {
                this.Token = this.Credentials.GetAuthorizeToken();
            }
            string authorization = this.Token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
        }

        var response = client.PostAsync(this.Url, data).Result;
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($">>> Successfully submitted {message.MessageId} of type {message.GetType().Name}");
            return true;
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // unauthorized, refresh token
            Console.WriteLine($">>> Unauthorized error submitting {message.MessageId}, status: {response.StatusCode}");
            return false;
        }
        else
        {
            Console.WriteLine($">>> Error submitting {message.MessageId}, status: {response.StatusCode}");
            return false;
        }
    }
}
