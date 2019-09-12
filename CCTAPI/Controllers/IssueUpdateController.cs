using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Reflection;
using CCTAPI.Models;


namespace CCTAPI.Controllers
{
     [RoutePrefix("Api/IssueUpdate")]  
    public class IssueUpdateController : ApiController
    {
       
         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        // ClientComplentTrackerEntities ccn = new ClientComplentTrackerEntities();
         // [Route("fillddlproduct")] 
        // [HttpGet]
         //public List<product> FillddlProduct()
        //{
        //    return ccn.products.ToList<product>();
        //}


         [Route("FillddlProduct")]
         [HttpGet]
         public List<product> FillddlProduct()
           {
               List<product> productlist = new List<product>();

               try
               {
                   con.Open();
                   SqlCommand cmd = new SqlCommand("sp_product_ir", con);
                   cmd.CommandType = CommandType.StoredProcedure;
                 //  cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserId"].ToString()));
                   SqlDataAdapter da = new SqlDataAdapter(cmd);
                   DataTable dt = new DataTable();
                   da.Fill(dt);
                   con.Close();

                   productlist=ConvertDataTable<product>(dt);       //calling a generic method for converting dt to list

                   //for (int i = 0; i < dt.Rows.Count; i++)
                   //{
                   //    product prod = new product();
                   //    prod.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                   //    prod.ProductName = dt.Rows[i]["ProductName"].ToString();
                   //    productlist.Add(prod);  
                   //}
                    
               }
               catch (Exception ex)
               {
               }
               return productlist;
               
           }

