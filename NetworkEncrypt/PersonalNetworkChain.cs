using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class NetworkChainNode
{
    static void Main()
    {
        int port = 1234;
        string nextNodeIpAddress = "next-node-ip-address";
        int nextNodePort = 5678;

        //Start listening for incoming connections
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        
        Console.WriteLine("Node started. Waiting for a connection...");
        
        while (true)
        {
            //Accept the incoming connection
            TcpClient client = listener.AcceptTcpClient();
            
            //Handle the client in a separate task
            Task.Run(() => HandleClient(client, nextNodeIpAddress, nextNodePort));
        }
    }

    static void HandleClient(TcpClient client, string nextNodeIpAddress, int nextNodePort)
    {
        try
        {
            //Read the incoming message
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            
            Console.WriteLine("Received message: " + message);
            
            //Forward the message to the next node
            ForwardMessageToNextNode(message, nextNodeIpAddress, nextNodePort);
            
            //Close the connection
            stream.Close();
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void ForwardMessageToNextNode(string message, string nextNodeIpAddress, int nextNodePort)
    {
        try
        {
            TcpClient client = new TcpClient(nextNodeIpAddress, nextNodePort);
            NetworkStream stream = client.GetStream();
            
            //Send the message to the next node
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            
            //Close the connection
            stream.Close();
            client.Close();
            
            Console.WriteLine("Forwarded message to next node: " + message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error forwarding message: " + ex.Message);
        }
    }
}
