<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GreenHouseTaskCompletion.aspx.cs" Inherits="Evo.GreenHouseTaskCompletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
            <h2>Germination Count Task Completion</h2>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" OnRowDataBound="gvGerm_RowDataBound"
                                    GridLines="None" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" Width="100%">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblGTAID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                <%--  <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />--%>

                                                <asp:HyperLink ID="lnkJobID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Main Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreenHouseID" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label20" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Germination Count Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("GermDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Germination Count Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGermNo" runat="server" Text='<%# Eval("GermNo")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Trays inspected" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinstray" runat="server" Text='<%# Eval("#TraysInspected")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Due Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("InspectionDueDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notes" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label46" runat="server" Text='<%# Eval("Notes")  %>'></asp:Label>
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
                                            <asp:Label ID="txtDate" runat="server" Text='<%# Eval("Date","{0:MM/dd/yyyy}")  %>'></asp:Label>
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
                        <br />

                        <br />

                        <div class="row">
                            <label>Commment:</label>
                            <div class="col-lg-12">
                                <asp:Label ID="lblCommment" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>





                <asp:Panel ID="PanelViewGJob" Visible="false" runat="server">
                    <br />
                
                    
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="GridViewGDetails" runat="server" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" OnRowDataBound="GridViewGDetails_RowDataBound"
                                GridLines="None"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInspectionDate" runat="server" Text='<%# Eval("InspectionDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="#TraysInspected" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTraysInspected" runat="server" Text="" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Germination%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGermination" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#BadPlants" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBadPlants" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="GermVigor" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGermVigor" runat="server" Text='<%# Eval("GermVigor")  %>'></asp:Label>
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
                        <br />

                        <br />

                        <div class="row">
                            <label>Commment:</label>
                            <div class="col-lg-12">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <div id="userinput" runat="server" class="assign__task d-flex">
                    <asp:Panel ID="pnlint" runat="server">
                        <div class="row">
                            <div class="col">
                                <label>Germination Count Date </label>
                                <asp:TextBox ID="txtInspectionDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col">
                                <label># Trays</label>
                                <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" OnTextChanged="txtTrays_TextChanged" AutoPostBack="true" class="input__control"></asp:TextBox>
                                <asp:RangeValidator ID="Range1" ControlToValidate="txtTrays" MinimumValue="1" MaximumValue="20" Type="Integer" Text="The value must be from 1 to 20" runat="server"></asp:RangeValidator>

                                <asp:Label runat="server" ID="lblSeedlot" Visible="false"></asp:Label>
                            </div>

                        </div>

                        <asp:Panel ID="PanelViewDetails" Visible="false" runat="server">

                            <div class="data__table">


                                <asp:GridView ID="GridSplitJob" runat="server" ShowFooter="true" Width="30%"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="RowNumber" HeaderStyle-Width="10%" HeaderText="Tray #" />


                                        <asp:TemplateField  HeaderStyle-Width="20%" HeaderText="Bad Plants #">
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtTrays" CssClass="input__control" runat="server"></asp:TextBox>


                                            </ItemTemplate>



                                        </asp:TemplateField>
                                    </Columns>


                                </asp:GridView>
                            </div>
                            <br />
                            <div class="col align-self-end">
                                <asp:Button ID="sbtTray" runat="server" Text="Calculate" CssClass="bttn bttn-primary bttn-action" OnClick="sbtTray_Click" />
                            </div>
                            <br />


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

                                        <%-- <asp:TableHeaderCell>
                                            <label>Bad Health</label>
                                    </asp:TableHeaderCell>--%>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>

                                        <asp:TableCell>
                                            <asp:Label ID="lblJobid" runat="server"></asp:Label>
                                            <asp:Label ID="lblwoid" runat="server" Visible="false"></asp:Label>
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

                                        <%--<asp:TableCell>
                                        <asp:Label ID="lblcrophealth" runat="server"></asp:Label>
                                    </asp:TableCell>--%>
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
                    </asp:Panel>
                </div>
            </div>
        </div>
</asp:Content>
