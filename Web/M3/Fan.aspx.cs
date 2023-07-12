using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class Fan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void viewavailablematches(object sender, EventArgs e)
        {
            Response.Redirect("viewMatchestoAttend.aspx");
        }

        protected void logout(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}