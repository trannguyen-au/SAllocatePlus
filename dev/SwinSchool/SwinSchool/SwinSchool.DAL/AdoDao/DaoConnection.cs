using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.AdoDao
{
    public class DaoConnection
    {
        private static readonly object _lock = new object();
        private SqlConnection _conn;
        public DaoConnection(string connectionString)
        {
            try
            {
                _conn = new SqlConnection(connectionString);
                _conn.Open();
                _conn.Close();
            }
            catch (Exception)
            {
                // cannot connect to database using this connection string
                throw;
            }
        }

        public void Open()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Closed)
            {
                _conn.Open();
            }
        }

        public void Close()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        public DataTable ExecuteDataTable(string query)
        {
            var adapt = new SqlDataAdapter(query, _conn);
            var dataTable = new DataTable();
            adapt.Fill(dataTable);
            return dataTable;
        }

        public int ExecuteStatement(string sqlStatement, SqlParameter[] paramList)
        {
            try
            {
                var cmd = _conn.CreateCommand();
                cmd.CommandText = sqlStatement;
                cmd.Parameters.AddRange(paramList);
                Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                Close();
            }
            
            
        }
    }
}
