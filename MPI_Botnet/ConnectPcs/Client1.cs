using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    private const int Port = 8888;

    public static void Main()
    {
        try
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", Port); //Set the server IP address

            Console.WriteLine("Connected to server.");

            NetworkStream stream = client.GetStream();

            //Send data to the server
            string requestData = "Client request";
            byte[] requestBytes = Encoding.ASCII.GetBytes(requestData);
            stream.Write(requestBytes, 0, requestBytes.Length);
            Console.WriteLine("Sent data: " + requestData);

            //Read the response from the server
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string responseData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received response: " + responseData);

            client.Close();
            Console.WriteLine("Disconnected from server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
