using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VirtualClassroom.Interfaces;
using VirtualClassroom.Model;

namespace VirtualClassroom.Repository
{
    public class ClassroomRepo : IClassroomsInterface
	{
		private SqlConnection con;
		private void Connection()
		{
			string constr = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			con = new SqlConnection(constr);
		}
		public bool Add(Classroom classroom)
		{
			try
			{
				string query = "INSERT INTO Institution (code, classroom_classroomNo, classroom_seatsNo, classroom_typeOfClassroom) VALUES (@code, @classroomNo, @seatsNo, @typeOfClassroom);";
				query += " SELECT SCOPE_IDENTITY()";

				Connection();

				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					cmd.Parameters.AddWithValue("@code", classroom.Code);
					cmd.Parameters.AddWithValue("@classroomNo", classroom.ClassroomNo);
					cmd.Parameters.AddWithValue("@seatsNo", classroom.SeatsNo);
					cmd.Parameters.AddWithValue("@typeOfClassroom", classroom.HasComp);


					con.Open();
					var newFormedId = cmd.ExecuteScalar();
					con.Close();

					if (newFormedId != null)
					{
						return true;    // upis uspesan, generisan novi id
					}

				}
				return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		public void Delete(int id)
		{
			try
			{
				string query = "DELETE FROM Classroom WHERE Id = @id;";

				Connection();

				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					cmd.Parameters.AddWithValue("@id", id);

					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IEnumerable<Classroom> GetAll()
		{
			List<Classroom> classrooms = new List<Classroom>();

			try
			{
				string query = "SELECT * FROM Classroom;";
				Connection();

				DataTable dt = new DataTable();
				DataSet ds = new DataSet();

				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					SqlDataAdapter dataAdapter = new SqlDataAdapter();
					dataAdapter.SelectCommand = cmd;
					dataAdapter.Fill(ds, "Classroom");
					dt = ds.Tables["Classroom"];
					con.Close();
				}

				foreach (DataRow dataRow in dt.Rows)
				{
					int classroomId = int.Parse(dataRow["Id"].ToString());
					string code = dataRow["Code"].ToString();
					string classroomNo = dataRow["Classroom_classroomNo"].ToString();
					string seatsNo = dataRow["Classroom_seatsNo"].ToString();
					bool hasComp = (dataRow["Classroom_typeOfClassroom"].ToString() == "0") ? false : true;

					classrooms.Add(new Classroom() { Id = classroomId, Code = code, ClassroomNo = GetNo(classroomNo), SeatsNo = GetNo(seatsNo), HasComp = hasComp });
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return classrooms;
		}
		private int GetNo(string text)
		{
			int outNumber;
			bool isValid = Int32.TryParse(text, out outNumber);

			if (isValid)
			{
				return outNumber;
			}
			return 0;
		}

		public void Update(Classroom classroom)
		{
			try
			{
				string query = "UPDATE Classroom SET code = @Code, classroom_classroomNo = @ClassroomNo, classroom_seatsNo = @SeatsNo, classroom_typeOfClassroom = @TypeOfClassroom WHERE id = @Id;";

				Connection();
				 
				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					cmd.Parameters.AddWithValue("@Id", classroom.Id);
					cmd.Parameters.AddWithValue("@Code", classroom.Code);
					cmd.Parameters.AddWithValue("@ClassroomNo", classroom.ClassroomNo);
					cmd.Parameters.AddWithValue("@SeatsNo", classroom.SeatsNo);
					cmd.Parameters.AddWithValue("@TypeOfClassroom", classroom.HasComp);


					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IEnumerable<Classroom> GetByParameters(Dictionary<string, string> searchParameters)
		{
			List<Classroom> classroom = new List<Classroom>();
			string obj, param;

			try
			{
				Connection();
				string query = GetQuery(searchParameters, out obj, out param);
				if (string.IsNullOrEmpty(query))
				{
					return GetAll();
				}
				DataTable dt = new DataTable();
				DataSet ds = new DataSet();

				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					cmd.Parameters.AddWithValue("@" + param, "%" + obj + "%");
					SqlDataAdapter dataAdapter = new SqlDataAdapter();
					dataAdapter.SelectCommand = cmd;
					dataAdapter.Fill(ds, "Classroom");
					dt = ds.Tables["Classroom"];
					con.Close();
				}

				foreach (DataRow dataRow in dt.Rows)
				{
					int classroomId = int.Parse(dataRow["Id"].ToString());
					string code = dataRow["Code"].ToString();
					string classroomNo = dataRow["Classroom_classroomNo"].ToString();
					string seatsNo = dataRow["Classroom_seatsNo"].ToString();
					bool hasComp = (dataRow["Classroom_typeOfClassroom"].ToString() == "0") ? false : true;

					classroom.Add(new Classroom() { Id = classroomId, Code = code, ClassroomNo = GetNo(classroomNo), SeatsNo = GetNo(seatsNo), HasComp = hasComp });
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return classroom;
		}
		
		string GetQuery(Dictionary<string, string> searchParameters, out string obj, out string parameter)
		{
			foreach (string param in searchParameters.Keys)
			{
				if (!string.IsNullOrEmpty(searchParameters[param]))
				{
					obj = searchParameters[param];
					parameter = param.ToLower();
					switch (param)
					{
						case "Code":
							return "SELECT * FROM Classroom WHERE Code LIKE @" + parameter + ";";
						case "Classroom Number":
							return "SELECT * FROM Classroom WHERE Classroom_classroomNo LIKE @" + parameter + ";";
						case "Seats Number":
							return "SELECT * FROM Classroom WHERE Classroom_seatsNo LIKE @" + parameter + ";";
						case "Type of Classroom":
							return "SELECT * FROM Classroom WHERE Classroom_typeOfClassroom LIKE @" + parameter + ";";
						default:
							break;
					}
				}
			}
			parameter = string.Empty;
			obj = string.Empty;
			return string.Empty;
		}
	}
}