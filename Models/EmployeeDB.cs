using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Employee_data.Models
{
    public class EmployeeDB
    {
        string Con = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        public List<EmployeeModel> ListAll()
        {
            List<EmployeeModel> lst = new List<EmployeeModel>();
            using (SqlConnection con = new SqlConnection(Con))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectUsers", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var objData = new EmployeeModel();
                    objData.ID = rdr["ID"].ToString();
                    objData.Firstname = rdr["Firstname"].ToString();
                    objData.Lastname = rdr["Lastname"].ToString();
                    objData.City = rdr["City"].ToString();
                    objData.State = rdr["State"].ToString();
                    objData.Country = rdr["Country"].ToString();
                    objData.Qualification = rdr["Qualification"].ToString();
                    objData.Emailid = rdr["Emailid"].ToString();
                    objData.Mobilenumber = rdr["Mobilenumber"].ToString();
                    lst.Add(objData);

                }
                return lst;
            }
        }


        public int Update(EmployeeModel emp)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Con))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateUsers", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", emp.ID);
                com.Parameters.AddWithValue("@Firstname", emp.Firstname);
                com.Parameters.AddWithValue("@Lastname", emp.Lastname);
                com.Parameters.AddWithValue("@DOB", emp.DOB);
                com.Parameters.AddWithValue("@City", emp.City);
                com.Parameters.AddWithValue("@State", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Qualification", emp.Qualification);
                com.Parameters.AddWithValue("@Emailid", emp.Emailid);
                com.Parameters.AddWithValue("@Mobilenumber", emp.Mobilenumber);                
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}