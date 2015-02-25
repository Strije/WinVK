using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VK.API
{
    public class App
    {
        public static IAppPlatform Platform;

        public static void SetPlatform(IAppPlatform p)
        {
            Platform = p;
        }
    }

    public interface IAppPlatform
    {
        String GetName();

        void OpenFile(String file);
        void OpenUrl(String url);

        String GetCacheDir();
    }
}
