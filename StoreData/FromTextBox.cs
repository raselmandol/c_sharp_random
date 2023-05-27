using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TextBoxToDatabase
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
            string text = textBox1.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO YourTable (TextColumn) VALUES (@Text)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Text", text);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Data saved to database successfully.");
            }
        }
    }
}
