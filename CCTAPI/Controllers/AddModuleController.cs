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
    [RoutePrefix("Api/AddModule")]
    public class AddModuleController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        [Route("loadmodule")]
        [HttpGet]
        public List<module> loadmodule()
        {
            List<module> ModuleList = new List<module>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_gridModule", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //da.Fill(ds);
                da.Fill(dt);
                //int count = ds.Tables[0].Rows.Count;
                int count = dt.Rows.Count;
                con.Close();
                //if (ds.Tables[0].Rows.Count > 0)
                if (dt.Rows.Count > 0)
                {
                    ModuleList = ConvertDataTable<module>(dt);
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ModuleList;
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
        [Route("Update")]
        [HttpPost]
        public string Update(string ModuleID, string ModuleName, string IsActive)
        {
            string U = " ";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateModule", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int Res = cmd.ExecuteNonQuery();
                con.Close();
                if (Res == 1)
                {
                    U = "Updated Successfully";
                }
                else
                {
                    U = "Failed to Update";
                }
                loadmodule();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return U;
        }

        [Route("Delete")]
        [HttpPost]
        public string Delete(int ModuleID)
        {
            string F = " ";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteModule", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result == 1)
                {
                    F = "Deleted Successfully";
                }
                else
                {
                    F = "Failed to Delete";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return F;
        }
        //protected void Delete()
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_FillStatusModule", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        //DataSet ds = new DataSet();
        //        da.Fill(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [Route("FillStatusDDL")]
        [HttpGet]
        public List<module> FillStatusDDL()
        {
            List<module> statuslist = new List<module>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FillStatusModule", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                statuslist = ConvertDataTable<module>(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return statuslist;
        }

        [Route("Add")]
        [HttpPost]
        public string Add(string ModuleName)
        {
            string S = " ";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddModule", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result == 1)
                {
                    S = "Added Successfully";
                }
                else
                {
                    S = "Failed to Add";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return S;
        }
    }
}