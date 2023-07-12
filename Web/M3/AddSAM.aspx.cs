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
    public partial class AddSAM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddAssocManager(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String user = SAMusername.Text;
            String pass = SAMpassword.Text;

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

            if(user == "")
            {
                Response.Write("Username cannot be empty");
            }
            else if(pass == "")
            {
                Response.Write("Password cannot be empty");
            }
            else if (success.Value.ToString() == "1")
            {
                String name = SAMname.Text;

                SqlCommand AddAssocManagerproc = new SqlCommand("addAssociationManager", conn);
                AddAssocManagerproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                AddAssocManagerproc.Parameters.Add(new SqlParameter("@name", name));
                AddAssocManagerproc.Parameters.Add(new SqlParameter("@username", user));
                AddAssocManagerproc.Parameters.Add(new SqlParameter("@password", pass));

                conn.Open();
                AddAssocManagerproc.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Write("Username already exists");
            }
        }
    }
}