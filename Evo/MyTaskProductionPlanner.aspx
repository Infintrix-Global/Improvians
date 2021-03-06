﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskProductionPlanner.aspx.cs" Inherits="Evo.MyTaskProductionPlanner" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon mb-4">My Task</h2>

        

        <div class="row">

<%--            <div class="col-lg-2">
                <label>Seedline Facility </label>
                <asp:DropDownList ID="ddlFacility" AutoPostBack="true" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>--%>
             <div class="col-lg-2">

                <label>Job No</label>
                <asp:TextBox ID="txtSearchJobNo" OnTextChanged="txtSearchJobNo_TextChanged" AutoPostBack="true" runat="server" class="input__control robotomd"></asp:TextBox>


                <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="txtSearchJobNo"
                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                </cc1:AutoCompleteExtender>
            </div>

            <div class="col-lg-2">
                <label>Crop Type </label>
                <asp:DropDownList ID="ddlCopTYpe" AutoPostBack="true" DataTextField="GenusCode" DataValueField="GenusCode" OnSelectedIndexChanged="ddlCopTYpe_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>

            <div class="col-lg-2">
                <label>Job No </label>
                <asp:DropDownList ID="ddlJobNo" AutoPostBack="true" DataTextField="Jobcode" DataValueField="Jobcode" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>

            <div class="col-lg-2">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" DataTextField="cname" DataValueField="cname" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>




            <div class="col m2">
                <br />
                <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />
            </div>
        </div>
               <br />
        <div class="portlet light mb-4">
            <asp:Label runat="server" Visible="false" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">
                <div class="data__table">
                    <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        class="striped" AllowSorting="true" OnPageIndexChanging="gvGerm_PageIndexChanging"
                        GridLines="None" OnRowCommand="gvGerm_RowCommand" OnRowDataBound="gvGerm_RowDataBound"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>

                            <%--    <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="Seedline Planning"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                            <%--  <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                      
                                                    <asp:Label ID="lbljID" runat="server" Text='<%# Eval("jobcode")  %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Order" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>

                                    <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemno" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Seedline Facility" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblloc" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="No. Of Tray" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("trays_actual")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Seeded Due Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblSoDate" runat="server" Text='<%# Eval("SoDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="Plan Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblplan_date" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Planned Due Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lbldue_date" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                                    <asp:Button ID="btnAssign" runat="server" Text="Reassign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Assign" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
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

        <div id="userinput" visible="false" runat="server" class="dashboard__block dashboard__block--asign">
            <h3>Reassign Task</h3>
            <div class="assign__task d-flex">
                <asp:Panel ID="pnlint" runat="server">
                    <div class="row">
                        <div class="col">
                            <label>Seedline Location</label>
                            <asp:DropDownList ID="ddlSeedlineLocation" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>

                        <div class="col-auto">
                            <br />
                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                        </div>
                        <div class="col-auto">
                            <br />
                            <asp:Button Text="Reset" ID="btnReset" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" runat="server" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
