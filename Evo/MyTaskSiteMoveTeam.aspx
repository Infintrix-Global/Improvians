<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskSiteMoveTeam.aspx.cs" Inherits="Evo.MyTaskSiteMoveTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="mb-4">My Tasks</h2>

        <%--  <div class="filter__row d-flex">
                <div class="row">
                    <div class="col m4">
                        <label>Department </label>
        --%>
        <asp:DropDownList ID="ddlDept" runat="server" class="custom__dropdown robotomd" Visible="false">
            <%-- <asp:ListItem Text="Grower" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Greenhouse Support" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Transplanting Team" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Seedline" Value="2"></asp:ListItem>--%>
        </asp:DropDownList>
        <%--            </div>--%>

        <div class="row">
            <div class="col-md-6 col-lg-4">
                <div class="dashboard__block">
                    <%--   <h3>My Tasks</h3>--%>
                    <div class="data__table">
                        <table>
                            <tbody>
                                <tr>
                                    <th>Task Name</th>
                                    <th class="text-center">No. of Tasks</th>
                                </tr>

                                <tr>
                                    <td>Put-Away  </td>
                                    <td class="text-center">
                                        <asp:LinkButton ID="lnkMove" PostBackUrl="~/MyTaskLogisticManager.aspx" runat="server"></asp:LinkButton></td>
                                </tr>

                                <tr>
                                    <td>Job Moves</td>
                                    <td class="text-center"><a href="#">0</a></td>
                                </tr>

                                <tr>
                                    <td>To Customer</td>
                                    <td class="text-center"><a href="#">0</a></td>
                                </tr>

                                <tr>
                                    <td>Materials</td>
                                    <td class="text-center"><a href="#">0</a></td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <%--        <div class="col m4">
                <label>Task Request Form </label>--%>
        <asp:DropDownList ID="ddlTaskRequest" Visible="false" AutoPostBack="true" runat="server" class="custom__dropdown robotomd">
            <asp:ListItem Text="Production Planning Request" Value="1"></asp:ListItem>
            <asp:ListItem Text="Seedline Request " Value="2"></asp:ListItem>
            <asp:ListItem Text="Seedline Move Request " Value="3"></asp:ListItem>
            <asp:ListItem Text="Germination Request" Value="4"></asp:ListItem>
            <asp:ListItem Text="Move Request" Value="5"></asp:ListItem>
            <asp:ListItem Text="Put Away Request" Value="6"></asp:ListItem>
            <asp:ListItem Text="Plant Ready Request" Value="7"></asp:ListItem>
            <asp:ListItem Text="Fertilization/Chemical Request" Value="8"></asp:ListItem>
        </asp:DropDownList>
        <%--</div>--%>
    </div>
</asp:Content>
