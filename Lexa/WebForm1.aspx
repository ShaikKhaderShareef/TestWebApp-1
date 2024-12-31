<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Lexa.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />
    <!-- BEGIN: Content-->
    <div class="pcoded-wrapper">
        <div class="pcoded-content">
            <div class="pcoded-inner-content">

                <!-- Main-body start -->
                <div class="main-body">
                    <div class="page-wrapper">
                        <!-- Page-header start -->
                        <div class="page-header m-t-50">
                            <div class="row align-items-end">
                                <div class="col-lg-8">
                                    <div class="page-header-title">
                                        <div class="d-inline">
                                            <h5>Horizontal fixed Layout</h5>
                                            <%--    <span>lorem ipsum dolor sit amet, consectetur adipisicing elit</span>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="page-header-breadcrumb">
                                        <ul class="breadcrumb-title">
                                            <li class="breadcrumb-item">
                                                <a href="https://colorlib.com/polygon/adminty/default/index.html">
                                                    <i class="icofont icofont-home"></i>
                                                </a>
                                            </li>
                                            <li class="breadcrumb-item"><a href="#!">Page Layouts</a>
                                            </li>
                                            <li class="breadcrumb-item"><a href="#!">Horizontal</a>
                                            </li>
                                            <li class="breadcrumb-item"><a href="#!">fixed Layout</a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Page-header end -->
                        <!-- Page body start -->
                        <div class="page-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <!-- Default card start -->


                                    <%--<div class="card">
                                                    <div class="card-header">
                                                        <h5>HTML Markup</h5>

                                                    </div>
                                                    <div class="card-block">
                                                        <p>Use the following code to use <strong>Horizontal Fixed Layout</strong> in horizontal.</p>
                                                        <h6 class="m-t-20 f-w-600">Usage:</h6>
                                                        <pre>
                                                            <code class="language-markup">
                                                                $( document ).ready(function() {
                                                                    $( "#pcoded" ).pcodedmenu({
                                                                        themelayout: 'horizontal',
                                                                        FixedNavbarPosition: true,
                                                                        FixedHeaderPosition: false,
                                                                    });
                                                                });
                                                            </code>
                                                        </pre>
                                                    </div>
                                                </div>--%>

                                    <div class="card">
                                        <div class="card-header">
                                            <h5>Sample Block</h5>
                                        </div>
                                        <div class="card-block">
                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" OnClick="ASPxButton1_Click"></dx:ASPxButton>
                                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
                                            <p>Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from "de Finibus Bonorum et Malorum" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.</p>
                                        </div>
                                    </div>
                                    <!-- Default card end -->
                                </div>
                            </div>
                        </div>
                        <!-- Page body end -->
                    </div>
                </div>
                <!-- Main-body end -->
                <div id="styleSelector">
                </div>
            </div>
        </div>
    </div>
    <!-- END: Content-->
</asp:Content>
