using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lexa
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
           // string path = Server.MapPath("~/Submittals/Selproject.aspx");
         //   Response.Redirect("~/Submittals/Selproject.aspx");

            //string relativeUrl = ("~/Submittals/Selproject.aspx");
            //string absoluteUrl = VirtualPathUtility.ToAbsolute(relativeUrl);
            //Response.Redirect(absoluteUrl);

            //string redirectUrl = ("~/Submittals/Selproject.aspx"); // Your method to get URL
            //System.Diagnostics.Debug.WriteLine("Redirecting to: " + redirectUrl);
            //Response.Redirect(redirectUrl);

            string redirectUrl = ("/Submittals/Selproject.aspx"); // Your method to get URL
            if (Uri.IsWellFormedUriString(redirectUrl, UriKind.RelativeOrAbsolute))
            {
                Response.Redirect(redirectUrl);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Invalid URL: " + redirectUrl);
            }



        }
    }
}