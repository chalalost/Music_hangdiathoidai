using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace OnlineMusic.Models
{
    // Get configuraiton from web.config file
    public class PaypalConfig
    {
        public readonly static string ClientID;
        public readonly static string ClientSecret;
        static PaypalConfig()
        {
            var config = GetConfig();
            ClientID = config["clientID"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        // Create access token
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientID, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        // This will return APIContext object
        public static APIContext GetAPIContext()
        {
            var apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}