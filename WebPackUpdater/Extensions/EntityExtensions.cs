using System;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace WebPackUpdater.Extensions
{
    public static class EntityExtensions
    {
        public static string GetPlainText(this Entity record)
        {
            if (!record.Contains("content"))
            {
                return string.Empty;
            }

            byte[] binary = Convert.FromBase64String(record.GetAttributeValue<string>("content"));
            string resourceContent = Encoding.UTF8.GetString(binary);
            string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (resourceContent.StartsWith("\""))
            {
                resourceContent = resourceContent.Remove(0, byteOrderMarkUtf8.Length);
            }

            return resourceContent;
        }
    }
}
