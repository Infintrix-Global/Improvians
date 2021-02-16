﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GerminationRequestForm.aspx.cs" Inherits="Improvians.GerminationRequestForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="site__container">
            <h2>Germination Count Task </h2>

       
                <div class="row">
                    <div class="col m3">
                        <label>Customer </label>
                        <asp:DropDownList ID="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <%-- <div class="col m3">
                        <label>GreenHouse </label>
                        <asp:DropDownList ID="ddlGreenhouse" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>--%>
                   
                    <div class="col m3">
                        <label>Job No </label>
                        <asp:DropDownList ID="ddlJobNo" runat="server" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                     <div class="col m3">
                        <label>Main Location </label>
                        <asp:DropDownList ID="ddlFacility" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" AutoPostBack="true" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                     <div class="col m3">
                        <label>Bench Location </label>
                        <asp:DropDownList ID="ddlBenchLocation" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" AutoPostBack="true" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col m4">
                        <asp:RadioButtonList ID="radweek" runat="server" Width="100%" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radweek_SelectedIndexChanged">
                            <asp:ListItem Text="Last Week" Value="1"></asp:ListItem>
                            <asp:ListItem Text="This Week" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Next Week" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="col m4">
                        <asp:RadioButtonList ID="radStatus" runat="server"  Width="100%" OnSelectedIndexChanged="radStatus_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Overdue" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Today" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Upcoming" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col m4">
                         <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />
                        <asp:Button ID="btnManual" runat="server" Text="Manual Request" CssClass="bttn bttn-primary bttn-action" OnClick="btnManual_Click" />
                    </div>
                </div>
           
            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                    GridLines="None" OnRowCommand="gvGerm_RowCommand"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Germination Count Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("GermDate","{0:dd MMM yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Germination Count No" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label16" runat="server" Text='<%# Eval("GermNo")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGrowerID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbljobID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Main Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:dd MMM yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Eval("ID")  %>'></asp:Button>

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
            <div class="dashboard__block dashboard__block--asign">




                <div id="userinput" runat="server" class="assign__task d-flex" visible="false">
                    <asp:Panel ID="pnlint" runat="server">
                        <h3>Assign Task</h3>
                        <div class="row align-items-end">
                            <div class="col-auto m6">
                                <label>Job No.</label><br />
                                <h3 class="robotobold">
                                    <asp:Label ID="lblJobID" runat="server"></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                </h3>
                            </div>
                            <div class="col m6">
                                <label>Assignment</label>
                                <%-- <h3 class="robotobold"><asp:Label ID="lblSupervisorName" runat="server" ></asp:Label></h3>--%>
                                <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            </div>
                            <div class="col m6">
                                <label>Inspection Due Date </label>
                                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <div class="col m6">
                                <label>Number Of Trays To Inspect</label>
                                <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <%--<div class="clearfix"></div>--%>

                            <div class="col-auto m6">
                                <br />
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="col-auto m6">
                                <br />
                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
