using VRDR;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace nvssclient.lib;
// The Credentials class includes the values required to authenticate to the API Gateway
public class Credentials
{
    /// <summary>The oauth authentication url</summary>
    public String Url { get; }
    /// <summary>The client ID provided by SAMS</summary>
    public String ClientId { get; }
    /// <summary>The client secret provided by SAMS</summary>
    public String ClientSecret { get; }
    /// <summary>The username provided by SAMS</summary>
    public String Username { get; }
    /// <summary>The password provided by SAMS</summary>
    public String Pass { get; }
    /// <summary>Constructor</summary>
    public Credentials() { }
    /// <summary>Constructor</summary>
    public Credentials(String url, String clientID, String clientSecret, String username, String pass)
    {
        this.Url = url;
        this.ClientId = clientID;
        this.ClientSecret = clientSecret;
        this.Username = username;
        this.Pass = pass;
    }
    /// <summary>Returns the token for the provided credientials</summary>
    public String GetAuthorizeToken()
    {
        var rclient = new RestClient(this.Url);
        var request = new RestRequest(Method.POST);

        // add credentials to the request
        String paramString = String.Format("grant_type=password&client_id={0}&client_secret={1}&username={2}&password={3}", this.ClientId, this.ClientSecret, this.Username, this.Pass);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddParameter("application/x-www-form-urlencoded", paramString, ParameterType.RequestBody);
        
        IRestResponse response = rclient.Execute(request);
        string content = response.Content;

        // parse the response to get the access token
        if (!String.IsNullOrEmpty(content))
        {
            JObject json = JObject.Parse(content);
            if (json["access_token"] != null)
            {
                String newtoken = json["access_token"].ToString();
                return newtoken;
            }
            
        }
        return "";
    }

}