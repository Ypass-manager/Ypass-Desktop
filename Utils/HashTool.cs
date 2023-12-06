using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YpassDesktop.Utils
{
    internal class HashTool
    {

        public static string HashPassword(string password, string salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes pour SHA-256
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }

        public static string GenerateRandomSalt()
        {
            const string charString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_-+=<>?/";
            char[] chars = charString.ToCharArray();

            // Génère une longueur de sel aléatoire entre 16 et 20 caractères
            int saltLength = new Random().Next(16, 20); // Modification pour inclure 20 dans la plage

            char[] saltChars = new char[saltLength];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                for (int i = 0; i < saltLength; i++)
                {
                    byte[] randomBytes = new byte[1];
                    rng.GetBytes(randomBytes);

                    int charIndex = randomBytes[0] % chars.Length;
                    saltChars[i] = chars[charIndex];
                }
            }

            return new string(saltChars);
        }
    }
}
