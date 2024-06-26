﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicPage.aspx.cs" Inherits="Crud__Asp.net_Web_form_.DynamicPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <title></title>
</head>
<body>
    <form id="DynamicPageID" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="script" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepannel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" Visible="true">
                    <div class="container my-5 d-flex flex-column justify-content-center">
                        <h3 class="mb-3 ms-5 text-center">Dynamic Form</h3>
                    </div>
                </asp:Panel>
                <asp:Panel ID="formViewId" runat="server">
                    <div id="buttonDisplayId">
                        <asp:Button ID="btnAddData" OnClick="AddDataButton_Click" runat="server" Text="Add" class="btn btn-dark me-2 ms-auto float-end" />
                        <asp:Button ID="btnOpenId" OnClick="EditButton_Click" runat="server" Text="Open" class="btn btn-dark me-2 ms-auto float-end" />
                        <asp:Button ID="btnDeleteId" OnClick="DeleteClick" runat="server" Text="Delete" class="btn btn-dark me-2 ms-auto float-end" />
                        <asp:Button ID="btnDeleteAllId" OnClick="DeleteAllClick" runat="server" Text="Delete All" class="btn me-2 btn-dark  ms-auto float-end" />
                    </div>
                    <asp:Button ID="SaveBtn" OnClick="SaveButton_Click" runat="server" Text="Save" class="btn btn-dark me-2 ms-auto float-end" />
                    <asp:Label runat="server" ID="ResultLabel"></asp:Label>
                    <asp:Label runat="server" ID="lblModuleName"></asp:Label>
                    <br />

                    <asp:GridView ID="ColumnControlData" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" autoFit="true" Style="margin-top: 0px;" AllowPaging="True" Height="186px" PageSize="5">
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="EditButton" runat="server" Text="Edit" CssClass="btn btn-danger btn-sm" OnClick="EditButton_Click"></asp:Button>
                                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="Delete"
                                        OnClientClick="return confirm('Are you sure you want to delete this record?');" OnClick="DeleteClick"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel runat="server" ID="FilePanel">
            <asp:CheckBox ID="SelectAllCheckBox" runat="server" Text="Select All" AutoPostBack="true" OnCheckedChanged="SelectAllCheckBox_CheckedChanged" />

            <asp:CheckBoxList ID="ColumnNameCheckBoxList" runat="server" BorderStyle="Solid"></asp:CheckBoxList>
            <asp:DropDownList runat="server" ID="FileFormat">
                <asp:ListItem Value="Excel">EXCEL</asp:ListItem>
                <asp:ListItem Value="Pdf">PDF</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btn_ExportId" Text="Export" OnClick="Export_Click" runat="server" />
            <%--<asp:Button ID="btn_PrintId" Text="Print" runat="server" OnClick="Print_Click" />--%>
        </asp:Panel>

    </form>
</body>
</html>
