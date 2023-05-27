using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CheckedListBoxToDatabase
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
            string selectedChoices = GetSelectedChoices();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO YourTable (ChoicesColumn) VALUES (@Choices)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Choices", selectedChoices);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Data saved to database successfully.");
            }
        }

        private string GetSelectedChoices()
        {
            string selectedChoices = string.Empty;

            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                selectedChoices += checkedListBox1.CheckedItems[i].ToString();

                if (i < checkedListBox1.CheckedItems.Count - 1)
                {
                    selectedChoices += ", ";
                }
            }

            return selectedChoices;
        }
    }
}
