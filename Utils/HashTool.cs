﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YpassDesktop.Utils
{
    internal class HashTool
    {

        public static byte[] DeriveKey(byte[] password, byte[] salt)
        {
            // Permet de hasher un mot de passe en SHA-256 avec un salt specific.

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes pour SHA-256
                return hashBytes;
                //return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }

        }

        public static byte[] GenerateRandomSalt()
        {
            const string charString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_-+=<>?/";
            char[] chars = charString.ToCharArray();

            // Génère une longueur de sel aléatoire entre 16 et 20 caractères
            int saltLength = 32; // On utilise 32 bytes pour être compatible avec AES-256

            byte[] saltBytes = new byte[saltLength];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                for (int i = 0; i < saltLength; i++)
                {
                    byte[] randomBytes = new byte[1];
                    rng.GetBytes(randomBytes);

                    int charIndex = randomBytes[0] % chars.Length;
                    saltBytes[i] = (byte)chars[charIndex];
                }
            }

            return saltBytes;

        }

    }
}
