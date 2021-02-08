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
            <h2>My Tasks</h2>

          <%--  <div class="filter__row d-flex">
                <div class="row">
                    <div class="col m4">
                        <label>Department </label>
          --%>              <asp:DropDownList ID="ddlDept" runat="server" class="custom__dropdown robotomd" Visible="false">
                           <%-- <asp:ListItem Text="Grower" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Greenhouse Support" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Transplanting Team" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Seedline" Value="2"></asp:ListItem>--%>
                        </asp:DropDownList>
        <%--            </div>--%>

            
                      <div class="col m8">
                        
                     <%--   <a href="GerminationRequestForm.aspx">Production Planning</a> <br />
                        <a href="Seeding_Plan_Form.aspx">Seedline</a> <br />
                        <a href="#">Seedline Move</a> <br />--%>
                           <div class="data__table">
                            <table>
                                <tbody>
                                <tr>
                                    <th>Task Name</th>
                                    <th class="text-center">No. of Tasks</th>
                                </tr>

                                <tr>
                                    <td>Put-Away  </td>
                                    <td class="text-center"> <asp:LinkButton ID="lnkPutAway" PostBackUrl="~/GrowerPutAwayForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                </tr>
                                    
                                <tr>
                                    <td>Germination</td>
                                    <td class="text-center"><asp:LinkButton ID="lnkGerm" PostBackUrl="~/GerminationRequestForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                </tr>

                                <tr>
                                    <td>Fertilization/Chemical</td>
                                    <td class="text-center"><asp:LinkButton ID="lnkFer" PostBackUrl="~/FertilizerTaskReq.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                </tr>

                                <tr>
                                    <td>Irrigation</td>
                                    <td class="text-center"><asp:LinkButton ID="lnkIrr" PostBackUrl="~/IrrigationRequestForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                </tr>

                                  <tr>
                                    <td>Plant Ready</td>
                                    <td class="text-center"><asp:LinkButton ID="lnkpr" PostBackUrl="~/PlantReadyRequestForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                </tr>

                                             <tr>
                                    <td>Move</td>
                                    <td class="text-center"><asp:LinkButton ID="lnkMove" PostBackUrl="~/MoveForm.aspx" runat="server" Text="0"></asp:LinkButton></td>
                                </tr>
                            </tbody></table>
                        </div>
                          <%--  <a href="GrowerPutAwayForm.aspx">Put Away</a> <br />
                          <a href="GerminationRequestForm.aspx">Germination </a> <br />
                      
                           <a href="FertilizerTaskReq.aspx">Fertilization/Chemical</a> <br />
                            <a href="IrrigationRequestForm.aspx">Irrigation</a> <br />
                       <a href="PlantReadyRequestForm.aspx">Plant Ready</a> <br />
                           <a href="MoveForm.aspx">Move</a> <br />--%>
                         
                        
                   

                        </div>

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
