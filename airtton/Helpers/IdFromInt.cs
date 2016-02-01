using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airtton.Helpers
{
    public class IdFromInt
    {
        private static Random _random = new Random();

        public static string Base64Hash(int id)
        {
            byte[] buffer = GetRandom(id);
            return Convert.ToBase64String(buffer).Replace("/", "_").Replace("+", "-").TrimEnd(new char[] { '=' });
        }

        public static string Base64Hash1(int id)
        {
            byte[] buffer = GetRandom(id);
            var str = Convert.ToBase64String(buffer).Replace("/", "_").Replace("+", "-"); 

            //string shortGuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            //.Substring(0, 22)
            //.Replace("/", "_")
            //.Replace("+", "-");

            return str;
        }

        // <summary>
        // This is used by all Unique identifier examples
        // </summary>
        public static byte[] GetRandom(int size)
        {
            byte[] buffer = new byte[size];
            _random.NextBytes(buffer);
            return buffer;
        }
    }
}