<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleRights.aspx.cs" Inherits="Crud__Asp.net_Web_form_.ModuleRights" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Module Rights</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepannel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="ListView" runat="server" Visible="true">
                    <div class="row mt-5 ">
                        <div class="col-12 justify-content-end">
                            <asp:GridView ID="ModuleRightsGridView" runat="server" AutoGenerateColumns="false"
                                Width="600px" CssClass="Grid" PageSize="8" OnRowDataBound="ModuleRightsGridView_RowDataBound">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridRow" />
                                <AlternatingRowStyle CssClass="AlternateGridRow" />
                                <PagerStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Clients">
                                        <ItemTemplate>
                                            <asp:Label ID="ClientIdLabel" runat="server" Text='<%# Bind("Id")%>' CssClass="Hide">  
                                            </asp:Label>
                                            <asp:Label ID="ClientNameLabel" runat="server" Text='<% #Bind("Name")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="AlignLeft" Width="200px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <input id="ModuleCount" type="hidden" runat="server" style="width: 50px" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
