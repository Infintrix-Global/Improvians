<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GerminationRequestForm.aspx.cs" Inherits="Improvians.GerminationRequestForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main__header">
        <div class="site__container">
            <h2 class="head__title-icon">
                <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Count">
                Germination Count Task </h2>


            <div class="row">
                <div class="col-lg-2">
                    <label>Facility Location</label>
                    <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown robotomd" AutoPostBack="true" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged"></asp:DropDownList>
                    <span class="error_message">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFacility" ValidationGroup="x"
                            SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                    </span>
                </div>
                <div class="col-lg-2">
                    <label>Bench Location </label>

                    <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    <span class="error_message">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                            SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                    </span>
                </div>
                <div class="col-lg-2">
                    <label>Job No </label>
                    <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <div class="col-lg-2">
                    <label>Customer </label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <div class="col-lg-2">
                    <label>Job Source </label>
                    <asp:DropDownList ID="RadioButtonListSourse" runat="server" OnSelectedIndexChanged="RadioButtonListSourse_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                        <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                        <asp:ListItem Text="App" Value="App"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-lg-2">
                    <label>Germination Count Type </label>
                    <asp:DropDownList ID="RadioButtonListGno" runat="server" OnSelectedIndexChanged="RadioButtonListF_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">


                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                        <asp:ListItem Text="Germination 1" Value="Germination 1"></asp:ListItem>
                        <asp:ListItem Text="Germination 2" Value="Germination 2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-3">
                    <label>Week </label>
                    <asp:RadioButtonList ID="radweek" runat="server" Width="100%" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radweek_SelectedIndexChanged">
                        <asp:ListItem Text="Last Week" Value="1"></asp:ListItem>
                        <asp:ListItem Text="This Week" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Next Week" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <div class="col-lg-3">
                    <label>Status </label>
                    <asp:RadioButtonList ID="radStatus" runat="server" Width="100%" OnSelectedIndexChanged="radStatus_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Overdue" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Today" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Upcoming" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-lg-3">
                    <label>From Date</label>
                    <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    <label>To Date </label>
                    <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-3">
                </div>

                <div class="col-lg-5">
                </div>

                <div class="col-lg-4">
                    <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearch_Click" />
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
                                    GridLines="None" OnRowCommand="gvGerm_RowCommand" OnRowDataBound="gvGerm_RowDataBound"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Germination Count Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGermDate" runat="server" Text='<%# Eval("GermDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Germination Count Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGermNo" runat="server" Text='<%# Eval("GermNo")  %>'></asp:Label>
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
                                                <asp:Label ID="lblBenchLocation" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrays" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
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
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job source" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsource" runat="server" Text='<%# Eval("RequestType")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschedule" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                <asp:Button ID="btndismiss" runat="server" Text="Dismiss" OnClientClick="return confirm('Are you sure you want to dismiss this ?'); " CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Eval("ID")  %>'></asp:Button>
                                                <asp:Button ID="btnStart" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>

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

                        <div class="row">
                            <div class="col-lg-3">
                                <label>Job No.</label><br />
                                <h3 class="robotobold">
                                    <asp:Label ID="lblJobID" runat="server"></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                </h3>
                            </div>
                            <div class="col-lg-3">
                                <label>Bench location</label><br />
                                <h3 class="robotobold">
                                    <asp:Label ID="lblBenchlocation" runat="server"></asp:Label>

                                </h3>
                            </div>
                            <div class="col-lg-3">
                                <label>Total Trays</label><br />
                                <h3 class="robotobold">
                                    <asp:Label ID="lblTotalTrays" runat="server"></asp:Label>

                                </h3>
                            </div>
                            <div class="col-lg-3">
                                <label>Description </label>
                                <br />
                                <h3 class="robotobold">
                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>

                                </h3>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-3">
                                <label>Assignment</label>

                                <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                <label>Germination Count Date </label>
                                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <div class="col-lg-3">
                                <label>Number Of Trays To Inspect</label>
                                <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-3">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>
                            <div class="col-lg-2">
                            </div>
                            <div class="col m3">
                            </div>

                        </div>

                    </asp:Panel>
                </div>

                <div id="divReschedule" runat="server" class="assign__task d-flex" visible="false">
                    <asp:Panel ID="Panel1" runat="server">
                        <h3>Reschedule Task</h3>
                        <div class="row align-items-end">
                            <div class="col-auto m6">
                                <label>Job No.</label><br />
                                <h3 class="robotobold">
                                    <asp:Label ID="lblRescheduleJobID" runat="server"></asp:Label>
                                    <asp:Label ID="lblRescheduleID" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblOldDate" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblGermNo" runat="server" Visible="false"></asp:Label>
                                </h3>
                            </div>
                            <div class="col-auto m6">
                                <label>New Germination Count Date </label>
                                <asp:TextBox ID="txtNewDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <div class="col-auto m6">
                                <asp:RadioButtonList runat="server" ID="radReschedule">
                                    <asp:ListItem Text="Only move this germination count" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Move all the remaining germination count by same number of days" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <%--<div class="clearfix"></div>--%>

                            <div class="col-auto m6">
                                <br />
                                <asp:Button Text="Submit" ID="btnReschedule" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnReschedule_Click" />
                            </div>
                            <div class="col-auto m6">
                                <br />
                                <asp:Button Text="Reset" ID="btnRescheduleReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnResetReschedule_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
