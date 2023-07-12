using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace M3
{
    public partial class Host_Req : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void send(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String un = Session["user"].ToString();
            String manager = SM.Text;

            SqlCommand checkSMproc = new SqlCommand("checkSMExists", conn);
            checkSMproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkSMproc.Parameters.Add(new SqlParameter("@name", manager));

            //OUTPUT
            SqlParameter success = checkSMproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            success.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            checkSMproc.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString() == "1")
            {
                if (date.Text == "")
                {
                    Response.Write("Date cannot be empty");
                }
                else
                {
                    DateTime d = DateTime.Parse(date.Text);

                    SqlCommand checkDateValidproc = new SqlCommand("checkDateValid", conn);
                    checkDateValidproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    checkDateValidproc.Parameters.Add(new SqlParameter("@username", un));
                    checkDateValidproc.Parameters.Add(new SqlParameter("@date", d));

                    //OUTPUT
                    SqlParameter succ = checkDateValidproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                    succ.Direction = System.Data.ParameterDirection.Output;

                    conn.Open();
                    checkDateValidproc.ExecuteNonQuery();
                    conn.Close();

                    if (succ.Value.ToString() == "1")
                    {
                        SqlCommand SendReqproc = new SqlCommand("sendRequest", conn);
                        SendReqproc.CommandType = System.Data.CommandType.StoredProcedure;

                        //INPUTS
                        SendReqproc.Parameters.Add(new SqlParameter("@CRusername", un));
                        SendReqproc.Parameters.Add(new SqlParameter("@SMname", manager));
                        SendReqproc.Parameters.Add(new SqlParameter("@date", d));

                        conn.Open();
                        SendReqproc.ExecuteNonQuery();
                        conn.Close();
                        Response.Redirect("ClubRep.aspx");
                    }
                    else
                    {
                        Response.Write("Invalid Match Date");
                    }
                }
            }
            else
            {
                Response.Write("Invalid Stadium Manager Name");
            }
        }

        protected void back(object sender, EventArgs e)
        {
            Response.Redirect("ClubRep.aspx");
        }
    }
}