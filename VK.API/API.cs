using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VK.API.LPServer;

namespace VK.API
{
    public static class API
    {
        public static APISession Session;

        public static String GetAuthUrl(int appID, String scope, String display="touch")
        {
            return String.Format(@"https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri=https://oauth.vk.com/blank.html&display={2}&response_type=token", appID, scope, display);
        }

        public static async Task<APISession> Auth(Func<String> authProvider)
        {
            String res = authProvider();

            if (res == null)
                return null;

            await APISession.Parse(res);

            LongPollServer.Connect();

            return Session;
        }

        public static async Task<String> Call(String method, String args = "")
        {
            if (Session == null)
                return null;

            if (args != "")
                args = "?" + args + "&";
            else
                args = "?";

            if(Session.User != null)
                args = args.Replace("%id", Session.User.ID.ToString());

            String u = "https://api.vk.com/method/" + method + args + "access_token=" + Session.APIToken + "&v=5.27";

            try
            {
                HttpWebRequest req = (HttpWebRequest) WebRequest.Create(u);

                WebResponse r = await req.GetResponseAsync();

                Stream s = r.GetResponseStream();

                if (s != null)
                {
                    StreamReader sr = new StreamReader(s);

                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public static async Task<APIResponse<T>> Call<T>(String method, String args = "")
        {
            String resp = await Call(method, args);

            if (!String.IsNullOrEmpty(resp))
            {
                return JsonConvert.DeserializeObject<APIResponse<T>>(resp);
            }
            return null;
        }
    }
}
