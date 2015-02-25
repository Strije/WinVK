using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VK.API.Data
{
    public class Message
    {
        [JsonProperty("id")]
        public int ID = 0;

        [JsonProperty("user_id")]
        public int UserID = 0;

        private User _usr;
        public User User {
            get
            {
                if (this._usr == null)
                {
                    User cusr = APICache.Get<User>(UserID);
                    if (cusr != null)
                        this._usr = cusr;
                }

                return this._usr;
            }
        }

        public User Author {
            get
            {
                User usr = this.User;
                if (this.Out)
                    if (API.Session != null) usr = API.Session.User;

                return usr;
            }
        }

        [JsonProperty("chat_id")]
        public int ChatID = 0;

        [JsonProperty("title")]
        public String Title = null;

        [JsonProperty("body")]
        public String Body = null;

        [JsonProperty("date")]
        public long Time = 0;

        [JsonProperty("read_state")]
        public bool Read = false;

        [JsonProperty("out")]
        public bool Out = false;

        [JsonProperty("attachments")]
        public Attachment[] Attachments = null;

        [JsonProperty("photo_200")]
        public String Photo = null;

        [JsonIgnore]
        public Dictionary<String, Object> RuntimeData = new Dictionary<String, Object>();

        public bool Crypted;

        public bool CryptedNow
        {
            get
            {
                return !String.IsNullOrEmpty(this.Body) && this.Body.StartsWith("AESSTART");
            }
        }

        public String Decrypt(String password)
        {
            if (this.CryptedNow)
            {
                this.Crypted = true;
                this.Body = MessageCrypt.Decrypt(this.Body.Replace("AESSTART", ""), password);
            }

            return this.Body;
        }

        public static String Encrypt(String msg, String password)
        {
            return "AESSTART" + MessageCrypt.Encrypt(msg, password);
        }

        public async Task<User> GetUser()
        {
            this._usr = await User.Get(UserID);

            return this._usr;
        }

        public static async Task<int> Send(String target, String message = "")
        {
            try
            {
                return (await API.Call<int>("messages.send", target + "&message=" + Uri.EscapeDataString(message))).Response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return -1;
        }

        public static async Task<Message> Get(int id)
        {
            try
            {
                Message msg = APICache.Get<Message>(id);

                if (msg != null)
                    return msg;

                msg = (await API.Call<ObjList<Message>>("messages.getById", "message_ids=" + id)).Response.Items[0];

                APICache.Put<Message>(msg.ID, msg);

                return msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
        public static async Task<List<Message>> Get(int[] ids)
        {
            try
            {
                List<Message> msgs = (await API.Call<ObjList<Message>>("messages.getById", "message_ids=" + String.Join(",", ids))).Response.ToList();

                foreach (Message msg in msgs)
                {
                    APICache.Put<Message>(msg.ID, msg);
                }

                return msgs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
