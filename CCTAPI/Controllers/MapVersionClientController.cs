using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CCTAPI.Controllers
{
    public class MapVersionClientController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        private void Map_Version_Client()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_BindClient", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //DataSet ds = new DataSet();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindList()
        {
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Edit(string ClientID)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_EditClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientID", ClientID);
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

        protected void Update(string VersionID,int ClientID,int Userid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UpdateVersionClient";
                cmd.Parameters.AddWithValue("@VersionId", VersionID);
                cmd.Parameters.AddWithValue("@ClientId", ClientID);
                cmd.Parameters.AddWithValue("@CreatedBy", Userid);
                cmd.Parameters.AddWithValue("@ModifiedBy", Userid);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
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
    }
}