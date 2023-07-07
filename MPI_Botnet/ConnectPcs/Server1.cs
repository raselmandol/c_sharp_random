using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    private const int Port = 8888;

    public static void Main()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); //Set the server IP address
        TcpListener server = new TcpListener(ipAddress, Port);
        
        server.Start();
        Console.WriteLine("Server started. Waiting for connections...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client connected.");

            //Handle client communication in a separate thread
            _ = HandleClientCommunication(client);
        }
    }

    private static async Task HandleClientCommunication(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();

            //Read incoming data from the client
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string requestData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received data: " + requestData);

            //Process the received data or perform necessary work

            //Send a response back to the client
            string responseData = "Server response";
            byte[] responseBytes = Encoding.ASCII.GetBytes(responseData);
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            Console.WriteLine("Sent response: " + responseData);

            client.Close();
            Console.WriteLine("Client disconnected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
