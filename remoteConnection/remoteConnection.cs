using System;
using System.Net.Sockets;
using System.Text;

class RemoteCodeExecutor
{
    static void Main()
    {
        //Remote machine information
        string remoteIpAddress = "remote-ip-address";
        int remotePort = 1234;
        
        //Connect to the remote machine
        TcpClient client = new TcpClient();
        client.Connect(remoteIpAddress, remotePort);
        
        //Get the network stream for sending and receiving data
        NetworkStream stream = client.GetStream();
        
        //Send code to be executed on the remote machine
        string code = "Console.WriteLine(\"Hello from the remote machine!\");";
        byte[] data = Encoding.ASCII.GetBytes(code);
        stream.Write(data, 0, data.Length);
        
        //Receive the response from the remote machine
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        
        //Display the response from the remote machine
        Console.WriteLine("Response from the remote machine:");
        Console.WriteLine(response);
        
        //Close the connection
        stream.Close();
        client.Close();
        
        Console.ReadLine();
    }
}
