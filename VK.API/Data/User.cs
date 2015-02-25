using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VK.API.Data
{
    public class User
    {
        [JsonProperty("uid")]
        public int ID;

        [JsonProperty("first_name")]
        public String Name = null;
        [JsonProperty("last_name")]
        public String LastName = null;

        public String FullName
        {
            get
            {
                String fn = "";

                if (!String.IsNullOrEmpty(this.Name))
                    fn += this.Name;

                if (!String.IsNullOrEmpty(this.LastName))
                    fn += " " + this.LastName;

                return fn;
            }
        }

        [JsonProperty("online")]
        public bool Online = false;

        [JsonProperty("photo_200")]
        public String Photo = null;

        public User(int id)
        {
            this.ID = id;
        }

        public static async Task<User> Get(int id)
        {
            User usr = APICache.Get<User>(id);
            if (usr != null)
                return usr;

            APIResponse<User[]> r = (await API.Call<User[]>("users.get", "user_ids=" + id + "&fields=photo_200,online"));

            if (r != null && r.Response != null)
                usr = r.Response[0];

            APICache.Put<User>(id, usr);
            return usr;
        }

        public static async Task<User[]> GetAll(IEnumerable<int> ids)
        {
            APIResponse<User[]> r = (await API.Call<User[]>("users.get", "user_ids=" + String.Join(",", ids) + "&fields=photo_200,online"));

            if (r != null && r.Response != null)
            {
                User[] users = r.Response;

                foreach (User usr in users)
                {
                    APICache.Put<User>(usr.ID, usr);
                }

                return users;
            }

            return null;
        }

        public Bitmap GetPhoto()
        {
            Bitmap img = null;

            if (!String.IsNullOrEmpty(this.Photo))
            {
                String p = Path.GetFileNameWithoutExtension(new Uri(this.Photo).LocalPath);
                img = APICache.Get<Bitmap>(p);

                if (img == null)
                {
                    WebRequest request = WebRequest.Create(this.Photo);
                    WebResponse response = request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    if (responseStream != null)
                        img = new Bitmap(responseStream);

                    APICache.Put<Bitmap>(p, img);
                }
            }

            return img;
        }
    }
}
