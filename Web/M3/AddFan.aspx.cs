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
    public partial class AddFan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddF(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String user = username.Text;
            String pass = password.Text;
            String natid = ID.Text;

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
            else if(natid == "")
            {
                Response.Write("National ID cannot be empty");
            }
            else if (success.Value.ToString() == "1")
            {
                SqlCommand checkNatidproc = new SqlCommand("checkNatidDistinct", conn);
                checkNatidproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                checkNatidproc.Parameters.Add(new SqlParameter("@id", natid));

                //OUTPUT
                SqlParameter succ = checkNatidproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                succ.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkNatidproc.ExecuteNonQuery();
                conn.Close();
                if (succ.Value.ToString() == "1")
                {
                    String name = Fname.Text;
                    String phoneno = phone.Text;
                    String bdate = birth.Text;
                    String add = address.Text;

                    SqlCommand AddFanproc = new SqlCommand("addFan", conn);
                    AddFanproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    AddFanproc.Parameters.Add(new SqlParameter("@name", name));
                    AddFanproc.Parameters.Add(new SqlParameter("@username", user));
                    AddFanproc.Parameters.Add(new SqlParameter("@password", pass));
                    AddFanproc.Parameters.Add(new SqlParameter("@id", natid));
                    AddFanproc.Parameters.Add(new SqlParameter("@birthdate", bdate));
                    AddFanproc.Parameters.Add(new SqlParameter("@address", add));
                    AddFanproc.Parameters.Add(new SqlParameter("@phone_number", phoneno));

                    conn.Open();
                    AddFanproc.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Write("National ID already registered");
                }
            }
            else
            {
                Response.Write("Username already exists");
            }
        }
    }
}