<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="IrrigationReqManual.aspx.cs" Inherits="Evo.IrrigationReqManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main main__header">
        <div class="site__container">
            <h2>Irrigation </h2>
            <div class="row">
                <div class="col m3">
                    <label>Bench Location </label>
                    <span style="color: red">*</span>
                    <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    <span class="error_message">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                            SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                    </span>
                </div>
                <div class="col m3">
                    <label>Job No </label>
                    <asp:DropDownList ID="ddlJobNo" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <div class="col m3">
                    <label>Customer </label>
                    <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col m3">
                </div>
                <div class="col m3">
                </div>
                <div class="col m3">
                </div>
                <div class="col m3">
                    <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnResetSearch_Click" />
                    <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" ValidationGroup="x" CssClass="bttn bttn-primary bttn-action my-1" />

                </div>

            </div>
            <br />
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
                        <asp:Button ID="btnSearch" runat="server" Visible="false" CssClass="bttn bttn-primary bttn-action mr-2" ValidationGroup="x" OnClick="btnSearch_Click" Text="Search" />
                        <asp:Button Text="Reset" ID="btnResetSearch" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click1" />
                    </div>
                    <div class="col-lg-6">
                    </div>
                </div>
            </asp:Panel>
            <br />
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

                <div class="col-lg-2">
                </div>
                <div class="col-lg-6">
                </div>
            </div>
            <br />

            <div class="data__table">
                <asp:GridView ID="GridIrrigation" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="GridIrrigation_PageIndexChanging"
                    GridLines="None" OnRowCommand="GridIrrigation_RowCommand" DataKeyNames="wo,jobcode,GrowerPutAwayId"
                    ShowHeaderWhenEmpty="True" Width="100%">
                    <Columns>

                        <%--<asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="5%">
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBoxall" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:CheckBox runat="server" ID="chkSelect"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                 <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

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


                        <%--                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
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



                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--   <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>



                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--  <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>


                                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                                <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Eval("wo")  %>'></asp:Button>

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

            <div class="text-left dashboard__block my-4">

                <div id="userinput" runat="server" visible="false" class="row justify-content-center">
                    <div class="col-12">
                        <div class="row">

                            <div class="col-12 col-sm-6 col-lg-3">
                                <label>Assignment</label>
                                <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            </div>

                            <%-- <div class="col-12 col-sm-6 col-lg-3">
                                <label>No. Of Trays to be Irrigated</label>

                                <asp:TextBox ID="txtIrrigatedNoTrays" ReadOnly="true" class="input__control" placeholder="Enter No." runat="server"></asp:TextBox>

                            </div>--%>

                            <div class="col-12 col-sm-6 col-md-auto">
                                <label class="pr-2 pr-lg-0 d-lg-block"># of passes</label>



                                <asp:TextBox ID="txtWaterRequired" class="mb-0 input__control input__control-auto" placeholder="" runat="server"></asp:TextBox>

                            </div>

                            <%-- <div class="col-12  col-sm-6 col-md-auto">
                                <label class="pr-2 pr-lg-0 d-lg-block">Irrigation Duration</label>
                               
                                <asp:TextBox ID="txtIrrigationDuration" class="mb-0 input__control input__control-auto" placeholder="00:00" runat="server"></asp:TextBox>

                            </div>--%>
                        </div>

                        <div class="row align-items-center mt-sm-3">
                            <div class="col-12 mt-4 mb-3 my-sm-0 col-sm-auto">
                                <h4 class="mb-0">Schedule:</h4>
                            </div>
                            <div class="col-auto">
                                <label class="d-block">Spray Date</label>

                                <asp:TextBox ID="txtSprayDate" class="input__control input__control-auto" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                            <%--  <div class="col-auto">
                                <label class="d-block">Spray Time</label>

                                <asp:TextBox ID="txtSprayTime" TextMode="Time" class="input__control input__control-auto" placeholder="00:00" runat="server"></asp:TextBox>
                            </div>--%>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Reset Spray Task For Days</label>
                                <asp:TextBox ID="txtResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                            </div>
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                        <br />
                        <div class="row align-items-center mt-sm-3">
                            <div class="col-12 col-sm-6 col-lg-4">

                                <asp:TextBox ID="txtNotes" TextMode="MultiLine" class="w-100 input__control" placeholder="Notes" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-12 my-3">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />

                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />

                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>
    </div>
</asp:Content>
