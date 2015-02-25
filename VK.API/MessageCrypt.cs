using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Noesis.Javascript;

namespace VK.API
{
    public class MessageCrypt
    {
        private static JavascriptContext _js = null;

        private static void _initJS()
        {
            if(_js != null)
                return;

            _js = new JavascriptContext();
            _js.Run(Resources.AES_js);
        }

        public static String Encrypt(String msg, String password)
        {
            _initJS();

            _js.SetParameter("msg", msg);
            _js.SetParameter("password", password);

            return _js.Run("CryptoJS.AES.encrypt(msg, password).toString()").ToString();
        }

        public static String Decrypt(String msg, String password)
        {
            _initJS();

            _js.SetParameter("msg", msg);
            _js.SetParameter("password", password);

            try
            {
                return _js.Run("CryptoJS.AES.decrypt(msg, password).toString(CryptoJS.enc.Utf8)").ToString();
            }
            catch (Exception e)
            {
                return "ERROR (this message can be encoded by another Secret Key)";
            }
        }
    }
}
