using NetTopologySuite.Precision;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CovidLAMap.Core.Tools
{
    public static class Hex
    {

        public static string StringToHex(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;

            var sb = new StringBuilder();
            var bytes = Encoding.UTF8.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        public static string HexToString(string hexString)
        {
            if (string.IsNullOrEmpty(hexString)) return string.Empty;
            hexString = Remove0x(hexString);

            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.UTF8.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
        
        public static string BinaryToHex(string binary)
        {
            return "0x" + Convert.ToInt32("11111111", 2).ToString("X");
        }

        public static string HexToBinary(string hexString)
        {
            hexString = Remove0x(hexString);
            return Convert.ToString(Convert.ToInt32(hexString, 16), 2);
        }


        private static string Remove0x(string hexString)
        {
            if (hexString[0] == '0' && hexString[1] == 'x') return hexString.Remove(0, 2);

            return hexString;
        }
    }
}
