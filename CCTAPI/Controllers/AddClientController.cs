using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using CCTAPI.Models;

namespace CCTAPI.Controllers
{
    [RoutePrefix("Api/AddClient")] 
    public class AddClientController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        [Route("testing")]
        [HttpGet]
        public object test()
        {
            string op = "Hi From API";
            return new Response {Status=true ,Message = "Hi From API" };
            
        }

        [Route("loadClient")]
        [HttpGet]
        public List<Client> loadClient()
        {
            List<Client> clist = new List<Client>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GridClient", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int count = dt.Rows.Count;
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    clist = ConvertDataTable<Client>(dt);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clist;

        }
        [Route("FillStatusDDL")]
        [HttpGet]
        public List<Client> FillStatusDDL()
        {
            List<Client> clist = new List<Client>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FillStatusClient", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                clist = ConvertDataTable<Client>(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clist;
        }
        [Route("Update")]
        [HttpPost]
        public Object Update(string ClientID, string Clients, string EmailID, string IsActive, int ModifiedBy)
        {
            string op = " ";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientID", ClientID);
                cmd.Parameters.AddWithValue("@Clients", Clients);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int result= cmd.ExecuteNonQuery();
                con.Close();
                if (result == 1)
                {
                    op = "Record Updated Successfully";
                }
                else
                {
                    op = "Failed to Update Record";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Response { Message = op };
        }

        [Route("Delete")]
        [HttpPost]
        public Object Delete(string ClientID, int ModifiedBY)
        {
            string op = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientID", ClientID);
                cmd.Parameters.AddWithValue("@ModifiedBY", ModifiedBY);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result == 1)
                {
                    op = "Record Deleted Successfully";
                }
                else
                {
                    op = "Failed to Delete Record";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Response { Message =op};
        }
        [Route("AddClient")]
        [HttpPost]
        public Object Add(string Clients, string EmailID, int CreatedBy)
        {
            string op="";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Clients", Clients);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result == 1)
                {
                    op = "Successfully Added";
                }
                else
                {
                    op = "Failed to Add";
                }
            }
            catch (Exception ex)
            {

            }
            return new Response { Message = op };
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
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