using SwinSchool.DAL.DAO;
using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.AdoDao
{
    public class MyUserDao : IMyUserDao
    {
        DaoConnection _daoConnection;
        public MyUserDao(DaoConnection daoConnection)
        {
            _daoConnection = daoConnection;
        }
        public List<Entities.MyUser> GetAll()
        {
            var query = "Select * from MyUser";
            var queryResult = _daoConnection.ExecuteDataTable(query);
            var listResult = new List<Entities.MyUser>();
            foreach (DataRow row in queryResult.Rows)
            {
                MyUser myUser = ConvertToObject(row);

                listResult.Add(myUser);

            }

            return listResult;
        }

        public Entities.MyUser GetById(string userId)
        {
            var query = "Select * from MyUser where UserID = '" + userId + "'";
            var queryResult = _daoConnection.ExecuteDataTable(query);
            if (queryResult.Rows.Count > 0)
            {
                return ConvertToObject(queryResult.Rows[0]);
            }
            return null;
        }

        public int Create(Entities.MyUser myUser)
        {
            var sqlStatement = "INSERT INTO MyUser(UserID, Name, Address, Email, Password, SecAns, SecQn, Tel) VALUES(@UserID, @Name, @Address, @Email, @Password, @SecAns, @SecQn, @Tel)";
            SqlParameter[] paramList = new SqlParameter[] {
                new SqlParameter("@UserID", myUser.UserID),
                new SqlParameter("@Name", myUser.Name),
                new SqlParameter("@Address", myUser.Address),
                new SqlParameter("@Email", myUser.Email),
                new SqlParameter("@Password", myUser.Password),
                new SqlParameter("@SecAns", myUser.SecAns),
                new SqlParameter("@SecQn", myUser.SecQn),
                new SqlParameter("@Tel", myUser.Tel)
            };
            return _daoConnection.ExecuteStatement(sqlStatement, paramList);
        }

        public int Update(Entities.MyUser myUser)
        {
            var sqlStatement = "UPDATE MyUser SET Name = @Name, Address = @Address, Email = @Email, Password = @Password, SecAns = @SecAns, SecQn = @SecQn, Tel = @Tel WHERE UserID = @UserID";
            SqlParameter[] paramList = new SqlParameter[] {
                new SqlParameter("@UserID", myUser.UserID),
                new SqlParameter("@Name", myUser.Name),
                new SqlParameter("@Address", myUser.Address),
                new SqlParameter("@Email", myUser.Email),
                new SqlParameter("@Password", myUser.Password),
                new SqlParameter("@SecAns", myUser.SecAns),
                new SqlParameter("@SecQn", myUser.SecQn),
                new SqlParameter("@Tel", myUser.Tel)
            };
            return _daoConnection.ExecuteStatement(sqlStatement, paramList);
        }

        public int Delete(Entities.MyUser myUser)
        {
            var sqlStatement = "DELETE FROM MyUser WHERE UserID = @UserID";
            SqlParameter[] paramList = new SqlParameter[] {
                new SqlParameter("@UserID", myUser.UserID)
            };
            return _daoConnection.ExecuteStatement(sqlStatement, paramList);

        }

        private MyUser ConvertToObject(DataRow row)
        {
            MyUser myUser = new MyUser()
            {
                UserID = row["UserID"].ToString(),
                Name = row["Name"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                Password = row["Password"].ToString(),
                SecAns = row["SecAns"].ToString(),
                SecQn = row["SecQn"].ToString(),
                Tel = row["Tel"].ToString()
            };

            return myUser;
        }
    }
}
