using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    private const int Port = 12345; //Port number for communication

    public static void Main()
    {
        //Start the server
        StartServer();
    }

    private static void StartServer()
    {
        TcpListener listener = null;
        try
        {
            //Listen for incoming connections
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); //IP address of the server
            listener = new TcpListener(ipAddress, Port);
            listener.Start();

            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                //Handle each client connection in a separate thread or task
                //To enable concurrent processing

                //Example: HandleClientAsync(client);
                //Or use a thread pool for handling multiple clients concurrently
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            listener?.Stop();
        }
    }
}
