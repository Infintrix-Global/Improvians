<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="JobReports.aspx.cs" Inherits="Evo.JobReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <div class="row">
            <div class="col m12">
                <div class="mb-3">
                    <h2>Job Information</h2>
                </div>

                <div class="row align-items-end" id="divFilter" runat="server" visible="false">

                    <div class="col-sm-auto mb-3">
                        <label>Job No</label>
                        <asp:TextBox ID="txtSearchJobNo" runat="server" class="input__control robotomd"></asp:TextBox>

                        <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtSearchJobNo"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                        </cc1:AutoCompleteExtender>
                    </div>
                    <div class="col-auto mb-3">
                        <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearch_Click" />
                        <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />

                    </div>
                </div>
                <div class="row" id="divFilter1" runat="server" visible="false">
                    <div class="col-sm-auto mb-3">
                        <label>Bench Location </label>
                        <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col-sm-auto mb-3">
                        <label>Job No </label>
                        <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                </div>

            </div>
        </div>
        <asp:Panel ID="PanelView" Visible="false" runat="server">
            <div class="row mt-2 align-items-center" id="divJobNo" runat="server">
                <div class="col-sm mb-3">
                    <h3 class="text-sm-left mb-0">
                        <label class="mb-0">Job No: </label>
                        <asp:Label ID="lblJobNo" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="col-sm-auto mb-3">
                    <asp:Button ID="backButton" runat="server" Text="Back" CssClass="bttn bttn-primary bttn-action"
                        OnClientClick="JavaScript:window.history.back(1);return false;"></asp:Button>
                </div>
            </div>

            <div class="row">
                <div class="col m12">
                    <div class="portlet light ">
                        <div class="portlet-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="data__table break__table mb-3 ml-0">
                                    <asp:GridView ID="GV2" runat="server" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true" OnRowDataBound="GV2_RowDataBound"
                                        GridLines="None"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Cust. Name">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SO No">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblsono" data-head="SO No" runat="server" Text='<%# Eval("so")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SO Line">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblsoline" data-head="SO Line" runat="server" Text='<%# Eval("solines")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblitem" data-head="Item" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblitemdesc" data-head="Description" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTraySize" data-head="Size" runat="server" Text='<%# Eval("ts")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Trays">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTotTray" data-head="Total Trays" runat="server" Text='<%# Eval("trays","{0:####}")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seed Date">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblSeededDate" data-head="Seed Date" runat="server" Text='<%# Eval("seeddt","{0:MM/dd/yyyy}")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Plan Due Date">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblReadyDate" runat="server" Text='<%# Eval("ready_date","{0:MM/dd/yyyy}")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <div class="col-lg-6 p-0">
                                        <asp:GridView ID="DGHead02" CssClass="striped" runat="server" GridLines="None" AutoGenerateColumns="false" DataKeyNames="seeddt" OnRowDataBound="DGHead02_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="Organic" DataField="org" />
                                                <asp:BoundField HeaderText="Plant Age" DataField="NoOfDay" />
                                                <asp:BoundField HeaderText="Germ %" DataField="germct" />
                                                <asp:BoundField HeaderText="Overage" DataField="overage" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="my-4">

            <div class="row">
                <div class=" col m12">
                    <div class="mb-2">
                        <h3>Lot Details</h3>
                    </div>
                    <div class="portlet light ">
                        <div class="portlet-body col-lg-6 p-0">
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="data__table">
                                    <asp:GridView ID="DGSeeds" runat="server" AutoGenerateColumns="false" DataKeyNames="">
                                        <Columns>
                                            <asp:BoundField HeaderText="Seed Code" DataField="seed" />
                                            <asp:BoundField HeaderText="Lot No" DataField="lot" />
                                            <asp:BoundField HeaderText="Seed Quantity Used" DataField="qty" DataFormatString="{0:###,0}" />
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
                    <div class="mb-2">
                        <h3>Inventory :
                            <asp:Label ID="lblTotalTrays" runat="server" Text=""></asp:Label>
                            Total Trays</h3>
                    </div>
                    <div class="portlet light ">

                        <div class="portlet-body  col-lg-6 p-0">

                            <asp:Panel ID="Panel4" runat="server">
                                <div class="data__table data__table-height ">
                                    <asp:GridView ID="GV5" runat="server" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10" OnRowDataBound="GV5_RowDataBound"
                                        ShowHeaderWhenEmpty="True" OnRowEditing="GV5_RowEditing" OnRowUpdating="GV5_RowUpdating" OnRowCancelingEdit="GV5_RowCancelingEdit">
                                        <Columns>

                                            <asp:TemplateField HeaderText="House/Section" Visible="false" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrowerId" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>'></asp:Label>
                                                    <asp:Label ID="lbljid" runat="server" Text='<%# Eval("jid")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="House/Section" ItemStyle-Width="30%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGHD" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlBenchLocation" DataTextField="GreenHouseId" DataValueField="GreenHouseId" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Trays" ItemStyle-Width="15%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrays" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("Trays") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="35%" HeaderStyle-CssClass="autostyle2">
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
            <hr class="my-4">

            <div class="row">
                <div class=" col m12">
                    <div class="mb-2 row align-items-center">
                        <h3 class="mb-0 col-12 col-sm-auto">Plant Production Schedule</h3>
                        <span class="col-auto mt-3 mt-sm-0"><span class="collapsed bttn bttn-primary" data-toggle="collapse" data-target="#plant_production_schedule">View</span></span>
                    </div>


                    <div class="portlet light ">

                        <div class="portlet-body col-lg-6 p-0">

                            <asp:Panel ID="plant_production_schedule" ClientIDMode="Static" runat="server" class="collapse mt-3 mt-sm-0 ">
                                <div class="data__table data__table-height">
                                    <asp:GridView ID="GV6" runat="server" AutoGenerateColumns="False"
                                        class="striped"
                                        GridLines="None" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Activity Type" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblSD" runat="server" Text='<%# Eval("activitycode")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Planning Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblNS" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}")  %>'></asp:Label>

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
                    <div class="mb-2">
                        <h3>Job Log</h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upJobLog">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                                    <label>Task Type</label>
                                    <asp:DropDownList ID="ddlDescription" DataTextField="Description" DataValueField="Description" AutoPostBack="true" OnSelectedIndexChanged="ddlDescription_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                                    <label>Bench Location</label>
                                    <asp:DropDownList ID="ddlBench" runat="server" DataTextField="GreenhouseID" DataValueField="GreenhouseID" AutoPostBack="true" OnSelectedIndexChanged="ddlGreenhouse_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                                    <label>Assigned By</label>
                                    <asp:DropDownList ID="ddlAssignedBy" runat="server" DataTextField="AssignedBy" DataValueField="AssignedBy" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignedBy_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                                    <label>Assigned To</label>
                                    <asp:DropDownList ID="ddlAssignedTo" runat="server" DataTextField="AssignedTo" DataValueField="AssignedTo" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignedTo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="col-xl-2 col-md-4 col-sm-6 mb-3 align-self-end">
                                    <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnReset_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class=" col m12">
                                    <div class="portlet light ">

                                        <div class="portlet-body">

                                            <asp:Panel ID="Panel3" runat="server">
                                                <div class="data__table data__table-height">
                                                    <asp:GridView ID="GV4" runat="server" AutoGenerateColumns="False"
                                                        class="striped"
                                                        GridLines="None"
                                                        ShowHeaderWhenEmpty="True" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Task Type" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblD" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Seedline Facility" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblSeedlineFacility" runat="server" Text='<%# Eval("SeedlineFacility")  %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bench Location" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGHD" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Assigned By" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblAssignedby" runat="server" Text='<%# Eval("AssignedBy")  %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Assigned To" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Eval("AssignedTo")  %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Assigned Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblSD" runat="server" Text='<%# Eval("StartingDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" Work Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompletionDate" runat="server" Text='<%# Eval("WorkDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Completion Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompletionDate" runat="server" Text='<%# Eval("EndingDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Task Information" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblG" runat="server" Text='<%# Eval("TaskInformation")  %>'></asp:Label>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div runat="server" id="divTaskRequest">
                <h4 class="mt-4 mt-lg-3">Task Requests:</h4>

                <div class="task_request_assignments mb-3" id="task_request-group">

                    <div class="task_request-buttons">

                        <asp:LinkButton runat="server" ID="btngermination" ForeColor="Black" class="request__block-head collapsed" OnClick="btngermination_Click">
                              <span class="">
                                <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Count" />
                                Germination Count
                             </span>
                        </asp:LinkButton>

                        <asp:LinkButton runat="server" ID="btnFertilization" ForeColor="Black" class="request__block-head collapsed" OnClick="btnFertilization_Click">
                            <span class="">
                                <img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization">
                                Fertilization
                            </span>
                        </asp:LinkButton>

                        <asp:LinkButton runat="server" ID="btnChemical" ForeColor="Black" class="request__block-head collapsed" OnClick="btnChemical_Click">
                            <span class="">
                                <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Chemical">
                                Chemical
                            </span>
                        </asp:LinkButton>


                        <asp:LinkButton runat="server" ID="btnIrrigation" ForeColor="Black" class="request__block-head collapsed" OnClick="btnIrrigation_Click">
                            <span class="">
                                <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation" />
                                Irrigation
                            </span>
                        </asp:LinkButton>

                        <asp:LinkButton runat="server" ID="btnPlantReady" ForeColor="Black" class="request__block-head collapsed" OnClick="btnPlantReady_Click">
                            <span class="">
                                <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready" />
                                Plant Ready
                            </span>
                        </asp:LinkButton>

                        <asp:LinkButton runat="server" ID="btnMoveRequest" ForeColor="Black" class="request__block-head collapsed" OnClick="btnMoveRequest_Click">
                            <span class="">
                                <img src="./images/dashboard_move-request.png" width="137" height="134" alt="Move Request" />
                                Move Request
                            </span>
                        </asp:LinkButton>

                        <asp:LinkButton runat="server" ID="btnDump" ForeColor="Black" class="request__block-head collapsed" OnClick="btnDump_Click">
                            <span class="">
                                <img src="./images/dashboard_dump-request.png" width="137" height="136" alt="Dump" />
                                Dump
                            </span>
                        </asp:LinkButton>

                        <asp:LinkButton runat="server" ID="btnGeneralTask" ForeColor="Black" class="request__block-head collapsed" OnClick="btnGeneralTask_Click">
                            <span class="">
                                <img src="./images/dashboard_general-task.png" width="137" height="134" alt="General Task" />
                                General Task
                            </span>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <div runat="server" id="divSalesComment" visible="false" class="sales__message">
                <h2 class="text-left pt-4">Contact Our Sales Representative:</h2>
                <label class='d-block robotomd'>Message: <span style="color: red">*</span></label>
                <asp:TextBox ValidationGroup="e" CssClass="w-100 input__control" TextMode="MultiLine" Rows="5" ID="msgs" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="msgs"
                    ErrorMessage="Message is required."
                    ForeColor="Red">
                </asp:RequiredFieldValidator><br />
                <asp:Button ID="btnSend" runat="server" CssClass="bttn bttn-primary" Text="Send" OnClick="btnSend_Click" />
            </div>

        </asp:Panel>
    </div>
</asp:Content>
