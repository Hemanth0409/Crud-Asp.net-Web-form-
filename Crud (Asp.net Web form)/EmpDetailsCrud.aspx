txtUserName<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpDetailsCrud.aspx.cs" Inherits="Crud__Asp.net_Web_form_.WebForm1" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html>
<head runat="server">
    <title>Employee Detail's</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript">
        function AssignSourceToIframe() {
            var hdnvalue = document.getElementById('<%= hdnPropertyAttributeIframe.ClientID %>').value;   
            document.getElementById("PropertyAttributeIframe").src = hdnvalue;
        }
    </script>
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
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="script" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepannel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="formViewId" runat="server">
                    <div class="container my-5 d-flex flex-column justify-content-center">txtUserName
                        <h1 class="mb-3 ms-5 text-center">Employee Details</h1>
                        <div class="row">
                            <div class="row justify-content-center align-items-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Name" class="form-label m-0"><span class="RequiredField">*</span>Name:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <input type="text" class="form-control float-end" runat="server" id="txtName">
                                </div>
                            </div>

                            <div class="row justify-content-center align-items-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="email" class="form-label m-0"><span class="RequiredField">*</span>Email:</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="text" class="form-control float-end mt-4" runat="server" id="txtEmail">
                                </div>
                            </div>

                            <div class="row justify-content-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="age" class="form-label m-0">Age:</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="text" class="form-control float-end mt-4" runat="server" min="0" id="txtAge">
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="form-group col-md-2 mt-4  text-center">
                                    <label for="Country">Country:</label>

                                </div>

                                <div class="col-md-4 mt-4 ms-4">
                                    <asp:DropDownList class="float-center ms-5" ID="txtCountry" AutoPostBack="true" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="country_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="CountryLinkButton" CssClass="PropertyAdd" runat="server"
                                        Text="[+]" OnClick="CountryLinkButton_Click"></asp:LinkButton>
                                </div>
                            </div>

                            <div class="row justify-content-center">
                                <div class="form-group col-md-2 mt-4 text-center">
                                    <label for="state">State:</label>
                                </div>
                                <div class="col-md-4 mt-4 ms-4">
                                    <asp:DropDownList class="float-center ms-5" ID="txtState" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                                        <asp:ListItem>Select a country</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="StateLinkButton" CssClass="PropertyAdd" runat="server"
                                        Text="[+]" OnClick="StateLinkButton_Click"></asp:LinkButton>
                                </div>
                            </div>
                            <div class="row justify-content-center mt-4">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Contact" class="form-label">Contact:</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="text" class="form-control float-end mt-3" runat="server" id="txtContact">
                                </div>
                            </div>
                            <div class="row justify-content-center mt-3">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="joinedDate" class="form-label">Joined Date:</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="date" class="form-control float-end mt-3" id="txtJoinDate" runat="server">
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Gender" class="form-label">Gender:</label>
                                </div>
                                <div class="col-md-4 mt-4 ms-4">
                                    <div class="float-center ms-5">
                                        <asp:RadioButton asp-for="gender" ID="rdoMale" Text="Male" runat="server" GroupName="Gender" />
                                        <asp:RadioButton ID="rdoFemale" Text="Female" runat="server" GroupName="Gender" />
                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-center mt-4">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Language" class="form-label">Language's Known:</label>
                                </div>
                                <div class="col-md-4 ms-5">
                                    <div class="ms-5">
                                        <asp:CheckBoxList ID="Language" runat="server">
                                            <asp:ListItem Value="Tamil" Text="Tamil">&nbsp;Tamil</asp:ListItem>
                                            <asp:ListItem Value="English" Text="English">&nbsp;English</asp:ListItem>
                                            <asp:ListItem Value="French" Text="French">&nbsp; French</asp:ListItem>
                                            <asp:ListItem Value="German" Text="German">&nbsp; German</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-center mt-5">

                            <div class="col-md-2 mt-5 text-center">
                                <label for="Address" class="form-label">Address:</label>
                            </div>
                            <div class="col-md-4 ">
                                <textarea class="form-control float-end" id="txtAddress" runat="server" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row justify-content-center align-items-center">
                            <div class="col-md-2 mt-4 text-center">
                                <label for="Name" class="form-label m-0"><span class="RequiredField">*</span>User Name:</label>
                            </div>

                            <div class="col-md-4 mt-4">
                                <input type="text" class="form-control float-end" runat="server" id="txtUserName">
                            </div>
                        </div>
                        <div class="row justify-content-center align-items-center">
                            <div class="col-md-2 mt-4 text-center">
                                <label for="password" class="form-label m-0"><span class="RequiredField">*</span>Password:</label>
                            </div>
                            <div class="col-md-4 mt-4">
                                <input type="text" class="form-control float-end" runat="server" id="txtPassword">
                            </div>
                        </div>
                        <div class="col-12 d-flex justify-content-center gap-3 mt-5">
                            <asp:Button ID="btnSave" runat="server" OnClick="Create_Click" Text="Save" class="btn btn-dark fw-bold" />
                            <asp:Button ID="btnResetClick" runat="server" Text="Cancel" OnClick="Reset_Click" class="btn btn-dark fw-bold" />
                        </div>
                </asp:Panel>
                <asp:Panel ID="PropertyAttributePanel" runat="server" Width="550" Height="385" CssClass="PopUpPanel">
                    <table cellpadding="0" cellspacing="0" class="PanelTable">
                        <tr style="height: 25px;">
                            <td class="PropertyAttributeTitle" style="padding-left: 5px; width: 500px;">
                                <asp:Label ID="PropertyAttributeHeaderLabel" runat="server" CssClass="PropertyHeading"
                                    Text="">
                                </asp:Label>
                            </td>
                            <td align="right" style="width: 40px;">
                                <asp:LinkButton ID="PropertyAttributeCancelButton" CssClass="PopUpButton" runat="server"
                                    Height="25" Text="[Close]" />
                            </td>
                        </tr>
                        <tr class="PropertyAttributeFrame">
                            <td colspan="2">
                                <iframe id="PropertyAttributeIframe" width="545" height="355" frameborder="0" src=""></iframe>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:LinkButton ID="PropertyAttributeButton" runat="server"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                <cc1:ModalPopupExtender ID="PropertyAttributeModalPopupExtender" BackgroundCssClass="modal"
                    TargetControlID="PropertyAttributeButton" PopupControlID="PropertyAttributePanel"
                    CancelControlID="PropertyAttributeCancelButton" runat="server">
                </cc1:ModalPopupExtender>
                <asp:HiddenField ID="hdnPropertyAttributeIframe" runat="server"></asp:HiddenField>
                <div class="row" id="searchFilter" runat="server">
                    <div class="col-12 d-flex  mt-5">
                        <div class="d-flex justify-content-center gap-1 mx-auto">
                            <input type="text" runat="server" id="searchText" class="text-center" autopostback="True">
                            <asp:Button ID="searchButton" OnClick="Search_Click" runat="server" Text="Search" class="btn btn-dark fw-bold" />
                            <asp:Button ID="ClearSearch" runat="server" Text="Cancel" OnClick="ResetSearch_Click" class="btn btn-dark fw-bold" />
                        </div>
                        <div class="pe-4" id="AddEmpButton" runat="server">
                            <asp:Button ID="AddEmployeeData" OnClick="AddEmployee" runat="server" Text="Add Employee" class="btn btn-dark fw-bold ms-auto float-end" />
                        </div>
                    </div>
                </div>
                <asp:Panel ID="ListView" runat="server">
                    <div class="row mt-5 ">
                        <div class="col-12 justify-content-end">
                            <asp:GridView ID="EmpDetails" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowDeleting="RowDeleting" autoFit="true" Style="margin-top: 0px;" 
                                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="True" Height="186px" PageSize="5">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"Id")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                    <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                    <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact" />
                                    <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                    <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="EditButton" runat="server" Text="Edit" CssClass="btn btn-danger btn-sm" OnClick="EditButton_Click" ></asp:Button>
                                            <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="Delete"
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>

            </ContentTemplate>

        </asp:UpdatePanel>
        <div class="row">
            <div class="col-12">
                <asp:FileUpload ID="UploadedFile1" runat="server" />
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LoadExcelData" Text="Load Excel Data"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="ExportToExcel">Download Template</asp:LinkButton>
            </div>
        </div>
    </form>

</body>
</html>

