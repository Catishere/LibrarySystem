using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Utils
{
    public static class Cryptography
    {
        #region Settings

        private static int _iterations = 2;
        private static int _keySize = 256;

        private static readonly HashAlgorithmName Hash = HashAlgorithmName.SHA1;
        private static string _salt = "ThatsAlotOfSaltz"; // Random
        private static string _vector = "secretysecretsww";// Random

        #endregion

        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }
        public static string Encrypt<T>(string value, string password)
                where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = Encoding.ASCII.GetBytes(value);

            byte[] encrypted;
            using T cipher = new T();
            Rfc2898DeriveBytes passwordBytes =
                new Rfc2898DeriveBytes(password, saltBytes, _iterations, Hash);
            byte[] keyBytes = passwordBytes.GetBytes(_keySize / 8);

            cipher.Mode = CipherMode.CBC;

            using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
            {
                using (MemoryStream to = new MemoryStream())
                {
                    using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                    {
                        writer.Write(valueBytes, 0, valueBytes.Length);
                        writer.FlushFinalBlock();
                        encrypted = to.ToArray();
                    }
                }
            }
            cipher.Clear();
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string value, string password)
        {
            return Decrypt<AesManaged>(value, password);
        }
        public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = Convert.FromBase64String(value);

            byte[] decrypted;
            int decryptedByteCount = 0;

            using T cipher = new T();
            Rfc2898DeriveBytes passwordBytes = new Rfc2898DeriveBytes(password, saltBytes, _iterations, Hash);
            byte[] keyBytes = passwordBytes.GetBytes(_keySize / 8);

            cipher.Mode = CipherMode.CBC;

            try
            {
                using ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes);
                using MemoryStream from = new MemoryStream(valueBytes);
                using CryptoStream reader = new CryptoStream(@from, decryptor, CryptoStreamMode.Read);
                decrypted = new byte[valueBytes.Length];
                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            cipher.Clear();
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }

        public static string EncodePassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
