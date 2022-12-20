<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="TestWebApp.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="horizontal-layout horizontal-menu 2-columns  "data-open="hover" data-menu="horizontal-menu" data-col="2-columns">

 
    <!-- BEGIN: Content-->
    <div class="app-content content">
      <div class="content-overlay"></div>
      <div class="content-wrapper">
        <div class="content-header row">
          <div class="content-header-left col-md-6 col-12 mb-2">
            <h3 class="content-header-title mb-0">Basic Tables</h3>
            <%--<div class="row breadcrumbs-top">
              <div class="breadcrumb-wrapper col-12">
                <ol class="breadcrumb">
                  <li class="breadcrumb-item"><a href="index.html">Home</a>
                  </li>
                  <li class="breadcrumb-item"><a href="#">Tables</a>
                  </li>
                  <li class="breadcrumb-item active">Basic Tables
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
                <h3 class="m-0">$5,668</h3><span class="text-muted">Sales</span>
              </div>
            </div>
          </div>--%>

             <div class="content-header-right col-md-6 col-12 mb-md-0 mb-2">
            <div class="media width-250 float-right">
               <div class="row breadcrumbs-top">
              <div class="breadcrumb-wrapper col-12">
                <ol class="breadcrumb">
                  <li class="breadcrumb-item"><a href="index.html">Home</a>
                  </li>
                  <li class="breadcrumb-item"><a href="#">Tables</a>
                  </li>
                  <li class="breadcrumb-item active">Basic Tables
                  </li>
                </ol>
              </div>
            </div>
              
            </div>
          </div>
             
        </div>
        <div class="content-body"><!-- Basic Tables start -->
<div class="row">
    <div class="col-12">
        <div class="card">            
            <div class="card-content collapse show">
                <div class="card-body">
                    
                    <p><span class="text-bold-600">Example 1:</span> Table with outer spacing</p>
                    <div class="table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Username</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th scope="row">1</th>
                                    <td>Mark</td>
                                    <td>Otto</td>
                                    <td>@mdo</td>
                                </tr>
                                <tr>
                                    <th scope="row">2</th>
                                    <td>Jacob</td>
                                    <td>Thornton</td>
                                    <td>@fat</td>
                                </tr>
                                <tr>
                                    <th scope="row">3</th>
                                    <td>Larry</td>
                                    <td>the Bird</td>
                                    <td>@twitter</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>               
                
            </div>
        </div>
    </div>
</div>
<!-- Basic Tables end -->






        </div>
      </div>
    </div>
    <!-- END: Content-->  
   </div>
</asp:Content>
