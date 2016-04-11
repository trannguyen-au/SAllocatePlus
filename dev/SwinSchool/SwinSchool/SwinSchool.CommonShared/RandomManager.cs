using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.CommonShared
{
    public class RandomManager
    {
        private const string ALLOWED_CHARS = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        private static readonly Random RANDOM = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Generates a random character string of specified length
        /// </summary>
        /// <param name="length">Length of string to be generated</param>
        /// <returns>Random character string</returns>
        public static string GenerateRandomString(int length)
        {
            var randomBytes = new Byte[length];
            var chars = new char[length];
            int allowedCharCount = ALLOWED_CHARS.Length;
            RANDOM.NextBytes(randomBytes);
            for (int i = 0; i < length; i++)
            {
                chars[i] = ALLOWED_CHARS[randomBytes[i] % allowedCharCount];
            }
            return new string(chars);
        }
    }
}
