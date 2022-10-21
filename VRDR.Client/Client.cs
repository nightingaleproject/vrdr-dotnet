using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
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
    public string? Token { get; set; }
    /// <summary>Constructor</summary>
    private HttpClient client = new HttpClient();
    public Client(String url, bool local, Credentials credentials)
    {
        this.Url = url;
        this.LocalTesting = local;
        this.Credentials = credentials;
    }
    // GetMessageResponsesAsync makes a GET request to the NVSS FHIR API server for any new messages
    // responses
    public async Task<HttpResponseMessage> GetMessageResponsesAsync()
    {
        var address = this.Url;
        Console.WriteLine($">>> Retrieving new messages from NCHS...");

        // if testing against the NVSS FHIR API server, add the authentication token
        if (!this.LocalTesting){
            if (String.IsNullOrEmpty(this.Token))
            {
                HttpResponseMessage authResponse = await RefreshAccessTokenAsync();
                // return authentication error
                if (!authResponse.IsSuccessStatusCode)
                {
                    return authResponse;
                }
            }
        }

        var response = await client.GetAsync(address);

        // check if the token expired, refresh and try again
        if (!response.IsSuccessStatusCode)
        {
            HttpResponseMessage authRetry = await RefreshAccessTokenAsync();
            // return authentication error
            if (!authRetry.IsSuccessStatusCode)
            {
                return authRetry;
            }
            response = await client.GetAsync(this.Url);
        }

        return response;
    }

    // PostMessageAsync POSTS a single message to the NVSS FHIR API server for processing
    public async Task<HttpResponseMessage> PostMessageAsync(BaseMessage message)
    {

        var json = message.ToJSON();
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // if testing against the NVSS FHIR API server, add the authentication token
        if (!this.LocalTesting){
            if (String.IsNullOrEmpty(this.Token))
            {
                HttpResponseMessage authResponse = await RefreshAccessTokenAsync();
                // return authentication error
                if (!authResponse.IsSuccessStatusCode)
                {
                    return authResponse;
                }
            }
        }
        var response = client.PostAsync(this.Url, data).Result;

        // check if the token expired, refresh and try again
        if (!response.IsSuccessStatusCode)
        {
            HttpResponseMessage authRetry = await RefreshAccessTokenAsync();
            // return authentication error
            if (!authRetry.IsSuccessStatusCode)
            {
                return authRetry;
            }
            response = await client.PostAsync(this.Url, data);
        }

        return response;
    }

    public static string CreateBulkUploadPayload(IEnumerable<BaseMessage> messages, string url, bool prettyPrint = false)
    {
        Bundle payload = new Bundle();
        payload.Id = Guid.NewGuid().ToString();
        payload.Type = Bundle.BundleType.Batch;
        payload.Timestamp = DateTime.Now;
        foreach(BaseMessage message in messages)
        {
            Bundle.EntryComponent entry = new Bundle.EntryComponent();
            entry.Resource = (Bundle)message;
            entry.Request = new Bundle.RequestComponent();
            entry.Request.Method = Bundle.HTTPVerb.POST;
            entry.Request.Url = url;
            payload.Entry.Add(entry);
        }
        return payload.ToJson(new FhirJsonSerializationSettings { Pretty = prettyPrint, AppendNewLine = prettyPrint });
    }

    // PostMessageAsync POSTS an array message to the NVSS FHIR API server for processing using bulk upload
    public async Task<HttpResponseMessage[]> PostMessagesAsync(IEnumerable<BaseMessage> messages, string url)
    {
        string json = CreateBulkUploadPayload(messages, url);
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        // if testing against the NVSS FHIR API server, add the authentication token
        if (!this.LocalTesting){
            if (String.IsNullOrEmpty(this.Token))
            {
                HttpResponseMessage authResponse = await RefreshAccessTokenAsync();
                // return authentication error
                if (!authResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Repeat(authResponse, messages.Count()).ToArray();
                }
            }
        }
        var response = client.PostAsync(this.Url, data).Result;

        // check if the token expired, refresh and try again
        if (!response.IsSuccessStatusCode)
        {
            HttpResponseMessage authRetry = await RefreshAccessTokenAsync();
            // return authentication error
            if (!authRetry.IsSuccessStatusCode)
            {
                return Enumerable.Repeat(authRetry, messages.Count()).ToArray();
            }
            response = await client.PostAsync(this.Url, data);
        }

        // At this point we have an overall HTTP response plus, if the request succeeded, one internal response for each
        // message in the bulk submission. To process this we first check for success and, if the request did succeed,
        // break out the internal responses, otherwise return the overall response as the response for each message
        // TODO NOTFORCHECKIN: Implement this!
        return Enumerable.Repeat(response, messages.Count()).ToArray();
    }

    private async Task<HttpResponseMessage> RefreshAccessTokenAsync(){
    // clear the authentication headers
    client.DefaultRequestHeaders.Authorization = null;

    HttpResponseMessage response = await GetAuthorizeTokenAsync();
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (!String.IsNullOrEmpty(content))
            {
                JObject json = JObject.Parse(content);
                this.Token = json["access_token"]?.ToString();
                if (this.Token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);
                }
            }
            return response;
        }
        else {
            return response;
        }
    }

    /// <summary>Returns the token for the client's credientials</summary>
    public async Task<HttpResponseMessage> GetAuthorizeTokenAsync()
    {
        // add credentials to the request
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("grant_type", "password");
        parameters.Add("client_id", this.Credentials.ClientId);
        parameters.Add("client_secret", this.Credentials.ClientSecret);
        parameters.Add("username", this.Credentials.Username);
        parameters.Add("password", this.Credentials.Pass);

        var request = new HttpRequestMessage(HttpMethod.Post, this.Credentials.Url){Content = new FormUrlEncodedContent(parameters)};
        var response = await client.SendAsync(request);

        return response;
    }
}
