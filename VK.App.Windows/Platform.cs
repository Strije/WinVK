using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using VK.API;
using VK.Logic;

namespace VK.App.Windows
{
    public class Platform : IAppPlatform
    {
        public string GetName()
        {
            return "Windows";
        }

        public void OpenFile(String file)
        {
            Process.Start("explorer", file);
        }

        public void OpenUrl(String url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://") && !url.StartsWith("ftp://"))
                url = "http://" + url;

            Process.Start(url);
        }

        public String GetCacheDir()
        {
            return Path.Combine(Path.GetFullPath("."), "cache");
        }
    }
}
