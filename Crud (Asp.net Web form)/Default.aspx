<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Crud__Asp.net_Web_form_._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <div>
       <div>
           <h2 class="text-center">Crud</h2>
           <table class="w-100" style="height: 238px">
               <tr>
                   <td style="width: 272px">&nbsp;</td>
                   <td style="width: 143px">Id</td>
                   <td>
                       <asp:TextBox ID="TextBox1" runat="server" Font-Size="Medium"></asp:TextBox>
                   &nbsp;&nbsp;
                       <asp:Button ID="Button5" OnClick="GetData_Click" runat="server" BackColor="Black" BorderColor="Black" BorderStyle="Inset" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="Get" Width="88px" />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   </td>
               </tr>
               <tr>
                   <td style="width: 272px">&nbsp;</td>
                   <td style="width: 143px">Name</td>

                   <td>
                       <asp:TextBox ID="TextBox2" runat="server" Font-Size="Medium"></asp:TextBox>

                   </td>
               </tr>
               <tr>
                   <td style="width: 272px; height: 27px;"></td>
                   <td style="width: 143px; height: 27px;">Salary</td>
                   <td style="height: 27px">
                       <asp:TextBox ID="TextBox3" runat="server" Font-Size="Medium"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td style="width: 272px">&nbsp;</td>
                   <td style="width: 143px">Contact</td>
                   <td>
                       <asp:TextBox ID="TextBox4" runat="server" Font-Size="Medium"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td style="width: 272px; height: 74px;"></td>
                   <td style="width: 143px; height: 74px;">Age</td>
                   <td style="height: 74px">
                       <asp:DropDownList ID="DropDownList1" runat="server" Height="57px">
                           <asp:ListItem>21</asp:ListItem>
                           <asp:ListItem>22</asp:ListItem>
                           <asp:ListItem>23</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
              
               <tr>
                   <td style="width: 272px; height: 333px;">
                       <asp:GridView ID="GridView1" runat="server" Width="246px">
                       </asp:GridView>
                   </td>
                   <td style="width: 143px; height: 333px;"></td>
                   <td style="height: 333px">
                       <asp:Button ID="Button1" OnClick="Insert_Click" runat="server" BackColor="Black" BorderColor="Black" BorderStyle="Inset" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="Insert" Width="88px" Height="29px" />
                   &nbsp;&nbsp;&nbsp;
                       <asp:Button ID="Button2" OnClick="Update_Click" runat="server" BackColor="Black" BorderColor="Black" BorderStyle="Inset" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="Update" Width="88px" />
                   &nbsp;&nbsp;&nbsp;
                       <asp:Button ID="Button3" OnClick="Delete_Click"  OnClientClick="return confirm('Are you sure you want to Delete?');" runat="server" BackColor="Black" BorderColor="Black" BorderStyle="Inset" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="Delete" Width="88px" />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:Button ID="Button4" OnClick="Search_Click" runat="server" BackColor="Black" BorderColor="Black" BorderStyle="Inset" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="Search" Width="88px" />
                   </td>
               </tr>
           </table>
       </div>
   </div>

</asp:Content>
