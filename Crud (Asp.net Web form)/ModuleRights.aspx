<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleRights.aspx.cs" Inherits="Crud__Asp.net_Web_form_.ModuleRights" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Module Rights</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">
        function validateCheckboxes() {
            var gridView = document.getElementById('<%= ModuleRightsGridView.ClientID %>');
            var checkboxes = gridView.getElementsByTagName('input');
            var checkedCheckboxes = [];
            var headerCells = gridView.getElementsByTagName('th');

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === 'checkbox' && checkboxes[i].checked) {
                    var columnIndex = checkboxes[i].parentNode.cellIndex;
                    var columnName = getColumnName(headerCells[columnIndex]);
                    var rowIndex = checkboxes[i].parentNode.parentNode.rowIndex;
                    checkedCheckboxes.push({ columnName: columnName, rowIndex: rowIndex });
                }
            }

            if (checkedCheckboxes.length === 0) {
                alert('Please select at least one module right.');
                return false;
            }
            document.getElementById('<%= CheckedCheckboxesHiddenField.ClientID %>').value = JSON.stringify(checkedCheckboxes);

            return true;
        }

        function getColumnName(headerCell) {
            var columnName = headerCell.innerText.trim();
            return columnName;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepannel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="ListView" runat="server" Visible="true">
                    <div class="row mt-5 ">
                        <div class="col-12 justify-content-end">
                            <asp:GridView ID="ModuleRightsGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" autoFit="true" Style="margin-top: 0px;" AllowPaging="True" Height="186px" PageSize="5">
                                <Columns>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("EmployeeId") %>' />
                                            <%# Eval("EmployeeName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="col-12 d-flex justify-content-center gap-3 mt-5">
                                <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="Button" OnClick="SaveButton_Click" OnClientClick="return validateCheckboxes();" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="CheckedCheckboxesHiddenField" runat="server" />
                <input id="EmpCount" type="hidden" runat="server" style="width: 50px" />
                <input id="ModuleCount" type="hidden" runat="server" style="width: 50px" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
