﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="ChemicalTaskRequest.aspx.cs" Inherits="Evo.ChemicalTaskRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon mb-3">
            <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Fertilization / Chemical">
            Chemical Task Completion
        </h2>
          <div class="row">
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Job No</label>
                <asp:TextBox ID="txtSearchJobNo" runat="server" OnTextChanged="txtSearchJobNo_TextChanged" AutoPostBack="true" class="input__control robotomd"></asp:TextBox>

                <cc1:autocompleteextender servicemethod="SearchCustomers"
                    minimumprefixlength="2"
                    completioninterval="100" enablecaching="false" completionsetcount="10"
                    targetcontrolid="txtSearchJobNo"
                    id="AutoCompleteExtender1" runat="server" firstrowselected="false">
                </cc1:autocompleteextender>

            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Bench Location </label>
                <asp:ListBox ID="ddlBenchLocation" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server"  CssClass="SelectBox custom__dropdown robotomd"></asp:ListBox>
                
              
            </div>

            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Job No </label>
                <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Assigned By </label>
                <asp:DropDownList ID="ddlAssignedBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignedBy_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
         
                 <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                     <br />
                <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSearch_Click" />
                <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSearchRest_Click" />

            </div>
        </div>

        <div runat="server" visible="false" class="row mb-1 mb-md-4 align-items-end">
               <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Crop </label>
                <asp:DropDownList ID="ddlCrop" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>

            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Job Source </label>
                <asp:DropDownList ID="RadioButtonListSourse" runat="server" OnSelectedIndexChanged="RadioButtonListSourse_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                    <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                    <asp:ListItem Text="Navision" Value="Manual"></asp:ListItem>
                    <asp:ListItem Text="App" Value="App"></asp:ListItem>
                </asp:DropDownList>
            </div>


            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Task Type</label>
                <asp:DropDownList ID="RadioButtonListGno" runat="server" OnSelectedIndexChanged="RadioButtonListF_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                </asp:DropDownList>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>From Date</label>
                <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>To Date </label>
                <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
            </div>
         



        </div>

        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="gvSpray" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="gvSpray_RowDataBound"
                                class="striped" AllowSorting="true" PageSize="10" DataKeyNames="ChemicalCode,TaskRequestKey"
                                GridLines="None" OnRowCommand="gvSpray_RowCommand" OnPageIndexChanging="gvSpray_PageIndexChanging"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            <asp:Label ID="lblFertilizationCode" Visible="false" runat="server" Text='<%#Bind("ChemicalCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bench Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGreenHouseID" runat="server" Text='<%#Bind("GreenHouseID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Method">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMethod" runat="server" Text='<%#Bind("Method") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Chemical">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFertilizer" runat="server" Text='<%#Bind("Fertilizer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTray" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Chemical Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChemicalDate" runat="server" Text='<%# Eval("ChemicalDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignedBy" data-head="Assigened By" runat="server" Text='<%# Eval("AssignedBy")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="SQFT of Bench">
                                            <ItemTemplate>

                                                <asp:Label ID="lblSQFT" runat="server" Text='<%#Bind("SQFT") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="21%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                            <asp:Button ID="btnView" runat="server" Width="140px" Text="View Job Details" CssClass="bttn bttn-primary bttn-action my-1" CommandName="ViewDetails" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

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
        <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
</asp:Content>
