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
            //Encrypt the secret key
            byte[] encryptedKey = Encrypt(secretKey, rsa.ExportParameters(false));

            Console.WriteLine("Encrypted Secret Key: " + Convert.ToBase64String(encryptedKey));

            //Decrypt the secret key
            string decryptedKey = Decrypt(encryptedKey, rsa.ExportParameters(true));

            Console.WriteLine("Decrypted Secret Key: " + decryptedKey);
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
}
