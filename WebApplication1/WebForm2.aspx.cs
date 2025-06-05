using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    // Use long-lived access token (or implement OAuth flow)
            //    var service = new DropboxService("sl.u.AFuGQvXZGtYp7woTLPibUCNDABngFiVt2Akn68Q4DO6mj9qIb4Nqq_1v6uwtv1Lrrs1Zc1mV65nvRJrT7cl51xid_mNOF8hiZTsZOAoqZ2glPR8ipkbvbMgpG1KzP9cO0MrZH02mp9O6YUjQ5MSNZskPA2WCUMlXit4WJDZDoUkKBpqU3qpRt-__zHMfpG2oYozZs6xWo1lD0E6MUyr6tbaq05PiWx6hCBQay1BDO8UtkKkrYRNoF8BLmMv3lOxDf9XzGEeDpzVIj2_nZ7Y9HQQo26g4TzZ94MRI6mlb_j0yHA8sjm0BEmzcuRcU0PqQIuOodnUbVDcwhg0u846MSXxM0_3wFyn6kgazm0Lz9A6eRy5LizCc6TtM0gIHvweYnSyVjRhIkM_RemSuz6ey7l9s_8-FSFl8DqP1E6bJLUJ5gofne6imbuTYRqHGuaZHvm7_aHrDkEYm06luzIWKQ29Qt5YSBxq2fN4gWthXiK_ppd18GGq9cSf2cPxrUfJeLFzVjs6PXQs5dbKTA_OUlyglnvRD676Brxr-xNH-M6VKXIE8XW2JpDzk7HN1mmTuTvrFDR4iJhuYID8liSlC_19VRR6zlflrmQI3seb92Tih4mS0iVN-AM1CcBiHbY90Q4l0W3NGYmKakMyxkwqhI231f_QLjedfsOEVAKRLsoE9GMw3ES21KB1Opc8qnHzvNFyoI1dAfA2jK1BWyf5wvJRbO1n0NEpdm_9b39DJeuFSAbz7O8jwBwDGY1uI7fnI0C0buRDfeO_nQMdUJmOu7-lT3In39_yscgFcR_iXdYP4eagCncB--BqZMh8pTvQGklidiXa91n8JILhQW8xq5UNPW1tCYkK0k4muIRcOey2rd5Jaboi_CsOa7ylTRL7aVGwyGZ8eUNy7f0wzdBbmk2h4M3gdiJOB5a9u9FJsndtJQ-EHPvk6g8cJd2-Ly3BV22WPao_wjY72VWoLpOd3zs86DK4jaHURzrASpP7FlxRe10HjcX0TLGBVHmfeHbYtNRauo6rnAQuP2aHuBaQ4kBYzNY-z5M8KDE_5zbCxi-egOXriHGMeBIH8vimNYcbigDMbjvOAv9MP6lxE5LDpXju2cG0fhdhT8srSFrLIhWfu2Dpr24VU_AgmH90gIRMf5c3ZfErBf7M5hNklAfBAWmeNCZrkOV7HxXdu8sdoAgQa2ELZvem2R_pPHNYjcWDeTLODJZX0V1kjexCLCG0pGQ2FnE0njpLfiEA4mW39PKmU-i5Z5XC0XGPCmupDq0MgGW4e3_DerEYNYc3Y98HirpuDSTmutcSwWclwGmlaPgIf4APahZy1CkKa741XyqvLDaf2oVki1cabkHhrasHmLLjExPcb1T3Xi3YIFovRREXNFyhbNyYOSviHQStrCqjSKD9NS3-FeBOzPRfQkUYpkHiDMzUTEAA-lTkgN6qxrkoqfg");
            //    var files = service.GetFiles("/Test");

            //    // Bind to DevExpress FileManager
            //    ASPxFileManager1.DataSource = files;
            //    ASPxFileManager1.DataBind();
            //}

            if (!IsPostBack)
            {
                //// Get token from secure storage (e.g., Session, database)
                //string token = "sl.u.AFxzxBeG1RPPKtq1AKZ1Nvdj15tq1jyB5GsIcET6n3WW-oMAXATpGjbR0ZkRUs3Ai4bEfiVOgoBHv784KIyFqvGy5D9W2cx7vxj-tIdpVkufXigwuOw1cYvTNWgS2oZI3ZKJrhFVoneD3eHv2cYsLf35-kKsKTUoSxCN9RS2HnpAqIooMHtfoobzG0Oqjjj2u8GjxTYMQAVEImweSZwP3aOwV9RdF-SGDWHNvaWNxy4O5iZQ0lmNVvGkYZxl8KjPNHDLCfEn1ji8xUFgSt6n4mo_rYRYYT90hjlZ1tQyK81xMpQsbgDRmbUCdmlkwS95_0RS3N4_Y32m8NInt8S1ZE4Z-4tbfIZARqrxGdR1IoYucTpIAOHxTUyt91HW8QwOiIj8cdr-tc8E4T4pIoLZ2JiOiJOTjzYT3uQ984kYstAxF69cVmcCNaerN9JogOGY7FX7HoaCdlFyCvN99OaXCT64gSdo02Wee_GKHg4w_6DiOS88mZZaE82T7KIvDyobr-GlYdDt7Ap_SbljOyJGtxqlgDsCUh1mWOFWBaf4HdKIya7syD7gIrvi5OB4ar87UDAH--CIwvzGH0nNiUCOkDQk4EuoRrMswIqMg97uPUDj7Q5uKoHn5YPPj1fny8y7PVk7lat4TuMhV_5SUdVeCus7VKJ0WBNooLjrQVzTMyayamGh02ZxMPpkuTO9tzPJK-BwzkFMGZW5J1tZaFFinv6S3e_9smsQgqcDQ1T1rnrDW5MO6CNFsjr0O4XNVPz9QILO_lYwn3NX_kY-S5jJIND9eFQKK2l1rfwjZH09IXihlj8SmuURu_ZIYyAoUmaVWIKPfPZL-hfPvYKciqxQRe3whM22XuSQXSBgu81WIzp8AnNpXQHmmRz_VBuNX4laKhYNg2Jie_M-w6a81nVGMn9Ycu-_UsxPR1dj_DHF_NpuWmiyAMS0bHGUEVn_Vq5JLqb3VarCU6yZ3HcNokLtJmYk-G_ESYR9fRurl66YRN9XmbXOGJZI1intwM62HMo29IUiFYAgioRqmcLEyw6uNgliQrzddmUizhCNRBJBwGHHkwGSw7D1x-m4c_su2CJS7gR1Z4W0liSfNyOuDypVG4G9nNqycOjpGwcvT0QkocLf5Ek2u37rFxCOLN1x3cFNRGhqCYf6s0RKwvx6yy-g0O0lXG_R9IvDjWK3Z7xcQ0szPSIAvv6SZz5mN-dnX2v0Xz4Ekjay19xmgZfDhtATYNCvNp3jZkTeb7V9LEdi2RE9ogNgepKlZy6O5MEz0V8M-5sm17lm20PadDK6YM2yB2kEb4khN2UxznocoGNMeTvWKflUS7mhkLUQb8YWOBTgzdwHh6bJFtIsWWOO4eGPlR05CQaNYSWu7Rry6yPgtwR5Xtv8LGdN-P6X9RzwcLzvtd2uMcmkKMKG8eX4CW8nXEvEGD4PYcWKSAC2CMKJxYstQQ";

                //// Initialize Dropbox service
                //var dropboxService = new DropboxService(token);

                //// Bind files to DevExpress FileManager
                //ASPxFileManager1.DataSource = dropboxService.GetFiles("/Test");
                //ASPxFileManager1.DataBind();

                ASPxFileManager1.Settings.RootFolder = "~/Test";
             
            }
        }
       

        private void UpdateUIAndFileManager(string dropboxPath)
        {
            throw new NotImplementedException();
        }

        protected void ASPxButton2_Click(object sender, EventArgs e)
        {
            //string folderName = "Test2";
            //string employeeId = "1136";

            //// Validation
            ////if (string.IsNullOrEmpty(folderName))
            ////{
            ////    FolderPanelError1.Visible = true;
            ////    return;
            ////}

            //// Construct Dropbox path: /{empid}/{folderName}
            //string dropboxPath = $"/{employeeId}/{folderName}";

            //try
            //{
            //    using (var dbx = new DropboxClient("sl.u.AFxzxBeG1RPPKtq1AKZ1Nvdj15tq1jyB5GsIcET6n3WW-oMAXATpGjbR0ZkRUs3Ai4bEfiVOgoBHv784KIyFqvGy5D9W2cx7vxj-tIdpVkufXigwuOw1cYvTNWgS2oZI3ZKJrhFVoneD3eHv2cYsLf35-kKsKTUoSxCN9RS2HnpAqIooMHtfoobzG0Oqjjj2u8GjxTYMQAVEImweSZwP3aOwV9RdF-SGDWHNvaWNxy4O5iZQ0lmNVvGkYZxl8KjPNHDLCfEn1ji8xUFgSt6n4mo_rYRYYT90hjlZ1tQyK81xMpQsbgDRmbUCdmlkwS95_0RS3N4_Y32m8NInt8S1ZE4Z-4tbfIZARqrxGdR1IoYucTpIAOHxTUyt91HW8QwOiIj8cdr-tc8E4T4pIoLZ2JiOiJOTjzYT3uQ984kYstAxF69cVmcCNaerN9JogOGY7FX7HoaCdlFyCvN99OaXCT64gSdo02Wee_GKHg4w_6DiOS88mZZaE82T7KIvDyobr-GlYdDt7Ap_SbljOyJGtxqlgDsCUh1mWOFWBaf4HdKIya7syD7gIrvi5OB4ar87UDAH--CIwvzGH0nNiUCOkDQk4EuoRrMswIqMg97uPUDj7Q5uKoHn5YPPj1fny8y7PVk7lat4TuMhV_5SUdVeCus7VKJ0WBNooLjrQVzTMyayamGh02ZxMPpkuTO9tzPJK-BwzkFMGZW5J1tZaFFinv6S3e_9smsQgqcDQ1T1rnrDW5MO6CNFsjr0O4XNVPz9QILO_lYwn3NX_kY-S5jJIND9eFQKK2l1rfwjZH09IXihlj8SmuURu_ZIYyAoUmaVWIKPfPZL-hfPvYKciqxQRe3whM22XuSQXSBgu81WIzp8AnNpXQHmmRz_VBuNX4laKhYNg2Jie_M-w6a81nVGMn9Ycu-_UsxPR1dj_DHF_NpuWmiyAMS0bHGUEVn_Vq5JLqb3VarCU6yZ3HcNokLtJmYk-G_ESYR9fRurl66YRN9XmbXOGJZI1intwM62HMo29IUiFYAgioRqmcLEyw6uNgliQrzddmUizhCNRBJBwGHHkwGSw7D1x-m4c_su2CJS7gR1Z4W0liSfNyOuDypVG4G9nNqycOjpGwcvT0QkocLf5Ek2u37rFxCOLN1x3cFNRGhqCYf6s0RKwvx6yy-g0O0lXG_R9IvDjWK3Z7xcQ0szPSIAvv6SZz5mN-dnX2v0Xz4Ekjay19xmgZfDhtATYNCvNp3jZkTeb7V9LEdi2RE9ogNgepKlZy6O5MEz0V8M-5sm17lm20PadDK6YM2yB2kEb4khN2UxznocoGNMeTvWKflUS7mhkLUQb8YWOBTgzdwHh6bJFtIsWWOO4eGPlR05CQaNYSWu7Rry6yPgtwR5Xtv8LGdN-P6X9RzwcLzvtd2uMcmkKMKG8eX4CW8nXEvEGD4PYcWKSAC2CMKJxYstQQ"))
            //    {
            //        // Check if folder exists
            //        var metadata = await dbx.Files.GetMetadataAsync(dropboxPath);

            //        if (metadata.IsFolder)
            //        {
            //            // Folder exists - proceed
            //            UpdateUIAndFileManager(dropboxPath);
            //        }
            //    }
            //}
            //catch (ApiException<GetMetadataError> ex) when (ex.ErrorResponse.IsPath &&
            //                                              ex.ErrorResponse.AsPath.Value.IsNotFound)
            //{
            //    // Folder doesn't exist - create it
            //    try
            //    {
            //        using (var dbx = new DropboxClient("sl.u.AFxzxBeG1RPPKtq1AKZ1Nvdj15tq1jyB5GsIcET6n3WW-oMAXATpGjbR0ZkRUs3Ai4bEfiVOgoBHv784KIyFqvGy5D9W2cx7vxj-tIdpVkufXigwuOw1cYvTNWgS2oZI3ZKJrhFVoneD3eHv2cYsLf35-kKsKTUoSxCN9RS2HnpAqIooMHtfoobzG0Oqjjj2u8GjxTYMQAVEImweSZwP3aOwV9RdF-SGDWHNvaWNxy4O5iZQ0lmNVvGkYZxl8KjPNHDLCfEn1ji8xUFgSt6n4mo_rYRYYT90hjlZ1tQyK81xMpQsbgDRmbUCdmlkwS95_0RS3N4_Y32m8NInt8S1ZE4Z-4tbfIZARqrxGdR1IoYucTpIAOHxTUyt91HW8QwOiIj8cdr-tc8E4T4pIoLZ2JiOiJOTjzYT3uQ984kYstAxF69cVmcCNaerN9JogOGY7FX7HoaCdlFyCvN99OaXCT64gSdo02Wee_GKHg4w_6DiOS88mZZaE82T7KIvDyobr-GlYdDt7Ap_SbljOyJGtxqlgDsCUh1mWOFWBaf4HdKIya7syD7gIrvi5OB4ar87UDAH--CIwvzGH0nNiUCOkDQk4EuoRrMswIqMg97uPUDj7Q5uKoHn5YPPj1fny8y7PVk7lat4TuMhV_5SUdVeCus7VKJ0WBNooLjrQVzTMyayamGh02ZxMPpkuTO9tzPJK-BwzkFMGZW5J1tZaFFinv6S3e_9smsQgqcDQ1T1rnrDW5MO6CNFsjr0O4XNVPz9QILO_lYwn3NX_kY-S5jJIND9eFQKK2l1rfwjZH09IXihlj8SmuURu_ZIYyAoUmaVWIKPfPZL-hfPvYKciqxQRe3whM22XuSQXSBgu81WIzp8AnNpXQHmmRz_VBuNX4laKhYNg2Jie_M-w6a81nVGMn9Ycu-_UsxPR1dj_DHF_NpuWmiyAMS0bHGUEVn_Vq5JLqb3VarCU6yZ3HcNokLtJmYk-G_ESYR9fRurl66YRN9XmbXOGJZI1intwM62HMo29IUiFYAgioRqmcLEyw6uNgliQrzddmUizhCNRBJBwGHHkwGSw7D1x-m4c_su2CJS7gR1Z4W0liSfNyOuDypVG4G9nNqycOjpGwcvT0QkocLf5Ek2u37rFxCOLN1x3cFNRGhqCYf6s0RKwvx6yy-g0O0lXG_R9IvDjWK3Z7xcQ0szPSIAvv6SZz5mN-dnX2v0Xz4Ekjay19xmgZfDhtATYNCvNp3jZkTeb7V9LEdi2RE9ogNgepKlZy6O5MEz0V8M-5sm17lm20PadDK6YM2yB2kEb4khN2UxznocoGNMeTvWKflUS7mhkLUQb8YWOBTgzdwHh6bJFtIsWWOO4eGPlR05CQaNYSWu7Rry6yPgtwR5Xtv8LGdN-P6X9RzwcLzvtd2uMcmkKMKG8eX4CW8nXEvEGD4PYcWKSAC2CMKJxYstQQ"))
            //        {
            //            await dbx.Files.CreateFolderV2Async(dropboxPath);
            //            UpdateUIAndFileManager(dropboxPath);
            //        }
            //    }
            //    catch (Exception createEx)
            //    {
            //        // Handle creation error
            //        //  FolderPanelError2.Text = $"Creation failed: {createEx.Message}";
            //        //  FolderPanelError2.Visible = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Handle other errors
            //    //   FolderPanelError2.Text = $"Error: {ex.Message}";
            //    //  FolderPanelError2.Visible = true;
            //}
        }

        protected async void btnCreateDropboxFolder_Click(object sender, EventArgs e)
        {
            //if (Session["empid"] == null || string.IsNullOrWhiteSpace(txtfoldername.Text))
            //{
            //    lblStatus.Text = "Employee ID or folder name is missing.";
            //    lblStatus.ForeColor = System.Drawing.Color.Red;
            //    return;
            //}

            // Construct Dropbox path
            string empId = "1136";
            string folderName = "dropbox";
            string dropboxPath = "/" + empId + "/" + folderName;

            try
            {
                bool created = await DropboxHelper.CreateFolder(dropboxPath);

                if (created)
                {
                    lblStatus.Text = "Dropbox folder created successfully.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = "Folder already exists in Dropbox.";
                    lblStatus.ForeColor = System.Drawing.Color.Orange;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}