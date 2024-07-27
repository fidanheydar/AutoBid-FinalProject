using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Extensions
{
    public static class CoderExtension
    {
        public static string EncodeToken(this string token)
        {
            byte[] bytesCode = Encoding.UTF8.GetBytes(token);
            return WebEncoders.Base64UrlEncode(bytesCode);
        }

        public static string DecodeToken(this string token)
        {
            var decodeToken = WebEncoders.Base64UrlDecode(token);
            return Encoding.UTF8.GetString(decodeToken);
        }
    }
}
