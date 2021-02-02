<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedingPlanForm.aspx.cs" Inherits="Improvians.SeedingPlan.SeedingPlanForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main">
            <div class="site__container">
                <h2>My Task</h2>
                
                <div class="filter__row d-flex">
                    <div class="row">
                        <div class="col m3">
                             <label>Customer </label>
                            <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        
                          <div class="col m3">
                               <label>Facility </label>
                            <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                          <div class="col m3">
                               <label>Job No </label>
                            <asp:DropDownList ID="ddlJobNo" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                       <div class="row">
                            <div class=" col m12">
                                <div class="portlet light ">
                                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                                    <div class="portlet-body">
                                        <div class="data__table">
                                            

                                            <asp:GridView ID="GridSeedingPlan" runat="server"></asp:GridView>

                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>

                </div>
         </div>

</asp:Content>
