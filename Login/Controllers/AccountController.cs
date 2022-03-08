using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using System.Data.SqlClient;
using System.Data;

namespace Login.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
       
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "Data Source=PERSONALSRV-KAR\\SQL2016;Initial Catalog=29dey;User ID=sa;Password=12341234";
        }
        [HttpPost]
        public ActionResult verify(UserAccount acc)
        {
            DataTable dt = new DataTable();
            connectionString();
            con.Open();
            com.Connection = con;
            var ff = acc.Username;
            TempData["idd"] = ff;
            com.CommandText = "SELECT * FROM[29dey].[dbo].[UserAccount] where Username='"+acc.Username+"' and Pass='"+acc.Pass+"'";
            
            string sqlquery = "select CompanyCode from [29dey].[dbo].[UserAccount] where Username='" + acc.Username + "'";
            string sqlquery2 = "select * from [29dey].[dbo].[Employee] where CompanyCode=" + sqlquery;
            com.CommandText = sqlquery;

            SqlDataAdapter sda = new SqlDataAdapter(sqlquery, con);
            sda.Fill(dt);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return View("Create");
            }
            else
            {
                con.Close();
                return View("Error");
            }
            
        }
        public ActionResult but()
        {
            var Userid = TempData["idd"];
            
            DataTable dt = new DataTable();
            //SampleDBEntities sd = new SampleDBEntities();
            Entities1 sd = new Entities1();
            var nel = sd.UserAccounts.Where(x => x.Username == Userid).ToList();
            return View(nel);
            //sda.Fill(dt);

            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            //return View(dt);
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
          
        }
    }
}