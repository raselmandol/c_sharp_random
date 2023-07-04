using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ServerProgram
{
    static void Main()
    {
        int port = 1234;
        
        //Listen for incoming connections
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        
        Console.WriteLine("Waiting for a client to connect...");
        
        //Accept the client connection
        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Client connected!");
        
        //Get the network stream for sending and receiving data
        NetworkStream stream = client.GetStream();
        
        //Receive the code from the client
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string code = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        
        //Execute the code
        string response = ExecuteCode(code);
        
        //Send the response back to the client
        byte[] data = Encoding.ASCII.GetBytes(response);
        stream.Write(data, 0, data.Length);
        
        //Close the connection
        stream.Close();
        client.Close();
        listener.Stop();
        
        Console.WriteLine("Execution completed. Server stopped.");
        Console.ReadLine();
    }
    
    static string ExecuteCode(string code)
    {
        //Execute the code and return the result
        try
        {
            var output = new StringBuilder();
            
            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new System.Reflection.AssemblyName("TempAssembly"),
                System.Reflection.Emit.AssemblyBuilderAccess.Run);
                
            var module = assembly.DefineDynamicModule("TempModule");
            var type = module.DefineType("TempType");
            var method = type.DefineMethod("TempMethod",
                System.Reflection.MethodAttributes.Public |
                System.Reflection.MethodAttributes.Static,
                null, null);
                
            var il = method.GetILGenerator();
            var endOfMethod = il.DefineLabel();
            
            il.Emit(System.Reflection.Emit.OpCodes.Nop);
            il.Emit(System.Reflection.Emit.OpCodes.Ldstr, code);
            il.Emit(System.Reflection.Emit.OpCodes.Call,
                typeof(System.Console).GetMethod("WriteLine",
                new[] { typeof(string) }));
            il.Emit(System.Reflection.Emit.OpCodes.Br_S, endOfMethod);
            
            il.MarkLabel(endOfMethod);
            il.Emit(System.Reflection.Emit.OpCodes.Ret);
            
            type.CreateType();
            assembly.EntryPoint.Invoke(null, null);
            
            return output.ToString();
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
}
