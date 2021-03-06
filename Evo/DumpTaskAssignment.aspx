﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="DumpTaskAssignment.aspx.cs" Inherits="Evo.DumpTaskAssignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2>Dump Assignment</h2>
        <asp:UpdatePanel runat="server" ID="upGrid">
            <ContentTemplate>
                <div class="row">
                    <div class=" col m12">
                        <div class="portlet light ">
                            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                            <div class="portlet-body">
                                <div class="data__table">
                                    <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%-- <asp:TemplateField HeaderText="Main Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBenchLoc" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dump Date" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblDumpDateR" runat="server" Text='<%# Eval("DumpDateR","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="dashboard__block dashboard__block--asign">
            <h3>Assign Task</h3>
            <asp:Panel ID="PanelCropHealth" Visible="false" runat="server">
                <br />
                <h2 class="text-left">Crop Health Report </h2>

                <br />
                <div class="portlet-body">
                    <div class="data__table">
                        <asp:GridView ID="gvCropHealth" runat="server" AutoGenerateColumns="False"
                            class="striped" AllowSorting="true"
                            GridLines="None" DataKeyNames="chid"
                            ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderText="Type of Problem" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("typeofProblem")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cause of Problem" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("Causeofproblem")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Severity of Problem" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitem" runat="server" Text='<%# Eval("Severityofproblem")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of Trays" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("NoTrays")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="% of Damage" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("PerDamage")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="New Estimated Ship Date" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("Date","{0:MM/dd/yyyy}")  %>'></asp:Label>
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
                    <div class="row">

                        <div class="col-lg-12">
                            <asp:Label ID="lblCommment" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <br />

            <div id="userinput" runat="server" class="assign__task d-flex">
                <asp:Panel ID="pnlint" runat="server">
                    <div class="row">
                        <div class="mb-3 col-12 col-md-auto">
                            <label>Operator </label>
                            <asp:DropDownList ID="ddlOperator" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="mb-3 col-12 col-md-auto">
                            <label class="d-block">Dump Date</label>
                            <asp:TextBox ID="txtDumpDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="mb-3 col-12 col-md-auto">

                            <label>Quantity of Tray </label>
                            <asp:TextBox ID="txtQuantityofTray" runat="server" CssClass="input__control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" runat="server"
                                Enabled="True" TargetControlID="txtQuantityofTray" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="mb-3 col-12 col-md-auto">
                            <label>Comments</label>
                            <asp:TextBox ID="txtNotes" TextMode="Multiline" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="" class="mb-3 col-12 col-md-auto align-self-end">
                            <br />
                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                            <asp:Button Text="Reset" ID="btnReset" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnReset_Click" />
                        </div>

                    </div>
                </asp:Panel>
            </div>
        </div>

    </div>
</asp:Content>
