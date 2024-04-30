<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuizVideoModule.aspx.cs" Inherits="Crud__Asp.net_Web_form_.QuizVideoModule" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <style>      
        .horizontal-table {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
            border-collapse: separate;
            border-spacing: 10px;
            opacity: 0;
            transition: opacity 0.3s ease;
        }

            .horizontal-table.active {
                opacity: 1;
            }

            .horizontal-table table {
                width: 100%;
                border-collapse: collapse;
                border-radius: 10px;
                overflow: hidden;
            }

            .horizontal-table th,
            .horizontal-table td {
                padding: 15px;
                border: 1px solid #ddd;
                text-align: left;
            }

            .horizontal-table th {
                background-color: #f2f2f2;
            }

        .sortable {
            cursor: move;
        }

        .end-button-container {
            display: flex;
            justify-content: flex-end;
            margin-top: 20px;
        }

        .upload-button {
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

            .upload-button:hover {
                background-color: #0056b3;
            }

        .table-container {
            display: flex;
            justify-content: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="text-center">
            <h3 runat="server" id="txtQuizModuleName"></h3>
        </div>
      
        <div class="end-button-container">
            <asp:Button runat="server" ID="btnAdd" class="btn btn-primary upload-button" Text="Add Video" OnClick="AddVideoMethod" />
        </div>
        <div class="table-container" runat="server" id="VideoTable" visible="true">
            <div class="horizontal-table active" id="table1">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" autoFit="true" Style="margin-top: 0px;" OnPageIndexChanging="OnPageIndexChanging" AllowPaging="True" Height="186px" PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="Order" HeaderText="Order" />
                        <asp:BoundField DataField="VideoTitle" HeaderText="Video Title" />
                        <asp:TemplateField HeaderText="Video Preview">
                            <ItemTemplate>
                                <video width="320" height="240" controls>
                                    <source src='<%# Eval("VideoFilePath") %>' type="video/mp4">
                                    Your browser does not support the video tag.
                                </video>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"Quiz_ModuleVideoId")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="VideoOrder" HeaderText="Video Order" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnAddQuizForm" runat="server" Text="Add Quiz Form" CssClass="btn btn-success" OnClick="AddQuizFormClick" />
                                <asp:Button ID="btnEditVideo" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="EditVideoClick" />
                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="Delete"
                                    OnClientClick="return confirm('Are you sure you want to delete this record?');" OnClick="DeleteRecord"></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div runat="server" id="videoFormId" visible="false">
            <h4>Upload Video</h4>
            <div class="mb-3 row">
                <label for="videoTitle" class="col-sm-3 col-form-label">Video Title</label>
                <div class="col-sm-9">
                    <input type="text" runat="server" class="form-control" id="videoTitle" name="videoTitle" />
                </div>
            </div>
            <div class="mb-3 row">
                <label for="videoFile" class="col-sm-3 col-form-label">Choose File</label>
                <div class="col-sm-9">
                    <input type="file" runat="server" class="form-control" id="videoFile" name="videoFile" onchange="previewVideo(this)" accept="video/*" />
                </div>
            </div>
            <div class="mb-3 row" runat="server" visible="false" id="perviousVideoSrc">
                <label for="videoFile" class="col-sm-3 col-form-label">Previous Video</label>
                <div class="col-sm-9">
                </div>
                <video runat="server" width="140" height="140" controls>
                    <source runat="server" id="videoSrc" type="video/mp4" />
                </video>
            </div>
            <div class="mb-3 row">
                <div class="col-sm-12">
                    <asp:Button runat="server" class="btn btn-primary" OnClick="UploadVideoMethod" ID="btnUploadVideo" Text="Upload" />
                    <asp:Button runat="server" class="btn btn-primary" OnClick="CancelVideoMethod" Text="Cancel" />
                </div>
            </div>
        </div>  
    </form>
    <script>
        $(function () {
            $("#<%= GridView1.ClientID %> tbody").sortable({
                items: "tr:not(:first-child)",
                axis: "y",
                containment: "parent",
                cursor: "move",
                update: function (event, ui) {
                    $(this).children().each(function (index) {
                        if (index !== 0) {
                            $(this).find('td:first').text(index);
                        }
                    });
                }
            }).disableSelection();
        });
    </script>
</body>
</html>