<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpDetailsCrud.aspx.cs" Inherits="Crud__Asp.net_Web_form_.WebForm1" %>

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
                        <h1 class="mb-3 ms-5 text-center">Employee Details</h1>
                        <div class="row">
                            <div class="row justify-content-center align-items-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Name" class="form-label m-0">Name:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <input type="text" class="form-control float-end" runat="server" id="TxtName">
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="email" class="form-label m-0">Email:</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="email" class="form-control float-end mt-4" runat="server" id="Txtemail">
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="age" class="form-label m-0">Age:</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="number" class="form-control float-end mt-4" runat="server" min="0" id="Txtage">
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="form-group col-md-2 mt-4  text-center">
                                    <label for="Country">Country:</label>
                                </div>

                                <div class="col-md-4 mt-4 ms-4">
                                    <asp:DropDownList class="float-center ms-5" ID="Txtcountry" AutoPostBack="true" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="country_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row justify-content-center">
                                <div class="form-group col-md-2 mt-4 text-center">
                                    <label for="state">State:</label>
                                </div>
                                <div class="col-md-4 mt-4 ms-4">
                                    <asp:DropDownList class="float-center ms-5" ID="Txtstate" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                                        <asp:ListItem>Select a country</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row justify-content-center mt-4">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Contact" class="form-label">Contact:</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="text" class="form-control float-end mt-3" runat="server" id="TxtContact">
                                </div>
                            </div>
                            <div class="row justify-content-center mt-3">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="joinedDate" class="form-label">Joined Date:</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="date" class="form-control float-end mt-3" id="TxtjoinDate" runat="server">
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Gender" class="form-label">Gender:</label>
                                </div>
                                <div class="col-md-4 mt-4 ms-4">
                                    <div class="float-center ms-5">
                                        <asp:RadioButton asp-for="gender" ID="RadioMale" Text="Male" runat="server" GroupName="Gender" />
                                        <asp:RadioButton ID="RadioFemale" Text="Female" runat="server" GroupName="Gender" />
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
                                <textarea class="form-control float-end" id="TxtAddress" runat="server" rows="5"></textarea>
                            </div>
                        </div>

                        <div class="row justify-content-center align-items-center">
                            <div class="col-md-2 mt-4 text-center">
                                <label for="Name" class="form-label m-0">User Name:</label>
                            </div>

                            <div class="col-md-4 mt-4">
                                <input type="text" class="form-control float-end" runat="server" id="UserName">
                            </div>
                        </div>
                        <div class="row justify-content-center align-items-center">
                            <div class="col-md-2 mt-4 text-center">
                                <label for="password" class="form-label m-0">Password:</label>
                            </div>

                            <div class="col-md-4 mt-4">
                                <input type="password" class="form-control float-end" runat="server" id="Password">

                            </div>
                        </div>

                        <div class="col-12 d-flex justify-content-center gap-3 mt-5">
                            <asp:Button ID="Button1" runat="server" OnClick="Create_Click" Text="Save" class="btn btn-dark fw-bold" />
                            <asp:Button ID="btn" runat="server" Text="Clear" OnClick="Reset_Click" class="btn btn-dark fw-bold" />
                        </div>
                </asp:Panel>
                <asp:Panel ID="ListView" runat="server">
                    <div class="row">
                        <div class="col-12 d-flex  mt-5">
                            <div class="d-flex justify-content-center gap-1 mx-auto">
                                <input type="text" runat="server" id="searchText" class="text-center" autopostback="True">
                                <asp:Button ID="searchButton" OnClick="Search_Click" runat="server" Text="Search" class="btn btn-dark fw-bold" />
                                <asp:Button ID="ClearSearch" runat="server" Text="Clear" OnClick="ResetSearch_Click" class="btn btn-dark fw-bold" />

                            </div>
                            <div class="pe-4">
                                <asp:Button ID="AddEmployeeData" OnClick="AddEmployee" runat="server" Text="Add Employee" class="btn btn-dark fw-bold ms-auto float-end" />

                            </div>
                        </div>
                    </div>
                    <div class="row mt-5 ">
                        <div class="col-12 justify-content-end">
                            <asp:GridView ID="EmpDetails" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowDeleting="RowDeleting" Width="1217px" Style="margin-left: 76px; margin-top: 0px;" OnPageIndexChanging="OnPageIndexChanging" AllowPaging="True" Height="186px" PageSize="5">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Name" Text='<%# Eval("Name") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Email") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Email" Text='<%# Eval("Email") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Contact") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Contact" Text='<%# Eval("Contact") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Age">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Age") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Age" Text='<%# Eval("Age") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Country">
                        <ItemTemplate>

                            <asp:Label Text='<%# Eval("Country") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Country" Text='<%# Eval("Country") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="State">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("State") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="State" Text='<%# Eval("State") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Address") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Address" Text='<%# Eval("Address") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Joined Date">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Joined_Date") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Joined_Date" Text='<%# Eval("Joined_Date") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Gender") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Joined_Date" Text='<%# Eval("Gender") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Language">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Language") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Joined_Date" Text='<%# Eval("Language") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                    <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                    <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact" />
                                    <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                    <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                                    <%-- <asp:BoundField DataField="Joined_Date" HeaderText="Joined Date" SortExpression="Joined_Date" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                    <asp:BoundField DataField="Language" HeaderText="Language" SortExpression="Language" />--%>
                                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />


                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="EditButton" runat="server" Text="Edit" CssClass="btn btn-danger btn-sm" OnClick="EditButton_Click"></asp:Button>
                                            <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="Delete"
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:Button>
                                        </ItemTemplate>
                                        <%--  <EditItemTemplate>
                            <asp:Button ID="Update" runat="server" Text="Update" CssClass="btn btn-danger btn-sm" CommandName="Update"></asp:Button>
                            <asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" CommandName="cancel"></asp:Button>
                        </EditItemTemplate>--%> 
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

