using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Owasp.Passwords
{
    class Program
    {
        static void Main(string[] args)
        {
            //passwordsHash1("Mi Password");
            //passwordsHash2("Mi Password");
            passwordsHash3("Mi Password");
            Console.ReadLine();
        }

        static void passwordsHash1(string password)
        {             
            byte[] byteRepresentation = UnicodeEncoding.Unicode.GetBytes(password);

            var sha512 = new SHA512CryptoServiceProvider();
            var hash = sha512.ComputeHash(byteRepresentation);
            string hashedText = Convert.ToBase64String(hash);

            Console.WriteLine("Password Hash Simple.");
            Console.WriteLine("Texto de Hash (base64): " + hashedText );
        }

        static void passwordsHash2(string password)
        {
            byte[] saltInBytes = new byte[64];
            var saltGenerator = new RNGCryptoServiceProvider();
            saltGenerator.GetBytes(saltInBytes);
            string saltAsString = Convert.ToBase64String(saltInBytes);

            var pwdb = new PasswordDeriveBytes(Encoding.Unicode.GetBytes(password), saltInBytes, "SHA512", 1);

            var hash = pwdb.GetBytes(64);

            Console.WriteLine("Password Hash + Salt.");
            Console.WriteLine("Texto de Hash (base64): " + Convert.ToBase64String(hash));
            Console.WriteLine("Texto de Salt (base64): " + Convert.ToBase64String(saltInBytes));

        }

        static void passwordsHash3(string password)
        {
            byte[] saltInBytes = new byte[64];
            var saltGenerator = new RNGCryptoServiceProvider();
            saltGenerator.GetBytes(saltInBytes);
            string saltAsString = Convert.ToBase64String(saltInBytes);

            var ticks = (int)DateTime.UtcNow.Ticks;
            var iterations = new Random(ticks).Next(1024, 2048);

            var pwdb = new PasswordDeriveBytes(Encoding.Unicode.GetBytes(password), saltInBytes, "SHA512", iterations);

            var hash = pwdb.GetBytes(64);

            Console.WriteLine("Password Hash + Salt + Random Iterations.");
            Console.WriteLine("Texto de Hash (base64): " + Convert.ToBase64String(hash));
            Console.WriteLine("Texto de Salt (base64): " + Convert.ToBase64String(saltInBytes));

        }

    }
}
