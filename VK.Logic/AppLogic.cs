using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VK.API.Data;

namespace VK.Logic
{
    public static class AppLogic
    {
        public const int AppID = 4695094;
        public const String APIKey = "dLmcgPZqzUZLsOL0OcxI";

        public const String APIScope = "messages";
        public const String AuthDisplay = "touch";

        public static void Startup()
        {
            Cache.Create();

            Cache.Load<User>();
            Cache.Load<Dialog>();
            Cache.Load<Message>();
            Cache.LoadImages();
        }

        public static void Exit()
        {
            Cache.Save<User>();
            Cache.Save<Dialog>();
            Cache.Save<Message>();
            Cache.SaveImages();
        }

        public static String GetAuthUrl()
        {
            return API.API.GetAuthUrl(AppID, APIScope, AuthDisplay);
        }
    }
}
