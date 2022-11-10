using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiniGround.API.Commons
{
    public class Uitilities
    {
        public static string HashMD5(string InputText)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            if (string.IsNullOrEmpty(InputText.Trim()))
                return "";
            var arrInput = Encoding.UTF8.GetBytes(InputText);
            var arrOutput = MD5.ComputeHash(arrInput);
            return Convert.ToBase64String(arrOutput);
        }
    }
}
