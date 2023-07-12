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
    public partial class SystemAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddClub(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String Name = CNA.Text;
            String Location = CLA.Text;

            SqlCommand checkClubproc = new SqlCommand("checkClubExists", conn);
            checkClubproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkClubproc.Parameters.Add(new SqlParameter("@name", Name));

            //OUTPUT
            SqlParameter success = checkClubproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            success.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            checkClubproc.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString() == "0")
            {
                if (Name == "")
                {
                    Response.Write("Name is empty");
                }
                else if (Location == "")
                {
                    Response.Write("Location is empty");
                }
                else
                {
                    SqlCommand AddClubproc = new SqlCommand("addClub", conn);
                    AddClubproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    AddClubproc.Parameters.Add(new SqlParameter("@name", Name));
                    AddClubproc.Parameters.Add(new SqlParameter("@location", Location));

                    conn.Open();
                    AddClubproc.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("SystemAdmin.aspx");
                }
            }
            else
            {
                Response.Write("Club name already exists");
            }
        }

        protected void DeleteClub(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String Name = CND.Text;

            SqlCommand checkClubproc = new SqlCommand("checkClubExists", conn);
            checkClubproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkClubproc.Parameters.Add(new SqlParameter("@name", Name));

            //OUTPUT
            SqlParameter success = checkClubproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            success.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            checkClubproc.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString() == "1")
            {
                SqlCommand DeleteClubproc = new SqlCommand("deleteClub", conn);
                DeleteClubproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                DeleteClubproc.Parameters.Add(new SqlParameter("@name", Name));

                conn.Open();
                DeleteClubproc.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("SystemAdmin.aspx");
            }
            else
            {
                Response.Write("Invalid Club Name");
            }
        }

        protected void AddStadium(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String Name = SNA.Text;
            String Location = SLA.Text;
            String Capacity = SCA.Text;

            SqlCommand checkStadproc = new SqlCommand("checkStadExists", conn);
            checkStadproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkStadproc.Parameters.Add(new SqlParameter("@name", Name));

            //OUTPUT
            SqlParameter success = checkStadproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            success.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            checkStadproc.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString() == "0")
            {
                if (Name == "")
                {
                    Response.Write("Name is empty");
                }
                else if (Location == "")
                {
                    Response.Write("Location is empty");
                }
                else if (Capacity == "")
                {
                    Response.Write("Capacity is empty");
                }
                else
                {
                    SqlCommand AddStadiumproc = new SqlCommand("addStadium", conn);
                    AddStadiumproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    AddStadiumproc.Parameters.Add(new SqlParameter("@name", Name));
                    AddStadiumproc.Parameters.Add(new SqlParameter("@location", Location));
                    AddStadiumproc.Parameters.Add(new SqlParameter("@capacity", Capacity));

                    conn.Open();
                    AddStadiumproc.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("SystemAdmin.aspx");
                }
            }
            else
            {
                Response.Write("Stadium name already exists");
            }
        }

        protected void DeleteStadium(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String Name = SND.Text;

            SqlCommand checkStadproc = new SqlCommand("checkStadExists", conn);
            checkStadproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkStadproc.Parameters.Add(new SqlParameter("@name", Name));

            //OUTPUT
            SqlParameter success = checkStadproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            success.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            checkStadproc.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString() == "1")
            {
                SqlCommand DeleteStadiumproc = new SqlCommand("deleteStadium", conn);
                DeleteStadiumproc.CommandType = System.Data.CommandType.StoredProcedure;

                //INPUTS
                DeleteStadiumproc.Parameters.Add(new SqlParameter("@name", Name));

                conn.Open();
                DeleteStadiumproc.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("SystemAdmin.aspx");
            }
            else
            {
                Response.Write("Invalid Stadium Name");
            }
        }

        protected void BlockFan(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String ID = FNB.Text;

            SqlCommand checkNatidproc = new SqlCommand("checkNatidDistinct", conn);
            checkNatidproc.CommandType = System.Data.CommandType.StoredProcedure;

            //INPUTS
            checkNatidproc.Parameters.Add(new SqlParameter("@id", ID));

            //OUTPUT
            SqlParameter success = checkNatidproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            success.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            checkNatidproc.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString() == "0")
            {
                if (ID == "")
                {
                    Response.Write("Fan national id is empty");
                }
                else
                {
                    SqlCommand BlockFanproc = new SqlCommand("blockFan", conn);
                    BlockFanproc.CommandType = System.Data.CommandType.StoredProcedure;

                    //INPUTS
                    BlockFanproc.Parameters.Add(new SqlParameter("@id", ID));

                    conn.Open();
                    BlockFanproc.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("SystemAdmin.aspx");
                }
            }
            else
            {
                Response.Write("Invalid Fan National ID");
            }
        }

        protected void logout(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}