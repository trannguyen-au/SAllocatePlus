using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.DAO.AdoImpl
{
    /// <summary>
    /// ADO.NET required to manage our own database connection. 
    /// This class provides a set of functions to interact with Database Layer (Sql Server DBMS)
    /// </summary>
    public class DaoConnection
    {
        private SqlConnection _conn;
        private bool _executeInTransaction;
        private bool _isRollbackTransaction;
        private SqlTransaction _transaction;

        public DaoConnection(string connectionString)  : this(new SqlConnection(connectionString))
        {
        }

        public DaoConnection(SqlConnection conn)
        {
            _conn = conn;
            _executeInTransaction = false;
            _isRollbackTransaction = false;
        }

        private void OpenConnection()
        {
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
                if (_executeInTransaction && _transaction == null)
                {
                    _transaction = _conn.BeginTransaction();
                }
            }
        }

        private void CloseConnection()
        {
            if (_conn.State == ConnectionState.Open && !_executeInTransaction)
            {
                if (_transaction != null)
                {
                    if (_isRollbackTransaction)
                    {
                        _transaction.Rollback();
                        _transaction.Dispose();
                        _transaction = null;
                        _conn.Close();
                    }
                    else if (!_isRollbackTransaction)
                    {
                        _transaction.Commit();
                        _transaction.Dispose();
                        _transaction = null;
                        _conn.Close();
                    }
                }
                else
                {
                    _conn.Close();
                }
            }
        }

        public void StartTransaction()
        {
            _executeInTransaction = true;
            OpenConnection();
        }

        public void CommitTransaction()
        {
            _executeInTransaction = false;
            _isRollbackTransaction = false;
            CloseConnection();
        }

        public void RollbackTransaction()
        {
            _executeInTransaction = false;
            _isRollbackTransaction = true;
            CloseConnection();
        }

        public DataTable ExecuteForTableResult(string query)
        {
            try
            {
                OpenConnection();
                DataTable tblResult = new DataTable();
                SqlDataAdapter adapt = new SqlDataAdapter(query, _conn);
                adapt.Fill(tblResult);
                return tblResult;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable ExecuteForTableResult(string statement, SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();
                DataTable tblResult = new DataTable();
                SqlCommand cmd = _conn.CreateCommand();
                cmd.CommandText = statement;
                cmd.Parameters.AddRange(parameters);

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                adapt.Fill(tblResult);
                return tblResult;
            }
            finally
            {
                CloseConnection();
            }
        }

        public int ExecuteNonQuery(string queryStatement)
        {
            try
            {
                OpenConnection();
                var cmd = _conn.CreateCommand();
                cmd.CommandText = queryStatement;
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }

        public int ExecuteNonQuery(string statement, SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();
                var cmd = _conn.CreateCommand();
                cmd.CommandText = statement;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }

        public T ExecuteScalar<T>(string queryStatement)
        {
            try
            {
                OpenConnection();
                var cmd = _conn.CreateCommand();
                cmd.CommandText = queryStatement;
                return (T)Convert.ChangeType(cmd.ExecuteScalar(), typeof(T));
            }
            finally
            {
                CloseConnection();
            }
        }

        public T ExecuteScalar<T>(string statement, SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();
                var cmd = _conn.CreateCommand();
                cmd.CommandText = statement;
                cmd.Parameters.AddRange(parameters);
                return (T)Convert.ChangeType(cmd.ExecuteScalar(), typeof(T));
            }
            finally
            {
                CloseConnection();
            }
        }

        public object ExecuteScalar(string queryStatement)
        {
            try
            {
                OpenConnection();
                var cmd = _conn.CreateCommand();
                cmd.CommandText = queryStatement;
                return cmd.ExecuteScalar();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
