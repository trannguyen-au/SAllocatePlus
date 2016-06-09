using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.CommonShared
{
    public class EncryptionService
    {
        public static byte[] EncryptPassword(string password)
        {
            byte[] hashBytes = Encoding.UTF8.GetBytes(password);

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(hashBytes);
        }

        public static string MD5Hash(string plainText)
        {
            byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(plainText));
            return Encoding.UTF8.GetString(hash);
        }
    }
}
