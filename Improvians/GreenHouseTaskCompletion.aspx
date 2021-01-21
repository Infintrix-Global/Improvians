<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GreenHouseTaskCompletion.aspx.cs" Inherits="Improvians.GreenHouseTaskCompletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="site__container">
            <h2>Germination Task Completion</h2>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
                                    GridLines="None" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

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
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("PutAwayMainLocation")  %>'></asp:Label>
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
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Trays inspected" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("#TraysInspected")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Due Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("InspectionDueDate")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notes" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("Notes")  %>'></asp:Label>
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
                <h3>Task Completion</h3>
                <div id="userinput" runat="server" class="assign__task d-flex">
                    <asp:Panel ID="pnlint" runat="server">
                        <div class="row">
                            <div class="col">
                                <label>Inspection Date </label>
                                <asp:TextBox ID="txtInspectionDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col">
                                <label># Trays</label>
                                <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" OnTextChanged="txtTrays_TextChanged" AutoPostBack="true" class="input__control"></asp:TextBox>
                                <asp:Label runat="server" ID="lblSeedlot" Visible="false"></asp:Label>
                            </div>
                            <div class="col align-self-end">
                                <asp:Button ID="sbtTray" runat="server" Text="Calculate" CssClass="bttn bttn-primary bttn-action" OnClick="sbtTray_Click" />
                            </div>
                        </div>
                        <div class="row py-3">
                            <div class="col-12">
                                <asp:Table ID="tbltray" runat="server" class="data__table">
                                </asp:Table>
                            </div>
                        </div>
                        <asp:Panel ID="pnlUpdated" runat="server">
                            <asp:Table ID="tblupdate" runat="server" class="data__table">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell>
                                            <label>JobID</label>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell>
                                            <label>#Trays</label>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell>
                                            <label>Germination %</label>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell>
                                            <label>#Bad Plants</label>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell>
                                            <label>Germ Vigor</label>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell>
                                            <label>Bad Health</label>
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>

                                    <asp:TableCell>
                                        <asp:Label ID="lblJobid" runat="server"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="lblnotrays" runat="server"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="lblGerm" runat="server"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="lblbadplants" runat="server"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="lblgermvigor" runat="server"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="lblcrophealth" runat="server"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                        </asp:Panel>
                        <div class="clearfix"></div>
                        <br />
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="col-auto">
                                <asp:Button Text="Reset" ID="btnReset" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
