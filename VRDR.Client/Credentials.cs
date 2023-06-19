using VRDR;
using Newtonsoft.Json.Linq;

namespace VRDR
{
    /// <summary>The Credentials class includes the values required to authenticate to the API Gateway</summary>
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
        public Credentials(String url, String clientID, String clientSecret, String username, String pass)
        {
            this.Url = url;
            this.ClientId = clientID;
            this.ClientSecret = clientSecret;
            this.Username = username;
            this.Pass = pass;
        }
    }
}