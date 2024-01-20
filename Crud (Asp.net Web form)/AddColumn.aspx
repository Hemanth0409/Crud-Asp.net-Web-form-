<%@ Page Language="C#" AutoEventWireup="true" EnableSessionState="True" CodeBehind="AddColumn.aspx.cs" Inherits="Crud__Asp.net_Web_form_.AddColumn" %>

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
                <asp:Panel ID="formViewId" runat="server" Visible="true">
                    <div class="container my-5 d-flex flex-column justify-content-center">
                        <h3 class="mb-3 ms-5 text-center">Dynamic Module(Column)</h3>
                        <div class="row">
                            <div class="row justify-content-center align-items-center" runat="server" visible="true">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="Name" class="form-label m-0"><span class="RequiredField">*</span>Column Name:</label>
                                </div>
                                <div class="col-md-4 mt-4">
                                    <input type="text" class="form-control float-end" runat="server" id="TxtColumnName" />
                                </div>
                            </div>
                            <div class="row justify-content-center" runat="server" visible="true">
                                <div class="col-md-2 mt-4 text-center">
                                    <label for="ColumnType" class="form-label">Column Type:</label>
                                </div>
                                <div class="col-md-4 mt-4 ms-4">
                                    <div class="row float-center ms-5">
                                        <asp:RadioButtonList runat="server" ID="RadioBtnIdForDisplay" OnSelectedIndexChanged="DisplayView" AutoPostBack="true">
                                            <asp:ListItem ID="RadioSingleLine" Value="0" Selected="True" Text="Single Line Txt" />
                                            <asp:ListItem ID="RadioMultiLine" Value="1" Text="Multi Line Txt" />
                                            <asp:ListItem ID="RadioChoice" Value="2" Text="Choice (menu)" />
                                            <asp:ListItem ID="RadioNumber" Value="3" Text="Number" />
                                            <asp:ListItem ID="RadioDateTime" Value="4" Text="Date and time" />
                                            <asp:ListItem ID="RadioCheckBox" Value="5" Text="Check box (Y/N)" />
                                            <asp:ListItem ID="RadioUploadFile" Value="6" Text="File Upload" />
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="Name" class="form-label m-0">Display the Column :</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <asp:CheckBox ID="IsActive" runat="server" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="RequiredfieldView" visible="true">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="RequiredField" class="form-label m-0">Column Required Feild:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <asp:RadioButton asp-for="RequiredField" ID="YesButton" Text="Yes" runat="server" CssClass="m-2" GroupName="RequiredField" />
                                    <asp:RadioButton asp-for="RequiredField" ID="NoButton" Text="No" runat="server" GroupName="RequiredField" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="CharactersView" visible="true">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="Characters" class="form-label m-0">Number of Characters:</label>
                                </div>

                                <div class="col-md-4 mt-4">
                                    <input type="number" id="Characters" min="1" max="50" runat="server" class="form-control" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="LinesToDisplayView" visible="false">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="LinesToDisplay" class="form-label m-0">Lines to Display:</label>
                                </div>
                                <div class="col-md-4 mt-4">
                                    <input type="number" id="LinesToDisplay" min="1" max="10" runat="server" class="form-control" />
                                </div>
                            </div>

                            <div class="row justify-content-center align-items-center" runat="server" id="SperateDataView" visible="false">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DataValue" class="form-label m-0">Type each choice on a Comma seperated Value:</label>
                                    <span class="text-danger">Example:Data1,Data2 </span>
                                </div>
                                <div class="col-md-4 mt-4">
                                    <textarea class="form-control float-end" id="DataForChoiceTxt" runat="server" rows="5"></textarea>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="ChoiceSelectView" visible="false">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DisplayChoice" class="form-label m-0">Display Type Choice :</label>
                                </div>
                                <div class="row col-md-4 mt-4">
                                    <asp:RadioButton asp-for="RequiredField" ID="RadioButton1" Text="Drop-Down" runat="server" GroupName="DisplayChoice" />
                                    <asp:RadioButton asp-for="RequiredField" ID="RadioButton2" Text="Radio Button" runat="server" GroupName="DisplayChoice" />
                                    <asp:RadioButton asp-for="RequiredField" ID="RadioButton3" Text="Check Box(multiple Select)" runat="server" GroupName="DisplayChoice" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="MinMaxValueView" visible="false">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="Characters" class="form-label m-0">Minimum and Maximum Value:</label>
                                </div>
                                <div class="col-md-2 mt-4">
                                    <input type="number" id="txtMin" runat="server" class="form-control" />
                                </div>
                                <div class="col-md-2 mt-4">
                                    <input type="number" id="txtMax" runat="server" class="form-control" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="DataView" visible="false">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DisplayChoice" class="form-label m-0">Display Date Value:</label>
                                </div>
                                <div class="row col-md-4 mt-4">
                                    <asp:RadioButtonList ID="RadioForDisplayDate" OnSelectedIndexChanged="DisplayDate" runat="server" AutoPostBack="true">
                                        <asp:ListItem Text="None" Value="0" />
                                        <asp:ListItem Text="CurrentDate" Value="1" />
                                        <asp:ListItem Text="Enter Date" Value="2" />
                                    </asp:RadioButtonList>
                                    <div class="col-md-10" runat="server" id="DateField" visible="false">
                                        <input type="date" class="form-control float-end mt-3" id="TxtjoinDate" runat="server">
                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="DefaultTxtView" visible="true">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="DefaultTxt" class="form-label m-0">Default Txt:</label>
                                </div>
                                <div class="col-md-4 mt-4">
                                    <input type="text" class="form-control float-end" runat="server" id="DefaultTxt" />
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center" runat="server" id="YesNoView" visible="false">
                                <div class="col-md-4 mt-4 text-center">
                                    <label for="Characters" class="form-label m-0">Default Value(Y/N):</label>
                                </div>
                                <div class="col-md-2 mt-4">
                                    <asp:DropDownList ID="DefaultValue" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-12 d-flex justify-content-center gap-3 mt-5">
                                <asp:Button ID="btnSave" runat="server" OnClick="Create_Click" Text="Save" class="btn btn-dark fw-bold" Visible="true" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="Reset_Click" class="btn btn-dark fw-bold" />
                            </div>
                        </div>

                </asp:Panel>          
                <asp:Panel ID="ListView" runat="server" Visible="true">
                    <div class="row mt-5 ">
                        <div class="col-12 justify-content-end">
                            <asp:GridView ID="ColumnControlData" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" 
                                autoFit="true" Style="margin-top: 0px;" OnPageIndexChanging="OnPageIndexChanging"  AllowPaging="True" Height="186px" PageSize="5">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ColumnCountrolId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ColumnName" HeaderText="Column Name" SortExpression="ColumnName" />
                                    <asp:BoundField DataField="RequiredField" HeaderText="Display" SortExpression="RequiredField" />
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
