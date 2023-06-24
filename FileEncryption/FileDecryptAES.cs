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

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string inputFile = txtInputFile.Text;
            string outputFile = txtOutputFile.Text;
            string password = txtPassword.Text;

            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    //Set the key and IV from the password
                    aes.Key = GenerateKey(password, aes.KeySize / 8);

                    //Read the IV from the beginning of the input file
                    byte[] iv = new byte[aes.IV.Length];
                    using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
                    {
                        inputFileStream.Read(iv, 0, iv.Length);
                    }

                    aes.IV = iv;

                    //Create a file stream for reading the encrypted input file
                    using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
                    {
                        //Create a file stream for writing the decrypted output file
                        using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
                        {
                            //Create a CryptoStream to perform the decryption
                            using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                //Decrypt the input file and write the decrypted data to the output file
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

                MessageBox.Show("File decrypted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Decryption failed: " + ex.Message);
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
