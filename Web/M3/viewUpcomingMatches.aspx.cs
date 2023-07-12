using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace M3
{
    public partial class viewUpcomingMatches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand u = new SqlCommand("SELECT * FROM dbo.upcomingMatchesfunc(@username)", conn);

            String un = Session["user"].ToString();

            //INPUTS
            u.Parameters.Add(new SqlParameter("@username", un));

            conn.Open();
            u.ExecuteNonQuery();

            SqlDataReader rdr2 = u.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr2.Read())
            {
                String host = rdr2.GetString(rdr2.GetOrdinal("Host"));
                String guest = rdr2.GetString(rdr2.GetOrdinal("Guest"));
                DateTime start = rdr2.GetDateTime(rdr2.GetOrdinal("Start_Time"));
                DateTime end = rdr2.GetDateTime(rdr2.GetOrdinal("End_Time"));
                String stad;

                if (rdr2.IsDBNull(rdr2.GetOrdinal("Stadium")))
                {
                    stad = "Not assigned";
                }
                else
                {
                    stad = rdr2.GetString(rdr2.GetOrdinal("Stadium"));
                }

                Label h = new Label();
                h.Text = host + " - ";

                Label g = new Label();
                g.Text = guest + " | ";

                Label st = new Label();
                st.Text = start + " - ";

                Label en = new Label();
                en.Text = end + " | ";

                Label s = new Label();
                s.Text = stad + "<br >" + "<br >";

                form1.Controls.Add(h);
                form1.Controls.Add(g);
                form1.Controls.Add(st);
                form1.Controls.Add(en);
                form1.Controls.Add(s);
            }
        }
    }
}