//encrypted mail using MimeKit
using MimeKit;
using MimeKit.Cryptography;
using MailKit.Net.Smtp;
using MailKit.Security;

class Program
{
    static void Main(string[] args)
    {
        string senderEmail = "sender@example.com";
        string recipientEmail = "recipient@example.com";
        string subject = "Encrypted Email";
        string body = "This is an encrypted email.";

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Sender", senderEmail));
        message.To.Add(new MailboxAddress("Recipient", recipientEmail));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        //Encrypt the message using the recipient's public key
        var recipientPublicKey = new CmsRecipient(recipientEmail);
        message.Body = ApplicationPkcs7Mime.Encrypt(message.Body, recipientPublicKey);

        //Send the encrypted email
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.example.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("username", "password");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
