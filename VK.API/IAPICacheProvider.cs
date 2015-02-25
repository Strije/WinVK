using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VK.API
{
    public class APICache
    {
        public static IAPICacheProvider CacheProvider;

        public static void SetProvider(IAPICacheProvider p)
        {
            CacheProvider = p;
        }

        public static T Get<T>(object key)
        {
            if (CacheProvider == null)
                throw new NotImplementedException("API cache provider is not set!");

            return CacheProvider.Get<T>(key);
        }

        public static void Put<T>(object key, T val)
        {
            if (CacheProvider == null)
                throw new NotImplementedException("API cache provider is not set!");

            CacheProvider.Put<T>(key, val);
        }
    }

    public interface IAPICacheProvider
    {
        T Get<T>(object key);
        void Put<T>(object key, T val);
    }
}
