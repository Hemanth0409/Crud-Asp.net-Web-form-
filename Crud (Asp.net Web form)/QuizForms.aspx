<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuizForms.aspx.cs" Inherits="Crud__Asp.net_Web_form_.QuizForms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stepper with Horizontal Tables</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <style>
        /* CSS for stepper */
        .stepper {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
            font-family: Arial, sans-serif;
        }

        .step {
            flex: 1;
            text-align: center;
            padding: 10px;
            background-color: #f1f1f1;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

            .step.active {
                background-color: #4CAF50;
                color: white;
            }

            .step:hover {
                background-color: #ddd;
            }

        .horizontal-table {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
            border-collapse: separate;
            border-spacing: 10px;
            opacity: 0;
            position: absolute;
            left: -9999px;
            transition: opacity 0.3s ease;
        }

            .horizontal-table.active {
                opacity: 1;
                left: 0;
            }

            .horizontal-table table {
                width: 100%;
                border-collapse: collapse;
            }

            .horizontal-table th,
            .horizontal-table td {
                padding: 10px;
                border: 1px solid #ddd;
                text-align: left;
            }

            .horizontal-table th {
                background-color: #f2f2f2;
            }

        .sortable {
            cursor: move;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="text-center">
            <h3 runat="server" id="txtQuizModuleId"></h3>
        </div>
        <div class="stepper">
            <div class="step active" runat="server" visible="true" onclick="showTable(1)">Video Upload</div>
            <div class="step" runat="server" visible="false" onclick="showTable(2)">Dynamic Quiz Form</div>
            <div class="step" runat="server" visible="false" onclick="showTable(3)">Form Over View</div>
        </div>
        <div class="horizontal-table active" id="table1">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table">
                <Columns>
                    <asp:BoundField DataField="Order" HeaderText="Order" />
                    <asp:BoundField DataField="VideoTitle" HeaderText="Video Title" />
                    <asp:BoundField DataField="VideoFilePath" HeaderText="Video File Path" />
                    <asp:BoundField DataField="VideoOrder" HeaderText="Video Order" />
                </Columns>
            </asp:GridView>

        </div>

        <div class="horizontal-table" id="table2">
            <table>
                <thead>
                    <tr>
                        <th>Header 11</th>
                        <th>Header 21</th>
                        <th>Header 31</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Data 1</td>
                        <td>Data 2</td>
                        <td>Data 3</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="horizontal-table" id="table3">
            <table>
                <thead>
                    <tr>
                        <th>Header 111</th>
                        <th>Header 121</th>
                        <th>Header 131</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Data 1</td>
                        <td>Data 2</td>
                        <td>Data 3</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>

    <script>
        function showTable(step) {
            $('.horizontal-table').removeClass('active');
            $('#table' + step).addClass('active');
            $('.step').removeClass('active');
            $('.step').eq(step - 1).addClass('active');
        }

        $(function () {
            $("#<%= GridView1.ClientID %> tbody").sortable({
            items: "tr:not(:first-child)", // Exclude header row from sorting
            axis: "y",
            containment: "parent",
            cursor: "move",
            update: function (event, ui) {
                $(this).children().each(function (index) {
                    if (index !== 0) { // Exclude header row
                        $(this).find('td:first').text(index); // Update index numbers
                    }
                });
            }
        }).disableSelection();
    });
    </script>


</body>
</html>
     });
   </script>
</body>
</html>
