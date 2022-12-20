using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class myadminpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxRibbon ribbon = new ASPxRibbon();
            ribbon.ID = "ASPxRibbon1";
            //ASPxRibbon ribbon = new ASPxRibbon();
            //ribbon.ID = "ASPxRibbon1";
            ribbon.Theme = "Aqua";
            ribbon.Font.Name = "Calibri Light";
            ribbon.Font.Size = 10;
            ribbon.Font.Bold = true;
            ribbon.Attributes.Add("style", "position: [top]");
            ribbon.Width = 1480;

            RibbonTab tab1 = new RibbonTab("Attendance");
            RibbonTab tab2 = new RibbonTab("Accomodation");
            RibbonTab tab3 = new RibbonTab("Legal Services");
            RibbonTab tab4 = new RibbonTab("Employee Self Service");
            RibbonTab tab5 = new RibbonTab("Projects Management");
            RibbonTab tab6 = new RibbonTab("Project Submittals");
            RibbonTab tab7 = new RibbonTab("Visitors");
            RibbonTab tab8 = new RibbonTab("Assests");

            //  ribbon.Tabs.Add(tab6);

            if (ribbon != null)
            {
                ribbon.Tabs.Add(tab6);
            }

            //  ribbon.Tabs.Add(tab2);
            //   ribbon.Tabs.Add(tab3);                             
            //  tab5.Groups.Find(t => t.Text == "Quotes").Items.Add(quotelistButton);
            //  ribbon.Find(t => t.Text == "ASPxRibbon1").Items.Add(tab5);

            var HomeButton = new RibbonOptionButtonItem("Home", "Home", RibbonItemSize.Large);
            HomeButton.OptionGroupName = "Group2";
            HomeButton.LargeImage.IconID = "iconbuilder_actions_home_svg_32x32";
            tab6.Groups.Add("Home").Items.Add(HomeButton);

            var selectButton = new RibbonOptionButtonItem("Select Project", "Select Project", RibbonItemSize.Large);
            selectButton.OptionGroupName = "Group2";
            selectButton.LargeImage.IconID = "iconbuilder_actions_home_svg_32x32";
            tab6.Groups.Add("Select").Items.Add(selectButton);
            selectButton.NavigateUrl = "~/Submittals/Selproject.aspx";

            var newquoteButton = new RibbonOptionButtonItem("Drawing Form", "Drawing Form", RibbonItemSize.Large);
            newquoteButton.OptionGroupName = "Group2";
            newquoteButton.LargeImage.IconID = "outlookinspired_shipmentawaiting_svg_32x32";
            tab6.Groups.Add("Submittal Form").Items.Add(newquoteButton);
            newquoteButton.NavigateUrl = "~/Submittals/Drawingform.aspx";

            var quotelistButton = new RibbonOptionButtonItem("Material Form", "Material Form", RibbonItemSize.Large);
            quotelistButton.OptionGroupName = "Group2";
            quotelistButton.LargeImage.IconID = "outlookinspired_deferred_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal Form").Items.Add(quotelistButton);
            quotelistButton.NavigateUrl = "~/Submittals/Materialform.aspx";

            var documentButton = new RibbonOptionButtonItem("Document Form", "Document Form", RibbonItemSize.Large);
            documentButton.OptionGroupName = "Group2";
            documentButton.LargeImage.IconID = "outlookinspired_deferred_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal Form").Items.Add(documentButton);
            documentButton.NavigateUrl = "~/Submittals/Documentform.aspx";

            var HSEButton = new RibbonOptionButtonItem("HSE Form", "HSE Form", RibbonItemSize.Large);
            HSEButton.OptionGroupName = "Group2";
            HSEButton.LargeImage.IconID = "outlookinspired_deferred_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal Form").Items.Add(HSEButton);
            HSEButton.NavigateUrl = "~/Submittals/Hseform.aspx";

            var RAMSButton = new RibbonOptionButtonItem("RAMS Form", "RAMS Form", RibbonItemSize.Large);
            RAMSButton.OptionGroupName = "Group2";
            RAMSButton.LargeImage.IconID = "outlookinspired_deferred_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal Form").Items.Add(RAMSButton);
            RAMSButton.NavigateUrl = "~/Submittals/Ramsform.aspx";

            var rfiButton = new RibbonOptionButtonItem("RFI Form", "RFI Form", RibbonItemSize.Large);
            rfiButton.OptionGroupName = "Group2";
            rfiButton.LargeImage.IconID = "outlookinspired_deferred_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal Form").Items.Add(rfiButton);
            rfiButton.NavigateUrl = "~/Submittals/Rfiform.aspx";

            var newprojButton = new RibbonOptionButtonItem("Drawing List", "Drawing List", RibbonItemSize.Large);
            newprojButton.OptionGroupName = "Group2";
            newprojButton.LargeImage.IconID = "scheduling_time_svg_32x32";
            tab6.Groups.Add("Submittal List").Items.Add(newprojButton);
            newprojButton.NavigateUrl = "~/Submittals/Drawinglist.aspx";

            var materiallistButton = new RibbonOptionButtonItem("Material List", "Material List", RibbonItemSize.Large);
            materiallistButton.OptionGroupName = "Group2";
            materiallistButton.LargeImage.IconID = "scheduling_time_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal List").Items.Add(materiallistButton);
            materiallistButton.NavigateUrl = "~/Submittals/Materiallist.aspx";

            var documentlistButton = new RibbonOptionButtonItem("Document List", "Document List", RibbonItemSize.Large);
            documentlistButton.OptionGroupName = "Group2";
            documentlistButton.LargeImage.IconID = "scheduling_time_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal List").Items.Add(documentlistButton);
            documentlistButton.NavigateUrl = "~/Submittals/Documentlist.aspx";

            var hselistButton = new RibbonOptionButtonItem("HSE List", "HSE List", RibbonItemSize.Large);
            hselistButton.OptionGroupName = "Group2";
            hselistButton.LargeImage.IconID = "scheduling_time_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal List").Items.Add(hselistButton);
            hselistButton.NavigateUrl = "~/Submittals/Hselist.aspx";

            var ramslistButton = new RibbonOptionButtonItem("RAMS List", "RAMS List", RibbonItemSize.Large);
            ramslistButton.OptionGroupName = "Group2";
            ramslistButton.LargeImage.IconID = "scheduling_time_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal List").Items.Add(ramslistButton);
            ramslistButton.NavigateUrl = "~/Submittals/Ramslist.aspx";

            var rfilistButton = new RibbonOptionButtonItem("RFI List", "RFI List", RibbonItemSize.Large);
            rfilistButton.OptionGroupName = "Group2";
            rfilistButton.LargeImage.IconID = "scheduling_time_svg_32x32";
            tab6.Groups.Find(t => t.Text == "Submittal List").Items.Add(rfilistButton);
            rfilistButton.NavigateUrl = "~/Submittals/Rfilist.aspx";

            var EmailButton = new RibbonOptionButtonItem("Email", "Email", RibbonItemSize.Large);
            EmailButton.OptionGroupName = "Group2";
            EmailButton.LargeImage.IconID = "outlookinspired_glyph_mail_svg_32x32";
            tab6.Groups.Add("Reports").Items.Add(EmailButton);
            EmailButton.NavigateUrl = "~/Admin/Genlicense.aspx";


            // Adds the created control to the page
            //   Page.Form.Controls.Add(ribbon);
            // ribbon.FindControl(t => t.Text == "ASPxRibbon1").Items.Add(tab5);                                   
            Panel1.Controls.Add(ribbon);
        }
    }
}