<%@ Page Title="" Language="C#" MasterPageFile="~/color.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- begin #content -->
		<div id="content" class="content">
			<%--<!-- begin breadcrumb -->
			<ol class="breadcrumb pull-right">
				<li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
				<li class="breadcrumb-item"><a href="javascript:;">Page Options</a></li>
				<li class="breadcrumb-item active">Page with Top Menu</li>
			</ol>
			<!-- end breadcrumb -->
			<!-- begin page-header -->
			<h1 class="page-header">Page with Top Menu <small>header small text goes here...</small></h1>
			<!-- end page-header -->--%>
			
			<!-- begin panel -->
			<div class="panel panel-inverse">
				<div class="panel-heading">
					<div class="panel-heading-btn">
						<a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
						<a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-redo"></i></a>
						<a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
						<a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
					</div>
					<h4 class="panel-title">Panel Title here</h4>
				</div>
				<div class="panel-body">

                    <dx:ASPxRibbon ID="ASPxRibbon1" runat="server" Theme="RedWine" EnableTheming="True">
                        <Tabs>
                            <dx:RibbonTab Text="Submittals" Name="Submittals"></dx:RibbonTab>
                            <dx:RibbonTab Text="Project" Name="Project">
                                <Groups>
                                    <dx:RibbonGroup Name="Project Group" Text="Project Group">
                                        <Items>
                                            <dx:RibbonButtonItem Name="Create Project" Text="Create Project">
                                                <SmallImage IconID="diagramicons_palette_svg_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
											   <dx:RibbonButtonItem Name="Update Project" Text="Update Project">
                                                <SmallImage IconID="diagramicons_palette_svg_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                        </Items>
                                    </dx:RibbonGroup>
                                </Groups>
                            </dx:RibbonTab>
                        </Tabs>
                        <Styles>
                          
                            <FileTab BackColor="#0033CC">
                            </FileTab>
                        </Styles>
                    </dx:ASPxRibbon>
					
					Panel Content Here

					<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

					<asp:Button ID="txtdev" runat="server" Text="Button" OnClick="Button1_Click" />

				</div>
			</div>
			<!-- end panel -->
		</div>
		<!-- end #content -->
</asp:Content>
