using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace VotingSistem.Services.Algorithms
{
    public class DesEncryption
    {
        // MULTIPART = SUBIDA DE LOS ARCHIVOS
        // VOTANTE = CODIGO, NOMBRE
        // 13467982
        private string key = "a#cd1234";

        public string encrypt(string plaintext)
        {
            using (DESCryptoServiceProvider provider = new ())
            {
                byte[] keys = Encoding.UTF8.GetBytes(key);
                ICryptoTransform encryptor = provider.CreateEncryptor(keys, keys);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                byte[] input = Encoding.UTF8.GetBytes(plaintext);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public string decrypt(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (DESCryptoServiceProvider provider = new ())
            {
                byte[] keys = Encoding.UTF8.GetBytes(key);
                ICryptoTransform encryptor = provider.CreateDecryptor(keys, keys);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(buffer, 0, buffer.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

    }
}
