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
    [RoutePrefix("Api/AddCategory")]  
    public class AddCategoryController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        [Route("loadcategory")]
        [HttpGet]
        public List<Category> loadcategory()
        {
            List<Category> catlist = new List<Category>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_gridCategory", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int count = dt.Rows.Count;
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    catlist = ConvertDataTable<Category>(dt); 
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return catlist;
        }

        [Route("FillStatusCategoryDDL")]
        [HttpGet]
        public List<Category> FillStatusCategoryDDL()
        {
            List<Category> catlist = new List<Category>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FillStatusCategory", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                catlist = ConvertDataTable<Category>(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return catlist;
        }
        [Route("Update")]
        [HttpPost]
        public Object Update(string CategoryID, string CategoryName, string IsActive, int ModifiedBy)
        {
            string op = " ";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int result=cmd.ExecuteNonQuery();
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
            return new Response { Message =op};
        }

        [Route("Delete")]
        [HttpPost]
        public Object Delete(int CategoryID, int ModifiedBy)
        {
            string op = " ";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@ModifiedBY", ModifiedBy);
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
            return new Response { Message = op };
        }
        [Route("CategoryAdd")]
        [HttpPost]
        public Object CategoryAdd(string CategoryName, int CreatedBy)
        {
            string op=" ";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result == 1)
                {
                    op = "Record Added Successfully";
                }
                else
                {
                    op = "Failed to Add Record";
                }

            }
            catch (Exception ex)
            {
                throw ex;
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