using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FileExtensionEncryption
{
    public partial class MainForm : Form
    {
        private const string Key = "mySecretKey";

        public MainForm()
        {
            InitializeComponent();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            string extension = extensionTextBox.Text;
            string encryptedExtension = EncryptExtension(extension, Key);

            encryptedExtensionTextBox.Text = encryptedExtension;
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            string encryptedExtension = encryptedExtensionTextBox.Text;
            string decryptedExtension = DecryptExtension(encryptedExtension, Key);

            decryptedExtensionTextBox.Text = decryptedExtension;
        }

        private string EncryptExtension(string extension, string key)
        {
            byte[] extensionBytes = Encoding.UTF8.GetBytes(extension);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(key, new byte[16]);
                byte[] keyBytes = keyDerivation.GetBytes(16);

                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] encryptedExtensionBytes = encryptor.TransformFinalBlock(extensionBytes, 0, extensionBytes.Length);

                return Convert.ToBase64String(encryptedExtensionBytes);
            }
        }

        private string DecryptExtension(string encryptedExtension, string key)
        {
            byte[] encryptedExtensionBytes = Convert.FromBase64String(encryptedExtension);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(key, new byte[16]);
                byte[] keyBytes = keyDerivation.GetBytes(16);

                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decryptedExtensionBytes = decryptor.TransformFinalBlock(encryptedExtensionBytes, 0, encryptedExtensionBytes.Length);

                return Encoding.UTF8.GetString(decryptedExtensionBytes);
            }
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
