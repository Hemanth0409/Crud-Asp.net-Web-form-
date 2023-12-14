<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="State.aspx.cs" Inherits="Crud__Asp.net_Web_form_.State" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center align-items-center">

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
