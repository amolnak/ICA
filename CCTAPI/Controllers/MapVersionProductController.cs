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

namespace CCTAPI.Controllers
{
    [RoutePrefix("Api/MapVersionProduct")] 
    public class MapVersionProductController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        [Route("BindProduct")]
        [HttpGet]
        public List<product> Map_Version_Product()
        {
            List<product> productlist = new List<product>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_BindProduct", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //DataSet ds = new DataSet();
                da.Fill(dt);
                con.Close();
                productlist = ConvertDataTable<product>(dt);
                return productlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("BindList")]
        [HttpGet]
        public List<Category> BindList()
        {
            List<Category> versionlist = new List<Category>();
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_BindVersion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                versionlist = ConvertDataTable<Category>(dt);
                return versionlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("Edit")]
        [HttpGet]
        public List<Map_Version_Product> Edit(string ProductID)
        {
            List<Map_Version_Product> productlist = new List<Map_Version_Product>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_EditProduct_ir", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                productlist = ConvertDataTable<Map_Version_Product>(dt);
                return productlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Edit")]
        [HttpGet]
        public void Update_Version(string VesrionID, string ReleaseDate, string ProductID, int UserId)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("VesrionID", typeof(string)), new DataColumn("ReleaseDate", typeof(string)) });

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_UpdateVersionProduct";
            cmd.Parameters.AddWithValue("@Dt", dt);
            cmd.Parameters.AddWithValue("@ProductId", ProductID);
            cmd.Parameters.AddWithValue("@CreatedBy", UserId);
            cmd.Parameters.AddWithValue("@ModifiedBy", UserId);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
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