using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FileEncryptionAndEmail
{
    public partial class MainForm : Form
    {
        private const string Key = "mySecretKey";
        private const string SenderEmail = "your_email@example.com";
        private const string SenderPassword = "your_email_password";
        private const string RecipientEmail = "recipient_email@example.com";
        private const string SmtpHost = "smtp.example.com";
        private const int SmtpPort = 587;

        public MainForm()
        {
            InitializeComponent();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            string filePath = fileTextBox.Text;

            if (File.Exists(filePath))
            {
                string encryptedFilePath = EncryptFile(filePath, Key);

                SendEmailWithAttachment(encryptedFilePath);
            }
            else
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string EncryptFile(string filePath, string key)
        {
            string encryptedFilePath = filePath + ".encrypted";

            using (FileStream inputFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (FileStream encryptedFileStream = new FileStream(encryptedFilePath, FileMode.Create, FileAccess.Write))
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(key, new byte[16]);
                byte[] keyBytes = keyDerivation.GetBytes(16);

                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor();

                using (CryptoStream cryptoStream = new CryptoStream(encryptedFileStream, encryptor, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }
            }

            return encryptedFilePath;
        }

        private void SendEmailWithAttachment(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string pcName = Environment.MachineName;

            MailMessage mail = new MailMessage(SenderEmail, RecipientEmail);
            mail.Subject = "Encrypted File";
            mail.Body = $"Please find the encrypted file attached.\n\nDirectory: {directory}\nPC Name: {pcName}";

            Attachment attachment = new Attachment(filePath);
            mail.Attachments.Add(attachment);

            SmtpClient smtpClient = new SmtpClient(SmtpHost, SmtpPort);
            smtpClient.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
                MessageBox.Show("Email sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mail.Dispose();
                smtpClient.Dispose();
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
