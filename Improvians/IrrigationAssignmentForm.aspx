<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="IrrigationAssignmentForm.aspx.cs" Inherits="Evo.IrrigationAssignmentForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
      <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation">
            Irrigation Task</h2>

            <div class="row">
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Job No</label>
                <asp:TextBox ID="txtSearchJobNo" runat="server" OnTextChanged="txtSearchJobNo_TextChanged" AutoPostBack="true" class="input__control robotomd"></asp:TextBox>

                <cc1:autocompleteextender servicemethod="SearchCustomers"
                    minimumprefixlength="2"
                    completioninterval="100" enablecaching="false" completionsetcount="10"
                    targetcontrolid="txtSearchJobNo"
                    id="AutoCompleteExtender1" runat="server" firstrowselected="false">
                </cc1:autocompleteextender>

            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Bench Location </label>
                <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

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
                <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>To Date </label>
                <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSearch_Click" />
                <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSearchRest_Click" />

            </div>



        </div>

        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging" DataKeyNames="IrrigationCode,TaskRequestKey"
                                GridLines="None" OnRowCommand="gvGerm_RowCommand" OnRowDataBound="gvGerm_RowDataBound"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                  


                                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            <asp:Label ID="lblIrrigationCode" Visible="false" runat="server" Text='<%#Eval("IrrigationCode") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bench Location">
                                        <ItemTemplate>

                                            <asp:Label ID="lblGreenHouseID" runat="server" Text='<%#Eval("BenchLocation") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Spray Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSprayDate" runat="server" Text='<%# Eval("SprayDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Number of Passes" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("WaterRequired")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignedBy" data-head="Assigened By" runat="server" Text='<%# Eval("AssignedBy")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action" CommandName="Assign" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

                                            <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                            <asp:Button ID="btnView" runat="server" Width="140px" CommandName="ViewDetails" Text="View Job Details" CssClass="bttn bttn-primary bttn-action my-1" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

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

    </div>
</asp:Content>
