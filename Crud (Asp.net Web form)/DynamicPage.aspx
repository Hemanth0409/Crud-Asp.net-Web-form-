<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicPage.aspx.cs" Inherits="Crud__Asp.net_Web_form_.DynamicPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="DynamicPageID" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="script" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepannel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="formViewId" runat="server">

                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
