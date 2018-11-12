using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebPackUpdater.Helpers
{
    public class CryptographyHelper
    {
        public static string GetMd5Hash(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                var text = File.ReadAllText(fileName, Encoding.UTF8);
                var bytes = Encoding.ASCII.GetBytes(text);
                var hash = md5.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        public static string GetMd5Hash(byte[] bytes)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
