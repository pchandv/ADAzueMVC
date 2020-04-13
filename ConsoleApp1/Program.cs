using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // HttpAuthenticationAsync().Wait();
            AdalAuthenticationAsync().Wait();

        }
        private static async Task AdalAuthenticationAsync()
        {
            //  Constants
            var tenant = "VisamVision.onmicrosoft.com";
            var serviceUri = "https://graph.windows.net/";// "https://VisamVision.onmicrosoft.com/d56f0dfb-1a4d-4587-8398-18a33be0f991";
            var clientID = "d56f0dfb-1a4d-4587-8398-18a33be0f991";
            var userName = $"Honey@{tenant}";
            var password = "Daddy@1990";
            var authParms = new PlatformParameters(PromptBehavior.Auto, false);
            //  Ceremony
            var authority = "https://login.microsoftonline.com/" + tenant;
            var authContext = new AuthenticationContext(authority);
            var credentials = new UserPasswordCredential(userName, password);
            var authResult = await authContext.AcquireTokenAsync(serviceUri,clientID, credentials);
            Console.WriteLine(authResult);
        }
        private static async Task HttpAuthenticationAsync()
        {
            //  Constants
            //var tenant = "VisamVision.onmicrosoft.com";
            //var serviceUri = "https://VisamVision.onmicrosoft.com/ADMVCAuto";
            //var clientID = "7f1c2a9e-c0b4-403d-b9f0-0b0c4f74e134";
            //var userName = $"Honey@{tenant}";
            //var password = "Daddy@1990";

            var tenant = "VisamVision.onmicrosoft.com";
            var serviceUri = "https://VisamVision.onmicrosoft.com/d56f0dfb-1a4d-4587-8398-18a33be0f991";
            var clientID = "d56f0dfb-1a4d-4587-8398-18a33be0f991";
            var userName = $"Honey@{tenant}";
            var password = "Daddy@1990";

            using (var webClient = new WebClient())
            {
                var requestParameters = new NameValueCollection();

                requestParameters.Add("resource", serviceUri);
                requestParameters.Add("client_id", clientID);
                requestParameters.Add("grant_type", "password");
                requestParameters.Add("username", userName);
                requestParameters.Add("password", password);
                requestParameters.Add("scope", "openid");

                var url = $"https://login.microsoftonline.com/a4167f4c-98b4-4bd1-a981-53bddd2aaee2/oauth2/token";
                var responsebytes = await webClient.UploadValuesTaskAsync(url, "POST", requestParameters);
                var responsebody = Encoding.UTF8.GetString(responsebytes);
                Console.WriteLine(responsebody);
            }
        }
    }
}
