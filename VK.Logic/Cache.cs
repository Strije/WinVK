using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using VK.API;

namespace VK.Logic
{
    public static class Cache<T>
    {
        public static Dictionary<object, T> _Cache = Cache.Load<T>();

        public static T Get(object key)
        {
            if (_Cache.ContainsKey(key))
            {
                return _Cache[key];
            }

            return default(T);
        }

        public static void Put(object key, T val)
        {
            _Cache[key] = val;
        }
    }

    public class CacheProvider : IAPICacheProvider
    {
        public T Get<T>(object key)
        {
            return Cache<T>.Get(key);
        }

        public void Put<T>(object key, T val)
        {
            Cache<T>.Put(key, val);
        }
    }

    public static class Cache
    {
        private static String Dir = null;
        private static String ImagesDir = null;

        public static void Create()
        {
            Dir = App.Platform.GetCacheDir();

            if (String.IsNullOrEmpty(Dir))
                Dir = @".cache";

            ImagesDir = Path.Combine(Dir, @"img");

            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }

            if (!Directory.Exists(ImagesDir))
            {
                Directory.CreateDirectory(ImagesDir);
            }

            APICache.SetProvider(new CacheProvider());
        }

        public static void Save<T>(Dictionary<object, T> dict = null)
        {
            if (dict == null)
                dict = Cache<T>._Cache;

            String fname = Path.Combine(Dir, typeof(T).FullName + ".dat");

            StreamWriter sw = File.CreateText(fname);
            JsonSerializer s = new JsonSerializer();
            s.Serialize(sw, dict, typeof(Dictionary<object, T>));
            sw.Flush();
            sw.Close();
        }

        public static void SaveImages(Dictionary<object, Bitmap> dict = null)
        {
            if (dict == null)
                dict = Cache<Bitmap>._Cache;

            if (!Directory.Exists(ImagesDir))
            {
                Directory.CreateDirectory(ImagesDir);
            }

            foreach (KeyValuePair<object, Bitmap> i in dict)
            {
                String file = Path.Combine(ImagesDir, i.Key + ".png");

                if (!File.Exists(file))
                {
                    Bitmap img = i.Value;
                    img.Save(file, ImageFormat.Png);
                }
            }
        }

        public static Dictionary<object, T> Load<T>()
        {
            Dictionary<object, T> dict = new Dictionary<object, T>();

            String fname = Path.Combine(Dir, typeof(T).FullName + ".dat");

            if (!File.Exists(fname))
                return dict;

            StreamReader r = new StreamReader(File.OpenRead(fname));
            JsonSerializer s = new JsonSerializer();
            dict = (Dictionary<object, T>)s.Deserialize(new JsonTextReader(r), typeof(Dictionary<object, T>));
            r.Close();

            return dict;
        }

        public static void LoadImages()
        {
            (new Thread(() =>
            {
                Dictionary<object, Bitmap> dict = new Dictionary<object, Bitmap>();

                if (!Directory.Exists(ImagesDir))
                    return;

                String[] imgs = Directory.GetFiles(ImagesDir, "*.png", SearchOption.AllDirectories);

                foreach (String img in imgs)
                {
                    Bitmap b = new Bitmap(img);

                    String p = Path.GetFileNameWithoutExtension(img);
                    if (p != null)
                    {
                        if (dict.ContainsKey(p))
                            dict[p] = b;
                        else
                            dict.Add(p, b);
                    }
                }

                Cache<Bitmap>._Cache = dict;
            })).Start();
        }
    }
}
