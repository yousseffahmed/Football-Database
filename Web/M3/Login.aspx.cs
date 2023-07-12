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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String user = username.Text;
            String pass = password.Text;

            SqlCommand loginproc = new SqlCommand("userlogin", conn);
            loginproc.CommandType = System.Data.CommandType.StoredProcedure;
            
            //INPUTS
            loginproc.Parameters.Add(new SqlParameter("@username", user));
            loginproc.Parameters.Add(new SqlParameter("@password", pass));

            //OUTPUT
            SqlParameter success = loginproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            SqlParameter type = loginproc.Parameters.Add("@type", System.Data.SqlDbType.Int);
            success.Direction = System.Data.ParameterDirection.Output;
            type.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            loginproc.ExecuteNonQuery();
            conn.Close();

            if(success.Value.ToString()=="1")
            {
                Session["user"] = user;
                Response.Write(Session["user"]);
                if (type.Value.ToString() == "1")
                    Response.Redirect("SystemAdmin.aspx");
                else if (type.Value.ToString() == "2")
                    Response.Redirect("AssocManager.aspx");
                else if (type.Value.ToString() == "3")
                    Response.Redirect("ClubRep.aspx");
                else if (type.Value.ToString() == "4")
                    Response.Redirect("StadiumManager.aspx");
                else if (type.Value.ToString() == "5")
                    Response.Redirect("Fan.aspx");
            }
            else
            {
                Response.Write("Invalid Credentials");
            }
        }
        protected void AddUser(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }
    }
}