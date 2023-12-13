<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Country.aspx.cs" Inherits="Crud__Asp.net_Web_form_.Country" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <title>Country and State </title>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <div class="row justify-content-center">


                <div class="row justify-content-center align-items-center">
                    <div class="col-md-2 mt-4 text-center">
                        <label for="country" class="form-label m-0">Country:</label>
                    </div>

                    <div class="col-md-5 mt-4 ">
                        <input type="text" class="form-control me-2" runat="server" id="InsertCountry" />
                    </div>
                    <div class="text-center">
                        <asp:Button ID="Button1" CssClass="mt-4 btn btn-dark fw-bold mb-4" runat="server" OnClick="InsertCountry_Click" Text="Add Country" />
                    </div>
                </div>
                <hr class="border-4"/>
                <div class="form-group col-md-2 mt-4  text-center">
                    <label for="Country">Country Name:</label>
                </div>

                <div class="col-md-4 mt-4">
                    <asp:DropDownList ID="Txtcountry" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row justify-content-center align-items-center">
                <div class="col-md-2 mt-4 text-center">
                    <label for="country" class="form-label m-0">State Name :</label>
                </div>

                <div class="col-md-4 mt-4">
                    <input type="text" class="form-control " runat="server" id="InsertState" />

                </div>
                <div class="text-center mt-3">
                    <asp:Button ID="Button2" CssClass="btn btn-dark fw-bold" runat="server" OnClick="InsertState_Click" Text="Add State" />
                </div>
            </div>


        </div>
    </form>
</body>
</html>
