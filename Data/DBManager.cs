
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;

namespace Data
{

    public class Test
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Test(int id, string text)
        {
            Id = id;
            Text = text;
        }


        public override string ToString()
        {
            return $"Id: {Id}, Text: {Text}";
        }
    }
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

        public List<Test> SelectFromDb(string query)
        {
            try
            {
                List<Test> result = new List<Test>();
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        result.Add(new Test(sqlReader.GetInt32(0), sqlReader.GetString(1)));
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
