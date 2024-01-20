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
                                       </li>
                                      <li class="nav-item">
                                        <a href="ModuleRights.aspx" class="nav-link px-0 text-white" target="switch-frame">
                                            <i class="fs-4 bi-house"></i><span class="ms-1 d-none d-sm-inline">ModuleRights </span>
                                        </a>
                               
                                    </li>
                                    <%--  <li>
                                        <a href="#submenu3" data-bs-toggle="collapse" class="nav-link px-0 align-middle">
                                            <i class="fs-4 bi-grid"></i><span class="ms-1 d-none d-sm-inline">Country & state </span></a>
                                        <ul class="collapse nav flex-column ms-1" id="submenu3" data-bs-parent="#menu">
                                            <li class="w-100">
                                            </li>
                                            <li>
                                                <a href="TextBro" class="nav-link px-0">State</a>
                                            </li>
                                        </ul>
                                    </li>--%>
                                    <%-- <li>
                                        <a href="#submenu1" data-bs-toggle="collapse" class="nav-link px-0 align-middle">
                                            <i class="fs-4 bi-speedometer2"></i><span class="ms-1 d-none d-sm-inline">Dashboard</span> </a>
                                        <ul class="collapse show nav flex-column ms-1" id="submenu1" data-bs-parent="#menu">
                                            <li class="w-100">
                                                <a href="#" class="nav-link px-0"><span class="d-none d-sm-inline">Item</span> 1 </a>
                                            </li>
                                            <li>
                                                <a href="#" class="nav-link px-0"><span class="d-none d-sm-inline">Item</span> 2 </a>
                                            </li>
                                        </ul>
                                    </li>
                                    <li>
                                        <a href="#" class="nav-link px-0 align-middle">
                                            <i class="fs-4 bi-table"></i><span class="ms-1 d-none d-sm-inline">Orders</span></a>
                                    </li>
                                    <li>
                                        <a href="#submenu2" data-bs-toggle="collapse" class="nav-link px-0 align-middle ">
                                            <i class="fs-4 bi-bootstrap"></i><span class="ms-1 d-none d-sm-inline">Bootstrap</span></a>
                                        <ul class="collapse nav flex-column ms-1" id="submenu2" data-bs-parent="#menu">
                                            <li class="w-100">
                                                <a href="#" class="nav-link px-0"><span class="d-none d-sm-inline">Item</span> 1</a>
                                            </li>
                                            <li>
                                                <iframe src="" onscroll="false" class="auto-style1"></iframe>
                                            </li>
                                        </ul>
                                    </li>
                                  
                                    <li>
                                        <a href="#" class="nav-link px-0 align-middle">
                                            <i class="fs-4 bi-people"></i><span class="ms-1 d-none d-sm-inline">Customers</span> </a>
                                    </li>
                                </ul>
                                <hr>
                                <div class="dropdown pb-4">
                                    <a href="#" class="d-flex align-items-center text-white text-decoration-none dropdown-toggle" id="dropdownUser1" data-bs-toggle="dropdown" aria-expanded="false">
                                        <img src="https://github.com/mdo.png" alt="hugenerd" width="30" height="30" class="rounded-circle">
                                        <span class="d-none d-sm-inline mx-1">loser</span>
                                    </a>
                                    <ul class="dropdown-menu dropdown-menu-dark text-small shadow">
                                        <li><a class="dropdown-item" href="#">New project...</a></li>
                                        <li><a class="dropdown-item" href="#">Settings</a></li>
                                        <li><a class="dropdown-item" href="#">Profile</a></li>
                                        <li>
                                            <hr class="dropdown-divider">
                                        </li>
                                        <li><a class="dropdown-item" href="#">Sign out</a></li>
                                    </ul>--%>
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
                            <iframe id="ContentIFrame" frameborder="0" name="switch-frame" style="height: 1000px; width: 98%" class="switch-target" src="BlankPage.aspx"></iframe>
                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
