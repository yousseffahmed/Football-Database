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
    public partial class viewRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand u = new SqlCommand("SELECT * FROM dbo.allRequestsfunc(@username)", conn);

            String un = Session["user"].ToString();

            //INPUTS
            u.Parameters.Add(new SqlParameter("@username", un));

            conn.Open();
            u.ExecuteNonQuery();

            SqlDataReader rdr2 = u.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr2.Read())
            {
                String clubrep = rdr2.GetString(rdr2.GetOrdinal("Club_Representative"));
                String host = rdr2.GetString(rdr2.GetOrdinal("Host_Club"));
                String guest = rdr2.GetString(rdr2.GetOrdinal("Guest_Club"));
                DateTime start = rdr2.GetDateTime(rdr2.GetOrdinal("Start_Time"));
                DateTime end = rdr2.GetDateTime(rdr2.GetOrdinal("End_Time"));
                String status = rdr2.GetString(rdr2.GetOrdinal("Status"));

                Label cr = new Label();
                cr.Text = clubrep + ", ";

                Label h = new Label();
                h.Text = host + " - ";

                Label g = new Label();
                g.Text = guest + " | ";

                Label st = new Label();
                st.Text = start + " - ";

                Label en = new Label();
                en.Text = end + " | ";

                Label s = new Label();
                s.Text = status + "<br >" + "<br >";

                form1.Controls.Add(cr);
                form1.Controls.Add(h);
                form1.Controls.Add(g);
                form1.Controls.Add(st);
                form1.Controls.Add(en);
                form1.Controls.Add(s);
            }
        }

        protected void accept(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand a = new SqlCommand("acceptRequest", conn);
            a.CommandType = CommandType.StoredProcedure;

            String un = Session["user"].ToString();
            String h = hostname.Text;
            String g = guestname.Text;

            if (h == "")
            {
                Response.Write("Host name cannot be empty");
            }
            else if (g == "")
            {
                Response.Write("Guest name cannot be empty");
            }
            else if (date.Text == "")
            {
                Response.Write("Date cannot be empty");
            }
            else
            {
                DateTime d = DateTime.Parse(date.Text);

                SqlCommand checkClubproc = new SqlCommand("checkClubExists", conn);
                checkClubproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                checkClubproc.Parameters.Add(new SqlParameter("@name", h));

                //OUTPUT
                SqlParameter success = checkClubproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                success.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkClubproc.ExecuteNonQuery();
                conn.Close();

                if (success.Value.ToString() == "1")
                {
                    SqlCommand checkClubproc2 = new SqlCommand("checkClubExists", conn);
                    checkClubproc2.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    checkClubproc2.Parameters.Add(new SqlParameter("@name", g));

                    //OUTPUT
                    SqlParameter succ = checkClubproc2.Parameters.Add("@success", System.Data.SqlDbType.Int);
                    succ.Direction = System.Data.ParameterDirection.Output;

                    conn.Open();
                    checkClubproc2.ExecuteNonQuery();
                    conn.Close();

                    if (succ.Value.ToString() == "1")
                    {
                        SqlCommand checkMatchproc = new SqlCommand("checkMatchExistsAndNotStarted", conn);
                        checkMatchproc.CommandType = System.Data.CommandType.StoredProcedure;

                        //INPUTS
                        checkMatchproc.Parameters.Add(new SqlParameter("@host", h));
                        checkMatchproc.Parameters.Add(new SqlParameter("@guest", g));

                        //OUTPUT
                        SqlParameter su = checkMatchproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                        su.Direction = System.Data.ParameterDirection.Output;

                        conn.Open();
                        checkMatchproc.ExecuteNonQuery();
                        conn.Close();

                        if (su.Value.ToString() == "1")
                        {
                            SqlCommand checkReqproc = new SqlCommand("checkRequestExists", conn);
                            checkReqproc.CommandType = System.Data.CommandType.StoredProcedure;

                            //INPUTS
                            checkReqproc.Parameters.Add(new SqlParameter("@username", un));
                            checkReqproc.Parameters.Add(new SqlParameter("@host", h));
                            checkReqproc.Parameters.Add(new SqlParameter("@guest", g));
                            checkReqproc.Parameters.Add(new SqlParameter("@date", d));

                            //OUTPUT
                            SqlParameter s = checkReqproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                            s.Direction = System.Data.ParameterDirection.Output;

                            conn.Open();
                            checkReqproc.ExecuteNonQuery();
                            conn.Close();

                            if (s.Value.ToString() == "1")

                            {
                                //INPUTS
                                a.Parameters.Add(new SqlParameter("@SM_username", un));
                                a.Parameters.Add(new SqlParameter("@HOSTname", h));
                                a.Parameters.Add(new SqlParameter("@GUESTname", g));
                                a.Parameters.Add(new SqlParameter("@MATCHdate", d));

                                conn.Open();
                                a.ExecuteNonQuery();
                                conn.Close();
                                Response.Redirect("viewRequests.aspx");
                            }
                            else
                            {
                                Response.Write("Request doesnot exist");
                            }
                        }
                        else
                        {
                            Response.Write("Invalid Match Data");
                        }
                    }
                    else
                    {
                        Response.Write("Invalid Guest Name");
                    }
                }
                else
                {
                    Response.Write("Invalid Host Name");
                }
            }
        }

        protected void reject(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand r = new SqlCommand("rejectRequest", conn);
            r.CommandType = CommandType.StoredProcedure;

            String un = Session["user"].ToString();
            String h = hostname.Text;
            String g = guestname.Text;

            if (h == "")
            {
                Response.Write("Host name cannot be empty");
            }
            else if (g == "")
            {
                Response.Write("Guest name cannot be empty");
            }
            else if (date.Text == "")
            {
                Response.Write("Date cannot be empty");
            }
            else
            {
                DateTime d = DateTime.Parse(date.Text);

                SqlCommand checkClubproc = new SqlCommand("checkClubExists", conn);
                checkClubproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                checkClubproc.Parameters.Add(new SqlParameter("@name", h));

                //OUTPUT
                SqlParameter success = checkClubproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                success.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkClubproc.ExecuteNonQuery();
                conn.Close();

                if (success.Value.ToString() == "1")
                {
                    SqlCommand checkClubproc2 = new SqlCommand("checkClubExists", conn);
                    checkClubproc2.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    checkClubproc2.Parameters.Add(new SqlParameter("@name", g));

                    //OUTPUT
                    SqlParameter succ = checkClubproc2.Parameters.Add("@success", System.Data.SqlDbType.Int);
                    succ.Direction = System.Data.ParameterDirection.Output;

                    conn.Open();
                    checkClubproc2.ExecuteNonQuery();
                    conn.Close();

                    if (succ.Value.ToString() == "1")
                    {
                        SqlCommand checkMatchproc = new SqlCommand("checkMatchExistsAndNotStarted", conn);
                        checkMatchproc.CommandType = System.Data.CommandType.StoredProcedure;

                        //INPUTS
                        checkMatchproc.Parameters.Add(new SqlParameter("@host", h));
                        checkMatchproc.Parameters.Add(new SqlParameter("@guest", g));

                        //OUTPUT
                        SqlParameter su = checkMatchproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                        su.Direction = System.Data.ParameterDirection.Output;

                        conn.Open();
                        checkMatchproc.ExecuteNonQuery();
                        conn.Close();

                        if (su.Value.ToString() == "1")
                        {
                            SqlCommand checkReqproc = new SqlCommand("checkRequestExists", conn);
                            checkReqproc.CommandType = System.Data.CommandType.StoredProcedure;

                            //INPUTS
                            checkReqproc.Parameters.Add(new SqlParameter("@username", un));
                            checkReqproc.Parameters.Add(new SqlParameter("@host", h));
                            checkReqproc.Parameters.Add(new SqlParameter("@guest", g));
                            checkReqproc.Parameters.Add(new SqlParameter("@date", d));

                            //OUTPUT
                            SqlParameter s = checkReqproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                            s.Direction = System.Data.ParameterDirection.Output;

                            conn.Open();
                            checkReqproc.ExecuteNonQuery();
                            conn.Close();

                            if (s.Value.ToString() == "1")

                            {
                                //INPUTS
                                r.Parameters.Add(new SqlParameter("@SM_username", un));
                                r.Parameters.Add(new SqlParameter("@HOSTname", h));
                                r.Parameters.Add(new SqlParameter("@GUESTname", g));
                                r.Parameters.Add(new SqlParameter("@MATCHdate", d));

                                conn.Open();
                                r.ExecuteNonQuery();
                                conn.Close();
                                Response.Redirect("viewRequests.aspx");
                            }
                            else
                            {
                                Response.Write("Request doesnot exist");
                            }
                        }
                        else
                        {
                            Response.Write("Invalid Match Data");
                        }
                    }
                    else
                    {
                        Response.Write("Invalid Guest Name");
                    }
                }
                else
                {
                    Response.Write("Invalid Host Name");
                }
            }
        }

        protected void back(object sender, EventArgs e)
        {
            Response.Redirect("StadiumManager.aspx");
        }
    }
}