using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddSAM(object sender, EventArgs e)
        {
            Response.Redirect("AddSAM.aspx");
        }

        protected void AddCR(object sender, EventArgs e)
        {
            Response.Redirect("AddCR.aspx");
        }

        protected void AddSM(object sender, EventArgs e)
        {
            Response.Redirect("AddSM.aspx");
        }

        protected void AddFan(object sender, EventArgs e)
        {
            Response.Redirect("AddFan.aspx");
        }
    }
}