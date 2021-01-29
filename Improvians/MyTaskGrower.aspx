<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskGrower.aspx.cs" Inherits="Improvians.MyTask1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header__bottom">
        <div class="header__tabs">
            <ul class="d-flex align-items-center justify-content-center list-inline">
                <li><a href="/my-tasks.html" class="bttn" title="My Task">My Task</a></li>
                <%--<li><a href="#" class="bttn" title="Site Task">Site Task</a></li>
                <li><a href="#" class="bttn active" title="Request Task">Request Task</a></li>--%>
                <li><a href="#" class="bttn" title="Job Reports">Job Reports</a></li>
              <%--  <li><a href="#" class="bttn" title="Track Task">Track Task</a></li>--%>
            </ul>
        </div>
    </div>
    <div class="main">
        <div class="site__container">
            <h2>My Task</h2>

            <div class="filter__row d-flex">
                <div class="row">
                    <div class="col m4">
                        <label>Department </label>
                        <asp:DropDownList ID="ddlDept" runat="server" class="custom__dropdown robotomd">
                           <%-- <asp:ListItem Text="Grower" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Greenhouse Support" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Transplanting Team" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Seedline" Value="2"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                    <div class="col m4">
                        <label>Task Request Form </label>
                        <asp:DropDownList ID="ddlTaskRequest" AutoPostBack="true" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlTaskRequest_SelectedIndexChanged">
                            <asp:ListItem Text="Production Planning Request" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Seedline Request " Value="2"></asp:ListItem>
                            <asp:ListItem Text="Seedline Move Request " Value="3"></asp:ListItem>
                            <asp:ListItem Text="Germination Request" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Move Request" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Put Away Request" Value="6"></asp:ListItem>
                              <asp:ListItem Text="Plant Ready Request" Value="7"></asp:ListItem>
                        </asp:DropDownList>
                    </div>



                </div>
            </div>
        </div>
    </div>
</asp:Content>
