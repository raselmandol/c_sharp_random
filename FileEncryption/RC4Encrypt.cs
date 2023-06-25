using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FileEncryptionExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string inputFile = openFileDialog.FileName;
                string outputFile = inputFile + ".encrypted";
                string password = passwordTextBox.Text;

                try
                {
                    using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                    using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                    using (RC4CryptoServiceProvider rc4 = new RC4CryptoServiceProvider(Encoding.ASCII.GetBytes(password)))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;

                        while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            byte[] encryptedBytes = rc4.TransformFinalBlock(buffer, 0, bytesRead);
                            fsOutput.Write(encryptedBytes, 0, encryptedBytes.Length);
                        }
                    }

                    MessageBox.Show("File encrypted successfully!", "Encryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during encryption: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
