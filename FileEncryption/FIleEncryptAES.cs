//using AES
using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace FileEncryptionExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string inputFile = txtInputFile.Text;
            string outputFile = txtOutputFile.Text;
            string password = txtPassword.Text;

            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    //Set the key and IV (Initialization Vector) from the password
                    aes.Key = GenerateKey(password, aes.KeySize / 8);
                    aes.GenerateIV();

                    //Create a file stream for reading the input file
                    using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
                    {
                        //Create a file stream for writing the encrypted output file
                        using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
                        {
                            //Write the IV to the beginning of the output file
                            outputFileStream.Write(aes.IV, 0, aes.IV.Length);

                            //Create a CryptoStream to perform the encryption
                            using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                //Encrypt the input file and write the encrypted data to the output file
                                byte[] buffer = new byte[1024];
                                int bytesRead;
                                while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    cryptoStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                }

                MessageBox.Show("File encrypted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Encryption failed: " + ex.Message);
            }
        }

        private static byte[] GenerateKey(string password, int keySize)
        {
            //Use a key derivation function (KDF) to generate a key from the password
            using (var deriveBytes = new Rfc2898DeriveBytes(password, keySize))
            {
                return deriveBytes.GetBytes(keySize);
            }
        }
    }
}
