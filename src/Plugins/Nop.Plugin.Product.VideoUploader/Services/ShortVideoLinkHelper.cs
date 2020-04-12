using System;

namespace Nop.Plugin.Product.VideoUploader.Infrastructure
{
    public class ShortVideoLinkHelper
    {
       	private static string corpus   = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private static string getBase62From10(ulong seed)
        {
            string number = seed.ToString();
            char[] buf = new char[number.Length];
            int charPos = number.Length - 1;
            ulong radix = 62;
            
            while (seed >= radix)
            {
                buf[charPos--] = corpus[(int)(seed % radix)];
                seed = seed / radix;
            }
            buf[charPos] = corpus[(int)seed];
            return new string(buf, charPos, (number.Length - charPos));
        }

        public static string GetVideoLink(ulong productVideoId)	
        {
            var mask = (ulong)Math.Pow(2, (sizeof(ulong) * 8 - 1));
            return getBase62From10(productVideoId | mask);
        }
    }
}