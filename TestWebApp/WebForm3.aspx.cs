using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWebApp
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string contentType = postedFile.ContentType;
                int fileSize = postedFile.ContentLength;
                int jobId = 32;
                using (Stream fs = postedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string constr = ConfigurationManager.ConnectionStrings["PeekterConnectionString"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            string query = "INSERT INTO JobImages (JobId, ImageName, ImageData, ImageSize, ImageType) VALUES (@JobId, @ImageName, @ImageData, @ImageSize, @ImageType)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@JobId", jobId);
                                cmd.Parameters.AddWithValue("@ImageName", filename);
                                cmd.Parameters.AddWithValue("@ImageData", bytes);
                                cmd.Parameters.AddWithValue("@ImageSize", fileSize);
                                cmd.Parameters.AddWithValue("@ImageType", contentType);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            //  Response.Redirect(Request.Url.AbsoluteUri);
            this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('Success!', 'Image Uploaded successfully', 'success');", true);
        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    string savePath = Server.MapPath("~/Uploads/" + filename);

                    // Save the file to the server
                    FileUpload1.SaveAs(savePath);

                    // Provide feedback to the user
                    ClientScript.RegisterStartupScript(this.GetType(), "UploadSuccess", "alert('File uploaded successfully!');", true);
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    ClientScript.RegisterStartupScript(this.GetType(), "UploadFail", $"alert('Upload failed: {ex.Message}');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "UploadFail", "alert('No file selected to upload.');", true);
            }
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            //foreach (UploadedFile postedFile in ASPxFilterControl1.UploadedFiles)
            //{
            //    if (postedFile.IsValid)
            //    {
            //        string filename = Path.GetFileName(postedFile.FileName);
            //        string contentType = postedFile.ContentType;
            //        int fileSize = postedFile.ContentLength;
            //        int jobId = 32;

            //        using (Stream fs = postedFile.FileContent)
            //        {
            //            using (BinaryReader br = new BinaryReader(fs))
            //            {
            //                byte[] bytes = br.ReadBytes((Int32)fs.Length);
            //                string constr = ConfigurationManager.ConnectionStrings["PeekterConnectionString"].ConnectionString;
            //                using (SqlConnection con = new SqlConnection(constr))
            //                {
            //                    string query = "INSERT INTO JobImages (JobId, ImageName, ImageData, ImageSize, ImageType) VALUES (@JobId, @ImageName, @ImageData, @ImageSize, @ImageType)";
            //                    using (SqlCommand cmd = new SqlCommand(query))
            //                    {
            //                        cmd.Connection = con;
            //                        cmd.Parameters.AddWithValue("@JobId", jobId);
            //                        cmd.Parameters.AddWithValue("@ImageName", filename);
            //                        cmd.Parameters.AddWithValue("@ImageData", bytes);
            //                        cmd.Parameters.AddWithValue("@ImageSize", fileSize);
            //                        cmd.Parameters.AddWithValue("@ImageType", contentType);
            //                        con.Open();
            //                        cmd.ExecuteNonQuery();
            //                        con.Close();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

        }
    }
}