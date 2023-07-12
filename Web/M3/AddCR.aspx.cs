using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class AddCR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddClubRep(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            
            String user = CRusername.Text;
            String pass = CRpassword.Text;
            String cname = club.Text;

            SqlCommand checkUserproc = new SqlCommand("checkUsernameDistinct", conn);
            checkUserproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkUserproc.Parameters.Add(new SqlParameter("@username", user));

            //OUTPUT
            SqlParameter success = checkUserproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            success.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            checkUserproc.ExecuteNonQuery();
            conn.Close();

            if (user == "")
            {
                Response.Write("Username cannot be empty");
            }
            else if (pass == "")
            {
                Response.Write("Password cannot be empty");
            }
            else if (success.Value.ToString() == "1")
            {
                SqlCommand checkClubproc = new SqlCommand("checkClubExists", conn);
                checkClubproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                checkClubproc.Parameters.Add(new SqlParameter("@name", cname));

                //OUTPUT
                SqlParameter succ = checkClubproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                succ.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkClubproc.ExecuteNonQuery();
                conn.Close();

                if (succ.Value.ToString() == "1")
                {
                    String name = CRname.Text;

                    SqlCommand AddClubRepproc = new SqlCommand("addRepresentative", conn);
                    AddClubRepproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    AddClubRepproc.Parameters.Add(new SqlParameter("@REPname", name));
                    AddClubRepproc.Parameters.Add(new SqlParameter("@CLUBname", cname));
                    AddClubRepproc.Parameters.Add(new SqlParameter("@username", user));
                    AddClubRepproc.Parameters.Add(new SqlParameter("@password", pass));

                    conn.Open();
                    AddClubRepproc.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Write("Invalid Club Name");
                }
            }
            else
            {
                Response.Write("Username already exists");
            }
        }
    }
}