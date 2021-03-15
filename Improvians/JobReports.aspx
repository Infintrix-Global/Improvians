<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="JobReports.aspx.cs" Inherits="Evo.JobReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main__header">
        <div class="site__container">
            <div class="row">
                <div class="col m12">
                    <div class="mb-2">
                        <h2>Job Information Page</h2>
                    </div>
                    <br />
                    <div class="row" id="divFilter" runat="server" visible="false">

                        <div class="col-lg-3">
                            <label>Job No</label>
                            <asp:TextBox ID="txtSearchJobNo" runat="server" Text="JB" class="input__control robotomd"></asp:TextBox>

                            <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                                MinimumPrefixLength="2"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="txtSearchJobNo"
                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                            </cc1:AutoCompleteExtender>
                        </div>
                        <div class="col-auto">
                            <br />
                            <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearch_Click" />
                            <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />
                        </div>
                    </div>
                    <div class="row" id="divFilter1" runat="server" visible="false">
                        <div class="col-lg-3">
                            <label>Bench Location </label>
                            <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <label>Job No </label>
                            <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="portlet light ">

                        <div class="portlet-body">

                            <asp:Panel ID="Panel1" runat="server">
                                <div class="data__table mb-2">
                                    <asp:GridView ID="gv1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Prod No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("wo")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <div class="data__table">
                                    <asp:GridView ID="GV2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Cust. No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                   
                                                    <asp:Label ID="lblcusno" runat="server" Text='<%# Eval("cusno")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Cust. Name" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item. No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblitem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Crop" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of trays" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("trays_actual")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("actual_date")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Due Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>                                                  
                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("due_date")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <hr class="my-4">

            <div class="row">
                <div class=" col m12">
                    <div class="mb-2"><strong>Lot Details</strong></div>
                    <div class="portlet light ">

                        <div class="portlet-body">

                            <asp:Panel ID="Panel2" runat="server">
                                <div class="data__table">
                                    <asp:GridView ID="Gv3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Posting Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblSD" runat="server" Text='<%# Eval("SeededDate")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Planning Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblCD" runat="server" Text='<%# Eval("CreatedOn")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lot ID" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblLOTID" runat="server" Text='<%# Eval("LotID")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Number of seeds" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblNS" runat="server" Text='<%# Eval("NumberOfSeed")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <hr class="my-4">

            <div class="row">
                <div class=" col m12">
                    <div class="mb-2"><strong>Routing Steps</strong></div>
                    <div class="portlet light ">

                        <div class="portlet-body">

                            <asp:Panel ID="Panel3" runat="server">
                                <div class="data__table">
                                    <asp:GridView ID="GV4" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblD" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Starting Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblSD" runat="server" Text='<%# Eval("StartingDate","{0:MM/dd/yyyy}")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="Operation No" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate> 
                                                    <asp:Label ID="lblON" runat="server" Text='<%# Eval("OperationNo")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         
                                            <asp:TemplateField HeaderText="RunTime" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRT" runat="server" Text='<%# Eval("RunTime")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                            <asp:TemplateField HeaderText="Completion Date " ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblED" runat="server" Text='<%# Eval("EndingDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Germ" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>                                                    
                                                    <asp:Label ID="lblG" runat="server" Text='<%# Eval("Germ")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <hr class="my-4">

            <div class="row">
                <div class=" col m12">
                    <div class="mb-2"><strong>Facility/House Detail</strong></div>
                    <div class="portlet light ">

                        <div class="portlet-body">

                            <asp:Panel ID="Panel4" runat="server">
                                <div class="data__table">
                                    <asp:GridView ID="GV5" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10" OnRowDataBound="GV5_RowDataBound"
                                        ShowHeaderWhenEmpty="True" Width="100%" OnRowEditing="GV5_RowEditing" OnRowUpdating="GV5_RowUpdating" OnRowCancelingEdit="GV5_RowCancelingEdit">
                                        <Columns>

                                            <asp:TemplateField HeaderText="House/Section" Visible="false" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                    <asp:Label ID="lblgrowerId" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>'></asp:Label>

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="House/Section" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                    <asp:Label ID="lblGHD" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>

                                                </ItemTemplate>
                                                <EditItemTemplate>


                                                    <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" DataTextField="GreenHouseId" DataValueField="GreenHouseId" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Trays" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTrays" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>

                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("Trays") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" CssClass="bttn bttn-primary bttn-action" CommandName="Edit" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btn_Update" runat="server" Text="Update" CssClass="bttn bttn-primary bttn-action" CommandName="Update" />
                                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="bttn bttn-primary bttn-action" CommandName="Cancel" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-xl-6">
                    <label>Comments </label>

                    <asp:TextBox ID="txtcomments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                </div>
                <div class="col-xl-6">
                </div>

            </div>
            <br />
            <h4 class="mt-4 mt-lg-3">Task Requests:</h4>

            <div class="task_request_assignments" id="task_request-group">

                <div class="task_request-buttons">
                    <button class="request__block-head collapsed" type="button" data-toggle="collapse" data-target="#germination_count">
                        <span class="">
                            <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Count" />
                            Germination Count
                        </span>
                    </button>

                    <button class="request__block-head collapsed" type="button" data-toggle="collapse" data-target="#fertilization_count">
                        <span class="">
                            <img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization">
                            Fertilization
                        </span>
                    </button>

                    <button class="request__block-head collapsed" type="button" data-toggle="collapse" data-target="#Chemical_count">
                        <span class="">
                            <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Chemical">
                            Chemical
                        </span>
                    </button>


                    <button class="request__block-head collapsed" type="button" data-toggle="collapse" data-target="#irrigation_count">
                        <span class="">
                            <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation" />
                            Irrigation
                        </span>
                    </button>

                    <button class="request__block-head collapsed" type="button" data-toggle="collapse" data-target="#plant_ready_count">
                        <span class="">
                            <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready" />
                            Plant Ready
                        </span>
                    </button>

                    <button class="request__block-head collapsed" type="button" data-toggle="collapse" data-target="#move_request">
                        <span class="">
                            <img src="./images/dashboard_move-request.png" width="137" height="134" alt="Move Request" />
                            Move Request
                        </span>
                    </button>

                    <button class="request__block-head collapsed" type="button" data-toggle="collapse" data-target="#dump_request">
                        <span class="">
                            <img src="./images/dashboard_dump-request.png" width="137" height="136" alt="Dump" />
                            Dump
                        </span>
                    </button>

                    <button class="request__block-head collapsed" type="button" data-toggle="collapse" data-target="#general_task_request">
                        <span class="">
                            <img src="./images/dashboard_general-task.png" width="137" height="134" alt="General Task" />
                            General Task
                        </span>
                    </button>

                </div>

                <div class="request__block">
                    <div id="germination_count" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">Germination Count</h2>
                            <br />
                            <div class="row">
                                <div class="col-xl-3">
                                    <label class="d-block">Assignment</label>
                                    <asp:DropDownList ID="ddlgerminationSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="col-xl-3">
                                    <label class="d-block">Germination Count Date</label>
                                    <asp:TextBox ID="txtGerDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                                </div>
                                <div class="col-xl-3">
                                    <label class="d-block">Number Of Trays To Inspect</label>
                                    <asp:TextBox ID="txtTGerTrays" TextMode="Number" runat="server" class="input__control robotomd"></asp:TextBox>
                                </div>
                                <div class="col-xl-3">
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="mb-xl-3 col-12 col-xl-6 align-self-end">
                                    <asp:Button Text="Submit" ID="btngerminationSumit" CssClass="bttn bttn-primary bttn-action" OnClick="btngerminationSumit_Click" runat="server" />
                                    <asp:Button Text="Reset" ID="btngerminationReset" runat="server" OnClick="btngerminationReset_Click" CssClass="bttn bttn-primary bttn-action" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="fertilization_count" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">Fertilization</h2>
                            <br />
                            <div class="row align-items-end">
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label class="d-block">Assignment</label>
                                    <asp:DropDownList ID="ddlFertilizationSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>

                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label class="d-block">Spray Date</label>
                                    <asp:TextBox ID="txtFDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>

                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label class="d-block">
                                        Fertilizer
                                    </label>
                                    <asp:DropDownList ID="ddlFertilizer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                </div>
                                <div class="mb-3 col-xl-2 col-md-6 col-12">
                                    <label class="d-block">Concentration [ppm]</label>
                                    <asp:TextBox ID="txtQty" TextMode="Number" Text="150" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="mb-3 col-xl-2 col-md-6 col-12">
                                    <label class="d-block">Trays</label>
                                    <asp:TextBox ID="txtFTrays" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="mb-3 col-xl-2 col-md-6 col-12">
                                    <label class="d-block">SQFT of Bench</label>
                                    <asp:TextBox ID="txtSQFT" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>

                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Reset Spray Task For Days</label>
                                    <asp:TextBox ID="txtResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                    <asp:TextBox ID="txtBenchIrrigationFlowRate" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                    <asp:TextBox ID="txtBenchIrrigationCoverage" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                    <asp:TextBox ID="txtSprayCoverageperminutes" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <%--<div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Bench Irrigation Flow Rate [Gallons/min]</label>
                                   
                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Bench Irrigation Coverage [Gallons/Sqft]</label>
                                 
                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Spray Coverage per minutes [sqft/min]</label>
                                   
                                </div>
                                --%>
                                <div class="mb-md-3 col-xl-3 col-md-6 col-12 align-self-end">


                                    <asp:Button Text="Submit" CausesValidation="true" ID="btnFSubmit" CssClass="mr-2 bttn bttn-primary" runat="server" OnClick="btnFSubmit_Click" />

                                    <asp:Button Text="Reset" ID="btnFReset" runat="server" CssClass="bttn bttn-primary" OnClick="btnFReset_Click" />
                                </div>

                            </div>
                        </div>
                    </div>


                    <div id="Chemical_count" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">Chemical</h2>
                            <br />
                            <%-- <div class="row align-items-end">--%>

                            <asp:Panel ID="pnlint" runat="server">
                                <div class="row">

                                    <div class="col-lg-3">
                                        <label class="d-block">Assignment </label>
                                        <asp:DropDownList ID="ddlChemical_supervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

                                    </div>


                                    <div class="col-lg-3">

                                        <label>
                                            <asp:Label ID="lbltype" runat="server" Text="Chemical"></asp:Label></label><br />
                                        <asp:DropDownList ID="ddlChemical" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

                                    </div>
                                    <div class="col-lg-3">
                                        <label class="d-block">Method</label>

                                        <asp:DropDownList ID="ddlMethod" Width="250px" class="custom__dropdown robotomd" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Hand" Value="Hand"></asp:ListItem>
                                            <asp:ListItem Text="Avion" Value="Avion"></asp:ListItem>
                                            <asp:ListItem Text="Drench" Value="Drench"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>Chemical Spray Date </label>

                                        <asp:TextBox ID="txtChemicalSprayDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                        <span class="error_message"></span>
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                </div>
                                <div class="row">



                                    <div class="col-lg-3">
                                        <label>Trays</label>
                                        <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtChemicalTrays" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                    </div>

                                    <div class="col-lg-3">
                                        <label>SQFT of Bench </label>

                                        <asp:TextBox ID="txtChemicalSQFTofBench" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                                        <span class="error_message">
                                            <asp:Label ID="Label2" runat="server" ForeColor="red"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSQFT" ValidationGroup="e"
                                                SetFocusOnError="true" ErrorMessage="Please Enter SQFT" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>

                                    <div class="col-lg-3">
                                        <label>Reset Spray Task For Days</label>
                                        <asp:TextBox ID="txtResetChemicalSprayTask" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                    </div>


                                </div>



                                <br />
                                <div class="row">
                                    <div class="col-auto">
                                        <asp:Button Text="Submit" CausesValidation="true" ID="btnChemicalSubmit" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnChemicalSubmit_Click" />

                                        <asp:Button Text="Reset" ID="btnChemicalReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnChemicalReset_Click" />
                                    </div>
                                </div>
                            </asp:Panel>

                            <%--</div>--%>
                        </div>
                    </div>



                    <div id="irrigation_count" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">Irrigation</h2>
                            <br />
                            <div class="row">
                                <div class="col-xl-3">
                                    <label class="d-block">Assignment</label>
                                    <asp:DropDownList ID="ddlirrigationSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="col-xl-3">

                                    <div class="d-flex flex-wrap align-items-center">
                                        <span class="mr-3 mb-2 mb-sm-0"># of passes </span>
                                        <asp:TextBox ID="txtWaterRequired" CssClass="input__control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xl-3">

                                    <div class="d-flex flex-wrap align-items-center">
                                        <span class="mr-3 mb-2 mb-sm-0">Spray Date: </span>
                                        <asp:TextBox ID="txtirrigationSprayDate" CssClass="input__control" TextMode="Date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xl-3">
                                    <label class="d-block">Reset Spray Task For Days </label>
                                    <asp:TextBox ID="txtirrigationResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                            </div>

                            <div class="row">

                                <div class="mb-xl-3 col-12 col-xl-6 align-self-end">

                                    <asp:Button Text="Submit" ID="btnirrigationSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnirrigationSubmit_Click" />

                                    <asp:Button Text="Reset" ID="btnirrigationReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnirrigationReset_Click1" />

                                </div>
                            </div>


                        </div>
                    </div>

                    <div id="plant_ready_count" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">Plant Ready</h2>
                            <br />
                            <div class="row">
                                <%--<div class="mb-3 mb-md-0 col-12 col-md-auto">
                                        <label class="d-block">Job No.</label>
                                        <input type="readonly" value="JB033372" size="10" class="input__control w-100 input__control-auto" />
                                    </div>--%>
                                <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                    <label class="d-block">Assignment</label>

                                    <asp:DropDownList ID="ddlplant_readySupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

                                </div>
                                <div class="mb-3 mb-md-0 col-12 col-md-auto align-self-end">
                                    <asp:Button Text="Submit" ID="btnplant_readySubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnplant_readySubmit_Click" />

                                    <asp:Button Text="Reset" ID="btnplant_readyReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnplant_readyReset_Click" />

                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="move_request" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">

                            <br />
                            <h2 class="text-left">Assign task form for Move Request</h2>
                            <br />

                            <asp:Panel ID="Panel5" runat="server">

                                <div class="row">

                                    <div class="col m3">
                                        <label>To Facility Location </label>
                                        <asp:DropDownList ID="ddlToFacility" runat="server" class="custom__dropdown robotomd" AutoPostBack="true" OnSelectedIndexChanged="ddlToFacility_SelectedIndexChanged"></asp:DropDownList>
                                        <span class="error_message">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlToFacility" ValidationGroup="md"
                                                SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select To Facility" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="col m3">
                                        <label>Bench Location </label>
                                        <asp:DropDownList ID="ddlToGreenHouse" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                        <span class="error_message">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlToGreenHouse" ValidationGroup="md"
                                                SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Greenhouse" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>

                                    <div class="col m3">
                                        <label>Number Of Trays </label>

                                        <asp:TextBox ID="txtMoveNumberOfTrays" runat="server" CssClass="input__control"></asp:TextBox>
                                        <span class="error_message"></span>
                                    </div>

                                    <div class="col-lg-3">
                                        <label>Date </label>

                                        <asp:TextBox ID="txtMoveDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                        <span class="error_message"></span>
                                    </div>

                                    <div class="col-lg-3">
                                        <label>Assignment </label>

                                        <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                        <asp:DropDownList ID="ddlLogisticManager" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                        <span class="error_message">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlLogisticManager" ValidationGroup="e"
                                                SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Enter Request Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-auto">

                                        <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnMoveSubmit" OnClick="btnMoveSubmit_Click" CssClass="bttn bttn-primary bttn-action" runat="server" />
                                    </div>
                                    <div class="col-auto">

                                        <asp:Button Text="Reset" ID="MoveReset" runat="server" OnClick="MoveReset_Click" CssClass="bttn bttn-primary bttn-action" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <div id="dump_request" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">Dumpt</h2>
                            <br />
                            <div class="row">
                                <div class="mb-3 col-12 col-md-auto">
                                    <label class="d-block">Job No.</label>
                                    <input type="text" disabled="disabled" value="JB033372" class="input__control input__control-auto">
                                </div>
                                <div class="mb-3 col-12 col-md-auto">
                                    <label class="d-block">Assignment</label>
                                    <select class="custom__dropdown input__control-auto">
                                        <option>--Select--</option>
                                        <option>Assistant Grower</option>
                                        <option>Supervisor</option>
                                        <option>Irrigator</option>
                                        <option>Crew Lead</option>
                                        <option>Sprayer</option>
                                    </select>
                                </div>
                                <div class="mb-3 col-12 col-md-auto align-self-end">
                                    <button type="button" class="bttn bttn-primary">
                                        Submit
                                    </button>
                                    <button type="reset" class="ml-2 bttn bttn-primary">
                                        Reset
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="general_task_request" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">General Task</h2>
                            <br />
                            <div id="divcomments" runat="server">

                                <div class=" col m12">
                                    <div class="portlet light ">

                                        <div class="portlet-body">
                                            <asp:UpdatePanel runat="server" ID="update2" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel6" runat="server">
                                                        <div class="row" style="margin-left: 15px;">
                                                            <%-- <div class="col-lg-4">
                                                            <label>Comments</label>
                                                            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtComment" CssClass="input__control"></asp:TextBox>
                                                            <span class="error_message">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtcomments" ValidationGroup="x"
                                                                    SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>--%>
                                                            <div class="col-lg-4">
                                                                <label>Task Type</label>

                                                                <asp:DropDownList ID="ddlTaskType" runat="server" OnSelectedIndexChanged="ddlTaskType_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                                                                    <asp:ListItem Text="--Select--" Value="0" />
                                                                    <asp:ListItem Text="Add Bird Neeting" Value="1" />
                                                                    <asp:ListItem Text="Remove Bird Neeting" Value="2" />
                                                                    <asp:ListItem Text="Move" Value="3" />
                                                                    <asp:ListItem Text="Other" Value="4" />
                                                                </asp:DropDownList>
                                                                <span class="error_message">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <label>Assignment</label>

                                                                <asp:DropDownList ID="ddlAssignments" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignments_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                                                                <span class="error_message">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>

                                                            <%-- <div class="col-lg-4">
                                                                <label>Comments</label>
                                                                <asp:TextBox ID="txtgeneralCommnet" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                                                                <span class="error_message">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>--%>

                                                            <div class="col-lg-4" id="divFrom" style="display: none;" runat="server">
                                                                <label>From</label>
                                                                <asp:TextBox ID="txtFrom" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                                                                <span class="error_message">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                            <div class="col-lg-4" id="divTo" style="display: none;" runat="server">
                                                                <label>To</label>
                                                                <asp:TextBox ID="txtTo" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                                                                <span class="error_message">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>

                                                            <div class="col-lg-4">
                                                                <div style="margin-top: 9%;">
                                                                    <asp:Button Text="Send Email" ID="btnSendMail" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSendMail_Click" />
                                                                    <asp:Button Text="Save" ID="btnGeneraltask" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" Visible="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
