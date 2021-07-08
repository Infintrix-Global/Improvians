<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="FertilizerTaskStart.aspx.cs" Inherits="Evo.FertilizerTaskStart1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function InIEvent() {
            if ($('.jsDatePicker').length > 0) {
                $('.jsDatePicker').datepicker();
            }
            if ($('.SelectBox').length > 0) {
                $('.SelectBox').SumoSelect({ placeholder: '--- Select ---' });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization">
            Fertilization
        </h2>

        <div class="row py-3 no-gutters align-items-center">
            <h4 class="mb-0 col-auto pr-2">Bench Location:
            </h4>
            <div class="col-auto">
                <asp:Label ID="lblbench" runat="server"></asp:Label>
            </div>
        </div>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel runat="server" ID="upFilter">
            <ContentTemplate>
                <div class="portlet light mt-2">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                            <h3>Selected Job</h3>
                            <asp:GridView ID="gvFer" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" PageSize="10"
                                GridLines="None"
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
                                            <asp:Label ID="lblJidF" runat="server" Text='<%# Eval("jid")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblPlantDueDate" runat="server" Text='<%# Eval("PlantDueDate")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblPlantReadyDate" runat="server" Text='<%# Eval("PlantReadyDate")  %>' Visible="false"></asp:Label>
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

                                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeededDate1" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

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

                        <h3 class="mt-3 mt-md-4 mb-3">Other Jobs</h3>

                        <div class="row mb-3">
                            <div class="col-auto col-lg-4">
                                <asp:RadioButtonList ID="RadioBench" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioBench_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Bench (A/B)" Value="1" class="custom-control custom-radio mr-2 my-2"></asp:ListItem>
                                    <asp:ListItem Text="Benches in house" Value="2" class="custom-control custom-radio mr-2 my-2"></asp:ListItem>
                                    <asp:ListItem Text="House" Value="3" class="custom-control custom-radio my-2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-lg-2">
                                <asp:Button ID="btnSearch" runat="server" Visible="false" CssClass="bttn bttn-primary bttn-action mr-2" OnClick="btnSearch_Click" Text="Search" />
                                <asp:Button Text="Reset" ID="btnResetSearch" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Panel ID="PanelBench" Visible="false" runat="server">
                                    <asp:Label ID="lblBench1" Visible="false" runat="server" Text="Label"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="PanelBenchesInHouse" Visible="false" runat="server">
                                    <asp:ListBox ID="ListBoxBenchesInHouse" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ListBoxBenchesInHouse_SelectedIndexChanged" CssClass="mb-4 p-2 pr-3" Height="150px" runat="server"></asp:ListBox>
                                </asp:Panel>
                                <asp:Panel ID="PanelHouse" Visible="false" runat="server">
                                </asp:Panel>
                            </div>
                        </div>

                        <div class="data__table data__table-height">
                            <asp:GridView ID="gvJobHistory" runat="server" AutoGenerateColumns="False"
                                class="striped"
                                GridLines="None"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGreenHouse1" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID1" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            <asp:Label ID="lblwo1" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblGrowerputawayID1" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblJid" runat="server" Text='<%# Eval("jid")  %>' Visible="false"></asp:Label>
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
                                            <asp:Label ID="lblFacility1" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotTray1" runat="server" Text='<%# Eval("Trays","{0:####}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeededDate1" Visible="false" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            <asp:Label ID="lblGenusCodeH" Visible="false" runat="server" Text='<%# Eval("GenusCode")  %>'></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="dashboard__block dashboard__block--asign">
            <div id="userinput" runat="server" class="assign__task d-flex">
                <asp:Panel ID="pnlint" runat="server">
                    <h3>Assign Task</h3>
                    <div class="row">


                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>Spray Date </label>

                            <asp:TextBox ID="txtDate" runat="server" CssClass="jsDatePicker input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="Label3" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDate" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Date" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>


                        <div class="col-sm-6 col-md-4 col-lg-3 mb-3">
                            <label>
                                <asp:Label ID="lbltype" runat="server" Text="Fertilizer"></asp:Label></label><br />
                            <asp:DropDownList ID="ddlFertilizer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col-sm-6 col-md-4 col-lg-3 mb-3">
                            <label>Trays</label>
                            <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtTrays" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="Filteredtextboxextender2" runat="server"
                                Enabled="True" TargetControlID="txtTrays" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                        </div>

                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>SQFT of Bench </label>

                            <asp:TextBox ID="txtSQFT" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="Label2" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSQFT" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter SQFT" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>

                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label class="pr-2 pr-lg-0 d-lg-block"># of passes</label>
                            <asp:TextBox ID="txtNoOfPasses" CssClass="input__control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>Concentration [ppm]</label>
                            <asp:TextBox ID="txtQty" Text="150" runat="server" CssClass="input__control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" runat="server"
                                Enabled="True" TargetControlID="txtQty" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQty" ValidationGroup="e" SetFocusOnError="true" ErrorMessage="Please Enter Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="col-sm-6 col-lg col-xl-3 mb-3">
                            <label>Minimum Days Until Next Fertilization</label>
                            <asp:TextBox ID="txtResetSprayTaskForDays" runat="server" CssClass="input__control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="txtResetSprayTaskForDays" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="col-sm-6 col-md-4 col-lg-3 mb-3">
                            <label>Comments</label>
                            <asp:TextBox ID="txtComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
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
