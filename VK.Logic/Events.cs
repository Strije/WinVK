using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VK.API;

namespace VK.Logic
{
    public enum AppEventType
    {
        Unknown = -1,

        AuthCompleted = 1,
        AuthCancelled = 2,
        AuthFailed = 3,
        AuthFailedNoInternet = 4,

        OpenChat = 5,
        ChatOpened = 6
    }

    public class AppEvent
    {
        public AppEventType EventType = AppEventType.Unknown;
        public object[] Data;

        public AppEvent(AppEventType type, params object[] data)
        {
            this.EventType = type;
            this.Data = data;
        }
    }

    public static class AppEvents
    {
        private static readonly Dictionary<AppEventType, ExEvent<AppEvent>> Events = new Dictionary<AppEventType, ExEvent<AppEvent>>();

        public static void On(AppEventType type, ExEventDelegate<AppEvent> d)
        {
            if (!Events.ContainsKey(type))
                Events.Add(type, new ExEvent<AppEvent>());

            Events[type] += d;
        }

        public static void Dispatch(AppEventType type, params object[] data)
        {
            if (Events.ContainsKey(type))
            {
                Events[type].Dispatch(new AppEvent(type, data));
            }
        }
    }
}
