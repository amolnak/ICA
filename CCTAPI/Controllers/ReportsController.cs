using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Reflection;
using CCTAPI.Models;

namespace CCTAPI.Controllers
{
    [RoutePrefix("Api/Reports")] 
    public class ReportsController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        [Route("FillddlProduct")]
        [HttpGet]
        public List<product> FillddlProduct()
        {
            List<product> Productlist = new List<product>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_product ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserId"].ToString()));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                Productlist =ConvertDataTable<product>(dt);
            }
            catch (Exception ex)
            {
            }
            return Productlist;
        }

        [Route("FillddlType")]
        [HttpGet]
        public List<Typ> FillddlType()
        {
            List<Typ> TypList = new List<Typ>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_type", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();
                TypList = ConvertDataTable<Typ>(dt);
            }
            catch (Exception ex)
            {

            }
            return TypList;
        }

        [Route("FillddlSev")]
        [HttpGet]
        public List<Severity> FillddlSev()
        {
            List<Severity> Severitylist = new List<Severity>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_Severity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                Severitylist = ConvertDataTable<Severity>(dt);
            }
            catch (Exception ex)
            {
            }
            return Severitylist;
        }
        //protected void FillddlAssign()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_assignto", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        [Route("FillddlLoggedBy")]
        [HttpGet]
        public List<Client_Login_Page> FillddlLoggedBy()
        {
            List<Client_Login_Page> LoggedList = new List<Client_Login_Page>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_assignto", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                LoggedList = ConvertDataTable<Client_Login_Page>(dt);
            }
            catch (Exception ex)
            {
            }
            return LoggedList;
        }
        [Route("FillddlAssignedTo")]
        [HttpGet]
        public List<Client_Login_Page> FillddlAssignedTo()
        {
            List<Client_Login_Page> Assignedlist = new List<Client_Login_Page>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_assignto", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                Assignedlist = ConvertDataTable<Client_Login_Page>(dt);
            }
            catch (Exception ex)
            {

            }
            return Assignedlist;
        }
        [Route("FillddlModule")]
        [HttpGet]
        public List<module> FillddlModule()
        {
            List<module> modulelist = new List<module>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_Module", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();
                modulelist = ConvertDataTable<module>(dt);
            }
            catch (Exception ex)
            {

            }
            return modulelist;
        }

       [Route("FillddlClientName")]
        [HttpGet]
        public List<Client> FillddlClientName(int UserId)
        {
            List<Client> Clientlist = new List<Client>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ClientName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                Clientlist = ConvertDataTable<Client>(dt);
            }
            catch (Exception ex)
            {
                
            }
            return Clientlist;
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

        [Route("FillddlStatus")]
        [HttpGet]
        public List<ActionStatus> FillddlStatus()
        {
            List<ActionStatus> StatusList = new List<ActionStatus>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Status", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();
                StatusList = ConvertDataTable<ActionStatus>(dt);
            }
            catch (Exception ex)
            {

            }
            return StatusList;
        }

        [Route("FillddlCategory")]
        [HttpGet]
        public List<Category> FillddlCategory(string ProductId)
        {
            List<Category> CategoryList = new List<Category>();
            try
            {
                con.Open();
                //SqlCommand cmd = new SqlCommand("sp_Category", con);
                SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.Parameters.AddWithValue("@All", ProductId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();
                CategoryList = ConvertDataTable<Category>(dt);
            }
            catch (Exception ex)
            {

            }
            return CategoryList;
        }

        [Route("Fillddlplannedversion")]
        [HttpGet]
        public List<Category> Fillddlplannedversion(string ProductId)
        {
            List<Category> PlannedList = new List<Category>();
            try
            
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.Parameters.AddWithValue("@All", ProductId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                PlannedList = ConvertDataTable<Category>(dt);
            }
            catch (Exception ex)
            {

            }
            return PlannedList;
        }
        //protected void FillddlIssuecategory()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_issuecategory", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        [Route("FillddlPriority")]
        [HttpGet]
        public List<Priority> FillddlPriority()
        {
            List<Priority> PriorityList = new List<Priority>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Priority", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();
                PriorityList = ConvertDataTable<Priority>(dt);
            }
            catch (Exception ex)
            {

            }
            return PriorityList;
        }


        //private DataTable Getalldata(string sp_name, int UserId, string ClientName, string Category, string Priority, string status, string Module, string AssignTo, string LoggedBy
        //    , string Severity, string Type, string Product, string FromDate, string ToDate, string PlannedVersion
        //    )
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "sp_GetReport";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@UserID", UserId);
        //    cmd.Parameters.Add("@ClientName", SqlDbType.NVarChar).Value = ClientName;
        //    cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = Category;
        //    cmd.Parameters.Add("@priority", SqlDbType.NVarChar).Value = Priority;
        //    cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
        //    cmd.Parameters.Add("@Module", SqlDbType.NVarChar).Value = Module;
        //    cmd.Parameters.Add("@AssignedTo", SqlDbType.NVarChar).Value = AssignTo;
        //    cmd.Parameters.Add("@LoggedBy", SqlDbType.NVarChar).Value = LoggedBy;
        //    cmd.Parameters.Add("@Severity", SqlDbType.NVarChar).Value = Severity;
        //    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
        //    cmd.Parameters.Add("@Product", SqlDbType.NVarChar).Value = Product;
        //    cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
        //    cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;
        //    cmd.Parameters.Add("@PlannedVersion", SqlDbType.NVarChar).Value = PlannedVersion;
        //    cmd.Connection = con;
        //    //cmd.CommandTimeout = 240;
        //    con.Open();
        //    //gvReport.EmptyDataText = "No Records Found";
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}


        //protected void EmailID(int Client)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_GETClientID", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ClientID", Client);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            string emailid = (dt.Rows[0]["EmailID"].ToString());
        //            string clientname = (dt.Rows[0]["Clients"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void UserEmailID(int UserId)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_UserEmailID", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@UserID", UserId);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            string Uemailid = (dt.Rows[0]["EmailID"].ToString());

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //protected void AssignUserEmailID(int Assign)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_UserEmailID", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@UserID", Assign);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            string AUemailid = (dt.Rows[0]["EmailID"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //protected void SearchByID(int UserID, string searchID, string Role, string Searchby)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "sp_searchrequestid";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@UserID", UserID);
        //        cmd.Parameters.AddWithValue("@RequestID", searchID);
        //        cmd.Parameters.AddWithValue("@UserRole", Role);
        //        cmd.Parameters.AddWithValue("@Mode", Searchby);
        //        cmd.Connection = con;
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count <= 0)
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}


        //protected void SummaryStatus(int UserId, string ClientName)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "sp_SummaryStatus";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@UserID", UserId);
        //        cmd.Parameters.AddWithValue("@ClientName", SqlDbType.VarChar).Value = ClientName;
        //        cmd.Connection = con;
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void Action(string ID, string Status, int Userid, string ReopenBy, string ResolvedBy, string Reopen, int ClosedBy, string AssignTo, string Comments, string Clientname,
        //    string priority, string severity, string category, string Module, string Title, string complaint, string Full_Name, string Assigncheck)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_UpdateStatus";
        //        cmd.Parameters.AddWithValue("@Requestid", ID);
        //        cmd.Parameters.AddWithValue("@Status", Status);
        //        cmd.Parameters.AddWithValue("@ModifiedBy", Userid);
        //        cmd.Parameters.AddWithValue("@ReopenBy", ReopenBy);
        //        cmd.Parameters.AddWithValue("@ResolvedBy", ResolvedBy);
        //        cmd.Parameters.AddWithValue("@ClosedBy", ClosedBy);
        //        cmd.Parameters.AddWithValue("@Assignto", AssignTo);
        //        cmd.Parameters.AddWithValue("@Comments", Comments);

        //        cmd.Connection = con;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        EmailID(0);
        //        UserEmailID(0);
        //        string Emailcombine = ("EmailID" + ',' + "UserEmailID");
        //        SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtp"].ToString())
        //        {
        //            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["srvrname"].ToString(), ConfigurationManager.AppSettings["srvrpwd"].ToString()),
        //            Port = Convert.ToInt32(ConfigurationManager.AppSettings["stmpport"].ToString())
        //        };
        //        MailMessage message = new MailMessage(ConfigurationManager.AppSettings["srvrname"].ToString(), Emailcombine);
        //        message.Subject = Clientname + " Complaint - " + ID + " | Status - " + Status;
        //        string strBody = "<div>" +
        //        "<table cellpadding='0' cellspacing='0' width='100%'>" +
        //        " <tr>" +
        //        "<th align='left'>RequestID</th>" +
        //         "<th align='left'>Client Name</th>" +
        //         "<th align='left'>Priority</th>" +
        //         "<th align='left'>Severity</th>" +
        //         "<th align='left'>Version</th>" +
        //         "<th align='left'>Module</th>" +
        //         "<th align='left'>Title</th>" +
        //         "<th align='left'>Complaint</th>" +
        //         "<th align='left'>Closed By</th>" +
        //         "<th align='left'>Assigned To</th>" +
        //        "</tr>" +
        //        "<tr>" +
        //        "<td align='left'>" + ID + "</td>" +
        //        "<td align='left'>" + Clientname + "</td>" +
        //        "<td align='left'>" + priority + "</td>" +
        //        "<td align='left'>" + severity + "</td>" +
        //        "<td align='left'>" + category + "</td>" +
        //        "<td align='left'>" + Module + "</td>" +
        //        "<td align='left'>" + Title + "</td>" +
        //        "<td align='left'>" + complaint + "</td>" +
        //        "<td align='left'>" + Full_Name + "</td>" +
        //        "<td align='left'>" + Assigncheck + "</td>" +
        //        "</tr>" +
        //        "</table>" +
        //        "</div>";
        //        string html = string.Empty;
        //        //string path = Server.MapPath("EmailTemplate.html");
        //        //html = File.ReadAllText(path);
        //        html = html.Replace("{dvUploadedFile}", strBody);
        //        message.Body = html;
        //        message.IsBodyHtml = true;
        //        client.Send(message);

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //protected void History(string comments)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_History";
        //        cmd.Parameters.AddWithValue("@comments", comments);
        //        cmd.Connection = con;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        EmailID(0);
        //        UserEmailID(0);
        //        string Emailcombine = ("EmailID" + ',' + "UserEmailID");
        //        SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtp"].ToString())
        //        {
        //            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["srvrname"].ToString(), ConfigurationManager.AppSettings["srvrpwd"].ToString()),
        //            Port = Convert.ToInt32(ConfigurationManager.AppSettings["stmpport"].ToString())
        //        };

        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //protected void FillddlCN(int UserId)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_ddlClient", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@UserID", UserId);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlTE()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_TestEnvironment", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlProd()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_product", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void Fillddlissue()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_issuecategory", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlTyp()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_type", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlVerRaised(string ProductId)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ProductId", ProductId);
        //        cmd.Parameters.AddWithValue("@All", 1);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlVerrel(string ProductId)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ProductId", ProductId);
        //        cmd.Parameters.AddWithValue("@All", 1);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void Fillddlplanver(string ProductId)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ProductId", ProductId);
        //        cmd.Parameters.AddWithValue("@All", 1);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlVerRaisedNew(string ProductId)
        //{
        //    try
        //    {
        //        con.Open();

        //        SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ProductId", ProductId);
        //        cmd.Parameters.AddWithValue("@All", 1);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlVerrelNew(string ProductId)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ProductId", ProductId);
        //        cmd.Parameters.AddWithValue("@All", 1);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //protected void FillddlplanverNew(string ProductId)
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ProductId", ProductId);
        //        cmd.Parameters.AddWithValue("@All", 1);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlAssigntoteam()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_assignto", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlPrior()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_Priority", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlSeverity()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_Severity", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void FillddlMod()
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SP_Module", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void Update(string RequestID, string ClientName, string LogComplaint, string Category, string Priority, int UserId, string Product, string Type,
        //    string Module, string Severity, string Title, string FilePath, string VerRel, string TestEnv, string PlanRel, string AssignTo, string IssueCategoryid, string LinkedIssues
        //    )
        //{
        //    try
        //    {
        //        con.Open();
        //        SqlCommand com = new SqlCommand("sp_Update_Complaint_Tracker", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@Requestid", RequestID);
        //        com.Parameters.AddWithValue("@ClientName", ClientName);
        //        com.Parameters.AddWithValue("@LogComplaint", LogComplaint);
        //        com.Parameters.AddWithValue("@Category", Category);
        //        com.Parameters.AddWithValue("@Priority", Priority);
        //        com.Parameters.AddWithValue("@ModifiedBy", UserId);
        //        com.Parameters.AddWithValue("@Product", Product);
        //        com.Parameters.AddWithValue("@Type", Type);
        //        com.Parameters.AddWithValue("@Module", Module);
        //        com.Parameters.AddWithValue("@Severity", Severity);
        //        com.Parameters.AddWithValue("@Title", Title);
        //        com.Parameters.AddWithValue("@FilePath", FilePath);
        //        com.Parameters.AddWithValue("@VerRel", VerRel);
        //        com.Parameters.AddWithValue("@TestEnv", TestEnv);
        //        com.Parameters.AddWithValue("@PlanRel", PlanRel);
        //        com.Parameters.AddWithValue("@AssignTo", AssignTo);
        //        com.Parameters.AddWithValue("@IssueCategoryid", IssueCategoryid);
        //        com.Parameters.AddWithValue("@LinkedIssues", LinkedIssues);

        //        DataTable dt = new DataTable();
        //        SqlDataAdapter da = new SqlDataAdapter(com);
        //        da.Fill(dt);
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

    }
}