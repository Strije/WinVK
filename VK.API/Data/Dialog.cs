using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VK.API.Data
{
    public class Dialog
    {
        [JsonProperty("message")]
        public Message Message = null;

        public String Title
        {
            get
            {
                Message msg = this.Message;
                if (msg == null)
                    return "";

                User usr = msg.User;
                String name = "";

                if (usr != null)
                {
                    name = usr.FullName;
                }

                String title = msg.Title;

                if (title.Trim() == "...")
                    title = name;
                else
                    title = msg.Title;

                return title;
            }
        }

        public bool Crypt = false;
        public String CryptKey = null;

        public static async Task<List<Dialog>> GetDialogs(int offset = 0, int count = 200)
        {
            return (await API.Call<ObjList<Dialog>>("messages.getDialogs", "offset=" + offset + "&count=" + count)).Response.ToList();
        }

        public async Task<List<Message>> GetMessages(int offset = 0, int count = 200)
        {
            try
            {
                String chat = "&user_id=" + this.Message.UserID;
                if (this.Message.ChatID > 0)
                    chat = "&chat_id=" + this.Message.ChatID;

                APIResponse<ObjList<Message>> r = (await API.Call<ObjList<Message>>("messages.getHistory", "offset=" + offset + "&count=" + count + chat));

                if (r != null && r.Response != null)
                {
                    List<Message> msgs = r.Response.ToList();

                    foreach(Message msg in msgs){
                        APICache.Put<Message>(msg.ID, msg);

                        if (msg.CryptedNow || msg.Crypted)
                        {
                            this.Crypt = true;
                            if (!String.IsNullOrEmpty(this.CryptKey) && msg.CryptedNow)
                            {
                                msg.Decrypt(this.CryptKey);
                            }
                        }
                    }

                    return msgs;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}
