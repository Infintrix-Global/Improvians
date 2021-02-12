<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskGreenSupervisorFinal.aspx.cs" Inherits="Improvians.MyTaskGreenSupervisorFinal" %>

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
            <div class="row">
                <div class="col-md-6 col-lg-3 col-xl-8">
                    <div class="dashboard__block">
                        <%--   <h3>My Tasks</h3>--%>
                        The list of tasks below are items for you to complete. For each task, you will either be completing the task or reviewing it and assigning it to someone else. These tasks have been assigned to you by Grower/Assistant Grower after it is seeded. You also have the ability to manually request or assign tasks as needed - just go into the form and choose.
                        <div class="data__table">
                            <table>
                                <tbody>
                                    <tr>
                                        <th>Task Name</th>
                                        <th>Description</th>
                                        <th class="text-center">No. of Tasks</th>
                                    </tr>
                                    <tr>
                                        <td>Put-Away</td>
                                        <td>Request a put away location for a job</td>
                                        <td class="text-center">NA</td>
                                    </tr>
                                    <tr>
                                        <td>Germination Count</td>
                                        <td>A list of germination count tasks to complete</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkGerm" PostBackUrl="~/GerminationAssignmentForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td>Fertilization/Chemical</td>
                                        <td>Request a spray job</td>
                                        <td class="text-center">NA</td>
                                    </tr>
                                    <tr>
                                        <td>Irrigation</td>
                                        <td>A list of irrigation tasks to complete</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkIrr" PostBackUrl="~/IrrigationAssignmentForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Plant Health Report</td>
                                        <td>A list of plant health reporting tasks to complete</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkpr" PostBackUrl="~/PlantReadyAssignmentForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td>Move</td>
                                        <td>Request a move </td>
                                        <td class="text-center">
                                           NA</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
