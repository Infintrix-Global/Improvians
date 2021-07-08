<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="FertilizerTaskReq.aspx.cs" Inherits="Evo.FertilizerTaskReq" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
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
        <h2 class="head__title-icon mb-3">
            <img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization / Chemical">
            Fertilization
        </h2>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel runat="server" ID="upFilter">
            <ContentTemplate>
                <div class="row">
                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>Job No</label>
                        <asp:TextBox ID="txtSearchJobNo" runat="server" OnTextChanged="txtSearchJobNo_TextChanged" AutoPostBack="true" class="input__control robotomd"></asp:TextBox>

                        <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtSearchJobNo"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                        </cc1:AutoCompleteExtender>

                    </div>
                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>Bench Location </label>
                        <%--<asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>--%>
                        <asp:ListBox ID="lstBenchLocation" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lstBenchLocation_SelectedIndexChanged" runat="server" CssClass="SelectBox custom__dropdown robotomd"></asp:ListBox>

                        <%-- <asp:TextBox ID="txtBatchLocation" runat="server" OnTextChanged="txtBatchLocation_TextChanged" AutoPostBack="true" class="input__control robotomd"></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="SearchBenchLocation"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="txtBatchLocation"
                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                </cc1:AutoCompleteExtender>--%>
                    </div>

                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>Job No </label>
                        <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>

                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>Assigned By </label>
                        <asp:DropDownList ID="ddlAssignedBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignedBy_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>

                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>Customer </label>
                        <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>

                </div>

                <div class="row mb-1 mb-md-4 align-items-end">
                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>Crop </label>
                        <asp:DropDownList ID="ddlCrop" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>

                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>Job Source </label>
                        <asp:DropDownList ID="RadioButtonListSourse" runat="server" OnSelectedIndexChanged="RadioButtonListSourse_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                            <asp:ListItem Text="Navision" Value="Manual"></asp:ListItem>
                            <asp:ListItem Text="App" Value="App"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>Task Type</label>
                        <asp:DropDownList ID="RadioButtonListGno" runat="server" OnSelectedIndexChanged="RadioButtonListF_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>From Date</label>
                        <asp:TextBox ID="txtFromDate" runat="server" class="jsDatePicker input__control robotomd"></asp:TextBox>
                    </div>
                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <label>To Date </label>
                        <asp:TextBox ID="txtToDate" runat="server" class="jsDatePicker input__control robotomd"></asp:TextBox>
                    </div>
                    <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                        <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSearch_Click" />
                        <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSearchRest_Click" />
                        <asp:Button ID="btnManual" runat="server" Visible="false" Text="Manual Request" CssClass="bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnManual_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="portlet light pt-3">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">
                <div class="data__table">
                    <asp:UpdatePanel runat="server" ID="upGrid">
                        <ContentTemplate>
                            <asp:GridView ID="gvFer" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" PageSize="10" DataKeyNames="GreenHouseID,jobcode,FertilizationCode,jid,TaskRequestKey,AssignedBy" OnRowDataBound="gvFer_RowDataBound"
                                GridLines="None" OnRowCommand="gvFer_RowCommand" OnPageIndexChanging="gvFer_PageIndexChanging"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bench Location" ItemStyle-Width="160px" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGreenHouseID" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="60px" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />
                                            <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblTaskRequestKey" runat="server" Text='<%# Eval("TaskRequestKey")  %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomer" data-head="Customer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays","{0:####}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlantDueDate" runat="server" Text='<%# Eval("PlantDueDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Fertilization Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFertilizeDate" runat="server" Text='<%# Eval("FertilizeSeedDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateCountNo" runat="server" Text='<%# Eval("DateCountNo")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job source" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsource" runat="server" Text='<%# Eval("RequestType")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignedBy" data-head="Assigened By" runat="server" Text='<%# Eval("AssignedBy")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="250px" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Job" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                            <asp:Button ID="btnStart" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="GStart" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <asp:Panel ID="Panel1" Visible="false" runat="server">
            <div class="dashboard__block dashboard__block--asign">
                <div id="userinput" runat="server" class="assign__task d-flex" visible="false">
                    <asp:Panel ID="pnlint" runat="server">
                        <div class="row">
                            <div class="col-12 col-md-4 col-lg-3">
                                <label class="d-block">Assignment </label>
                                <asp:DropDownList ID="ddlsupervisor" runat="server" class="d-block custom__dropdown"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsupervisor" ValidationGroup="e"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Supervisor" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>

                            <div class="col-12 col-md-4 col-lg-auto">
                                <label>Type of Request</label>

                                <asp:RadioButtonList ID="radtype" runat="server" OnSelectedIndexChanged="radtype_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Chemical" Value="Chemical" class="custom-control custom-radio mr-2 my-2"></asp:ListItem>
                                    <asp:ListItem Text="Fertilizer" Value="Fertilizer" class="custom-control custom-radio my-2" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-12 col-md-4 col-lg-3">
                                <label>
                                    <asp:Label ID="lbltype" runat="server" Text="Fertilizer"></asp:Label></label>
                                <asp:DropDownList ID="ddlFertilizer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlFertilizer" ValidationGroup="e"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Fertilizer" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="w-100"></div>
                            <div class="col-12 col-sm-6 col-md-3">
                                <label>Quantity</label>
                                <asp:TextBox ID="txtQty" AutoPostBack="true" TextMode="Number" OnTextChanged="txtQty_TextChanged" runat="server" CssClass="input__control"></asp:TextBox>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQty" ValidationGroup="e"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3">
                                <label>Unit </label>
                                <asp:DropDownList ID="ddlUnit" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUnit" ValidationGroup="e"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Unit" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3 mb-3">
                                <label>Trays</label>
                                <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtTrays" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3">
                                <label>SQFT </label>

                                <asp:TextBox ID="txtSQFT" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                                <span class="error_message">
                                    <asp:Label ID="Label5" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSQFT" ValidationGroup="e"
                                        SetFocusOnError="true" ErrorMessage="Please Enter SQFT" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-12">
                                <div class="data__table">
                                    <asp:Panel ID="pnlPoints" runat="server" CssClass="pnlpoint">
                                        <asp:GridView runat="server" ID="gvFerDetails" AutoGenerateColumns="false" class="Grid1 mb-3"
                                            GridLines="None" CaptionAlign="NotSet" Width="801px" ForeColor="Black" OnRowDeleting="gvFerDetails_RowDeleting"
                                            ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Fertilizer" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFertilizer" runat="server" Text='<%# Bind("[Fertilizer]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="QTY" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" Text='<%# Bind("[Quantity]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit of Measurement" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("[Unit]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tray" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTray" runat="server" Text='<%# Bind("[Tray]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SQFT" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSQFT" runat="server" Text='<%# Bind("[SQFT]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Button class="submit-bttn bttn bttn-primary mb-0" ID="deletebtn" runat="server" CommandName="Delete"
                                                            Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="col-auto">
                                <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnSubmit_Click" />
                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

        </asp:Panel>
        <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
</asp:Content>
