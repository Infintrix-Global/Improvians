<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskSpray.aspx.cs" Inherits="Improvians.MyTaskSpray" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header__bottom">
        <div class="header__tabs">
            <ul class="d-flex align-items-center justify-content-center list-inline">
                <li><a href="#" class="bttn active" title="My Task">My Tasks</a></li>
                <%--<li><a href="#" class="bttn" title="Site Task">Site Task</a></li>
                <li><a href="#" class="bttn active" title="Request Task">Request Task</a></li>--%>
                <li><a href="#" class="bttn" title="Job Reports">Job Reports</a></li>
                <%--  <li><a href="#" class="bttn" title="Track Task">Track Task</a></li>--%>
            </ul>
        </div>
    </div>
    <div class="main">
        <div class="site__container">
            <h2>My Tasks</h2>

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
                <div class="col-md-6 col-lg-3 col-xl-8">
                    <div class="dashboard__block">
                        <%--   <h3>My Tasks</h3>--%>
                     
     The list of tasks below are items for you to complete.

                        
                        <div class="data__table">
                            <table>
                                <tbody>
                                    <tr>
                                        <th>Task Name</th>
                                        <th>Description</th>
                                        <th class="text-center">No. of Tasks</th>
                                    </tr>

                                    <tr>
                                        <td>Put-Away </td>
                                        <td>A list of put away location for a job.</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkPutAway" PostBackUrl="~/MyTaskLogisticManager.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Germination Count</td>
                                        <td>A list of germination count tasks to complete.</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkGerm" PostBackUrl="~/GerminationAssignmentForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Fertilization/Chemical</td>
                                        <td>A list of spray task to complete</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkFertilization" PostBackUrl="~/SprayTaskReq.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Irrigation</td>
                                        <td>A list of irrigation tasks to complete</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkIrr" PostBackUrl="~/IrrigationAssignmentForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td>Crop Health Report</td>
                                        <td>Assign Crop Health Report Task </td>
                                        <td class="text-center"></td>
                                    </tr>
                                    <tr>
                                        <td>Plant Ready</td>
                                        <td>A list of Plant Ready Reporting tasks to complete</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkpr" PostBackUrl="PlantReadyAssignmentForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Move Request</td>
                                        <td>A lisf ot Move request to complete </td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkMove" PostBackUrl="#" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>



                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />



        </div>
    </div>
</asp:Content>
