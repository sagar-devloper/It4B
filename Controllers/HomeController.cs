using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT4B.Controllers
{
	public class HomeController : Controller
	{
		SqlConnection con = new SqlConnection("Data Source=DESKTOP-ES8VMF6;Initial Catalog=IT4B;Integrated Security=True;Encrypt=False");
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Index(string companyName,string currentPrice,string targetPrice,string stoploss,string duration,string riskLevel)
		{
			
			SqlCommand cmd = new SqlCommand("sp_company",con);
			cmd.CommandType=System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Company_name", companyName);
			cmd.Parameters.AddWithValue("@Current_price", currentPrice);
			cmd.Parameters.AddWithValue("@Target_price",targetPrice);
			cmd.Parameters.AddWithValue("@SLoss_price", stoploss);
			cmd.Parameters.AddWithValue("@Duration", duration);
			cmd.Parameters.AddWithValue("@Category", riskLevel);
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
			return Content("<script> alert('Add Sussecfully'); location.href='/home/index';</script>");
		}
		public ActionResult Details()
		{
			SqlCommand cmd = new SqlCommand("select * from tbl_company", con);
			cmd.CommandType = System.Data.CommandType.Text;
			DataTable dt = new DataTable();
			SqlDataAdapter sda = new SqlDataAdapter(cmd);
			sda.Fill(dt);
			return View(dt);
		}
		
		public ActionResult DeleteRecord(int sr)
		{
			try
			{

				using (SqlCommand cmd = new SqlCommand("DELETE FROM tbl_company WHERE sr = @sr", con))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@sr", sr);

					con.Open();
					cmd.ExecuteNonQuery();
				}

				return Content("<script> alert('Delete Sussecfully'); location.href='/home/Details';</script>");
			}
			catch (Exception ex)
			{
				// Handle exception
				TempData["Error"] = "An error occurred while deleting the record.";
				return RedirectToAction("Details", "Home");
			}
		}


	}
}