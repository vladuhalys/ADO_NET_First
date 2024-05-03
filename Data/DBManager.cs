
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
        public string ConnectionString = "Data Source=\"10.0.0.40, 1433\";Initial Catalog=ado_test;User ID=student;Password=1111;Encrypt=True;Trust Server Certificate=True";

        public DBManager() {
  
        }
        public List<Test> SelectFromDb()
        {
            try
            {
                List<Test> result = new List<Test>();
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM test", connection);
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

        public void InsertToDb(Test test)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO test (text) VALUES (@text)", connection);
                    cmd.Parameters.AddWithValue("@text", test.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateToDb(Test test)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE test SET text = @text WHERE id = @id", connection);
                    cmd.Parameters.AddWithValue("@text", test.Text);
                    cmd.Parameters.AddWithValue("@id", test.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
