﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="DumpRequestForm.aspx.cs" Inherits="Evo.DumpRequestForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>

    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_dump-request.png" width="137" height="132" alt="Plant Ready">
            Dump
        </h2>

        <div class="row">
            <div class="col-lg-3 col-md-4">
                <label>Job No</label>
                <asp:TextBox ID="txtSearchJobNo" runat="server" OnTextChanged="txtSearchJobNo_TextChanged" AutoPostBack="true" class="input__control robotomd"></asp:TextBox>

                <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="txtSearchJobNo"
                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                </cc1:AutoCompleteExtender>
            </div>

            <div class="col-lg-3 col-md-4">
                <label>Bench Location </label>
                <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-lg-3 col-md-4">
                <%--<div class="col-lg-3 col-md-4 mb-3 mb-lg-0">--%>
                <label>Job No </label>
                <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
        </div>

        <div class="row mb-3 align-items-end">
            <div class="col-lg-3 col-md-4">
                <label>Assigned By </label>
                <asp:DropDownList ID="ddlAssignedBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignedBy_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>

            <div class="col-lg-3 col-md-4">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>

            <%--<div class="col-xl-4 col-12 mb-3">--%>
            <div class="col-auto">
                <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action mr-2" OnClick="btnSearch_Click" />
                <asp:Button Text="Reset" ID="btnResetSearch" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnResetSearch_Click" />
            </div>
        </div>

        <div class="data__table">
            <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="gvPlantReady_RowDataBound"
                class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                GridLines="None" OnRowCommand="gvPlantReady_RowCommand" DataKeyNames="wo,DumpId,jid,jobcode,GreenHouseID,TaskRequestKey"
                ShowHeaderWhenEmpty="True" Width="100%">
                <Columns>

                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblBenchLoc" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                            <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                            <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomer" data-head="Customer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" data-head="Plant Type" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblTrays" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblSeededDate" data-head="Seeded Date" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblPlantDueDate" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dump Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblDumpdate" runat="server" Text='<%# Eval("DumpDateR","{0:MM/dd/yyyy}")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Job Source" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblsource" data-head="Job Source" runat="server" Text='<%# Eval("RequestType")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedBy" data-head="Assigened By" runat="server" Text='<%# Eval("AssignedBy")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                            <asp:Button ID="btnStart" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action" CommandName="StartDump" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                            <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                            <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
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

        <div class="text-left dashboard__block my-4">
            <div id="userinput" runat="server" visible="false" class="row justify-content-center">
                <div class="col-12">
                    <div class="row">
                        <div class="mb-3 col-12 col-md-auto">
                            <label>Assignment </label>
                            <asp:HiddenField ID="HiddenFieldJid" runat="server" />
                            <asp:HiddenField ID="HiddenFieldDid" runat="server" />
                            <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                            <asp:DropDownList ID="ddlDumptAssignment" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>

                        <div class="mb-3 col-12 col-md-auto">
                            <label class="d-block">Dump Date</label>
                            <asp:TextBox ID="txtDumpDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="mb-3 col-12 col-md-auto">
                            <label>Quantity of Tray </label>
                            <asp:TextBox ID="txtQuantityofTray" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="mb-3 col-12 col-md-auto">
                            <label>Comments </label>
                            <asp:TextBox ID="txtCommentsDump" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>
                        <div class="mb-3 col-12 col-md-auto align-self-end">
                            <asp:Button Text="Submit" ID="btnDumpSumbit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />
                            <asp:Button Text="Reset" ID="btnDumpReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
