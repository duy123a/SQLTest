using Microsoft.Data.SqlClient;
using System.Data;

namespace SQLTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = "172.24.17.122,1433",
                InitialCatalog = "Contacts",
                UserID = "sa",
                Password = "7551656As!",
                TrustServerCertificate = true,

            }.ConnectionString;

            var connection = new SqlConnection
            {
                ConnectionString = connectionString
            };

            //var command = new SqlCommand
            //{
            //    Connection = connection,
            //    CommandText = "SELECT COUNT(*) FROM CONTACTS"
            //};

            //// Open connection
            //connection.Open();

            //// Do the query and get a result
            //var result = (int)command.ExecuteScalar();

            //// Close the connection
            //connection.Close();

            //Console.WriteLine($"{result} contacts found in the database");
            //Console.ReadLine();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM CONTACTS"
            };

            var dataTable = new DataTable();

            try
            {
                connection.Open();

                // Use SqlDataAdapter to fill the DataTable
                using var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);

                // Create a DataView from the DataTable
                var dataView = dataTable.DefaultView;

                // Close the connection
                connection.Close();

                // Example: Print out the rows in the DataView (for debugging)
                foreach (DataRowView row in dataView)
                {
                    Console.WriteLine($"{row["Id"]}, {row["ContactName"]}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
