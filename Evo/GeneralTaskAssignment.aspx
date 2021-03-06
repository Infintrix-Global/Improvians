﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GeneralTaskAssignment.aspx.cs" Inherits="Evo.GeneralTaskAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2>General Task Assignment</h2>

        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="gvTask" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true"
                                GridLines="None" PageSize="10" OnPageIndexChanging="gvTask_PageIndexChanging"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                            <%--  <asp:Label ID="Label2" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>--%>
                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBenchLocation" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotaltrays" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaskType" runat="server" Text='<%# Eval("TaskType")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Move From" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label21" runat="server" Text='<%# Eval("MoveFrom")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Move To" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label22" runat="server" Text='<%# Eval("MoveTo")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="General Task Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>

                                            <asp:Label ID="lblGeneralDates" runat="server" Text='<%# Eval("GeneralTaskDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmp" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
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

                        <div class="col-12 col-md-4 col-lg-3 mb-3">
                            <label>Operator </label>
                            <asp:DropDownList ID="ddlOperator" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>

                        <div class="col-12 col-md-4 col-lg-3 mb-3">
                            <label>Task Type</label>

                            <asp:DropDownList ID="ddlTaskType" runat="server" OnSelectedIndexChanged="ddlTaskType_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                            <%--    <asp:ListItem Text="--Select--" Value="0" />
                                <asp:ListItem Text="Add Bird Netting" Value="1" />
                                <asp:ListItem Text="Remove Bird Netting" Value="2" />
                                <asp:ListItem Text="Move" Value="3" />
                                <asp:ListItem Text="Other" Value="4" />--%>
                            </asp:DropDownList>
                            <%--<span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>--%>
                        </div>

                        <div class="mb-3 col-12 col-md-auto">
                            <label>Comments</label>
                            <asp:TextBox ID="txtNotes" TextMode="Multiline" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="d-none d-sm-block w-100"></div>
                        <div class="col-xl-3" id="divFrom" style="display: none;" runat="server">
                            <label>From</label>
                            <asp:TextBox ID="txtFrom" runat="server" CssClass="input__control"></asp:TextBox>


                        </div>

                        <div class="col-xl-3" id="divTo" style="display: none;" runat="server">
                            <label>To</label>
                            <asp:TextBox ID="txtTo" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="col-auto">
                            <br />
                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                        </div>
                        <div class="col-auto">
                            <br />
                            <asp:Button Text="Reset" ID="btnReset" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
