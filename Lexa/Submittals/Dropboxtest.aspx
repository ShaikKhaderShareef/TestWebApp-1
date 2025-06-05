<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dropboxtest.aspx.cs" Inherits="Lexa.Submittals.Dropboxtest" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" ProviderType="Dropbox" Theme="DevEx">
                <Settings RootFolder="~/" ThumbnailFolder="~/Thumb/" />
                <SettingsEditing AllowCreate="True" AllowRename="True" AllowMove="True" AllowCopy="True" AllowDelete="True" AllowDownload="True"></SettingsEditing>

                <SettingsDropbox AccessTokenValue="sl.u.AFvt26okGHy93HSWyo6Oca6iwdd6boCKpKevIgSg9bqU5UCwiKovoPK3r1SdDqFCFZ92mabHWa9pjZV6zntJsykQftd-1VQ1Grl9C35ETLHFepcnKFMij6AQiUGpMBjJ0QII7EyDIERHhtqjF2yUOYsjfNPhExrDgv_2f5a_c3M7sxETJ3AlRkzBPk10DUxtnVV3jdTgundULK71CuhJBncPJSso1QfHLpc2cAJ-u4JoMXE5u9QrOSa6FdhXrOxj8fH3I2QcrAJkx9dH8w_hTnsfgIg3ajGXnx6QKturYuTEKke-xoH_5bu8idLnr_jYagw5yZLDJP7ZASgDiY-ijsrIq52CPikw6IlJXV7v0g2HSGmj8O4gGZtoIvW8eUtJEZLFpy_Cdh78VGdwpDYbQQ69-FNGzk62QC60NyVDggrCTslX-rD3vqZCG0MwjM8BbRbQtsSz10QBP2DICuKmEicLURDYaxCM32gDltumq5EWijYUSjwEqgmtQa8e9dSU4_1DydCVl5gU04Vudb8LbrGIKDwYMP8xx6xD64dORIob5CrTtZ2E7TY75NIiarQbESrFwMhfkwjj3g9E--hC82jCKnrgHELaxpw650vNoAnbZ42LShXsmpJzD2JY70rIAjSYdyE3Bn9S6chOCjiNZhJ-tkZaelwBmv7a8H22pn7_hQGzjps8RsB437ow8jKD8i8FQC7-uUfwb-z25BqXbNJMA4IIeXIGxc3V4ytLz7ueQQ_zuFzQQcgzt7YejNY6wfnUKPtl87E79sxIkdIxFj0LA2PLNexaEh8cPVIW6pkNYWWbIdFAdlmr0brnwAZBYAMI8krFIKc4kLsArtR-ZBag8VeVu6vJUfsu4FF_ye-mw0mJsvFmvfZMBXcda4qZlX3ehFHAJ4WPyXs9XmdV8xABj2H__thg0b6szf9JYx_vYhUDFsGjltZJVeoKFHrkT7qwZTKOEMY3xaEUV5a-vAUt3edyjEaOb4Tsv7-1pl9lJ918WMsy-1hFwyT1Kln60xljlEAbEeSPUQGO0nSU0Xo_odzgEXRiI1IyJioqVA658qhZSDeAF9B91gktaeJbxQ4dyBUU0sd6Zmuw7VYDXqsBL9DqXuUI_e4XGfoC-6wNr7TxX4VRuafg19q5rej3s_mjvHC6JXWoADDXy8fDrIRf9I8V0gWFl1q10QNSeEgOR6YIiwBLJ6zuAL3KIyXB2PRO5YjDLfAoWiD9uWYBbrPk5kwfQ7wBKyQwXPZaGoEwdhotq77UNvp5lGPaxpbHaQqIktX0W9ZlojxEmKSGx2njXkWBfOVzg-xvebb9YbWTj9UnRwt8OBi8fGk8QtxP4SpoaZpVWsX3Nw4H9LXSsTvf50yeDyb8bxAtr3VBGXU0ggo8rgQTAEvEEtRy_0P_ivc3ns-USaoNlyVbeiHcxezS4vmGMPQBaXpKmptfd9Pg5w" />
            </dx:ASPxFileManager>
        </div>
    </form>
</body>
</html>
