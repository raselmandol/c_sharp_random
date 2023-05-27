using System;
using System.Data.SqlClient;
using System.IO;

namespace TextFileToDatabase
{
    class Program
    {
        private const string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD";

        static void Main(string[] args)
        {
            string filePath = "path_to_your_text_file.txt";

            try
            {
                string fileContents = File.ReadAllText(filePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO YourTable (TextColumn) VALUES (@Text)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Text", fileContents);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    Console.WriteLine("Data saved to database successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
