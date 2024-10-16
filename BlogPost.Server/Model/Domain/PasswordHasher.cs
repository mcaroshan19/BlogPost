//using System;
//using System.Security.Cryptography;
//namespace BlogPost.Server.Model.Domain

//{
















//    public class PasswordHasher
//    {
//        private static readonly int SaltSize = 16;
//        private static readonly int HashSize = 20;
//        private static readonly int Iterations = 10000;

//        public static string HashPassword(string password)
//        {
//            byte[] salt = new byte[SaltSize];
//            using (var rng = RandomNumberGenerator.Create())
//            {
//                rng.GetBytes(salt);
//            }

//            using (var key = new Rfc2898DeriveBytes(password, salt, Iterations))
//            {
//                byte[] hash = key.GetBytes(HashSize);

//                byte[] hashBytes = new byte[SaltSize + HashSize];
//                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
//                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

//                return Convert.ToBase64String(hashBytes);
//            }
//        }

//        public static bool VerifyPassword(string password, string base64Hash)
//        {
//            byte[] hashBytes = Convert.FromBase64String(base64Hash);

//            byte[] salt = new byte[SaltSize];
//            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

//            using (var key = new Rfc2898DeriveBytes(password, salt, Iterations))
//            {
//                byte[] hash = key.GetBytes(HashSize);

//                for (int i = 0; i < HashSize; i++)
//                {
//                    if (hashBytes[i + SaltSize] != hash[i])
//                        return false;
//                }
//                return true;
//            }
//        }
//    }


//}







































using System;
using System.Security.Cryptography;

namespace BlogPost.Server.Model.Domain
{
    public class PasswordHasher
    {
        private static readonly int SaltSize = 16;  // Size of the salt in bytes
        private static readonly int HashSize = 20;  // Size of the hash in bytes
        private static readonly int Iterations = 10000;  // Number of iterations for the hashing algorithm

        /// <summary>
        /// Hashes a password using a random salt and returns the salted hash as a base64 string.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>A base64 encoded string containing the salt and hashed password.</returns>
        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Generate the hash using PBKDF2 algorithm
            using (var key = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = key.GetBytes(HashSize);

                // Combine the salt and hash
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // Return the final salted hash as a base64 string
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Verifies a password against the stored salted hash.
        /// </summary>
        /// <param name="password">The password provided by the user during login.</param>
        /// <param name="base64Hash">The base64 encoded string containing the salt and hashed password stored in the database.</param>
        /// <returns>True if the password matches the hash, false otherwise.</returns>
        public static bool VerifyPassword(string password, string base64Hash)
        {
            // Decode the base64 encoded salted hash
            byte[] hashBytes = Convert.FromBase64String(base64Hash);

            // Extract the salt
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Hash the input password using the extracted salt
            using (var key = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = key.GetBytes(HashSize);

                // Compare the stored hash and the computed hash
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}


