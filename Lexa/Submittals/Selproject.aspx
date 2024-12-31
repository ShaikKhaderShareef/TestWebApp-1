<%@ Page Title="" Language="C#" MasterPageFile="~/Submittals/Submittals.Master" AutoEventWireup="true" CodeBehind="Selproject.aspx.cs" Inherits="Lexa.Submittals.Selproject" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

 <!-- BEGIN: Content-->
    <div class="app-content content">
        <div class="content-overlay"></div>
        <div class="content-wrapper">
            <div class="content-header row">
                <div class="content-header-left col-md-6 col-12 mb-2">
                    <h3 class="content-header-title mb-0">Home Page</h3>
                    <%--<div class="row breadcrumbs-top">
                        <div class="breadcrumb-wrapper col-12">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="index.html">Home</a>
                                </li>
                                <li class="breadcrumb-item"><a href="#">Page</a>
                                </li>
                                <li class="breadcrumb-item active">Form Wizard
                                </li>
                            </ol>
                        </div>
                    </div>--%>
                </div>
                <%--<div class="content-header-right col-md-6 col-12 mb-md-0 mb-2">
                    <div class="media width-250 float-right">
                        <div class="media-left media-middle">
                            <div id="sp-bar-total-sales"></div>
                        </div>
                        <div class="media-body media-right text-right">
                            <h3 class="m-0">$5,668</h3>
                            <span class="text-muted">Sales</span>
                        </div>
                    </div>
                </div>--%>
                 <div class="content-header-right col-md-6 col-12 mb-md-0 mb-2">
                    <div class="media width-250 float-right">
                        <%--<div class="media-left media-middle">
                            <div id="sp-bar-total-sales"></div>
                        </div>--%>
                        <%--<div class="media-body media-right text-right">
                            <h3 class="m-0">$5,668</h3>
                            <span class="text-muted">Sales</span>
                        </div>--%>
                           <div class="row breadcrumbs-top">
                        <div class="breadcrumb-wrapper col-12">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="index.html">Home</a>
                                </li>
                                <li class="breadcrumb-item"><a href="#">Page</a>
                                </li>
                                <li class="breadcrumb-item active">Home Page
                                </li>
                            </ol>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
            <div class="content-body">
                <!-- Form wizard with number tabs section start -->
                <section id="number-tabs">
                    <div class="row">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-header">
                                 <%--   <h4 class="card-title">Home Page</h4>--%>
                                    <a class="heading-elements-toggle"><i class="fa fa-ellipsis-h font-medium-3"></i></a>
                                    <div class="heading-elements">
                                     <%--   <ul class="list-inline mb-0">
                                            <li><a data-action="collapse"><i class="feather icon-minus"></i></a></li>
                                            <li><a data-action="reload"><i class="feather icon-rotate-cw"></i></a></li>
                                            <li><a data-action="expand"><i class="feather icon-maximize"></i></a></li>
                                            <li><a data-action="close"><i class="feather icon-x"></i></a></li>
                                        </ul>--%>
                                    </div>
                                </div>
                                <div class="card-content collapse show">
                                    <div class="card-body">

                                            <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2"
                            ColumnCount="2" Theme="MetropolisBlue" Font-Names="Calibri Light" Font-Size="Small">
                            <Items>
                                <dx:LayoutGroup Caption="Select Project" ColCount="2" ColumnCount="2" ColSpan="2" ColumnSpan="2">
                                    <Items>
                                        <dx:LayoutItem Caption="" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxGridLookup runat="server" KeyFieldName="projectid" DataSourceID="ProjSqlDataSource2" AutoGenerateColumns="False" NullText="SELECT PROJECT" Width="450px" Height="25px" Caption="Project ID" Theme="MetropolisBlue" EnableTheming="True" Font-Names="Calibri Light" Font-Size="Small" ID="cmbprojectid" OnTextChanged="cmbprojectid_TextChanged">
                                                        <GridViewProperties>
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

                                                            <SettingsPopup>
                                                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                            </SettingsPopup>
                                                        </GridViewProperties>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="projectid" ShowInCustomizationForm="True" Caption="PROJECT ID" VisibleIndex="0"></dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="projectname" ShowInCustomizationForm="True" Caption="PROJECT NAME" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridLookup>

                                                    <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:EstimationNewConnectionString %>" SelectCommand="SELECT DISTINCT newproject.projectid, newproject.projectname FROM  tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.type = 'CONTRACTOR') AND (tblprojectkey.empid = 1136) and newproject.companyid = 1" ID="ProjSqlDataSource2">
                                                        <SelectParameters>
                                                            <asp:SessionParameter SessionField="empid" Name="empid"></asp:SessionParameter>
                                                            <asp:SessionParameter SessionField="companyid" Name="companyid"></asp:SessionParameter>
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxButton runat="server" Text="UPDATE" Theme="MetropolisBlue" Width="150px" Height="25px" ID="ASPxFormLayout1_E6" OnClick="ASPxFormLayout1_E6_Click">
                                                        <ClientSideEvents Click="function(s, e) {
                                                                                Callback.PerformCallback();
                                                                                LoadingPanel.Show();
                                                                                }" />
                                                    </dx:ASPxButton>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>

                        <hr />

                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2" DataSourceID="SelectedSqlDataSource1" ColumnCount="2" Font-Names="Segoe UI" Font-Size="Small" Width="100%" Theme="Glass">
                            <Items>
                                <dx:LayoutGroup Caption="Selected Project" ColCount="2" ColumnCount="2" ColSpan="2" ColumnSpan="2">
                                    <Items>
                                        <dx:LayoutItem Caption="ProjectID" FieldName="projectid" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" Width="100%" ReadOnly="True" Theme="Glass" Font-Names="Segoe UI" Font-Size="Small" ID="txtprojectid1"></dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Project Name" FieldName="projectname" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" Width="100%" ReadOnly="True" Theme="Glass" Font-Names="Segoe UI" Font-Size="Small" ID="txtprojectname1"></dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Client Name" FieldName="clientname" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" Width="100%" ReadOnly="True" Theme="Glass" Font-Names="Segoe UI" Font-Size="Small" ID="ASPxFormLayout1_E1"></dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Consultant Name" FieldName="consultantname" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" Width="100%" ReadOnly="True" Theme="Glass" Font-Names="Segoe UI" Font-Size="Small" ID="ASPxFormLayout1_E2"></dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Location" FieldName="projectlocation" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" Width="100%" ReadOnly="True" Theme="Glass" Font-Names="Segoe UI" Font-Size="Small" ID="txtlocation1"></dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Project Manager" FieldName="projectmanager" ColSpan="1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox runat="server" Width="100%" ReadOnly="True" Theme="Glass" Font-Names="Segoe UI" Font-Size="Small" ID="ASPxFormLayout1_E3"></dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>

                        <asp:SqlDataSource runat="server" ID="SelectedSqlDataSource1" ConnectionString='<%$ ConnectionStrings:EstimationConnectionString %>' SelectCommand="SELECT  tblprojectselect.empid, tblprojectselect.projectid, newproject.projectname, newproject.projectlocation, newproject.clientname, newproject.consultantname, newproject.projectmanager  FROM tblprojectselect INNER JOIN newproject ON tblprojectselect.projectid = newproject.projectid where  tblprojectselect.empid=@empid">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="empid" Name="empid"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>

                        <asp:Panel ID="SuccessProject" runat="server">
                            <div class="alert alert-success background-success">
                                <%--<button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <i class="icofont icofont-close-line-circled text-white"></i>
                                    </button>--%>
                                <strong>Success!</strong>  <code>Project Updated Successfully </code>
                            </div>
                        </asp:Panel>
                        <asp:SqlDataSource ID="CompanySqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:LogonServices %>' SelectCommand="SELECT * FROM [tblcompany]"></asp:SqlDataSource>
                        <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>

                        <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="TextBox4" runat="server" Visible="False"></asp:TextBox>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- Form wizard with number tabs section end -->


            </div>
        </div>
    </div>
    <!-- END: Content-->         

            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True" Theme="Moderno">
            </dx:ASPxLoadingPanel>
            <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback">
                <ClientSideEvents CallbackComplete="function(s, e) { LoadingPanel.Hide(); }" />
            </dx:ASPxCallback>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
