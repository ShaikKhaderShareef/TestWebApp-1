using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class DropboxHelper
    {
        // Use a short-lived or long-lived token (for testing, use personal token)
        private static string AccessToken = "sl.u.AFxzxBeG1RPPKtq1AKZ1Nvdj15tq1jyB5GsIcET6n3WW-oMAXATpGjbR0ZkRUs3Ai4bEfiVOgoBHv784KIyFqvGy5D9W2cx7vxj-tIdpVkufXigwuOw1cYvTNWgS2oZI3ZKJrhFVoneD3eHv2cYsLf35-kKsKTUoSxCN9RS2HnpAqIooMHtfoobzG0Oqjjj2u8GjxTYMQAVEImweSZwP3aOwV9RdF-SGDWHNvaWNxy4O5iZQ0lmNVvGkYZxl8KjPNHDLCfEn1ji8xUFgSt6n4mo_rYRYYT90hjlZ1tQyK81xMpQsbgDRmbUCdmlkwS95_0RS3N4_Y32m8NInt8S1ZE4Z-4tbfIZARqrxGdR1IoYucTpIAOHxTUyt91HW8QwOiIj8cdr-tc8E4T4pIoLZ2JiOiJOTjzYT3uQ984kYstAxF69cVmcCNaerN9JogOGY7FX7HoaCdlFyCvN99OaXCT64gSdo02Wee_GKHg4w_6DiOS88mZZaE82T7KIvDyobr-GlYdDt7Ap_SbljOyJGtxqlgDsCUh1mWOFWBaf4HdKIya7syD7gIrvi5OB4ar87UDAH--CIwvzGH0nNiUCOkDQk4EuoRrMswIqMg97uPUDj7Q5uKoHn5YPPj1fny8y7PVk7lat4TuMhV_5SUdVeCus7VKJ0WBNooLjrQVzTMyayamGh02ZxMPpkuTO9tzPJK-BwzkFMGZW5J1tZaFFinv6S3e_9smsQgqcDQ1T1rnrDW5MO6CNFsjr0O4XNVPz9QILO_lYwn3NX_kY-S5jJIND9eFQKK2l1rfwjZH09IXihlj8SmuURu_ZIYyAoUmaVWIKPfPZL-hfPvYKciqxQRe3whM22XuSQXSBgu81WIzp8AnNpXQHmmRz_VBuNX4laKhYNg2Jie_M-w6a81nVGMn9Ycu-_UsxPR1dj_DHF_NpuWmiyAMS0bHGUEVn_Vq5JLqb3VarCU6yZ3HcNokLtJmYk-G_ESYR9fRurl66YRN9XmbXOGJZI1intwM62HMo29IUiFYAgioRqmcLEyw6uNgliQrzddmUizhCNRBJBwGHHkwGSw7D1x-m4c_su2CJS7gR1Z4W0liSfNyOuDypVG4G9nNqycOjpGwcvT0QkocLf5Ek2u37rFxCOLN1x3cFNRGhqCYf6s0RKwvx6yy-g0O0lXG_R9IvDjWK3Z7xcQ0szPSIAvv6SZz5mN-dnX2v0Xz4Ekjay19xmgZfDhtATYNCvNp3jZkTeb7V9LEdi2RE9ogNgepKlZy6O5MEz0V8M-5sm17lm20PadDK6YM2yB2kEb4khN2UxznocoGNMeTvWKflUS7mhkLUQb8YWOBTgzdwHh6bJFtIsWWOO4eGPlR05CQaNYSWu7Rry6yPgtwR5Xtv8LGdN-P6X9RzwcLzvtd2uMcmkKMKG8eX4CW8nXEvEGD4PYcWKSAC2CMKJxYstQQ"; // Replace with your actual token

        public static async Task<bool> CreateFolder(string folderPath)
        {
            using (var dbx = new DropboxClient(AccessToken))
            {
                try
                {
                    await dbx.Files.CreateFolderV2Async(folderPath);
                    return true;
                }
                catch (ApiException<CreateFolderError> ex)
                {
                    // Folder may already exist
                    if (ex.ErrorResponse.IsPath && ex.ErrorResponse.AsPath.Value.IsConflict)
                        return false; // Folder exists
                    else
                        throw;
                }
            }
        }
    }
}