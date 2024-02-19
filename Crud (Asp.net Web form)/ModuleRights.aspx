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

                            <asp:GridView ID="ModuleRightsGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"  autoFit="true" Style="margin-top: 0px;" AllowPaging="True" Height="186px" PageSize="5">
                                <Columns>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("EmployeeId") %>' />
                                            <%# Eval("EmployeeName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                            <div class="col-12 d-flex justify-content-center gap-3 mt-5">
                                <asp:Button ID="SaveClick" runat="server" OnClick="SaveModuleRights" Text="Save" class="btn btn-dark fw-bold" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <input id="EmpCount" type="hidden" runat="server" style="width: 50px" />
                <input id="ModuleCount" type="hidden" runat="server" style="width: 50px" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>