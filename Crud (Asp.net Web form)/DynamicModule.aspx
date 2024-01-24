<%@ Page Language="C#" AutoEventWireup="true" EnableSessionState="True"  CodeBehind="DynamicModule.aspx.cs" Inherits="Crud__Asp.net_Web_form_.Dynamic_Module" %>


<html>
<head runat="server">
    <title>Employee Detail's</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <style>
        body {
            background: #fafbfb;
        }

        .RequiredField {
            color: Red
        }

        .error {
            color: Red
        }
      
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepannel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="formViewId" runat="server">
                    <div class="container my-5 d-flex flex-column justify-content-center">
                        <h1 class="mb-3 ms-5 text-center">Dynamic Module</h1>
                        <div class="row">
                            <div class="row justify-content-center align-items-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Name" class="form-label m-0"><span class="RequiredField">*</span>Module Name:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <input type="text" class="form-control float-end" runat="server" id="TxtModule" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Name" class="form-label m-0">Add for Clients:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </div>
                            </div>
                            <div class="col-12 d-flex justify-content-center gap-3 mt-5">
                                <asp:Button ID="Button1" runat="server" OnClick="Create_Click" Text="Save" class="btn btn-dark fw-bold" />
                                <asp:Button ID="btn" runat="server" Text="Cancel" OnClick="Reset_Click" class="btn btn-dark fw-bold" />
                            </div>
                        </div>
                </asp:Panel>
                <asp:Panel ID="ListView" runat="server">
                    <div class="row mt-5 ">
                        <div class="col-12 justify-content-end">
                            <asp:GridView ID="ModuleData" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowDeleting="RowDeleting" autoFit="true" Style="margin-top: 0px;" OnPageIndexChanging="OnPageIndexChanging" AllowPaging="True" Height="186px" PageSize="5">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ModuleId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ModuleName" HeaderText="Module Name" SortExpression="ModuleName" />
                                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="AddColumn" runat="server" Text="Add" CssClass="btn btn-primary btn-sm" OnClick="ModuleGridView_RowCommand"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="45px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="EditButton" runat="server" Text="Edit" CssClass="btn btn-danger btn-sm" OnClick="EditButton_Click"></asp:Button>
                                             <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="Delete" 
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');" OnClick="DeleteRecord"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
