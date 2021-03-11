<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="Evo.CreateTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main__header">
        <div class="site__container">
            <h2 class="head__title-icon">

                <%--img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization / Chemical">--%>
                Create Task


            </h2>



            <div class="filter__row d-flex">
                <div class="row">
                    <div class="col-lg-3">
                        <label>Bench Location </label>
                        <span style="color: red">*</span>
                        <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        <span class="error_message">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                    </div>
                    <div class="col-lg-3">
                        <label>Job No </label>
                        <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <label>Customer </label>
                        <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <br />
                        <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" Visible="false" ValidationGroup="x" />
                        <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />

                    </div>

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
                        <asp:Button ID="Button1" ValidationGroup="x" Visible="false" runat="server" CssClass="bttn bttn-primary bttn-action mr-2" OnClick="btnSearch_Click" Text="Search" />
                        <asp:Button Text="Reset" ID="btnResetSearch" ValidationGroup="x" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                    </div>
                    <div class="col-lg-6">
                    </div>


                </div>
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
            </asp:Panel>
            <br />

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table data__table-height">
                                <asp:GridView ID="gvFer" runat="server" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
                                    GridLines="None" OnRowCommand="gvFer_RowCommand"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
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
                                                <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
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
                        </div>

                    </div>
                </div>

            </div>


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
                                    <asp:TextBox ID="txtQty" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
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
                                    <label>Bench Irrigation Flow Rate [Gallons/min]</label>
                                    <asp:TextBox ID="txtBenchIrrigationFlowRate" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Bench Irrigation Coverage [Gallons/Sqft]</label>
                                    <asp:TextBox ID="txtBenchIrrigationCoverage" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Spray Coverage per minutes [sqft/min]</label>
                                    <asp:TextBox ID="txtSprayCoverageperminutes" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Reset Spray Task For Days</label>
                                    <asp:TextBox ID="txtResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <div class="mb-md-3 col-xl-3 col-md-6 col-12 align-self-end">


                                    <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnFSubmit" CssClass="mr-2 bttn bttn-primary" runat="server" OnClick="btnFSubmit_Click" />

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
                            <div class="row align-items-end">
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label class="d-block">Assignment</label>
                                    <asp:DropDownList ID="ddlChemicalSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>

                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label class="d-block">Spray Date</label>
                                    <asp:TextBox ID="TextBox1" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label class="d-block">
                                        Chemical
                                    </label>
                                    <asp:DropDownList ID="ddlChemical" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="mb-3 col-xl-2 col-md-6 col-12">
                                    <label class="d-block">Concentration [ppm]</label>
                                    <asp:TextBox ID="TextBox2" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="mb-3 col-xl-2 col-md-6 col-12">
                                    <label class="d-block">Trays</label>
                                    <asp:TextBox ID="TextBox3" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="mb-3 col-xl-2 col-md-6 col-12">
                                    <label class="d-block">SQFT of Bench</label>
                                    <asp:TextBox ID="TextBox4" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Bench Irrigation Flow Rate [Gallons/min]</label>
                                    <asp:TextBox ID="TextBox5" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Bench Irrigation Coverage [Gallons/Sqft]</label>
                                    <asp:TextBox ID="TextBox6" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Spray Coverage per minutes [sqft/min]</label>
                                    <asp:TextBox ID="TextBox7" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Reset Spray Task For Days</label>
                                    <asp:TextBox ID="TextBox8" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <div class="mb-md-3 col-xl-3 col-md-6 col-12 align-self-end">


                                    <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="Button2" CssClass="mr-2 bttn bttn-primary" runat="server" />

                                    <asp:Button Text="Reset" ID="Button3" runat="server" CssClass="bttn bttn-primary" />
                                </div>

                            </div>
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
                                <div class="col-xl-3">

                                    <label class="d-block">Notes:</label>
                                    <asp:TextBox ID="txtirrigationNotes" TextMode="MultiLine" class="w-100 input__control" placeholder="Notes" runat="server"></asp:TextBox>
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
                            Assign task form for Move Request
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
                                                    <asp:Panel ID="Panel1" runat="server">
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

                                                            <div class="col-lg-4">
                                                                <label>Comments</label>
                                                                <asp:TextBox ID="txtgeneralCommnet" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                                                                <span class="error_message">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>

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
