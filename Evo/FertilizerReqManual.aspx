﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="FertilizerReqManual.aspx.cs" Inherits="Evo.FertilizerReqManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon mb-3">
            <img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization / Chemical">
            Fertilization
        </h2>
        <div class="row mt-4">
            <div class="col-12 col-md-4 col-lg-3">
                <label>Bench Location </label>
                <span style="color: red">*</span>
                <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                <span class="error_message">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                </span>
            </div>
            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Job No </label>
                <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-12 col-lg-4 mb-3">
                <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action mr-2" Visible="false" ValidationGroup="x" />
                <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-actio mr-2" OnClick="btnSearchRest_Click" />
                <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" CssClass="bttn bttn-primary bttn-action" ValidationGroup="x" />
            </div>
        </div>
        <asp:Panel ID="Panel_Bench" Visible="false" runat="server">
            <div class="row my-3">
                <div class="col-auto col-lg-4">
                    <asp:RadioButtonList ID="RadioBench" Width="100%" runat="server" AutoPostBack="true" ValidationGroup="x" OnSelectedIndexChanged="RadioBench_SelectedIndexChanged" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Bench (A/B)" Value="1" class="custom-control custom-radio mr-2 my-2"></asp:ListItem>
                        <asp:ListItem Text="Benches in house" Value="2" class="custom-control custom-radio mr-2 my-2"></asp:ListItem>
                        <asp:ListItem Text="House" Value="3" class="custom-control custom-radio my-2"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-lg-2">
                    <asp:Button ID="Button1" ValidationGroup="x" Visible="false" runat="server" CssClass="bttn bttn-primary bttn-action mr-2" OnClick="btnSearch_Click" Text="Search" />
                    <asp:Button Text="Reset" ID="btnResetSearch" ValidationGroup="x" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:Panel ID="PanelBench" Visible="false" runat="server">
                        <asp:Label ID="lblBench1" Visible="false" runat="server" Text="Label"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="PanelBenchesInHouse" Visible="false" runat="server">
                        <asp:ListBox ID="ListBoxBenchesInHouse" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ListBoxBenchesInHouse_SelectedIndexChanged" Height="150px" runat="server" CssClass="mb-4 p-2 pr-3"></asp:ListBox>
                    </asp:Panel>
                    <asp:Panel ID="PanelHouse" Visible="false" runat="server">
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>

        <div class="portlet light ">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">
                <div class="data__table data__table-height">
                    <asp:GridView ID="gvFer" runat="server" AutoGenerateColumns="False"
                        class="striped" AllowSorting="true"
                        GridLines="None" OnRowCommand="gvFer_RowCommand"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                    <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblitem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Facility Location" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays","{0:####}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--
                                <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
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

        <div class="dashboard__block dashboard__block--asign">
            <div id="userinput" runat="server" class="assign__task" visible="false">
                <asp:Panel ID="pnlint" runat="server">
                    <h3>Assign Task</h3>
                    <div class="row">
                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label class="d-block">Assignment </label>
                            <asp:DropDownList ID="ddlsupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsupervisor" ValidationGroup="e"
                                    SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Supervisor" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>Spray Date </label>

                            <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="Label3" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDate" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Date" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>
                                <asp:Label ID="lbltype" runat="server" Text="Fertilizer"></asp:Label></label>
                            <asp:DropDownList ID="ddlFertilizer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlFertilizer" ValidationGroup="md"
                                    SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Fertilizer" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>Concentration [ppm]</label>
                            <asp:TextBox ID="txtQty" TextMode="Number" Text="150" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQty" ValidationGroup="md"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>Trays</label>
                            <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="lblerrmsg" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTrays" ValidationGroup="md" SetFocusOnError="true" ErrorMessage="Please Enter Trays" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>

                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>SQFT of Bench </label>
                            <asp:TextBox ID="txtSQFT" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="Label2" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSQFT" ValidationGroup="md"
                                    SetFocusOnError="true" ErrorMessage="Please Enter SQFT" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <%--  <div class="col-lg-3">
                                <label>Bench Irrigation Flow Rate [Gallons/min]</label>
                                <asp:TextBox ID="txtBenchIrrigationFlowRate" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                            </div>
                            <div class="col-lg-3">
                                <label>Bench Irrigation Coverage [Gallons/Sqft]</label>
                                <asp:TextBox ID="txtBenchIrrigationCoverage" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                            </div>
                            <div class="col-lg-3">
                                <label>Spray Coverage per minutes [sqft/min]</label>
                                <asp:TextBox ID="txtSprayCoverageperminutes" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                            </div>--%>
                        <div class="col-sm-6 col-lg col-xl-3 mb-3">
                            <label>Minimum Days Until Next Fertilization</label>
                            <asp:TextBox ID="txtResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>
                        <div class="col-12">
                            <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnSubmit_Click" />
                            <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

    </div>
</asp:Content>
