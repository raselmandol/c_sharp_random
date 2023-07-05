using System;
using System.Net.Sockets;
using System.Text;

public class Client
{
    private const int Port = 12345; //Port number for communication

    public static void Main()
    {
        //Connect to the server
        StartClient();
    }

    private static void StartClient()
    {
        TcpClient client = null;
        try
        {
            // Connect to the server
            client = new TcpClient("127.0.0.1", Port); //IP address and port of the server

            Console.WriteLine("Connected to the server.");

            //Perform processing tasks
            //Example: Send data to the server
            byte[] data = Encoding.ASCII.GetBytes("Processing task data");
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);

            // Receive results from the server
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string result = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Console.WriteLine("Received result from the server: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            client?.Close();
        }
    }
}
