
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;

namespace Data
{
    public class DBManager
    {
        public string? ConnectionString { get; set; } = null;

        public DBManager(string? connectionString = null) {
            this.ConnectionString = connectionString;
        }
        
        public bool ConnectToDB()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                }
                return true;
            }
            catch(SqlException ex) 
            {
                throw new Exception(ex.Message);
            }
           
        }

        public int CreateOrInsertOrDelete(string query)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    result = cmd.ExecuteNonQuery();
                }
                return result;
                
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> SelectFromDb(string query)
        {
            try
            {
                List<string> result = new List<string>();
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    var sqlReader = cmd.ExecuteReader();
                    while(sqlReader.Read())
                    {
                        for (int i = 1; i < sqlReader.FieldCount; i++)
                        {
                            result.Add(sqlReader.GetValue(i).ToString());
                        }
                        
                    }
                }
                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
