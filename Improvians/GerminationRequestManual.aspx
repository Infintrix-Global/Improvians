<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GerminationRequestManual.aspx.cs" Inherits="Evo.GerminationRequestManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main__header">
        <div class="site__container">
            <h2 class="mb-3">Germination Count Task </h2>
            <div class="row">                   
                <div class="col-12 col-md-4 col-lg-3">
                    <label>Bench Location </label>
                    <span style="color: red">*</span>
                    <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    <span class="error_message">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x" SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
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
                <div class="col-12 col-lg-4 m3 mb-3">
                    <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />
                    <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" CssClass="ml-2 bttn bttn-primary bttn-action" ValidationGroup="x" />
                </div>
            </div>
            <asp:Panel ID="Panel_Bench" Visible="false" runat="server">
                <div class="row">
                    <div class="col-lg-4">
                        <asp:RadioButtonList ID="RadioBench" Width="100%" runat="server" AutoPostBack="true" ValidationGroup="x" OnSelectedIndexChanged="RadioBench_SelectedIndexChanged" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Bench (A/B)" Value="1" class="custom-control custom-radio mr-2"></asp:ListItem>
                            <asp:ListItem Text="Benches in house" Value="2" class="custom-control custom-radio"></asp:ListItem>
                            <asp:ListItem Text="House" Value="3" class="custom-control custom-radio"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="col-lg-2">
                        <asp:Button ID="btnRRE" ValidationGroup="x" Visible="false" runat="server" CssClass="bttn bttn-primary bttn-action mr-2" OnClick="btnRRE_Click" Text="Search" />
                        <asp:Button Text="Reset" ID="btnResetSearch" ValidationGroup="x" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Panel ID="PanelBench" Visible="false" runat="server">
                            <asp:Label ID="lblBench1" Visible="false" runat="server" Text="Label"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="PanelBenchesInHouse" Visible="false" runat="server">

                            <asp:ListBox ID="ListBoxBenchesInHouse" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ListBoxBenchesInHouse_SelectedIndexChanged" Width="60%" Height="150px" runat="server"></asp:ListBox>
                        </asp:Panel>
                        <asp:Panel ID="PanelHouse" Visible="false" runat="server">
                        </asp:Panel>

                    </div>
                </div>
            </asp:Panel>
            <div class="portlet light mt-3">
                <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                <div class="portlet-body">
                    <div class="data__table data__table-height">
                        <asp:GridView ID="gvGerm" runat="server" AutoGenerateColumns="False"
                            class="striped" AllowSorting="true"
                            GridLines="None" OnRowCommand="gvGerm_RowCommand"
                            ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>

                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <%--                                        <asp:TemplateField HeaderText="Germination Count Date" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("GermDate","{0:dd MMM yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Germination Count Type" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label16" runat="server" Text='<%# Eval("GermNo")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                        <%-- <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>--%>
                                        <asp:Label ID="lblGrowerID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Main Location" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--   <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                        <%--  <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                        <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Eval("ID")  %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                            <div class="col-sm-6 col-sm-6 col-lg-3 mb-3">
                                 <asp:Label ID="lblJobID" runat="server" Visible="false"></asp:Label>
                                 <asp:Label ID="lblGrowerID" runat="server" Visible="false"></asp:Label>
                                <label runat="server" id="lblfacsupervisor">Assignment</label>
                                <%-- <h3 class="robotobold"><asp:Label ID="lblSupervisorName" runat="server" ></asp:Label></h3>--%>
                                <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            </div>
                            <div class="col-sm-6 col-sm-6 col-lg-3 mb-3">
                                <label>Inspection Due Date </label>
                                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <div class="col-sm-auto col-lg-3 mb-3">
                                <label class="d-block">Number Of Trays To Inspect</label>
                                <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" class="input__control input__control-auto robotomd"></asp:TextBox>
                            </div>
                            <%--<div class="clearfix"></div>--%>

                            <div class="col-12 m6">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="ml-2 bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
