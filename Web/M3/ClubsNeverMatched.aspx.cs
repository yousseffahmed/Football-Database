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
    public partial class ClubsNeverMatched : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cnm = new SqlCommand("select * from clubsNeverMatched_M3", conn);
            cnm.CommandType = CommandType.Text;
            conn.Open();
            SqlDataReader rdr = cnm.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String club1 = rdr.GetString(rdr.GetOrdinal("First_Club"));
                String club2 = rdr.GetString(rdr.GetOrdinal("Second_Club"));

                Label c1 = new Label();
                c1.Text = club1 + " - ";

                Label c2 = new Label();
                c2.Text = club2 + "<br >" + "<br >";

                form1.Controls.Add(c1);
                form1.Controls.Add(c2);
            }
        }
    }
}