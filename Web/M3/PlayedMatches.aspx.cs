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
    public partial class PlayedMatches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand um = new SqlCommand("select * from playedMatches", conn);
            um.CommandType = CommandType.Text;
            conn.Open();
            SqlDataReader rdr = um.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String host = rdr.GetString(rdr.GetOrdinal("Host"));
                String guest = rdr.GetString(rdr.GetOrdinal("Guest"));
                DateTime start = rdr.GetDateTime(rdr.GetOrdinal("Start_Time"));
                DateTime end = rdr.GetDateTime(rdr.GetOrdinal("End_Time"));

                Label h = new Label();
                h.Text = host + " - ";

                Label g = new Label();
                g.Text = guest + " | ";

                Label s = new Label();
                s.Text = start + " - ";

                Label en = new Label();
                en.Text = end + "<br >" + "<br >";

                form1.Controls.Add(h);
                form1.Controls.Add(g);
                form1.Controls.Add(s);
                form1.Controls.Add(en);
            }
        }
    }
}