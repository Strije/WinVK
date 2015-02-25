using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace VK.API.Data.Attachments
{
    public enum PhotoSize
    {
        _130,
        _604,
        _807,
        _1280,
        _2560
    }

    public class Photo
    {
        [JsonProperty("id")]
        public int ID = 0;

        [JsonProperty("album_id")]
        public int AlbumID = 0;

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

        [JsonProperty("text")]
        public String Text = null;

        [JsonProperty("date")]
        public long Time = 0;

        private Bitmap _photo130 = null;
        [JsonProperty("photo_130")]
        public String Photo130 = null;

        private Bitmap _photo604 = null;
        [JsonProperty("photo_604")]
        public String Photo604 = null;

        private Bitmap _photo807 = null;
        [JsonProperty("photo_807")]
        public String Photo807 = null;

        private Bitmap _photo1280 = null;
        [JsonProperty("photo_1280")]
        public String Photo1280 = null;

        private Bitmap _photo2560 = null;
        [JsonProperty("photo_2560")]
        public String Photo2560 = null;

        public Bitmap Load(PhotoSize sz = PhotoSize._130)
        {
            Bitmap img = null;
            String url = "";

            switch (sz)
            {
                case PhotoSize._130:
                    img = this._photo130;
                    url = this.Photo130;
                    break;

                case PhotoSize._604:
                    img = this._photo604;
                    url = this.Photo604;
                    break;

                case PhotoSize._807:
                    img = this._photo807;
                    url = this.Photo807;
                    break;

                case PhotoSize._1280:
                    img = this._photo1280;
                    url = this.Photo1280;
                    break;

                case PhotoSize._2560:
                    img = this._photo2560;
                    url = this.Photo2560;
                    break;
            }

            if (img == null && !String.IsNullOrEmpty(url))
            {
                String p = Path.GetFileNameWithoutExtension(new Uri(url).LocalPath);
                img = APICache.Get<Bitmap>(p);

                if (img == null)
                {
                    WebRequest request = WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    if (responseStream != null)
                        img = new Bitmap(responseStream);

                    APICache.Put<Bitmap>(p, img);
                }
            }

            return img;
        }

        [JsonProperty("width")]
        public int Width = 0;

        [JsonProperty("height")]
        public int Height = 0;

        public Size GetSize(int max = 130)
        {
            int w = this.Width;
            int h = this.Height;

            if (w <= max && h <= max)
            {
                return new Size(w, h);
            }

            float k;

            if (this.Width > this.Height)
            {
                k = this.Width / (float) max;
                w = max;
                h = (int) (this.Height / k);
            }
            else
            {
                k = this.Height / (float) max;
                w = (int) (this.Width / k);
                h = max;
            }

            return new Size(w, h);
        }

        public void Open()
        {
            PhotoSize sz = PhotoSize._2560;
            Bitmap bmp;
            while((bmp = this.Load(sz)) == null){
                switch (sz)
                {
                    case PhotoSize._2560:
                        sz = PhotoSize._1280;
                        break;

                    case PhotoSize._1280:
                        sz = PhotoSize._807;
                        break;

                    case PhotoSize._807:
                        sz = PhotoSize._604;
                        break;

                    case PhotoSize._604:
                        sz = PhotoSize._130;
                        break;

                    case PhotoSize._130:
                        return;
                }
            }

            String path = Path.GetTempFileName() + ".png";
            bmp.Save(path, ImageFormat.Png);

            App.Platform.OpenFile(path);
        }
    }
}
