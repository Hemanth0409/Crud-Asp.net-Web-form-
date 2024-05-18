<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Crud__Asp.net_Web_form_.Index" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Employee Detail's</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepannel" runat="server">
            <ContentTemplate>
                <div class="container-fluid p-0 m-0">
                    <div class="row flex-nowrap">
                        <div class="col-auto col-md-3 col-xl-2 px-sm-2 px-0 bg-dark bg-primary">
                            <div class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100">
                                <a href="EmpDetailsCrud.aspx" class="d-flex align-items-center pb-3 mb-md-0 me-md-auto text-white text-decoration-none">
                                    <span class="fs-5 d-none d-sm-inline">DashBoard</span>
                                </a>
                                <ul class=" switcher nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start" id="menu">
                                    <li class="nav-item ">
                                        <a href="EmpDetailsCrud.aspx" class="nav-link align-middle text-white px-0" target="switch-frame">
                                            <i class="fs-4 bi-house"></i><span class="ms-1 d-none d-sm-inline">Employee Details</span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="Country.aspx" class="nav-link px-0 text-white" target="switch-frame">
                                            <i class="fs-4 bi-house"></i><span class="ms-1 d-none d-sm-inline">Country </span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="State.aspx" class="nav-link px-0 text-white" target="switch-frame">
                                            <i class="fs-4 bi-house"></i><span class="ms-1 d-none d-sm-inline">State</span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="DynamicModule.aspx" class="nav-link px-0 text-white" target="switch-frame">
                                            <i class="fs-4 bi-house"></i><span class="ms-1 d-none d-sm-inline">DynamicModule </span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="ModuleRights.aspx" class="nav-link px-0 text-white" target="switch-frame">
                                            <i class="fs-4 bi-house"></i><span class="ms-1 d-none d-sm-inline">ModuleRights </span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="QuizModule.aspx" class="nav-link px-0 text-white" target="switch-frame">
                                            <i class="fs-4 bi-house"></i><span class="ms-1 d-none d-sm-inline">QuizModule </span>
                                        </a>
                                    </li>
                                    <ul class=" switcher nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start" id="Ul1" runat="server" clientidmode="Static">
                                        <asp:PlaceHolder runat="server" ID="navMenu"></asp:PlaceHolder>
                                    </ul>
                                </ul>
                            </div>
                        </div>
                        <script>
                            var switcher$ = $('.switcher');
                            var switchTarget$ = $('.switch-target');

                            switcher$.on('click', switchIframeSrc);

                            function switchIframeSrc() {
                                switchTarget$.attr('src', switcher$.val());
                            }
                            switchIframeSrc();
                        </script>
                        <div class="col">
                            <iframe id="ContentIFrame" frameborder="0" name="switch-frame" style="height: 100%; width: 100%" class="switch-target" src="BlankPage.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
