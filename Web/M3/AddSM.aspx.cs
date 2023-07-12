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
    public partial class AddSM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void AddStadiumManager(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            
            String user = SMusername.Text;
            String pass = SMpassword.Text;
            String sname = stadium.Text;

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
                SqlCommand checkStadproc = new SqlCommand("checkStadExists", conn);
                checkStadproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                checkStadproc.Parameters.Add(new SqlParameter("@name", sname));

                //OUTPUT
                SqlParameter succ = checkStadproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                succ.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkStadproc.ExecuteNonQuery();
                conn.Close();

                if (succ.Value.ToString() == "1")
                {

                    String name = SMname.Text;

                    SqlCommand AddStadiumManagerproc = new SqlCommand("addStadiumManager", conn);
                    AddStadiumManagerproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    AddStadiumManagerproc.Parameters.Add(new SqlParameter("@name", name));
                    AddStadiumManagerproc.Parameters.Add(new SqlParameter("@STADname", sname));
                    AddStadiumManagerproc.Parameters.Add(new SqlParameter("@username", user));
                    AddStadiumManagerproc.Parameters.Add(new SqlParameter("@password", pass));

                    conn.Open();
                    AddStadiumManagerproc.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Write("Invalid Stadium Name");
                }
            }
            else
            {
                Response.Write("Username already exists");
            }
        }
    }
}