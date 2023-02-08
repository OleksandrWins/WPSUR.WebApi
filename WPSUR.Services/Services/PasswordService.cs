using System.Security.Cryptography;
using WPSUR.Services.Exceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        private readonly int iterations = 1000;
        private readonly int hashSize = 496;
        private readonly int saltSize = 16;
        private readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA1;

        public string HashPassword(string password)
        {
            byte[] saltBuffer;
            byte[] hashBuffer;

            using (var keyDerivation = new Rfc2898DeriveBytes(password, saltSize, iterations, hashAlgorithmName))
            {
                saltBuffer = keyDerivation.Salt;
                hashBuffer = keyDerivation.GetBytes(hashSize);
            }

            byte[] result = new byte[hashSize + saltSize];
            Buffer.BlockCopy(hashBuffer, 0, result, 0, hashSize);
            Buffer.BlockCopy(saltBuffer, 0, result, hashSize, saltSize);
            var hashedPassword = Convert.ToBase64String(result);
            return hashedPassword;
        }

        public bool ValidatePassword(string providedPassword, string hashedPassword)
        {
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            if (hashedPasswordBytes.Length != hashSize + saltSize)
            {
                throw new PasswordInDBException();
            }

            byte[] hashBytes = new byte[hashSize];
            Buffer.BlockCopy(hashedPasswordBytes, 0, hashBytes, 0, hashSize);
            byte[] saltBytes = new byte[saltSize];
            Buffer.BlockCopy(hashedPasswordBytes, hashSize, saltBytes, 0, saltSize);
            byte[] providedHashBytes;
            using (var keyDerivation = new Rfc2898DeriveBytes(providedPassword, saltBytes, iterations, hashAlgorithmName))
            {
                providedHashBytes = keyDerivation.GetBytes(hashSize);
            }

            return hashBytes.SequenceEqual(providedHashBytes);
        }
    }
}


