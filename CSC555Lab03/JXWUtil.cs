using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC555Lab03
{
    public class JXWUtil
    {

        static char[] allowableKeyChars = new char[]
        {
            '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b',
            'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm',
            'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
            'y', 'z'
        };

        public static bool CheckLicence(string email, string key)
        {
            key = key.Trim().ToLower();
            if (TestKey(email, key))
                return true;

            int atIndex = email.IndexOf('@');
            if (atIndex > -1)
            {
                string companyName = email.Substring(atIndex + 1);
                if (TestKey(companyName, key))
                    return true;

                while (companyName.IndexOf('.') > 0)
                {
                    companyName = companyName.Substring(companyName.IndexOf('.') + 1);
                    if (TestKey(companyName, key))
                        return true;
                }
            }
            return false;
        }

        private static bool TestKey(string licenceName, string key)
        {
            return key.Equals(GenerateCode(licenceName, true)) ||
                   key.Equals(GenerateCode(licenceName, false));
        }

        public static string GenerateCode(string seed)
        {
            return GenerateCode(seed, true);
        }

        public static string GenerateCode(string seed, bool forceLowerCase)
        {
            string bloop = seed.Trim();
            if (forceLowerCase)
                bloop = bloop.ToLower();

            int hash = CalculateHash(bloop);
            return HashToLicenceString(hash);
        }

        private static string HashToLicenceString(int seed)
        {
            var vNumber = new System.Text.StringBuilder("jx");
            for (int i = 0; i < 6; i++)
            {
                char c = allowableKeyChars[seed & 0x1F];
                vNumber.Insert(2, c);
                seed >>= 5;
            }
            return vNumber.ToString();
        }

        public static int CalculateHash(string input)
        {
            int h = 0;
            int len = input.Length;
            for (int i = 0; i < len; i++)
            {
                h = 31 * h + input[i];
            }
            return h;
        }
    }

}