        [Route("FillddlType")]
         [HttpGet]
         public List<Typ> FillddlType()
         {
            List<Typ> typelist = new List<Typ>();
             try
             {
                 con.Open();
                 SqlCommand cmd = new SqlCommand("sp_type", con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 //cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserId"].ToString()));
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 con.Close();

                typelist=ConvertDataTable<Typ>(dt);  

             }
             catch (Exception ex)
             {
             }

             return typelist;
         }
        [Route("FillddlSeverity")]
        [HttpGet]
        public List<Severity> FillddlSeverity()
         {
             List<Severity> sevlist = new List<Severity>();
             try
             {
                 con.Open();
                 SqlCommand cmd = new SqlCommand("SP_Severity", con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 //cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserId"].ToString()));
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 con.Close();
                 sevlist = ConvertDataTable<Severity>(dt);  
             }
             catch (Exception ex)
             {
             }
             return sevlist;
         }
        [Route("FillddlClientName")]
        [HttpGet]
        public List<Client> FillddlClientName(int UserID)
       {
           List<Client> clientlist = new List<Client>();
           try
           {
               con.Open();
               SqlCommand cmd = new SqlCommand("sp_ddlClient", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@UserID", UserID);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               con.Close();
               clientlist = ConvertDataTable<Client>(dt);  
           }
           catch (Exception ex)
           {
           }
           return clientlist;
       }


        [Route("FillddlCategory")]
        [HttpGet]
        public List<Category> FillddlCategory(string ProductID)
       {
            List<Category> catlist = new List<Category>();
           try
           {
               
               con.Open();
               SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", ProductID);
               cmd.Parameters.AddWithValue("@All", 1);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               con.Close();
               catlist = ConvertDataTable<Category>(dt);  
           }
           catch (Exception ex)
           {
           }
           return catlist;
       }
        [Route("FillddlVerrel")]
        [HttpGet]
      public List<Category> FillddlVerrel(string Product)
       {
           List<Category> catlist = new List<Category>();
           try
           {
               con.Open();
               SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", Product);
               cmd.Parameters.AddWithValue("@All", 1);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               con.Close();
               catlist = ConvertDataTable<Category>(dt); 
           }
           catch (Exception ex)
           {
           }
           return catlist;
       } 

        [Route("Fillddlplanver")]
        [HttpGet]
        public List<Category> Fillddlplanver(string Product)
       {
           List<Category> catlist = new List<Category>();
           try
           {
               con.Open();
               SqlCommand cmd = new SqlCommand("SP_Map_Version_Product", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", Product);
               cmd.Parameters.AddWithValue("@All", 1);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               con.Close();
               catlist = ConvertDataTable<Category>(dt); 
           }
           catch (Exception ex)
           {
           }
           return catlist;
       } 

        [Route("FillddlAssignto")]
        [HttpGet]
       public List<Client_Login_Page> FillddlAssignto()
       {
           List<Client_Login_Page> userlist = new List<Client_Login_Page>();
           try
           {
               con.Open();
               SqlCommand cmd = new SqlCommand("sp_assignto", con);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               con.Close();
               userlist = ConvertDataTable<Client_Login_Page>(dt);
           }
           catch (Exception ex)
           {
           }
           return userlist;
       }
        [Route("FillddlModule")]
        [HttpGet]
       public List<module> FillddlModule()
      {
          List<module> modlist = new List<module>();
          try
          {
              con.Open();
              SqlCommand cmd = new SqlCommand("SP_Module", con);
              SqlDataAdapter da = new SqlDataAdapter(cmd);
              DataTable dt = new DataTable();
              da.Fill(dt);
              con.Close();
              modlist = ConvertDataTable<module>(dt);
          }
          catch (Exception ex)
          {
          }
          return modlist;
      }
        [Route("FillddlTestEnvironment")]
        [HttpGet]
       public List<TestEnvironment> FillddlTestEnvironment()
         {
            List<TestEnvironment> envlist=new List<TestEnvironment>();

             try
             {
                 con.Open();
                 SqlCommand cmd = new SqlCommand("SP_TestEnvironment", con);
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 con.Close();
                 envlist = ConvertDataTable<TestEnvironment>(dt); 
             }
             catch (Exception ex)
             {
             }
             return envlist;
         }


        [Route("FillddlPriority")]
        [HttpGet]
        public List<Priority> FillddlPriority()
         {
             List<Priority> prioritylist = new List<Priority>();
             try
             {
                 con.Open();
                 SqlCommand cmd = new SqlCommand("sp_Priority", con);
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 con.Close();
                 prioritylist = ConvertDataTable<Priority>(dt); 
             }
             catch (Exception ex)
             {
             }
             return prioritylist;
         }
        
        [Route("FillddlIssuecategory")]
        [HttpGet]
        public List<IssueCategory> FillddlIssuecategory()
        {
            List<IssueCategory> issuelist = new List<IssueCategory>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_issuecategory", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

               issuelist = ConvertDataTable<IssueCategory>(dt); 
            }
            catch (Exception ex)
            {
            }
            return issuelist;
        }
        
      protected void EmailID(string ClientName)
      {
          try
          {
              con.Open();
              SqlCommand cmd = new SqlCommand("sp_GETClientID", con);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@ClientID", ClientName);
              SqlDataAdapter da = new SqlDataAdapter(cmd);
              DataTable dt = new DataTable();
              da.Fill(dt);
              con.Close();
              if (dt.Rows.Count > 0)
              {
                  string emailid = (dt.Rows[0]["EmailID"].ToString());
                  string clientname = (dt.Rows[0]["Clients"].ToString());
                  string clientID = (dt.Rows[0]["ClientID"].ToString());
              }
          }
          catch (Exception ex)
          {
          }
      }

      protected void UserEmailID(string UserId)
      {
          try
          {
              con.Open();
              SqlCommand cmd = new SqlCommand("SP_UserEmailID", con);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@UserID", (UserId));
              SqlDataAdapter da = new SqlDataAdapter(cmd);
              DataTable dt = new DataTable();
              da.Fill(dt);
              con.Close();
              if (dt.Rows.Count > 0)
              {
                  string Uemailid = (dt.Rows[0]["EmailID"].ToString());
              }
          }
          catch (Exception ex)
          {
          }
      }

      [Route("SubmitIssue")]
      [HttpPost]
      public IHttpActionResult SubmitIssue(string ClientName, string UserId, string LogComplaint, string Category, string Priority,
          string Product, string Type, string Module, string Severity, string Title, string Fpath,
          string verrel, string TestEnv, string planver, string Assignto, string issuecategory, string Related, string FullName)
      {
          string result = "";
          try
          {
              
              con.Open();
              SqlCommand com = new SqlCommand("sp_Complaint_Tracker", con);
              com.CommandType = CommandType.StoredProcedure;
              com.Parameters.AddWithValue("@ClientName", ClientName);
              com.Parameters.AddWithValue("@UserID", UserId);
              com.Parameters.AddWithValue("@LogComplaint", LogComplaint);
              com.Parameters.AddWithValue("@Category", Category);
              com.Parameters.AddWithValue("@Priority", Priority);
              com.Parameters.AddWithValue("@CreatedBy", UserId);
              com.Parameters.AddWithValue("@Product", Product);
              com.Parameters.AddWithValue("@Type", Type);
              com.Parameters.AddWithValue("@Module", Module);
              com.Parameters.AddWithValue("@Severity", Severity);
              com.Parameters.AddWithValue("@Title", Title);
              com.Parameters.AddWithValue("@FilePath", Fpath);
              com.Parameters.AddWithValue("@VerRel", verrel);
              com.Parameters.AddWithValue("@TestEnv", TestEnv);
              com.Parameters.AddWithValue("@PlanRel", planver);
              com.Parameters.AddWithValue("@AssignTo", Assignto);
              com.Parameters.AddWithValue("@IssueCategory", issuecategory);
              com.Parameters.AddWithValue("@LinkedIssues", Related);
              DataTable dt = new DataTable();
              SqlDataAdapter da = new SqlDataAdapter(com);
              da.Fill(dt);
              con.Close();

              //StatusLabel.Text = "Upload status: File uploaded!";
              int requestid=0;
              string status="";
              if (dt.Rows.Count > 0)
              {
                  result = "Data Submitted Successfully";
                  requestid = Convert.ToInt32(dt.Rows[0]["RequestID"].ToString());
                  status = (dt.Rows[0]["Status"].ToString());
              }
              //Status();
              EmailID("");
              UserEmailID("");
              string Emailcombine = ("EmailID"+ ',' + "UserEmailID");
              string assigncheck = "";
              SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtp"].ToString())
              {
                  Credentials = new NetworkCredential(ConfigurationManager.AppSettings["srvrname"].ToString(), ConfigurationManager.AppSettings["srvrpwd"].ToString()),
                  Port = Convert.ToInt32(ConfigurationManager.AppSettings["stmpport"].ToString())
              };
              MailMessage message = new MailMessage(ConfigurationManager.AppSettings["srvrname"].ToString(), Emailcombine);
              message.Subject = ClientName + " ComplaintID - " + requestid + " | Status - " + status;
              string strBody = "<div>" +
                               "<table cellpadding='0' cellspacing='0' width='100%'>" +
                               " <tr>" +
                               "<th align='left'>RequestID</th>" +
                               "<th align='left'>Client Name</th>" +
                               "<th align='left'>Priority</th>" +
                               "<th align='left'>Severity</th>" +
                               "<th align='left'>Version</th>" +
                               "<th align='left'>Module</th>" +
                               "<th align='left'>Title</th>" +
                               "<th align='left'>Complaint</th>" +
                               "<th align='left'>Logged By</th>" +
                               "<th align='left'>Assigned To</th>" +
                               "</tr>" +
                               "<tr>" +
                               "<td>" + requestid + "</td>" +
                               "<td align='left'>" + ClientName + "</td>" +
                               "<td align='left'>" + Priority + "</td>" +
                               "<td align='left'>" + Severity + "</td>" +
                               "<td align='left'>" + Category + "</td>" +
                               "<td align='left'>" + Module + "</td>" +
                               "<td align='left'>" + Title + "</td>" +
                               "<td align='left'>" + LogComplaint + "</td>" +
                               "<td align='left'>" + FullName  + "</td>" +
                               "<td align='left'>" + assigncheck + "</td>" +
                               "</tr>" +
                               "</table>" +
                               "</div>";
              string html = string.Empty;
              //string path = Server.MapPath("EmailTemplate.html");
              //html = File.ReadAllText(path);
              html = html.Replace("{dvUploadedFile}", strBody);
              message.Body = html;
              message.IsBodyHtml = true;
              client.Send(message);
          }
          catch (Exception ex)
          {
          }
          return Json(new Response { Message = result });
      }

      protected void Search(int UserID, string SearchId, string Role, string Mode)
      {
          try
          {
                
                  SqlCommand cmd = new SqlCommand();
                  cmd.CommandText = "sp_searchrequestid_popup";
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@UserID", UserID);
                  cmd.Parameters.AddWithValue("@RequestID", SearchId);
                  cmd.Parameters.AddWithValue("@UserRole", Role);
                  cmd.Parameters.AddWithValue("@Mode", Mode);
                  cmd.Connection = con;
                  con.Open();

                  //gvReport.EmptyDataText = "No Records Found";
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataTable dt = new DataTable();
                  da.Fill(dt);
                  con.Close();
                  if (dt.Rows.Count <= 0)
                  {

                      
                  }
          }


          catch (Exception ex)
          {
          }

      }
         [HttpPost]
        public IHttpActionResult UploadFiles()
        {
            int i = 0;
            int cntSuccess = 0;
            var uploadedFileNames = new List<string>();
            var filePath="";
            string result = string.Empty;

            HttpResponseMessage response = new HttpResponseMessage();

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[i];
                    filePath = HttpContext.Current.Server.MapPath("~/Files/" + postedFile.FileName);
                    try
                    {
                        postedFile.SaveAs(filePath);
                        uploadedFileNames.Add(httpRequest.Files[i].FileName);
                        cntSuccess++;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    i++;
                }
            }

            result = cntSuccess.ToString() + " files uploaded succesfully.<br/>";

            result += "<ul>";

            //foreach (var f in uploadedFileNames)
            //{
            //    result += "<li>" + f + "</li>";
            //}

            //result += "</ul>";

            return Json(new Response { fileph = filePath, Message = result });
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