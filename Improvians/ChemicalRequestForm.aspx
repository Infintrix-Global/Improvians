<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="ChemicalRequestForm.aspx.cs" Inherits="Evo.ChemicalRequestForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon mb-3">
            <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Chemical">
            Chemical
        </h2>

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
            <%--<div class="col-lg-3 col-md-4">--%>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Bench Location </label>

                <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                <%--     <span class="error_message">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
            <%--<div class="col-lg-3 col-md-4 mb-3 mb-lg-0">--%>
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
        <%--        <div class="row align-items-end">--%>
        <div class="row mb-1 mb-md-4 align-items-end">

            <div class="col-lg-2 col-md-4 col-sm-12 mb-3">
                <label>Job Source </label>
                <asp:DropDownList ID="RadioButtonListSourse" runat="server" OnSelectedIndexChanged="RadioButtonListSourse_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Navision" Value="Manual"></asp:ListItem>
                    <asp:ListItem Text="App" Value="App"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-3 col-md-4 col-sm-6 mb-3">
                <label>From Date</label>
                <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
            </div>
            <div class="col-lg-3 col-md-4 col-sm-6 mb-3">
                <label>To Date </label>
                <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
            </div>
            <div class="col-xl-4 col-12 mb-3">
                <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearch_Click1" />
                <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="ml-2 bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />
                <%--<asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" ValidationGroup="x" />--%>
                <asp:Button ID="btnManual" runat="server" Visible="false" Text="Manual Request" CssClass="ml-2 bttn bttn-primary bttn-action" OnClick="btnManual_Click1" />
                <%-- <asp:Button ID="btnJob" runat="server" Text="JobBuildUp" CssClass="bttn bttn-primary bttn-action" OnClick="btnJob_Click" />--%>
            </div>
        </div>

        <div class="portlet light pt-3">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">
                <div class="data__table">
                    <asp:GridView ID="gvFer" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        class="striped" AllowSorting="true" PageSize="10" DataKeyNames="GreenHouseID,jobcode,ChemicalCode,jid,TaskRequestKey" OnRowDataBound="gvFer_RowDataBound"
                        GridLines="None" OnRowCommand="gvFer_RowCommand" OnPageIndexChanging="gvFer_PageIndexChanging"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblBatchlocation1" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>

                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />
                                    <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>' Visible="false"></asp:Label>

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
                            <asp:TemplateField HeaderText="Chemical Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblChemDate" runat="server" Text='<%# Eval("ChemicalSeedDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlantDueDate" runat="server" Text='<%# Eval("PlantDueDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
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

                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelect" runat="server" Text="Job Build Up" CssClass="bttn bttn-primary bttn-action my-1 mx-auto d-block w-100" CommandName="Job" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                    <asp:Button ID="btnStart" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1 mx-auto d-block w-100" CommandName="GStart" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <%--  <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateCountNo" runat="server" Text='<%# Eval("DateCountNo")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Facility Location" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
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
            </div>
        </div>
        <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
</asp:Content>
