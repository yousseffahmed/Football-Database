using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.Caching;

namespace M3
{
    public partial class viewMatchestoAttend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void viewmatches(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand v = new SqlCommand("SELECT * FROM dbo.availableMatchesToAttend_M3rpoc(@date)", conn);
            
            String d = date.Text;

            //INPUTS
            v.Parameters.Add(new SqlParameter("@date", d));

            conn.Open();
            v.ExecuteNonQuery();

            SqlDataReader rdr2 = v.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr2.Read())
            {
                String host = rdr2.GetString(rdr2.GetOrdinal("Host"));
                String guest = rdr2.GetString(rdr2.GetOrdinal("Guest"));
                String stad = rdr2.GetString(rdr2.GetOrdinal("Stadium"));
                String loc = rdr2.GetString(rdr2.GetOrdinal("Location"));

                Label h = new Label();
                h.Text = host + " - ";

                Label g = new Label();
                g.Text = guest + " | ";

                Label s = new Label();
                s.Text = stad + ", ";

                Label l = new Label();
                l.Text = loc + "<br >" + "<br >";

                form1.Controls.Add(h);
                form1.Controls.Add(g);
                form1.Controls.Add(s);
                form1.Controls.Add(l);
            }
        }

        protected void purchase(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String id = natid.Text;
            String h = hostname.Text;
            String g = guestname.Text;

            if(id == "")
            {
                Response.Write("Fan national id is empty");
            }
            else if(h == "")
            {
                Response.Write("Host name is empty");
            }
            else if (g == "")
            {
                Response.Write("Guest name is empty");
            }
            else
            {
                SqlCommand checkNatidproc = new SqlCommand("checkNatidDistinct", conn);
                checkNatidproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                checkNatidproc.Parameters.Add(new SqlParameter("@id", id));

                //OUTPUT
                SqlParameter success = checkNatidproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                success.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkNatidproc.ExecuteNonQuery();
                conn.Close();

                if (success.Value.ToString() == "0")
                {
                    SqlCommand checkClubproc = new SqlCommand("checkClubExists", conn);
                    checkClubproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    checkClubproc.Parameters.Add(new SqlParameter("@name", h));

                    //OUTPUT
                    SqlParameter succ = checkClubproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                    succ.Direction = System.Data.ParameterDirection.Output;

                    conn.Open();
                    checkClubproc.ExecuteNonQuery();
                    conn.Close();

                    if (succ.Value.ToString() == "1")
                    {
                        SqlCommand checkClubproc2 = new SqlCommand("checkClubExists", conn);
                        checkClubproc2.CommandType = System.Data.CommandType.StoredProcedure;

                        //INPUTS
                        checkClubproc2.Parameters.Add(new SqlParameter("@name", g));

                        //OUTPUT
                        SqlParameter su = checkClubproc2.Parameters.Add("@success", System.Data.SqlDbType.Int);
                        su.Direction = System.Data.ParameterDirection.Output;

                        conn.Open();
                        checkClubproc2.ExecuteNonQuery();
                        conn.Close();

                        if (su.Value.ToString() == "1")
                        {
                            SqlCommand checkMatchproc = new SqlCommand("checkMatchExistsAndNotStarted", conn);
                            checkMatchproc.CommandType = System.Data.CommandType.StoredProcedure;

                            //INPUTS
                            checkMatchproc.Parameters.Add(new SqlParameter("@host", h));
                            checkMatchproc.Parameters.Add(new SqlParameter("@guest", g));

                            //OUTPUT
                            SqlParameter s = checkMatchproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                            s.Direction = System.Data.ParameterDirection.Output;

                            conn.Open();
                            checkMatchproc.ExecuteNonQuery();
                            conn.Close();

                            if (s.Value.ToString() == "1")
                            {
                                SqlCommand checkTicketavailableproc = new SqlCommand("checkTicketAvailable", conn);
                                checkTicketavailableproc.CommandType = System.Data.CommandType.StoredProcedure;

                                //INPUTS
                                checkTicketavailableproc.Parameters.Add(new SqlParameter("@host", h));
                                checkTicketavailableproc.Parameters.Add(new SqlParameter("@guest", g));

                                //OUTPUT
                                SqlParameter c = checkTicketavailableproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                                c.Direction = System.Data.ParameterDirection.Output;

                                conn.Open();
                                checkTicketavailableproc.ExecuteNonQuery();
                                conn.Close();

                                if (c.Value.ToString() == "1")
                                {
                                    SqlCommand p = new SqlCommand("purchaseTicket_M3", conn);
                                    p.CommandType = CommandType.StoredProcedure;

                                    //INPUTS
                                    p.Parameters.Add(new SqlParameter("@fanid", id));
                                    p.Parameters.Add(new SqlParameter("@host", h));
                                    p.Parameters.Add(new SqlParameter("@guest", g));

                                    conn.Open();
                                    p.ExecuteNonQuery();
                                    conn.Close();
                                    Response.Redirect("viewMatchestoAttend.aspx");
                                }
                                else
                                {
                                    Response.Write("Tickets unavailable/sold out");
                                }
                            }
                            else
                            {
                                Response.Write("Invalid Match Data");
                            }
                        }
                        else
                        {
                            Response.Write("Invalid Guest Club Name");
                        }
                    }
                    else
                    {
                        Response.Write("Invalid Host Club Name");
                    }
                }
                else
                {
                    Response.Write("Invalid Fan National ID");
                }
            }
        }

        protected void back(object sender, EventArgs e)
        {
            Response.Redirect("Fan.aspx");
        }
    }
}