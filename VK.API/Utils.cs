using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace VK.API
{
    public delegate void ExEventDelegate<in T>(T data);
    public delegate void ExEventDelegate(object data);

    public class ExEvent<T>
    {
        protected event ExEventDelegate<T> eventdelegate;

        public void Dispatch(T data)
        {
            if (eventdelegate != null)
            {
                eventdelegate(data);
            }
        }

        public static ExEvent<T> operator +(ExEvent<T> kElement, ExEventDelegate<T> kDelegate)
        {
            kElement.eventdelegate += kDelegate;
            return kElement;
        }

        public static ExEvent<T> operator -(ExEvent<T> kElement, ExEventDelegate<T> kDelegate)
        {
            kElement.eventdelegate -= kDelegate;
            return kElement;
        }
    }

    public class ExEvent : ExEvent<object>
    { }

    public static class HttpWebRequestExtensions
    {
        public static Task<Stream> GetRequestStreamAsync(this HttpWebRequest request)
        {
            var tcs = new TaskCompletionSource<Stream>();
   
            try
            {
                request.BeginGetRequestStream(iar =>
                {
                    try
                    {
                        var response = request.EndGetRequestStream(iar);
                        tcs.SetResult(response);
                    }
                    catch (Exception exc)
                    {
                        tcs.SetException(exc);
                    }
                }, null);
            }
            catch (Exception exc)
            {
                tcs.SetException(exc);
            }
   
            return tcs.Task;
        }
   
        public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var tcs = new TaskCompletionSource<HttpWebResponse>();
   
            try
            {
                request.BeginGetResponse(iar =>
                {
                    try
                    {
                        var response = (HttpWebResponse)request.EndGetResponse(iar);
                        tcs.SetResult(response);
                    }
                    catch (Exception exc)
                    {
                        tcs.SetException(exc);
                    }
                }, null);
            }
            catch (Exception exc)
            {
                tcs.SetException(exc);
            }
   
            return tcs.Task;
        }
    }
}
