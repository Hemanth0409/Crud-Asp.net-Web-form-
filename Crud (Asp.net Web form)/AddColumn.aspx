<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddColumn.aspx.cs" Inherits="Crud__Asp.net_Web_form_.AddColumn" %>


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
                        <h3 class="mb-3 ms-5 text-center">Dynamic Module</h3>
                        <div class="row">
                            <div class="row justify-content-center align-items-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Name" class="form-label m-0"><span class="RequiredField">*</span>Column Name:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <input type="text" class="form-control float-end" runat="server" id="TxtColumnName" />
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="ColumnType" class="form-label">Column Type:</label>
                                </div>
                                <div class="col-md-4 mt-4 ms-4">
                                    <div class="row float-center ms-5">
                                        <asp:RadioButtonList runat="server" ID="RadioForDisplay" OnSelectedIndexChanged="DisplayView" AutoPostBack="true">
                                            <asp:ListItem ID="RadioSingleLine" Selected="True" Text="Single Line Txt" />
                                            <asp:ListItem ID="RadioMultiLine" Text="Multi Line Txt" />
                                            <asp:ListItem ID="RadioChoice" Text="Choice (menu)" />
                                            <asp:ListItem ID="RadioNumber" Text="Number" />
                                            <asp:ListItem ID="RadioDateTime" Text="Date and time" />
                                            <asp:ListItem ID="RadioCheckBox" Text="Check box (Y/N)" />
                                            <asp:ListItem ID="RadioUploadFile" Text="File Upload" />
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="RequiredfieldView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="RequiredField" class="form-label m-0">Column Required Feild:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <asp:RadioButton asp-for="RequiredField" ID="YesButton" Text="Yes" runat="server" CssClass="m-2" GroupName="RequiredField" />
                                    <asp:RadioButton asp-for="RequiredField" ID="NoButton" Text="No" runat="server" GroupName="RequiredField" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="CharactersView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="Characters" class="form-label m-0">Number of Characters:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <input type="number" id="Characters" value="30" min="1" max="50" runat="server" class="form-control" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="LinesToDisplayView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="Characters" class="form-label m-0">Lines to Display:</label>
                                </div>
                                <div class="col-md-4 mt-4">
                                    <input type="number" id="LinesToDisplay" value="5" min="1" max="10" runat="server" class="form-control" />
                                </div>
                            </div>

                            <div class="row justify-content-center align-items-center" runat="server" id="SperateDataView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DataValue" class="form-label m-0">Type each choice on a separate line:</label>
                                </div>
                                <div class="col-md-4 mt-4">
                                    <textarea class="form-control float-end" id="DataForChoiceTxt" runat="server" rows="5"></textarea>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="ChoiceSelectView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DisplayChoice" class="form-label m-0">Display Type Choice :</label>
                                </div>
                                <div class="row col-md-4 mt-4">
                                    <asp:RadioButton asp-for="RequiredField" ID="RadioButton1" Text="Drop-Down" runat="server" GroupName="DisplayChoice" />
                                    <asp:RadioButton asp-for="RequiredField" ID="RadioButton2" Text="Radio Button" runat="server" GroupName="DisplayChoice" />
                                    <asp:RadioButton asp-for="RequiredField" ID="RadioButton3" Text="Check Box(multiple Select)" runat="server" GroupName="DisplayChoice" />
                                </div>
                            </div>                       

                            <div class="row justify-content-center align-items-center" runat="server" id="ChoiceLineView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DataValue" class="form-label m-0">Type each choice on a separate line:</label>
                                </div>
                                <div class="col-md-4 mt-4">
                                    <textarea class="form-control float-end" id="Textarea1" runat="server" rows="5"></textarea>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="MinMaxValueView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="Characters" class="form-label m-0">Minimum and Maximum Value:</label>
                                </div>
                                <div class="col-md-2 mt-4">
                                    <input type="number" id="Number1" value="1" min="1" runat="server" class="form-control" />
                                </div>
                                <div class="col-md-2 mt-4">
                                    <input type="number" id="Number2" value="100" runat="server" class="form-control" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="DataView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DisplayChoice" class="form-label m-0">Display Date Value:</label>
                                </div>
                                <div class="row col-md-4 mt-4">
                                    <asp:RadioButtonList ID="RadioForDisplayDate" OnSelectedIndexChanged="DisplayDate" runat="server" AutoPostBack="true">
                                        <asp:ListItem Text="None" />
                                        <asp:ListItem Text="CurrentDate" />
                                        <asp:ListItem Text="Enter Date" />

                                    </asp:RadioButtonList>
                                    <div class="col-md-10" runat="server" id="DateField" visible="false">
                                        <input type="date" class="form-control float-end mt-3" id="TxtjoinDate" runat="server">
                                    </div>
                                </div>
                            </div>
                               <div class="row justify-content-center align-items-center" runat="server" id="DefaultTxtView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DefaultTxt" class="form-label m-0">Default Txt:</label>
                                </div>
                                <div class="col-md-4 mt-4">
                                    <input type="text" class="form-control float-end" runat="server" id="DefaultTxt" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="YesNoView">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="Characters" class="form-label m-0">Default Value(Y/N):</label>
                                </div>
                                <div class="col-md-2 mt-4">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>

                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-12 d-flex justify-content-center gap-3 mt-5">
                                <asp:Button ID="Button1" runat="server" OnClick="Create_Click" Text="Save" class="btn btn-dark fw-bold" />
                                <asp:Button ID="btn" runat="server" Text="Cancel" OnClick="Reset_Click" class="btn btn-dark fw-bold" />
                            </div>
                        </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
