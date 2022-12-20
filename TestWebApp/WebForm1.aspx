<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TestWebApp.WebForm1" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<%@ Register assembly="DevExpress.Dashboard.v19.2.Web.WebForms, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.DashboardWeb" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v19.2.Web, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web.Designer" tagprefix="dxchartdesigner" %>
<%@ Register assembly="DevExpress.XtraCharts.v19.2.Web, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EstimationNewConnectionString %>" SelectCommand="SELECT * FROM [ArchitectDrawingItems]"></asp:SqlDataSource>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <br />
            <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource2" Height="336px" Width="793px">
                <series>
                    <asp:Series Name="Series1" XValueMember="status" YValueMembers="Count">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EstimationNewConnectionString2 %>" SelectCommand="SELECT count(status) as Count, isnull(sum(poamount), '0.00') as sum, [status], CITY FROM [newproject] WHERE CITY = 'Riyadh' and status <> 'UNDEFINED' GROUP BY STATUS, CITY "></asp:SqlDataSource>
            <br />
            <br />
            <dx:WebChartControl ID="WebChartControl1" runat="server" CrosshairEnabled="True" DataSourceID="SqlDataSource2" Height="405px" Width="378px" AutoLayout="True">
                <BorderOptions Visibility="True" />
                <DiagramSerializable>
                    <dx:XYDiagram>
                        <AxisX VisibleInPanesSerializable="-1">
                        </AxisX>
                        <AxisY VisibleInPanesSerializable="-1">
                        </AxisY>
                    </dx:XYDiagram>
                </DiagramSerializable>
<Legend Name="Default Legend"></Legend>
                <SeriesSerializable>
                    <dx:Series ArgumentDataMember="status" LabelsVisibility="True" Name="Series 2" ToolTipHintDataMember="Count" ValueDataMembersSerializable="Count" ColorDataMember="sum">
                        <ViewSerializable>
                            <dx:StackedBarSeriesView ColorEach="True" BarWidth="0.7">
                                <Border Visibility="True" />
                            </dx:StackedBarSeriesView>
                        </ViewSerializable>
                    </dx:Series>
                    <dx:Series ArgumentDataMember="status" Name="Series 1" ToolTipHintDataMember="sum" ValueDataMembersSerializable="sum">
                        <ViewSerializable>
                            <dx:StackedBarSeriesView ColorEach="True">
                            </dx:StackedBarSeriesView>
                        </ViewSerializable>
                    </dx:Series>
                </SeriesSerializable>
                <Titles>
                    <dx:ChartTitle Text="Project Dashboard" />
                </Titles>
            </dx:WebChartControl>
            <br />
            <dx:ASPxButton ID="ASPxButton1" runat="server" OnClick="ASPxButton1_Click" Text="ASPxButton">
            </dx:ASPxButton>
            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
            </dx:ASPxTextBox>
            <br />
            <dx:ASPxFileManager ID="ASPxFileManager1" runat="server">
                <Settings RootFolder="~/" ThumbnailFolder="~/Thumb/" />
            </dx:ASPxFileManager>
            <asp:Panel ID="filesError" runat="server">
                <div class="alert alert-danger">
                    Files not Exists in this folder.!!!
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
