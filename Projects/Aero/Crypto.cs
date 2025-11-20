using System;
using System.Security.Cryptography;

namespace Aero
{
    public interface ICrypto
    {
        string HashPassword(string password, int numberOfIterations = 300000, HashAlgorithmName hashAlgorithmName = default);
        bool ValidatePassword(string password, string correctHash);
    }

    public class Crypto : ICrypto
    {
        public const int SaltByteSize = 24;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;
        public const int AlgorithmIndex = 3;

        public string HashPassword(string password, int numberOfIterations = 300000, HashAlgorithmName hashAlgorithmName = default)
        {
            if (hashAlgorithmName == default)
                hashAlgorithmName = HashAlgorithmName.SHA512;

            byte[] salt = new byte[SaltByteSize];
            RandomNumberGenerator.Fill(salt);

            var hash = GetPbkdf2Bytes(password, salt, numberOfIterations, hashAlgorithmName);
            return numberOfIterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash) + ":" +
                   hashAlgorithmName.Name;
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            // Handle legacy hashes (3 elements) - assume SHA1
            HashAlgorithmName hashAlgorithmName;
            if (split.Length == 3)
            {
                hashAlgorithmName = HashAlgorithmName.SHA1;
            }
            else
            {
                // New format with algorithm name
                hashAlgorithmName = new HashAlgorithmName(split[AlgorithmIndex]);
            }

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hashAlgorithmName, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithmName, int? outputLength = null)
        {
            if (outputLength == null)
            {
                outputLength = hashAlgorithmName == HashAlgorithmName.SHA1 ? 20 :
                               hashAlgorithmName == HashAlgorithmName.SHA256 ? 32 :
                               hashAlgorithmName == HashAlgorithmName.SHA384 ? 48 :
                               hashAlgorithmName == HashAlgorithmName.SHA512 ? 64 :
                               throw new ArgumentOutOfRangeException(nameof(hashAlgorithmName), "Unsupported hash algorithm");
            }

            return Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithmName, outputLength.Value);
        }
    }
}
