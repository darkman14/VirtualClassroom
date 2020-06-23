using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VirtualClassroom.Model;

namespace VirtualClassroom.Repository
{
    public class UserRepo : IUserInterface
    {
        private SqlConnection con;
        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsLogged(string username, string password, out User user)
        {
            try
            {
                string query = "SELECT * FROM UserX WHERE USERNAME = @Username;";
                Connection();

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();


                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Username", username);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(ds, "UserX");
                    dt = ds.Tables["UserX"];
                    con.Close();
                }

                if (dt.Rows.Count == 0)
                {
                    user = null;
                    return false;
                }
                user = new User();
                foreach (DataRow dataRow in dt.Rows)
                {
                    user.Id = int.Parse(dataRow["Id"].ToString());
                    user.Name = dataRow["Uname"].ToString();
                    user.Surname = dataRow["Usurname"].ToString();
                    user.Username = username;
                    user.Email = dataRow["email"].ToString();
                    if (!password.Equals(dataRow["User_password"].ToString()))
                    {
                        return false;
                    }
                    user.UserRole = (User.Role)(short.Parse(dataRow["roleId"].ToString()));
                    user.ProfesorId = int.Parse(dataRow["Id"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
