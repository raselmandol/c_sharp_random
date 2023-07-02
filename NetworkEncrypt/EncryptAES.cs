using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

public class NetworkEncryptionExample
{
    private const string AESKey = "YourSecretKey";
    private const int Port = 12345;

    public static void Main()
    {
        // Start a TCP listener on the specified port
        TcpListener listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();
        Console.WriteLine("Server started. Waiting for connections...");

        //Accept a client connection
        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Client connected.");

        //Create an AES encryptor with the shared key
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(AESKey);
            aes.IV = new byte[16]; // Initialization Vector (IV)

            //Create encryptor and decryptor from AES instance
            ICryptoTransform encryptor = aes.CreateEncryptor();
            ICryptoTransform decryptor = aes.CreateDecryptor();

            //Get the network stream for communication
            NetworkStream stream = client.GetStream();

            //Receive and decrypt the incoming message
            byte[] encryptedMessage = new byte[4096];
            int bytesRead = stream.Read(encryptedMessage, 0, encryptedMessage.Length);
            byte[] decryptedBytes = new byte[bytesRead];
            decryptor.TransformBlock(encryptedMessage, 0, bytesRead, decryptedBytes, 0);
            string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine("Received: " + decryptedMessage);

            //Respond with an encrypted message
            string response = "Hello from the server!";
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            byte[] encryptedResponse = encryptor.TransformFinalBlock(responseBytes, 0, responseBytes.Length);
            stream.Write(encryptedResponse, 0, encryptedResponse.Length);
            Console.WriteLine("Sent: " + response);

            //Close the connection
            client.Close();
        }

        listener.Stop();
        Console.WriteLine("Server closed.");
    }
}
