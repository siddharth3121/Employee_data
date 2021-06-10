using Employee_data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_data.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDB empDB = new EmployeeDB();
        string Con = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        public ActionResult Getdata()
        {
            var lstdata = new List<EmployeeModel>();
            String sQuery = "Select * from Qualification";
            SqlConnection sqlCon = new SqlConnection(Con);
            SqlCommand sqlcom = new SqlCommand(sQuery, sqlCon);
            sqlCon.Open();
            var objReader = sqlcom.ExecuteReader();

            while (objReader.Read())
            {
                var obj = new EmployeeModel();
                obj.key = objReader["Qualification"].ToString();
                obj.Value = objReader["Qualification"].ToString();
                lstdata.Add(obj);
            }
            ViewBag.Qualification = lstdata;
            return View();
        }

        public JsonResult List()
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
                    objData.DOB =   rdr["DOB"].ToString();
                    objData.State = rdr["State"].ToString();
                    objData.Country = rdr["Country"].ToString();
                    objData.Qualification = rdr["Qualification"].ToString();
                    objData.Emailid = rdr["Emailid"].ToString();
                    objData.Mobilenumber = rdr["Mobilenumber"].ToString();

                    lst.Add(objData);

                }
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Add(EmployeeModel emp)
        {
            string Status = "";
            using (SqlConnection con = new SqlConnection(Con))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Insertupdateemployeedata", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Firstname", emp.Firstname);
                com.Parameters.AddWithValue("@Lastname", emp.Lastname);
                com.Parameters.AddWithValue("@DOB", emp.DOB);
                com.Parameters.AddWithValue("@City", emp.City);
                com.Parameters.AddWithValue("@State", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Qualification", emp.Qualification);
                com.Parameters.AddWithValue("@Emailid", emp.Emailid);
                com.Parameters.AddWithValue("@Mobilenumber", emp.Mobilenumber);
                com.Parameters.AddWithValue("@Action", "Insert");
                int i = com.ExecuteNonQuery();
                if (i > 0)
                {
                    Status = "Data Added";
                }
                else
                {
                    Status = "Invalid or incomplete data";
                }

            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(EmployeeModel emp)
        {
            string Status = "";
            using (SqlConnection con = new SqlConnection(Con))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Insertupdateemployeedata", con);
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
                int i = com.ExecuteNonQuery();
                if (i > 0)
                {
                    Status = "Data Inserted";
                }
                else
                {
                    Status = "Data not Inserted";
                }
            }
            return Json(Status, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Con))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteUsers", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", ID);
                i = com.ExecuteNonQuery();
            }

            return Json("Data deleted Successfully", JsonRequestBehavior.AllowGet);
        }
                
        public JsonResult GetbyID(int ID)
        {
            try
            {
                List<EmployeeModel> lst = new List<EmployeeModel>();
                using (SqlConnection con = new SqlConnection(Con))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("Select * from Employee_data where ID=@ID", con);
                    com.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        var objData = new EmployeeModel();
                        objData.ID = rdr["ID"].ToString();
                        objData.Firstname = rdr["Firstname"].ToString();
                        objData.Lastname = rdr["Lastname"].ToString();
                        objData.City = rdr["City"].ToString();
                        objData.State = rdr["State"].ToString();
                        objData.DOB = rdr["DOB"].ToString();
                        objData.Country = rdr["Country"].ToString();
                        objData.Qualification = rdr["Qualification"].ToString();
                        objData.Emailid = rdr["Emailid"].ToString();
                        objData.Mobilenumber = rdr["Mobilenumber"].ToString();

                        lst.Add(objData);

                    }
                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
     
            
        }


    }
}