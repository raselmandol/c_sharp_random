using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string originalText = "Hello, World!";
        string key = "YourSecretKey"; //Replace with your own secret key

        byte[] encryptedBytes = Encrypt(originalText, key);
        string encryptedText = Convert.ToBase64String(encryptedBytes);

        Console.WriteLine("Encrypted Text: " + encryptedText);

        //decrypted text
        string decryptedText = Decrypt(encryptedBytes, key);
        Console.WriteLine("Decrypted Text: " + decryptedText);
    }

    static byte[] Encrypt(string text, string key)
    {
        byte[] encryptedBytes;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                memoryStream.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(textBytes, 0, textBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }

                encryptedBytes = memoryStream.ToArray();
            }
        }

        return encryptedBytes;
    }

    static string Decrypt(byte[] encryptedBytes, string key)
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

        return decryptedText;
    }
}
