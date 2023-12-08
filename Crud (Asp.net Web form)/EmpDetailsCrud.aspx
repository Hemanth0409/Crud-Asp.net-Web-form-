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
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-lg-9">
                <h1 class="mb-3">Employee Details</h1>

                <div class="row g-3">
                    <div class="col-md-2">
                        <label for="Name" class="form-label mt-2">Name:</label>
                    </div>

                    <div class=" col-md-7 mt-4">
                        <input type="text" class="form-control" runat="server" id="TxtName">
                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="TxtName" class="error" ErrorMessage="***Name field Required***" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TxtName" class="error" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic" ErrorMessage="***Name Should contain only Alphabets***"></asp:RegularExpressionValidator>
                    --%></div>
                </div>
                <div class="row g-3">
                    <div class="col-md-2">
                        <label for="email" class="form-label mt-2">Email:</label>
                    </div>
                    <div class=" col-md-7 mt-4">
                        <input type="email" class="form-control" runat="server" id="Txtemail">
                   <%--     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="Txtemail" ErrorMessage="***Email field Required***" class="error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="Txtemail" class="error" ValidationExpression="^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$" Display="Dynamic" ErrorMessage="***Email Required in a correct formate***"></asp:RegularExpressionValidator>
                   --%> </div>
                </div>
                    <div class="row g-3">
                    <div class="col-md-2">
                        <label for="age" class="form-label mt-2">Age:</label>
                    </div>
                    <div class=" col-md-7 mt-4">
                        <input type="number" class="form-control" runat="server" min="0" id="Txtage">
                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="Txtage" ErrorMessage="***Age field Required***" class="error" Display="Dynamic"></asp:RequiredFieldValidator>--%>

                    </div>
                </div>
                <div class="row g-3">
                    <div class="form-group col-md-2">
                        <label for="Country">Country:</label>
                    </div>

                    <div class=" col-md-7 mt-4">
                        <asp:DropDownList ID="Txtcountry"  AutoPostBack="true" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="country_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            
                <div class="row g-3">
                    <div class="form-group col-md-2">
                        <label for="state">State:</label>
                    </div>
                    <div class=" col-md-7 mt-4">
                        <asp:DropDownList ID="Txtstate" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem>Select a country</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row g-3">
                    <div class="col-md-2">
                        <label for="Contact" class="form-label mt-2">Contact:</label>
                    </div>
                    <div class=" col-md-7 mt-4">
                        <input type="text" class="form-control" runat="server" id="TxtContact">
                    <%--    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="TxtContact" ErrorMessage="***Contact field Required***" class="error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ControlToValidate="TxtContact" class="error" ValidationExpression="^[0-9]+$" Display="Dynamic" ErrorMessage="***Contact should contain only numbers***"></asp:RegularExpressionValidator>
                    --%></div>
                </div>
                <div class="row g-3">

                    <div class="col-md-2">
                        <label for="joinedDate" class="form-label mt-2">Joined Date:</label>
                    </div>
                    <div class=" col-md-7 mt-4">
                        <input type="date" class="form-control" id="TxtjoinDate" runat="server" >
                    </div>
                </div>
                <div class="row g-3">

                    <div class="col-md-2">
                        <label for="Address" class="form-label mt-2">Address:</label>
                    </div>
                    <div class=" col-md-7 mt-4">
                        <textarea class="form-control" id="TxtAddress" runat="server" rows="5"></textarea>
                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="TxtAddress" ErrorMessage="***Address field Required***" class="error" Display="Dynamic"></asp:RequiredFieldValidator>--%>

                    </div>
                </div>

                <div class="col-12 mt-5">
                    <asp:Button ID="Button1" runat="server" OnClick="Create_Click" Text="Save" class="btn btn-dark fw-bold" />
                    <asp:Button ID="btn" runat="server" Text="Clear" OnClick="Reset_Click" class="btn btn-dark fw-bold" />
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-1"></div>
        <div class="col-10">
            <input type="text" runat="server" id="searchText" class="auto-style2">
            <asp:Button ID="searchButton" OnClick="Search_Click" runat="server" Text="Search" class="btn btn-dark fw-bold" />
            &nbsp;
        </div>
    </div>
    <div class="row mt-5">
        <div class="col-1"></div>
        <div class="auto-style3">

            <asp:GridView ID="EmpDetails" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowDeleting="RowDeleting" 
             OnRowCancelingEdit="CancelingEditedRow"  Width="1072px">
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
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="EditButton" runat="server" Text="Edit"  CssClass="btn btn-danger btn-sm" OnClick="EditButton_Click"></asp:Button>
                            <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="Delete" 
                                OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:Button>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="Update" runat="server" Text="Update" CssClass="btn btn-danger btn-sm" CommandName="Update"></asp:Button>
                            <asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" CommandName="cancel"></asp:Button>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>


</asp:Content>

