using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using VRDR;


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
    public HttpResponseMessage GetMessageResponsesAsync(String lastUpdated)
    {
        var address = this.Url;
        Console.WriteLine($">>> Get messages since: {lastUpdated}");

        // if testing against the NVSS FHIR API server, add the authentication token
        if (!this.LocalTesting){
            if (String.IsNullOrEmpty(this.Token))
            {
                HttpResponseMessage authResponse = RefreshAccessToken();
                // return authentication error
                if (!authResponse.IsSuccessStatusCode)
                {
                    return authResponse;
                }
            }
        }

        if (lastUpdated != null){
            address = this.Url + "?_since=" + lastUpdated;
        }
        var response = client.GetAsync(address).Result;

        // check if the token expired, refresh and try again
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            HttpResponseMessage authRetry = RefreshAccessToken();
            // return authentication error
            if (!authRetry.IsSuccessStatusCode)
            {
                return authRetry;
            }
            response = client.GetAsync(address).Result;
        }
        
        return response;
    }

    // PostMessageAsync POSTS a single message to the NVSS FHIR API server for processing
    public HttpResponseMessage PostMessageAsync(BaseMessage message)
    {

        var json = message.ToJSON();
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // if testing against the NVSS FHIR API server, add the authentication token
        if (!this.LocalTesting){
            if (String.IsNullOrEmpty(this.Token))
            {
                HttpResponseMessage authResponse = RefreshAccessToken();
                // return authentication error
                if (!authResponse.IsSuccessStatusCode)
                {
                    return authResponse;
                }
            }
        }

        var response = client.PostAsync(this.Url, data).Result;

        // check if the token expired, refresh and try again
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            HttpResponseMessage authRetry = RefreshAccessToken();
            // return authentication error
            if (!authRetry.IsSuccessStatusCode)
            {
                return authRetry;
            }
            response = client.PostAsync(this.Url, data).Result;
        }

        return response;
    }

    private HttpResponseMessage RefreshAccessToken(){
        HttpResponseMessage response = GetAuthorizeToken();
        if (response.IsSuccessStatusCode)
        {
            String content = response.Content.ToString();
            // parse the response to get the access token
            if (!String.IsNullOrEmpty(content))
            {
                JObject json = JObject.Parse(content);
                if (json["access_token"] != null)
                {
                    string authorization = this.Token;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
                }
            }
            return response;
        }
        else {
            return response;
        }
    }

    /// <summary>Returns the token for the client's credientials</summary>
    public HttpResponseMessage GetAuthorizeToken()
    {
        // add credentials to the request
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("grant_type", "password");
        parameters.Add("client_id", this.Credentials.ClientId);
        parameters.Add("client_secret", this.Credentials.ClientSecret);
        parameters.Add("username", this.Credentials.Username);
        parameters.Add("pass", this.Credentials.Pass);
        
        var request = new HttpRequestMessage(HttpMethod.Post, this.Credentials.Url){Content = new FormUrlEncodedContent(parameters)};
        var response = client.Send(request);

        return response;
    }
}
