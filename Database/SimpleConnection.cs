using System;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Integrated Security=True;";
        SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
            Console.WriteLine("Connection successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Couldn't Connect to Database " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        Console.ReadLine();
    }
}


//Replace SERVER_NAME with the name of your SQL Server instance and DATABASE_NAME with the name of your database.
//this program uses the ADO.NET library
//change other details according to your needs

