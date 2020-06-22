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
	public class InstitutionRepo : IInstitutionInterface
	{
		private SqlConnection con;
		private void Connection()
		{
			string constr = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString; ;
			con = new SqlConnection(constr);
		}

		public bool Add(Institution institution)
		{
			try
			{
				string query = "INSERT INTO Institution (code, institution_name, institution_address) VALUES (@code, @name, @address);";
				query += " SELECT SCOPE_IDENTITY()";        

				Connection();  

				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					cmd.Parameters.AddWithValue("@code", institution.Code);
					cmd.Parameters.AddWithValue("@name", institution.Name);
					cmd.Parameters.AddWithValue("@address", institution.Address);

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
				string query = "DELETE FROM Institution WHERE Id = @id;";

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

		public IEnumerable<Institution> GetAll()
		{
			List<Institution> institutions = new List<Institution>();

			try
			{
				string query = "SELECT * FROM Institution;";
				Connection();

				DataTable dt = new DataTable();
				DataSet ds = new DataSet();

				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					SqlDataAdapter dataAdapter = new SqlDataAdapter();
					dataAdapter.SelectCommand = cmd;
					dataAdapter.Fill(ds, "Institution");
					dt = ds.Tables["Institution"];
					con.Close();
				}

				foreach (DataRow dataRow in dt.Rows)            
				{
					int institutionId = int.Parse(dataRow["Id"].ToString());   
					string code = dataRow["Code"].ToString();
					string name = dataRow["Institution_name"].ToString();
					string address = dataRow["Institution_address"].ToString();

					institutions.Add(new Institution() { Id = institutionId, Code = code, Name = name, Address = address });
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return institutions;
		}

		public void Update(Institution institution)
		{
			try
			{
				string query = "UPDATE Institution SET code = @Code, institution_name = @Name, institution_address = @Address WHERE id = @Id;";

				Connection();   

				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;   
					cmd.Parameters.AddWithValue("@Id", institution.Id);  
					cmd.Parameters.AddWithValue("@Code", institution.Code);
					cmd.Parameters.AddWithValue("@Name", institution.Name);
					cmd.Parameters.AddWithValue("@Address", institution.Address);

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

		public IEnumerable<Institution> GetByParameters(Dictionary<string, string> searchParameters)
		{
			List<Institution> institutions = new List<Institution>();
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
					dataAdapter.Fill(ds, "Institution");
					dt = ds.Tables["Institution"];
					con.Close();
				}

				foreach (DataRow dataRow in dt.Rows)
				{
					int institutionId = int.Parse(dataRow["Id"].ToString());
					string code = dataRow["Code"].ToString();
					string name = dataRow["Institution_name"].ToString();
					string address = dataRow["Institution_address"].ToString();

					institutions.Add(new Institution() { Id = institutionId, Code = code, Name = name, Address = address });
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return institutions;
		}

		private string GetQuery(Dictionary<string, string> searchParameters, out string obj, out string parameter)
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
							return "SELECT * FROM Institution WHERE Code LIKE @" + parameter + ";";
						case "Name":
							return "SELECT * FROM Institution WHERE Institution_name LIKE @" + parameter + ";";
						case "Address":
							return "SELECT * FROM Institution WHERE Institution_address LIKE @" + parameter + ";";
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
