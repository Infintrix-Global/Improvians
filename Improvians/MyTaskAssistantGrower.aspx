<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskAssistantGrower.aspx.cs" Inherits="Evo.MyTaskAssistantGrower" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
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
                        The list of tasks below are items for you to complete. For each task, you will either be completing the task or reviewing it and assigning it to someone else. Most of these tasks are auto-generated based on the plant's production profile schedule after it is seeded. You also have the ability to manually request or assign tasks as needed - just go into the form and choose. 
                        <div class="data__table">
                            <table>
                                <tbody>
                                    <tr>
                                        <th>Task Name</th>
                                        <th>Description</th>
                                        <th class="text-center">No. of Tasks</th>

                                    </tr>

                                    <tr>
                                        <td>Put-Away  </td>
                                        <td>Assign a put away location for a job</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkPutAway" PostBackUrl="~/GrowerPutAwayForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Germination Count</td>
                                        <td>Review and assign these tasks to the Greenhouse Supervisor</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkGerm" PostBackUrl="~/GerminationRequestForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Fertilization/Chemical</td>
                                        <td>Review and assign these tasks to the Sprayer</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkFer" PostBackUrl="~/FertilizerTaskReq.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Irrigation</td>
                                        <td>Review and assign irrigation tasks to Greenhouse Supervisor</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkIrr" PostBackUrl="~/IrrigationRequestForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                     <tr>
                                        <td>Crop Health Report</td>
                                        <td>Assign Crop Health Report Task </td>
                                        <td class="text-center">
                                          </td>
                                    </tr>
                                    <tr>
                                        <td>Plant Ready</td>
                                        <td>Review and assign Plant Ready Reporting tasks to Greenhouse Supervisor</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkpr" PostBackUrl="~/PlantReadyRequestForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Move Request</td>
                                        <td>Review and assign move tasks </td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkMove" PostBackUrl="~/MoveForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
          <%--  <h2>Request Tasks</h2>--%>

<%--            <div class="row">
                <div class="col-md-6 col-lg-3 col-xl-4">
                    <div class="dashboard__block">
                        <div class="data__table">
                            <table>
                                <tbody>
                                    <tr>
                                        <th>Task Request Name</th>
                                        <th class="text-center">No. of Tasks</th>
                                    </tr>

                                    <tr>
                                        <td>Put-Away Request </td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="LinkButton1" PostBackUrl="~/GrowerPutAwayForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Germination Count Request</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="LinkButton2" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Fertilization/Chemical Request</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="LinkButton3" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Irrigation Request</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="LinkButton4" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Plant Ready Request</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="LinkButton5" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>

                                    <tr>
                                        <td>Move Request</td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="LinkButton6" runat="server" Text="0"></asp:LinkButton></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
--%>
            <%--                    <div class="col m4">
                        <label>Task Request Form </label>--%>
            <asp:DropDownList ID="ddlTaskRequest" Visible="false" AutoPostBack="true" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlTaskRequest_SelectedIndexChanged">
                <asp:ListItem Text="Put Away Request" Value="6"></asp:ListItem>
                <asp:ListItem Text="Germination Request" Value="4"></asp:ListItem>
                <asp:ListItem Text="Fertilization/Chemical Request" Value="8"></asp:ListItem>
                <asp:ListItem Text="Irrigation Request" Value="9"></asp:ListItem>
                <asp:ListItem Text="Plant Ready Request" Value="7"></asp:ListItem>
                <asp:ListItem Text="Move Request" Value="5"></asp:ListItem>

                <%--<asp:ListItem Text="Production Planning Request" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Seedline Request " Value="2"></asp:ListItem>
                          <asp:ListItem Text="Seedline Move Request " Value="3"></asp:ListItem>--%>
            </asp:DropDownList>
            <%--</div>--%>
        </div>
    </div>
</asp:Content>
