using System.Net.Http.Headers;
using System.Net.Mail;

namespace CSC555Lab03
{
    internal class Program
    {

        private static bool IsEmailValid(string email)
        {
            bool valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Email Address to create key for: ");
            string emailAddress = Console.ReadLine();
            if (!IsEmailValid(emailAddress))
            {
                Console.WriteLine("You must enter a valid email address");
                System.Environment.Exit(1);
            }
            string key = JXWUtil.GenerateCode(emailAddress);
            bool testKey = JXWUtil.CheckLicence(emailAddress, key);
            Console.WriteLine($"License Key : {key}");
            Console.WriteLine($"Key is valid: {testKey}");
        }
    }
}
