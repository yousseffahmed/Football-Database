using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace M3
{
    public partial class AssocManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddMatch(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String host = HCA.Text;
            String guest = GCA.Text;

            SqlCommand checkClubproc = new SqlCommand("checkClubExists", conn);
            checkClubproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkClubproc.Parameters.Add(new SqlParameter("@name", host));

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
                checkClubproc2.Parameters.Add(new SqlParameter("@name", guest));

                //OUTPUT
                SqlParameter succ = checkClubproc2.Parameters.Add("@success", System.Data.SqlDbType.Int);
                succ.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkClubproc2.ExecuteNonQuery();
                conn.Close();

                if (succ.Value.ToString() == "1")
                {
                    if (STA.Text == "")
                    {
                        Response.Write("Start time cannot be empty");
                    }
                    else if (ETA.Text == "")
                    {
                        Response.Write("End time cannot be empty");
                    }
                    else
                    {
                        DateTime start = DateTime.Parse(STA.Text);
                        DateTime end = DateTime.Parse(ETA.Text);

                        SqlCommand checktimeproc = new SqlCommand("checkTimeStamp", conn);
                        checktimeproc.CommandType = System.Data.CommandType.StoredProcedure;

                        //INPUTS
                        checktimeproc.Parameters.Add(new SqlParameter("@date", start));

                        //OUTPUT
                        SqlParameter su = checktimeproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                        su.Direction = System.Data.ParameterDirection.Output;

                        conn.Open();
                        checktimeproc.ExecuteNonQuery();
                        conn.Close();

                        if (su.Value.ToString() == "1")
                        {

                            SqlCommand checkstartendproc = new SqlCommand("checkStartToEnd", conn);
                            checkstartendproc.CommandType = System.Data.CommandType.StoredProcedure;

                            //INPUTS
                            checkstartendproc.Parameters.Add(new SqlParameter("@start", start));
                            checkstartendproc.Parameters.Add(new SqlParameter("@end", end));

                            //OUTPUT
                            SqlParameter s = checkstartendproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                            s.Direction = System.Data.ParameterDirection.Output;

                            conn.Open();
                            checkstartendproc.ExecuteNonQuery();
                            conn.Close();

                            if (s.Value.ToString() == "1")
                            {
                                SqlCommand AddMatchproc = new SqlCommand("addNewMatch", conn);
                                AddMatchproc.CommandType = System.Data.CommandType.StoredProcedure;

                                //INPUTS
                                AddMatchproc.Parameters.Add(new SqlParameter("@host", host));
                                AddMatchproc.Parameters.Add(new SqlParameter("@guest", guest));
                                AddMatchproc.Parameters.Add(new SqlParameter("@start", start));
                                AddMatchproc.Parameters.Add(new SqlParameter("@end", end));

                                conn.Open();
                                AddMatchproc.ExecuteNonQuery();
                                conn.Close();
                                Response.Redirect("AssocManager.aspx");
                            }
                            else
                            {
                                Response.Write("Invalid end time");

                            }
                        }
                        else
                        {
                            Response.Write("Invalid start time");
                        }
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

        protected void DeleteMatch(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String host = HCD.Text;
            String guest = GCD.Text;

            SqlCommand checkClubproc = new SqlCommand("checkClubExists", conn);
            checkClubproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkClubproc.Parameters.Add(new SqlParameter("@name", host));

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
                checkClubproc2.Parameters.Add(new SqlParameter("@name", guest));

                //OUTPUT
                SqlParameter succ = checkClubproc2.Parameters.Add("@success", System.Data.SqlDbType.Int);
                succ.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkClubproc2.ExecuteNonQuery();
                conn.Close();

                if (succ.Value.ToString() == "1")
                {
                    SqlCommand checkMatchproc = new SqlCommand("checkMatchExists", conn);
                    checkMatchproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    checkMatchproc.Parameters.Add(new SqlParameter("@host", host));
                    checkMatchproc.Parameters.Add(new SqlParameter("@guest", guest));

                    //OUTPUT
                    SqlParameter su = checkMatchproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                    su.Direction = System.Data.ParameterDirection.Output;

                    conn.Open();
                    checkMatchproc.ExecuteNonQuery();
                    conn.Close();

                    if (su.Value.ToString() == "1")
                    {
                        SqlCommand deleteMatchproc = new SqlCommand("deleteMatch", conn);
                        deleteMatchproc.CommandType = System.Data.CommandType.StoredProcedure;

                        //INPUTS
                        deleteMatchproc.Parameters.Add(new SqlParameter("@host", host));
                        deleteMatchproc.Parameters.Add(new SqlParameter("@guest", guest));

                        conn.Open();
                        deleteMatchproc.ExecuteNonQuery();
                        conn.Close();
                        Response.Redirect("AssocManager.aspx");
                    }
                    else
                    {
                        Response.Write("Match doesnot exist");
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

        protected void ViewUpcomingMatches(object sender, EventArgs e)
        {
            Response.Redirect("UpcomingMatches.aspx");
        }

        protected void ViewPlayedMatches(object sender, EventArgs e)
        {
            Response.Redirect("PlayedMatches.aspx");
        }

        protected void ViewClubsNeverMatched(object sender, EventArgs e)
        {
            Response.Redirect("ClubsNeverMatched.aspx");
        }

        protected void logout(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}