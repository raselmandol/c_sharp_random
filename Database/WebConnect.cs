using System;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        //Replace your sever details here-->
        string connectionString = "Data Source=SERVER_NAME;Initial Catalog=DATABASENAME;User ID=USERNAME;Password=PASSWORD;";
        SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
            Console.WriteLine("Connection successful.");

            //Data Insertion here--->
            string insertSql = "INSERT INTO TableName (Column1, Column2) VALUES (@Value1, @Value2)";
            SqlCommand insertCommand = new SqlCommand(insertSql, connection);
            insertCommand.Parameters.AddWithValue("@Value1", "Hello");
            insertCommand.Parameters.AddWithValue("@Value2", "World!");
            int rowsAffected = insertCommand.ExecuteNonQuery();
            Console.WriteLine(rowsAffected + " rows inserted.");

           //delete data
            string deleteSql = "DELETE FROM TableName WHERE Column1 = @Value1";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@Value1", "Hello");
            rowsAffected = deleteCommand.ExecuteNonQuery();
            Console.WriteLine(rowsAffected + " rows deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Couldn't connect to database: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        Console.ReadLine();
    }
}
