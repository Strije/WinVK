using System;
using System.Threading.Tasks;
using VK.API.Data;

namespace VK.API
{
    public class APISession
    {
        public String APIToken;
        public User User;

        private APISession(){}

        public static async Task Parse(String u)
        {
            API.Session = new APISession();

            if(u.StartsWith("https://oauth.vk.com/blank.html#"))
            {
                String f = u.Replace("https://oauth.vk.com/blank.html#", "");

                String[] kv_s = f.Split('&');

                foreach (String kv in kv_s)
                {
                    String[] k_v = kv.Split('=');

                    String k = k_v[0];
                    String v = k_v[1];

                    if (k == "access_token")
                    {
                        API.Session.APIToken = v;
                    }
                    else if (k == "user_id")
                    {
                        API.Session.User = await User.Get(int.Parse(v));
                    }
                }
            }
        }
    }
}
