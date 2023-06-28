using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DirectoryEncryptionApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            string directoryPath = directoryTextBox.Text;

            if (Directory.Exists(directoryPath))
            {
                string password = passwordTextBox.Text;

                EncryptDirectory(directoryPath, password);

                MessageBox.Show("Directory encrypted successfully.", "Encryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invalid directory path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            string directoryPath = directoryTextBox.Text;

            if (Directory.Exists(directoryPath))
            {
                string password = passwordTextBox.Text;

                DecryptDirectory(directoryPath, password);

                MessageBox.Show("Directory decrypted successfully.", "Decryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invalid directory path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EncryptDirectory(string directoryPath, string password)
        {
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                EncryptFile(file, password);
            }
        }

        private void DecryptDirectory(string directoryPath, string password)
        {
            string[] encryptedFiles = Directory.GetFiles(directoryPath, "*.encrypted", SearchOption.AllDirectories);

            foreach (string encryptedFile in encryptedFiles)
            {
                string decryptedFilePath = encryptedFile.Substring(0, encryptedFile.Length - ".encrypted".Length);

                DecryptFile(encryptedFile, decryptedFilePath, password);
            }
        }

        private void EncryptFile(string filePath, string password)
        {
            string encryptedFilePath = filePath + ".encrypted";

            using (Aes aes = Aes.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                //Generate a salt (random bytes) for key derivation
                byte[] salt = new byte[16];
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(salt);
                }

                //Derive a key and IV from the password and salt
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(passwordBytes, salt, 10000);
                byte[] key = keyDerivation.GetBytes(32); // 256-bit key
                byte[] iv = keyDerivation.GetBytes(16); // 128-bit IV

                aes.Key = key;
                aes.IV = iv;

                //Encrypt the file
                using (FileStream inputFileStream = new FileStream(filePath, FileMode.Open))
                using (FileStream outputFileStream = new FileStream(encryptedFilePath, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    //Write the salt to the output file
                    outputFileStream.Write(salt, 0, salt.Length);

                    //Copy the input file data to the crypto stream
                    inputFileStream.CopyTo(cryptoStream);
                }
            }

            //Delete the original file
            File.Delete(filePath);
        }

        private void DecryptFile(string encryptedFilePath, string decryptedFilePath, string password)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                //Read the salt from the encrypted file
                byte[] salt = new byte[16];
                using (FileStream encryptedFileStream = new FileStream(encryptedFilePath, FileMode.Open))
                {
                    encryptedFileStream.Read(salt, 0, salt.Length);
                }

                //Derive a key and IV from the password and salt
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(passwordBytes, salt, 10000);
                byte[] key = keyDerivation.GetBytes(32); // 256-bit key
                byte[] iv = keyDerivation.GetBytes(16); // 128-bit IV

                aes.Key = key;
                aes.IV = iv;

                //Decrypt the file
                using (FileStream encryptedFileStream = new FileStream(encryptedFilePath, FileMode.Open))
                using (CryptoStream cryptoStream = new CryptoStream(encryptedFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (FileStream decryptedFileStream = new FileStream(decryptedFilePath, FileMode.Create))
                {
                    //Skip the salt bytes
                    encryptedFileStream.Seek(salt.Length, SeekOrigin.Begin);

                    //Copy the decrypted data to the output file
                    cryptoStream.CopyTo(decryptedFileStream);
                }
            }

            //Delete the encrypted file
            File.Delete(encryptedFilePath);
        }
    }
}
