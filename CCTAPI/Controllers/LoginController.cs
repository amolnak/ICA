using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCTAPI.Models;

namespace CCTAPI.Controllers
{
    public class LoginController : ApiController
    {
        //For user login   
        [Route("Api/Login/UserLogin")]
        [HttpPost]
        public object UserLogin(string UserName, string Password)
        {
            var Obj = Login(UserName, Password);
            if (Obj == false)
                return new ResponseEntity { Status = "Invalid", Message = "Invalid User." };
            else
                return new ResponseEntity { Status = "Success", Message = UserName };
        }
        public bool Login(string UserName,string Password)
        {
            try
            {
                bool result = false;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Login_Page", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", UserName);
                cmd.Parameters.AddWithValue("@password", Password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}