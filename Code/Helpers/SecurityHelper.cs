using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helpers
{
    public class SecurityHelper
    {

        public  String Md5Encryption(String src)
        {
            System.Text.UnicodeEncoding encode = new System.Text.UnicodeEncoding();
            System.Text.Decoder decode = encode.GetDecoder();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] password = new byte[encode.GetByteCount(src)];
            char[] char_password = new char[encode.GetCharCount(password)];
            password = encode.GetBytes(src);
            password = md5.ComputeHash(password);
            int length = encode.GetByteCount(src);
            if (length > 16)
                length = 16;
            decode.GetChars(password, 0, length, char_password, 0);
            return new String(char_password);
        }

        public int getUserIDFromToken()
        {
            //Get the current claims principal
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            // Get the claims values
            string APPID = identity.Claims.Where(c => c.Type == "UserId")
                               .Select(c => c.Value).SingleOrDefault();
            int id = int.Parse(APPID);
            return id;
        }

        public int getRoleIDFromToken()
        {
            //Get the current claims principal
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            // Get the claims values
            string APPID = identity.Claims.Where(c => c.Type == "RoleId")
                               .Select(c => c.Value).SingleOrDefault();
            int id = int.Parse(APPID);
            return id;
        }

    }
}
