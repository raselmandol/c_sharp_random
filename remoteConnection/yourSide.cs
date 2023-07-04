using System;
using System.Net.Sockets;
using System.Text;

class ClientProgram
{
    static void Main()
    {
        //Server machine information
        string serverIpAddress = "server-ip-address";
        int serverPort = 1234;
        
        //Connect to the server
        TcpClient client = new TcpClient();
        client.Connect(serverIpAddress, serverPort);
        
        //Get the network stream for sending and receiving data
        NetworkStream stream = client.GetStream();
        
        //Send code to be executed on the server
        string code = "Console.WriteLine(\"Hello from the client!\");";
        byte[] data = Encoding.ASCII.GetBytes(code);
        stream.Write(data, 0, data.Length);
        
        //Receive the response from the server
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        
        //Display the response from the server
        Console.WriteLine("Response from the server:");
        Console.WriteLine(response);
        
        //Close the connection
        stream.Close();
        client.Close();
        
        Console.ReadLine();
    }
}
