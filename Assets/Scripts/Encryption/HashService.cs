using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace AnimArch.Encryption
{
    public class HashService
    {
        public static string GenerateSHA256(string InputToHash)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(InputToHash);
            StringBuilder sb = new StringBuilder();

            using (SHA256Managed sha256 = new SHA256Managed())
            {
                byte[] hash = sha256.ComputeHash(stringBytes);
                foreach (Byte b in hash)
                    sb.Append(b.ToString("X2"));
            }
            
            return 	sb.ToString();    
        }
    }
}