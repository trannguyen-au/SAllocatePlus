using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinSchool.DAL.Entities;
using System.Data.SqlClient;

namespace SwinSchool.DAL.DAO.AdoImpl
{
    /// <summary>
    /// Implementation of IMyUserDao using ADO.Net
    /// </summary>
    public class MyUserAdoDao : IMyUserDao
    {
        DaoConnection _daoConn;
        public MyUserAdoDao(string connectionString)
        {
            _daoConn = new DaoConnection(connectionString);
        }
        public int Create(MyUser myUser)
        {
            string query = @"INSERT INTO MyUser(UserID, Name, Password, Email, Tel, Address, SecQn, SecAns) 
                VALUES(@UserID, @Name, @Password, @Email, @Tel, @Address, @SecQn, @SecAns)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", myUser.UserID),
                new SqlParameter("@Name", myUser.Name),
                new SqlParameter("@Password", myUser.Password),
                new SqlParameter("@Email", myUser.Email),
                new SqlParameter("@Tel", myUser.Tel),
                new SqlParameter("@Address", myUser.Address),
                new SqlParameter("@SecQn", myUser.SecQn),
                new SqlParameter("@SecAns", myUser.SecAns)
            };
            return _daoConn.ExecuteNonQuery(query, parameters);
        }

        public int Update(MyUser myUser)
        {
            string query = @"UPDATE MyUser SET Name=@Name
                                , Password=@Password
                                , Email=@Email
                                , Tel=@Tel
                                , Address=@Address
                                , SecQn=@SecQn
                                , SecAns=@SecAns
                            WHERE UserID=@UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", myUser.UserID),
                new SqlParameter("@Name", myUser.Name),
                new SqlParameter("@Password", myUser.Password),
                new SqlParameter("@Email", myUser.Email),
                new SqlParameter("@Tel", myUser.Tel),
                new SqlParameter("@Address", myUser.Address),
                new SqlParameter("@SecQn", myUser.SecQn),
                new SqlParameter("@SecAns", myUser.SecAns)
            };
            return _daoConn.ExecuteNonQuery(query, parameters);
        }

        public int Delete(MyUser myUser)
        {
            string query = @"DELETE FROM MyUser 
                            WHERE UserID=@UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", myUser.UserID)
            };
            return _daoConn.ExecuteNonQuery(query, parameters);
        }

        public List<MyUser> GetAll()
        {
            var query = "SELECT * FROM MyUser";
            var resultTable = _daoConn.ExecuteForTableResult(query);
            var resultList = resultTable.ToObjects<MyUser>();
            return resultList;
        }

        public MyUser GetById(string userId)
        {
            var query = "SELECT * FROM MyUser WHERE UserID = @UserID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };
            var resultTable = _daoConn.ExecuteForTableResult(query, parameters);
            if (resultTable.Rows.Count > 0)
            {
                return resultTable.ToObjects<MyUser>()[0];
            }
            else return null;
        }


        public MyUser GetByUserName(string username)
        {
            throw new NotImplementedException();
        }
    }
}
