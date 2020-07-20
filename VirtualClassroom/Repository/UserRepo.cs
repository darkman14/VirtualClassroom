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

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();

            try
            {
                string query = "SELECT * FROM UserX;";
                Connection();

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(ds, "UserX");
                    dt = ds.Tables["UserX"];
                    con.Close();
                }

                foreach (DataRow dataRow in dt.Rows)
                {
                    int institutionId = int.Parse(dataRow["Id"].ToString());
                    string name = dataRow["Uname"].ToString();
                    string surname = dataRow["Usurname"].ToString();
                    string email = dataRow["email"].ToString();
                    string username = dataRow["username"].ToString();
                    int roleId = 0;
                    int profesorId = 0;
                    bool isRole = int.TryParse(dataRow["RoleId"].ToString(), out roleId);
                    bool isProfesorId = int.TryParse(dataRow["Profesor_Id"].ToString(), out profesorId);

                    users.Add(new User() { Id = institutionId, Name = name, Surname = surname, Email = email, Username = username, UserRole = (User.Role)roleId, ProfesorId = profesorId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return users;
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

        public User GetByUsername(string username)
        {
            User user = new User();

            try
            {
                Connection();
                string query = "SELECT * FROM Institution WHERE Username = " + username;

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(ds, "UserX");
                    dt = ds.Tables["UserX"];
                    con.Close();
                }

                foreach (DataRow dataRow in dt.Rows)
                {
                    int institutionId = int.Parse(dataRow["Id"].ToString());
                    user.Name = dataRow["Uname"].ToString();
                    user.Surname = dataRow["Usurname"].ToString();
                    user.Email = dataRow["Email"].ToString();
                    user.Username = dataRow["Username"].ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;
        }
    }
}
