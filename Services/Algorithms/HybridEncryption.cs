using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;

namespace VotingSistem.Services.Algorithms
{
    public class DesEncryption
    {
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

    public class RsaEncryption
    {
        private readonly RSACryptoServiceProvider _privateKey;
        private readonly RSACryptoServiceProvider _publicKey;

        public RsaEncryption()
        {
            string public_pem = @".\Keys\posvendor.pub.pem";
            string private_pem = @".\Keys\posvendor.key.pem";

            _privateKey = GetPrivateKeyFromPemFile(private_pem);
            _publicKey = GetPublicKeyFromPemFile(public_pem);
        }

        public string Encrypt(string text)
        {
            var encryptedBytes = _publicKey.Encrypt(Encoding.UTF8.GetBytes(text), false);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string encrypted)
        {
            var decryptedBytes = _privateKey.Decrypt(Convert.FromBase64String(encrypted), false);
            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        }

        private RSACryptoServiceProvider GetPrivateKeyFromPemFile(string filePath)
        {
            using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(filePath)))
            {
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }

        private RSACryptoServiceProvider GetPublicKeyFromPemFile(String filePath)
        {
            using (TextReader publicKeyTextReader = new StringReader(File.ReadAllText(filePath)))
            {
                RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParam);

                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }

    }

    public class HybridEncryption
    {
        private readonly DesEncryption _desEncryption;
        private readonly RsaEncryption _rsaEncryption;

        public HybridEncryption()
        {
            _desEncryption = new DesEncryption();
            _rsaEncryption = new RsaEncryption();
        }


        public string Encrypt(string text)
        {
            string des_text = _desEncryption.encrypt(text);
            return _rsaEncryption.Encrypt(des_text);
        }

        public string Decrypt(string encrypted)
        {
            string rsa_text = _rsaEncryption.Decrypt(encrypted);
            return _desEncryption.decrypt(rsa_text);
        }

    }
}
