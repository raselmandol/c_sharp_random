using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string server = "mail.example.com";
        int port = 465;
        string username = "your_username";
        string password = "your_password";

        //Create a TCP client and connect to the server
        using (TcpClient tcpClient = new TcpClient(server, port))
        {
            //Create an SSL stream for secure communication
            using (SslStream sslStream = new SslStream(tcpClient.GetStream()))
            {
                //Authenticate the server and encrypt the connection
                sslStream.AuthenticateAsClient(server);

                //Read and write data using the SSL stream
                StreamReader reader = new StreamReader(sslStream);
                StreamWriter writer = new StreamWriter(sslStream);

                //Send username and password to the server
                writer.WriteLine(username);
                writer.WriteLine(password);
                writer.Flush();

                //Read the response from the server
                string response = reader.ReadLine();
                Console.WriteLine(response);
            }
        }

        Console.ReadLine();
    }
}
