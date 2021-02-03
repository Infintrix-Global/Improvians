<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskProductionPlanner.aspx.cs" Inherits="Improvians.MyTaskProductionPlanner" %>
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
                                            <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                class="striped"  AllowSorting="true"  OnPageIndexChanging="gvGerm_PageIndexChanging"
                                                GridLines="None"  OnRowCommand="gvGerm_RowCommand"
                                                ShowHeaderWhenEmpty="True" Width="100%">
                                                <Columns>

                                                       <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="Seedline Planning"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                        <ItemTemplate>
                                                          <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                            <asp:Label ID="lbljID" runat="server" Text='<%# Eval("jobcode")  %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Work Order" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                        <ItemTemplate>
                                                         
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("wo")  %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                              

                                                 <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2" >
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action" CommandName="Select" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                
                                                </Columns>

                                                <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <EmptyDataTemplate>
                                                    No Record Available
                                                </EmptyDataTemplate>
                                            </asp:GridView>



                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>

                </div>
         </div>
</asp:Content>
