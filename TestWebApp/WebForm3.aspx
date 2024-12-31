<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="TestWebApp.WebForm3" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.0/sweetalert.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.0/sweetalert.min.css" rel="stylesheet" type="text/css" />
       <style>
        #dropZone {
            width: 300px;
            height: 200px;
            border: 2px dashed #cccccc;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
            color: #666;
            font-size: 14px;
            margin-top: 20px;
        }
        #dropZone.dragover {
            border-color: #0000ff;
            background-color: #f0f8ff;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:FileUpload ID="FileUpload1" runat="server" Style="display: none;" />
        <div id="dropZone">Drag and drop files here or click to select</div>
     <%--   <asp:Button ID="UploadButton" runat="server" Text="Upload Files" OnClick="UploadButton_Click" />   --%>
        <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" />

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const dropZone = document.getElementById('dropZone');
            const fileUploadControl = document.getElementById('<%= FileUpload1.ClientID %>');

            // Trigger file selection dialog when clicking the drop zone
            dropZone.addEventListener('click', () => {
                fileUploadControl.click();
            });

            // Handle dragover event
            dropZone.addEventListener('dragover', (e) => {
                e.preventDefault();
                dropZone.classList.add('dragover');
            });

            // Handle dragleave event
            dropZone.addEventListener('dragleave', () => {
                dropZone.classList.remove('dragover');
            });

            // Handle drop event
            dropZone.addEventListener('drop', (e) => {
                e.preventDefault();
                dropZone.classList.remove('dragover');

                if (e.dataTransfer.files.length > 0) {
                    // Assign the dropped files to the FileUpload control
                    fileUploadControl.files = e.dataTransfer.files;
                    alert('Files added to upload.');
                }
            });
        });
    </script>
             
          

    <br />

     <%-- <dx:ASPxUploadControl ID="UploadControl" runat="server" ClientInstanceName="UploadControl" Width="100%"
            NullText="Select multiple files..." UploadMode="Advanced" ShowUploadButton="True" ShowProgressPanel="True"
            OnFileUploadComplete="UploadControl_FileUploadComplete">
            <AdvancedModeSettings EnableMultiSelect="True" EnableFileList="True" EnableDragAndDrop="True" />
            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.gif,.png">
            </ValidationSettings>
            <ClientSideEvents FilesUploadStart="function(s, e) { DXUploadedFilesContainer.Clear(); }"
                              FileUploadComplete="onFileUploadComplete" />
        </dx:ASPxUploadControl>--%>

    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server">
        <Settings RootFolder="~/" ThumbnailFolder="~/Thumb/" />
        <SettingsUpload>
            <AdvancedModeSettings EnableMultiSelect="True"></AdvancedModeSettings>
        </SettingsUpload>
    </dx:ASPxFileManager>
    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" OnClick="ASPxButton1_Click"></dx:ASPxButton>
</asp:Content>
