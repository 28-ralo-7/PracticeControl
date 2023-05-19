﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace PracticeControl.WebAPI.Helpers
{
    public class PasswordHelper
    {
        public static byte[] GetSalt()
        {
            return RandomNumberGenerator.GetBytes(128 / 8);
        }

        public static string GetHash(byte[]? salt, string password)
        {
            string hashedKey = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashedKey;
        }
    }
}