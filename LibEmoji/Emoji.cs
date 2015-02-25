using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;

namespace LibEmoji
{
    public partial class Emoji
    {
        private static Regex _rgx = new Regex(@"(?:\uD83C|\uD83D)[\uDC00-\uDEFF](?:\uD83C[\uDC00-\uDEFF])?|[\u2600-\u26FF]|[\u0030-\u0039]\u20E3");

        public static Bitmap ResolveEmoji(String str)
        {
            String code = "";
            foreach (char c in str)
            {
                code += ((int) c).ToString("X4");
            }

            Console.WriteLine("Emoji: " + code);

            Bitmap bmp = (Bitmap) ResourceManager.GetObject(code);

            if(bmp == null)
                bmp = (Bitmap) ResourceManager.GetObject("_" + code);

            return bmp;
        }

        public static List<String> FindEmojis(String str)
        {
            return (from Match m in _rgx.Matches(str) select m.Captures[0].Value).ToList();
        }
    }
}
