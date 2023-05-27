using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RichTextBoxToDatabase
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string richText = richTextBox1.Rtf;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO YourTable (RichTextColumn) VALUES (@RichText)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RichText", richText);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Data saved to database successfully.");
            }
        }
    }
}
