using System.Security.Cryptography;

namespace Security.Business.Authentication
{
    public static class Encryptor
    {
        public static byte[] CreateSalt()
        {
            //Generate a cryptographic random number.
            const int saltSize = 32;
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[saltSize];
            rng.GetBytes(buff);

            // Return random number
            return buff;
        }

        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            return algorithm.ComputeHash(ConcatBytes(plainText, salt));
        }

        private static byte[] ConcatBytes(byte[] plainText, byte[] salt)
        {
            var plainTextWithSaltBytes =
                new byte[plainText.Length + salt.Length];

            for (var i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }

            for (var i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return plainTextWithSaltBytes;
        }
    }
}