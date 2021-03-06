﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskShippingCoordinator.aspx.cs" Inherits="Evo.MyTaskShippingCoordinator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon mb-4">
            <img src="./images/dashboard_put-away.png" width="137" height="140" alt="Put-Away">
            Put-Away Completion Form
        </h2>

        <div class="row pt-1 mb-3">
            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd mb-0"></asp:DropDownList>
            </div>

            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Facility </label>
                <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown robotomd mb-0"></asp:DropDownList>
            </div>
            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Job No </label>
                <asp:DropDownList ID="ddlJobNo" runat="server" class="custom__dropdown robotomd mb-0"></asp:DropDownList>
            </div>
        </div>

        <div class="portlet light mb-4">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">
                <div class="data__table">
                    <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        class="striped" AllowSorting="true" OnRowDataBound="gvGerm_RowDataBound"
                        GridLines="None" OnRowCommand="gvGerm_RowCommand"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("JobCode")  %>'></asp:Label>
                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                    <asp:Label ID="lblWorkorder" Visible="false" runat="server" Text='<%# Eval("wo")  %>'></asp:Label>
                                    <%-- <asp:Label ID="lblMoveID" runat="server" Visible="false" Text='<%# Eval("MoveID")  %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="Label7" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>--%>
                                    <asp:Label ID="lblGreenHouseName" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblTray" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--                                <asp:TemplateField HeaderText="From Facility" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFacilityFrom" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To Facility" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                             

                                <asp:TemplateField HeaderText="Request Date" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("CreateOn","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                            <%--                                    <asp:TemplateField HeaderText="Trays Left To be Moved" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTraysRequest" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                            <asp:TemplateField HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--<asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="bttn bttn-primary bttn-action" CommandName="Select1" CommandArgument='<%# Container.DataItemIndex %>'></asp:Button>--%>
                                    <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="bttn bttn-primary bttn-action" CommandName="Select" CommandArgument='<%# Eval("GrowerPutAwayId")  %>'></asp:Button>
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
</asp:Content>
