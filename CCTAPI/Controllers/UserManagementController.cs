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
using System.Reflection;

namespace CCTAPI.Controllers
{

     [RoutePrefix("Api/UserManagement")] 
    public class UserManagementController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        private void DeactiveClient()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DeactiveClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("BindList")]
        [HttpGet]
        public List<Client> BindList()
        {
            List<Client> clientlist = new List<Client>();
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                clientlist = ConvertDataTable<Client>(dt);
                return clientlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("FillGrid")]
        [HttpGet]
        public List<Client_Login_Page> FillGrid()
        {
            List<Client_Login_Page> userlist = new List<Client_Login_Page>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_users", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                userlist = ConvertDataTable<Client_Login_Page>(dt);
                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("FillddlRole")]
        [HttpGet]
        public  List<UserRole> FillddlRole()
        {
            List<UserRole> rolelist=new List<UserRole>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_UserRole", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            rolelist=ConvertDataTable<UserRole>(dt);
            return rolelist;
        }

        [Route("FillddlStatus")]
        [HttpGet]
        public  List<Client_Login_Page> FillddlStatus()
        {
            List<Client_Login_Page> statuslist=new List<Client_Login_Page>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_userstatus", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            statuslist=ConvertDataTable<Client_Login_Page>(dt);
            return statuslist;
        }

        [Route("Save")]
        [HttpPost]
        public Object Save(string UserName, string Password, string FullName, string Role, string EmailId, string Clientname, int UserID)
        {
            string op = " ";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertUsers";
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Fullname", FullName);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@EmailID", EmailId);
                cmd.Parameters.AddWithValue("@ClientName", Clientname);
                cmd.Parameters.AddWithValue("@CreatedBy", UserID);
                cmd.Connection = con;
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                //
                if (result >= 1)
                {
                    op = "Record Saved Successfully";
                }
                else
                {
                    op = "Failed to Save Record";
                }
              
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return new Response { Message = op };
        }

        [Route("Update")]
        [HttpPost]
        public Object Update(string UserName, string Password, string FullName, string Role, string EmailId, string Clientname, string Status, string UpdatedUserID, string UserID)
        {
            string op = " ";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UpdateUsers";
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Fullname", FullName);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@EmailID", EmailId);
                cmd.Parameters.AddWithValue("@ClientName", Clientname);
                cmd.Parameters.AddWithValue("@IsActive", Status);
                cmd.Parameters.AddWithValue("@UserID", UpdatedUserID);
                cmd.Parameters.AddWithValue("@ModifiedBy", UserID);
                cmd.Parameters.AddWithValue("@CreatedBy", UserID);
                cmd.Connection = con;
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result >= 1)
                {
                    op = "Record Updated Successfully";
                }
                else
                {
                    op = "Failed to Update Record";
                }

                return new Response { Message = op };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
          
        }

        [Route("Delete")]
        [HttpGet]
        public Object Delete(int DeletedUserID, int UserID)
        {
            string op = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_DeleteUsers";
                cmd.Parameters.AddWithValue("@UserID", DeletedUserID);
                cmd.Parameters.AddWithValue("@ModifiedBy", UserID);
                cmd.Connection = con;
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result >= 1)
                {
                    op = "Record Deleted Successfully";
                }
                else
                {
                    op = "Failed to Delete Record";
                }
                FillGrid();

                return new Response { Message = op };
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
           
        }

        [Route("SearchUser")]
        [HttpGet]
        public List<Client_Login_Page> SearchUser(string SearchUser)
        {
            List<Client_Login_Page> userlist=new List<Client_Login_Page>();
            SqlCommand cmd = new SqlCommand("sp_SearchUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = SearchUser;
            con.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                userlist=ConvertDataTable<Client_Login_Page>(dt);
               
                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        } 

    }
}