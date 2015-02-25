using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace VK.API.Data.Attachments
{
    public class Document
    {
        [JsonProperty("id")]
        public int ID = 0;

        [JsonProperty("owner_id")]
        public int OwnerID = 0;
        private User _owner;
        public User Owner
        {
            get
            {
                if (this._owner == null)
                {
                    User cusr = APICache.Get<User>(OwnerID);
                    if (cusr != null)
                        this._owner = cusr;
                }

                return this._owner;
            }
        }

        [JsonProperty("user_id")]
        public int UserID = 0;

        private User _usr;
        public User User
        {
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

        [JsonProperty("title")]
        public String Title = null;

        [JsonProperty("ext")]
        public String Extension = null;

        [JsonProperty("size")]
        public int Size = 0;
        
        [JsonProperty("url")]
        public String Url = null;

        [JsonProperty("photo_130")]
        public String Photo = null;

        public void Open()
        {
            if (this.Url != null)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    DefaultExt = this.Extension,
                    Title = this.Title,
                    FileName = this.Title,
                    Filter = this.Extension + @" files|*." + this.Extension + @"|All files|*.*"
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    String path = dlg.FileName;

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(this.Url, path);
                    }

                    App.Platform.OpenFile(path);
                }
            }
        }
    }
}
