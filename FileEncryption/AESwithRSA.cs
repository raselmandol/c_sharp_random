using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string secretKey = "YourSecretKey"; //Replace with your own secret key

        //Generate RSA key pair
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            //Encrypt the secret key with RSA
            byte[] encryptedKey = Encrypt(secretKey, rsa.ExportParameters(false));

            //Encrypt the already encrypted key with AES
            string aesKey = "YourAESKey"; //Replace with your own AES key
            byte[] doubleEncryptedKey = EncryptWithAES(encryptedKey, aesKey);

            Console.WriteLine("Double Encrypted Secret Key: " + Convert.ToBase64String(doubleEncryptedKey));

            //Decrypt the double encrypted key with AES
            string decryptedKey = DecryptWithAES(doubleEncryptedKey, aesKey);

            //Decrypt the decrypted key with RSA
            string originalKey = Decrypt(decryptedKey, rsa.ExportParameters(true));

            Console.WriteLine("Original Secret Key: " + originalKey);
        }
    }

    static byte[] Encrypt(string plainText, RSAParameters publicKey)
    {
        byte[] encryptedBytes;

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(publicKey);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            encryptedBytes = rsa.Encrypt(plainBytes, true);
        }

        return encryptedBytes;
    }

    static string Decrypt(byte[] encryptedBytes, RSAParameters privateKey)
    {
        string decryptedText;

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(privateKey);

            byte[] plainBytes = rsa.Decrypt(encryptedBytes, true);
            decryptedText = Encoding.UTF8.GetString(plainBytes);
        }

        return decryptedText;
    }

    static byte[] EncryptWithAES(byte[] plainBytes, string key)
    {
        byte[] encryptedBytes;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                memoryStream.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }

                encryptedBytes = memoryStream.ToArray();
            }
        }

        return encryptedBytes;
    }

    static string DecryptWithAES(byte[] encryptedBytes, string key)
    {
        string decryptedText;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);

            byte[] iv = new byte[16];
            Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length);
                    cryptoStream.FlushFinalBlock();
                }

                decryptedText = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        //return decrypted text
        return decryptedText;
    }
}
