<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EmpDetailsCrud.aspx.cs" Inherits="Crud__Asp.net_Web_form_.WebForm1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style>
        body {
            background: #fafbfb;
        }

        .error {
            color: Red
        }
    </style>
    <div class="container my-5 d-flex flex-column justify-content-center">

        <h1 class="mb-3 ms-5">Employee Details</h1>
        <div class="row">
            <div class="row justify-content-center align-items-center">
                <div class="col-md-2 mt-4 text-center">
                    <label for="Name" class="form-label m-0">Name:</label>
                </div>

                <div class="col-md-4 mt-4">
                    <input type="text" class="form-control float-end" runat="server" id="TxtName">
                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="TxtName" class="error" ErrorMessage="***Name field Required***" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TxtName" class="error" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic" ErrorMessage="***Name Should contain only Alphabets***"></asp:RegularExpressionValidator>
                    --%>
                </div>
            </div>
            <div class="row justify-content-center align-items-center">
                <div class="col-md-2 mt-4 text-center">
                    <label for="email" class="form-label m-0">Email:</label>
                </div>
                <div class="col-md-4">
                    <input type="email" class="form-control float-end mt-4" runat="server" id="Txtemail">
                    <%--     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="Txtemail" ErrorMessage="***Email field Required***" class="error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="Txtemail" class="error" ValidationExpression="^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$" Display="Dynamic" ErrorMessage="***Email Required in a correct formate***"></asp:RegularExpressionValidator>
                    --%>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="col-md-2 mt-4 text-center">
                    <label for="age" class="form-label m-0">Age:</label>
                </div>
                <div class="col-md-4">
                    <input type="number" class="form-control float-end mt-4" runat="server" min="0" id="Txtage">
                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="Txtage" ErrorMessage="***Age field Required***" class="error" Display="Dynamic"></asp:RequiredFieldValidator>--%>
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
                    <%--     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="TxtContact" ErrorMessage="***Contact field Required***" class="error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ControlToValidate="TxtContact" class="error" ValidationExpression="^[0-9]+$" Display="Dynamic" ErrorMessage="***Contact should contain only numbers***"></asp:RegularExpressionValidator>
                    --%>
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
                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="TxtAddress" ErrorMessage="***Address field Required***" class="error" Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>

    <div class="col-12 d-flex justify-content-center gap-3 mt-5">
        <asp:Button ID="Button1" runat="server" OnClick="Create_Click" Text="Save" class="btn btn-dark fw-bold" />
        <asp:Button ID="btn" runat="server" Text="Clear" OnClick="Reset_Click" class="btn btn-dark fw-bold" />
    </div>



    <div class="row">
        <div class="col-12 d-flex justify-content-center mt-5">
            <div class="d-flex gap-1 align-items-center">
                <input type="text" runat="server" id="searchText" class="">
                <asp:Button ID="searchButton" OnClick="Search_Click" runat="server" Text="Search" class="btn btn-dark fw-bold" />
            </div>
        </div>
    </div>
    <div class="row mt-5 ">

        <div class="col-12 justify-content-end">

            <asp:GridView ID="EmpDetails" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowDeleting="RowDeleting" OnRowEditing="Edit"
                Width="1217px" Style="margin-left: 63px">
                <%--OnRowCancelingEdit="CancelingEditedRow"--%>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
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
                    </asp:TemplateField>
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


</asp:Content>

