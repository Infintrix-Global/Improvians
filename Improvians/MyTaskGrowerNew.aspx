<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskGrowerNew.aspx.cs" Inherits="Improvians.MyTaskGrowerNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main">
        <div class="site__container">
            <asp:Panel ID="pnlGermTitle" runat="server">
                <h2>Germination Task Request</h2>

            </asp:Panel>

            <asp:Panel ID="pnlGermination" runat="server">
                <div class="filter__row d-flex">
                    <div class="row">
                        <div class="col m3">
                            <label>Customer </label>
                            <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col m3">
                            <label>GreenHouse </label>
                            <asp:DropDownList ID="ddlGreenhouse" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col m3">
                            <label>Facility </label>
                            <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col m3">
                            <label>Job No </label>
                            <asp:DropDownList ID="ddlJobNo" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col m3">
                            <label>Status </label>
                            <asp:DropDownList ID="ddlStatus" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
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
                                        class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                        GridLines="None" OnRowCommand="gvGerm_RowCommand"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("JobID")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Item")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Put Away Location" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("PutAwayLocation")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("PutAwayMainLocation")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("#Tray")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Seed Lot" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("SeedLots")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:dd MMM yyyy}")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Germination Date" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label12" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                    <asp:Button ID="btnReschdule" runat="server" Text="Reschdule" CssClass="bttn bttn-primary bttn-action" CommandName="Reschdule" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                    <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action" CommandName="Dismiss" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
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
                                        <asp:Label ID="lblJobID" runat="server"></asp:Label></h3>
                                </div>
                                <div class="col m6">
                                    <label runat="server" id="lblfacsupervisor">Greenhouse Supervisor</label>
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
            </asp:Panel>
            <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapseControlID="pnlGermTitle"
                Collapsed="true" ExpandControlID="pnlGermTitle" CollapsedText="Showing.."
                ExpandedText="Hide" ImageControlID="imgArrows" CollapsedImage="~/images/plus.png"
                ExpandedImage="~/images/minus.png" ExpandDirection="Vertical" TargetControlID="pnlGermination"
                ScrollContents="false">
            </ajax:CollapsiblePanelExtender>
        </div>
    </div>
</asp:Content>
