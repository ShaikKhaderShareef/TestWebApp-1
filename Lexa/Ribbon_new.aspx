<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ribbon_new.aspx.cs" Inherits="Lexa.Ribbon_new" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Persist Active Tab</title>
   <script type="text/javascript">
       // Store the active tab index in sessionStorage
       function storeActiveTabIndex(tabIndex) {
           sessionStorage.setItem('ActiveTabIndex', tabIndex);
       }

       // Retrieve the active tab index from sessionStorage
       function getActiveTabIndex() {
           return sessionStorage.getItem('ActiveTabIndex');
       }

       // Set the active tab index when the page loads
       function setActiveTabOnLoad(ribbon) {
           var activeTabIndex = getActiveTabIndex();
           if (activeTabIndex !== null) {
               ribbon.SetActiveTabIndex(parseInt(activeTabIndex));
           }
       }
</script>
 
</head>
<body>
    <form id="form1" runat="server">
        <div>
         <dx:ASPxRibbon ID="ASPxRibbon1" runat="server" ClientInstanceName="ribbon" Font-Size="Medium">
             <ContextTabCategories>
                 <dx:RibbonContextTabCategory Name="tab1">
                     <Tabs>
                         <dx:RibbonTab Text="Tab1" Name="Tab1">
                             <Groups>
                                 <dx:RibbonGroup Text="Group1" Name="Group1">
                                     <Items>
                                         <dx:RibbonButtonItem Text="text1" Name="text11"></dx:RibbonButtonItem>
                                         <dx:RibbonButtonItem Text="text2" Name="text22"></dx:RibbonButtonItem>
                                     </Items>
                                 </dx:RibbonGroup>
                             </Groups>
                         </dx:RibbonTab>
                         <dx:RibbonTab Text="Tab2" Name="Tab2">
                             <Groups>
                                 <dx:RibbonGroup Text="Group2" Name="Group2">
                                     <Items>
                                         <dx:RibbonButtonItem Text="text21" Name="text21"></dx:RibbonButtonItem>
                                         <dx:RibbonButtonItem Text="text22" Name="text22"></dx:RibbonButtonItem>
                                     </Items>
                                 </dx:RibbonGroup>
                             </Groups>
                         </dx:RibbonTab>
                     </Tabs>
                 </dx:RibbonContextTabCategory>
             </ContextTabCategories>

             <ClientSideEvents ActiveTabChanged="function(s, e) { storeActiveTabIndex(e.tab.index); }" />    
   
</dx:ASPxRibbon>

        </div>
    </form>
     <script type="text/javascript">
         // Set the active tab when the page loads
         setActiveTabOnLoad(ribbon);
    </script>
</body>
</html>
