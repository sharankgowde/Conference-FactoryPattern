using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace Design_patterns
{
  
    internal class SQLDatabase : IDatabase
    {

        SqlConnection conn = new
      SqlConnection("Server=tcp:dbservertest-1.database.windows.net,1433;Initial Catalog=CustomerDb;Persist Security Info=False;User ID=sharankgowde;Password=satyam123$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        public async Task<int> insertUserdata(UserDetails userDetails)
        {
            int rowsAffected = 0;
            //using (SqlConnection connection = new SqlConnection(conn))
            //{
                string query = "INSERT INTO UserDetails (Id, Username, Salary, Comments) VALUES (@Value1, @Value2, @Value3, @Value4)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Value1", Guid.NewGuid());
                    command.Parameters.AddWithValue("@Value2", userDetails.Username);
                    command.Parameters.AddWithValue("@Value3", userDetails.Salary);
                command.Parameters.AddWithValue("@Value4", userDetails.Comments    );

                try
                    {
                   await conn.OpenAsync();
                    rowsAffected = await command.ExecuteNonQueryAsync();
                      Console.WriteLine("Rows Inserted: " + rowsAffected);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
           // }

            return rowsAffected;
        }

        public void updateUserdata(string username)
        {
            Console.WriteLine("update into SQL database");
        }

        public async Task<IEnumerable<UserDetails>> GetUserDetailsAsync()
        {
            string sql = "select * from Userdetails";
            var result = new List<UserDetails>();
          
            try
            {
                  await conn.OpenAsync();
              //  connection.Wait();

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserDetails userDetails = new UserDetails();
                 
                            userDetails.Id = reader.GetString(0); 
                            userDetails.Username = reader.GetString(1); 
                            userDetails.Salary = reader.GetString(2);
                            userDetails.Comments = reader.GetString(3);
                            result.Add(userDetails);

                        }
                    }
                }

                }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return result;

        }

       
    }
}
