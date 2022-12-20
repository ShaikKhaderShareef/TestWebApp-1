using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class color : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  BuildMenu(ASPxMenu1, SqlDataSource1);
        }
        //protected void BuildMenu(DevExpress.Web.ASPxMenu menu, SqlDataSource dataSource)
        //{
        //    // Get DataView
        //    DataSourceSelectArguments arg = new DataSourceSelectArguments();
        //    DataView dataView = dataSource.Select(arg) as DataView;
        //    dataView.Sort = "ParentID";

        //    // Build Menu Items
        //    Dictionary<string, DevExpress.Web.MenuItem> menuItems =
        //        new Dictionary<string, DevExpress.Web.MenuItem>();

        //    for (int i = 0; i < dataView.Count; i++)
        //    {
        //        DataRow row = dataView[i].Row;
        //        DevExpress.Web.MenuItem item = CreateMenuItem(row);
        //        string itemID = row["ID"].ToString();
        //        string parentID = row["ParentID"].ToString();

        //        if (menuItems.ContainsKey(parentID))
        //            menuItems[parentID].Items.Add(item);
        //        else
        //        {
        //            if (parentID == "0") // It's Root Item
        //                menu.Items.Add(item);
        //        }
        //        menuItems.Add(itemID, item);
        //    }
        //}

        //private DevExpress.Web.MenuItem CreateMenuItem(DataRow row)
        //{
        //    DevExpress.Web.MenuItem ret = new DevExpress.Web.MenuItem();
        //    ret.Text = row["Text"].ToString();
        //    ret.NavigateUrl = row["NavigateUrl"].ToString();
        //    return ret;
        //}
    }
}