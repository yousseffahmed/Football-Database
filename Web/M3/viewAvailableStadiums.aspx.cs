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
    public partial class viewavstad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void viewavailablestads(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            DateTime d = DateTime.Parse(date.Text);

            SqlCommand avs = new SqlCommand("select * from dbo.viewAvailableStadiumsOn(@date)", conn);

            avs.Parameters.Add(new SqlParameter("@date", d));

            conn.Open();
            avs.ExecuteNonQuery();

            SqlDataReader rdr = avs.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String name = rdr.GetString(rdr.GetOrdinal("Name"));
                String loc = rdr.GetString(rdr.GetOrdinal("Location"));
                int cap = rdr.GetInt32(rdr.GetOrdinal("Capacity"));

                Label n = new Label();
                n.Text = name + ", ";

                Label l = new Label();
                l.Text = loc + ", ";

                Label c = new Label();
                c.Text = cap + "<br >" + "<br >";

                form1.Controls.Add(n);
                form1.Controls.Add(l);
                form1.Controls.Add(c);
            }
            conn.Close();
        }

        protected void back(object sender, EventArgs e)
        {
            Response.Redirect("ClubRep.aspx");
        }
    }
}