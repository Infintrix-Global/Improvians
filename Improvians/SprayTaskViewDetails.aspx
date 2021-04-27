<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SprayTaskViewDetails.aspx.cs" Inherits="Evo.SprayTaskViewDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="text-left">Fertilization Task Completion </h2>
        <%-- <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>--%>

        <div class="row">
            <div class="col-lg-6">
                <h3 class="robotobold">
                    <label>Bench Location</label><br />
                    <asp:Label ID="lblBenchLocation" runat="server" Text=""></asp:Label>
                </h3>
            </div>
        </div>
        <br />
        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="gvSpray" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" PageSize="10" DataKeyNames="wo,jobcode,GrowerPutAwayId"
                                GridLines="None" OnRowCommand="gvSpray_RowCommand" OnPageIndexChanging="gvSpray_PageIndexChanging"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                            <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblFertilizationId" runat="server" Text='<%# Eval("FertilizationId")  %>' Visible="false"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
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


            <div id="userinput" runat="server">
                <asp:Panel ID="PanelCropHealth" Visible="false" runat="server">
                    <br />
                    <h2 class="text-left">Crop Health Report </h2>
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
                <asp:Panel ID="pnlint" runat="server">
                    <h2 class="text-left">
                     
                         Task Request Flow</h2>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="GridViewDetails"
                                AutoGenerateColumns="false" runat="server">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Assigned By">
                                        <ItemTemplate>

                                            <asp:Label ID="lblAassignedTo" runat="server" Text='<%#Bind("Aassignedby") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Assigned To">
                                        <ItemTemplate>

                                            <asp:Label ID="lblAassignedby" runat="server" Text='<%#Bind("AassignedTo") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Fertilizer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFertilizer" runat="server" Text='<%#Bind("Fertilizer") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Concentration [ppm]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray">
                                        <ItemTemplate>

                                            <asp:Label ID="lblTray" runat="server" Text='<%#Bind("Tray") %>'></asp:Label>


                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SQFT of Bench">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSQFT" runat="server" Text='<%#Bind("SQFT") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Bench Irrigation Flow Rate [Gallons/min]">
                                            <ItemTemplate>

                                                <asp:Label ID="lblBenchIrrigationFlowRat" runat="server" Text='<%#Bind("BenchIrrigationFlowRat") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bench Irrigation Coverage [Gallons/Sqft]">
                                            <ItemTemplate>

                                                <asp:Label ID="lblBenchIrrigationCoverage" runat="server" Text='<%#Bind("BenchIrrigationCoverage") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Spray Coverage per minutes [sqft/min]">
                                            <ItemTemplate>

                                                <asp:Label ID="lblSprayCoverageperminutes" runat="server" Text='<%#Bind("SprayCoverageperminutes") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>--%>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                    </div>

                </asp:Panel>
                <br />
                <asp:Panel ID="PanlTaskComplition" runat="server">
                    <h2 class="text-left">
                     
                        Completion  </h2>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="GridViewCompletion"
                                AutoGenerateColumns="false" runat="server">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Completed By">
                                        <ItemTemplate>

                                            <asp:Label ID="lblCAassignedby" runat="server" Text='<%#Bind("AassignedTo") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Spray Date">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSprayDate" runat="server" Text='<%#Bind("SprayDate","{0:MM/dd/yyyy}") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>

                                            <asp:Label ID="lblCommentsC" runat="server" Text='<%#Bind("Nots") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                    </div>

                </asp:Panel>

            </div>
        </div>


        <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
</asp:Content>
