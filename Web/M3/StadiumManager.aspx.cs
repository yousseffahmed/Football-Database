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
    public partial class StadiumManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand s = new SqlCommand("stadiumManaged", conn);
            s.CommandType = CommandType.StoredProcedure;

            String un = Session["user"].ToString();
            //INPUTS
            s.Parameters.Add(new SqlParameter("@username", un));

            conn.Open();
            s.ExecuteNonQuery();

            SqlDataReader rdr = s.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String name = rdr.GetString(rdr.GetOrdinal("Name"));
                String loc = rdr.GetString(rdr.GetOrdinal("Location"));
                int cap = rdr.GetInt32(rdr.GetOrdinal("Capacity"));

                info.Text = name + ", " + loc + ", " + cap + "<br >";
            }
            conn.Close();
        }

        protected void viewreqs(object sender, EventArgs e)
        {
            Response.Redirect("viewRequests.aspx");
        }

        protected void logout(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}