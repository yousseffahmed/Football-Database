using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class ClubRep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
           
            SqlCommand c = new SqlCommand("clubRepresented", conn);
            c.CommandType = CommandType.StoredProcedure;

            String un = Session["user"].ToString();
            //INPUTS
            c.Parameters.Add(new SqlParameter("@username",un));
           
            conn.Open();
            c.ExecuteNonQuery();

            SqlDataReader rdr = c.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String name = rdr.GetString(rdr.GetOrdinal("Name"));
                String loc = rdr.GetString(rdr.GetOrdinal("Location"));

                info.Text = name + ", " + loc + "<br >";
            }
        }

        protected void viewupcomingmatches(object sender, EventArgs e)
        {
            Response.Redirect("viewUpcomingMatches.aspx");
        }

        protected void viewavailablestad(object sender, EventArgs e)
        {
            Response.Redirect("viewAvailableStadiums.aspx");
        }

        protected void sr(object sender, EventArgs e)
        {
            Response.Redirect("Host_Req.aspx");
        }

        protected void logout(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}