<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="Evo.CreateTask" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btngermination", function () {
            alert("Button was clicked.");
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main__header">
        <div class="site__container">
            <h2 class="head__title-icon">

                <%--img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization / Chemical">--%>
                Create Task


            </h2>




            <div class="row">

                <div class="col-lg-3">

                    <label>Job No</label>
                    <asp:TextBox ID="txtSearchJobNo" runat="server" class="input__control robotomd"></asp:TextBox>


                    <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtSearchJobNo"
                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                    </cc1:AutoCompleteExtender>
                </div>


                <div class="col-lg-3">
                    <br />
                    <asp:Button Text="Search" ID="btnSearchDet" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchDet_Click" />

                    <asp:Button Text="Reset" ID="Button4" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="Button4_Click" />

                </div>

                <div class="col-lg-3">
                </div>

                <div class="col-lg-3">
                </div>

            </div>
            <br />
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
                                    ShowHeaderWhenEmpty="True" Width="70%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBoxall" AutoPostBack="true" OnCheckedChanged="chckchanged1" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox runat="server" Checked="true" ID="chkSelect"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />
                                                <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Facility Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitem" Visible="false" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                                <asp:Label ID="lblFacility" Visible="false" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
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

                <%--<div class="task_request-buttons">
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

                </div>--%>

                <%-- 
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"
                    runat="server">
                    <ContentTemplate>--%>
            

                        <div class="task_request-buttons">


                            <asp:LinkButton runat="server" ID="btngermination" ForeColor="Black" class="request__block-head collapsed" OnClick="btngermination_Click1">
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

                            <asp:LinkButton runat="server" ID="btnGeneral_Task" ForeColor="Black" class="request__block-head collapsed" OnClick="btnGeneralTask_Click">
                            <span class="">
                                <img src="./images/dashboard_general-task.png" width="137" height="134" alt="General Task" />
                                General Task
                            </span>
                            </asp:LinkButton>

                        </div>

                <%--       </ContentTemplate>
                </asp:UpdatePanel>--%>

                <div class="request__block">
                    <div id="germination_count" runat="server" class="collapse request__block-collapse" data-parent="#task_request-group">
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
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtGcomments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                            </div>
                            <br />
                            <br />

                            <div class="row">
                                <div class="mb-xl-3 col-12 col-xl-6 align-self-end">
                                    <asp:Button Text="Submit" ID="btngerminationSumit" CssClass="bttn bttn-primary bttn-action" OnClick="btngerminationSumit_Click" runat="server" />
                                    <asp:Button Text="Reset" ID="btngerminationReset" runat="server" OnClick="btngerminationReset_Click" CssClass="bttn bttn-primary bttn-action" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="fertilization_count" runat="server" class="collapse request__block-collapse" data-parent="#task_request-group">
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
                                    <label>Minimum Days Until Next Fertilization</label>
                                    <asp:TextBox ID="txtResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                    <asp:TextBox ID="txtBenchIrrigationFlowRate" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                    <asp:TextBox ID="txtBenchIrrigationCoverage" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                    <asp:TextBox ID="txtSprayCoverageperminutes" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>

                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtFComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

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


                    <div id="Chemical_count" runat="server" class="collapse request__block-collapse" data-parent="#task_request-group">
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
                                        <asp:DropDownList ID="ddlMethod" class="custom__dropdown robotomd" runat="server" RepeatDirection="Horizontal" />
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
                                        <label>Minimum Days Until Next Chemical</label>
                                        <asp:TextBox ID="txtResetChemicalSprayTask" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                    </div>

                                    <div class="col-lg-3">
                                        <label>Comments </label>

                                        <asp:TextBox ID="txtCComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

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



                    <div id="irrigation_count" runat="server" class="collapse request__block-collapse" data-parent="#task_request-group">
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
                                    <label class="d-block">Minimum Days Until Next Irrigation</label>
                                    <asp:TextBox ID="txtirrigationResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xl-3">
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtIrrComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

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

                    <div id="plant_ready_count" runat="server" class="collapse request__block-collapse" data-parent="#task_request-group">
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
                                <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                    <label class="d-block">Plant Ready Date</label>
                                    <asp:TextBox ID="txtPlantDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>

                                <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtPlantComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>

                                <div class="mb-3 mb-md-0 col-12 col-md-auto align-self-end">
                                    <asp:Button Text="Submit" ID="btnplant_readySubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnplant_readySubmit_Click" />

                                    <asp:Button Text="Reset" ID="btnplant_readyReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnplant_readyReset_Click" />

                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="move_request" runat="server" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">

                            <br />
                            <h2 class="text-left">Move Request</h2>
                            <br />

                            <asp:Panel ID="Panel3" runat="server">

                                <div class="row">
                                    <div class="col-lg-3">
                                        <label>Assignment </label>

                                        <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                        <asp:DropDownList ID="ddlLogisticManager" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                        <span class="error_message">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlLogisticManager" ValidationGroup="e"
                                                SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Enter Request Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
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


                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label>Comments </label>

                                        <asp:TextBox ID="txtMoveComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

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

                    <div id="dump_request" runat="server" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">Dump</h2>
                            <br />
                            <div class="row">
                                <div class="mb-3 col-12 col-md-auto">
                                    <label>Assignment </label>

                                    <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                    <asp:DropDownList ID="ddlDumptAssignment" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

                                </div>

                                <div class="mb-3 col-12 col-md-auto">
                                    <label class="d-block">Dump Date</label>
                                    <asp:TextBox ID="txtDumpDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>

                                <div class="mb-3 col-12 col-md-auto">

                                    <label>Quantity of Tray </label>
                                    <asp:TextBox ID="txtQuantityofTray" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>

                                <div class="mb-3 col-12 col-md-auto">

                                    <label>Comments </label>

                                    <asp:TextBox ID="txtCommentsDump" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>


                                </div>
                                <div class="mb-3 col-12 col-md-auto align-self-end">
                                    <asp:Button Text="Submit" ID="btnDumpSumbit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnDumpSumbit_Click" />

                                    <asp:Button Text="Reset" ID="btnDumpReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnDumpReset_Click" />

                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="general_task_request" runat="server" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">General Task</h2>
                            <br />
                            <div id="divcomments" runat="server">

                                <div class=" col m12">
                                    <div class="portlet light ">

                                        <div class="portlet-body">
                                            <%--  <asp:UpdatePanel runat="server" ID="update2" UpdateMode="Conditional">
                                                <ContentTemplate>--%>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="row">
                                                    <%-- <div class="col-lg-4">
                                                            <label>Comments</label>
                                                            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtComment" CssClass="input__control"></asp:TextBox>
                                                            <span class="error_message">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtcomments" ValidationGroup="x"
                                                                    SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>--%>

                                                    <div class="col-xl-3">
                                                        <label>Assignment</label>
                                                        <asp:DropDownList ID="ddlAssignments" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

<%--                                                        <asp:DropDownList ID="ddlAssignments" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignments_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>--%>
                                                        <span class="error_message">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>

                                                   <div class="col-xl-3">
                                                        <label>Task Type</label>

                                                        <asp:DropDownList ID="ddlTaskType" runat="server" OnSelectedIndexChanged="ddlTaskType_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                                                            <asp:ListItem Text="--Select--" Value="0" />
                                                            <asp:ListItem Text="Add Bird Netting" Value="1" />
                                                            <asp:ListItem Text="Remove Bird Netting" Value="2" />
                                                            <asp:ListItem Text="Move" Value="3" />
                                                            <asp:ListItem Text="Other" Value="4" />
                                                        </asp:DropDownList>
                                                        <span class="error_message">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>

                                                    <div class="col-xl-3">
                                                        <label>General Task Date</label>
                                                        <asp:TextBox ID="txtgeneralDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                                                        <span class="error_message">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtgeneralDate" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>

                                                    <div class="col-xl-3">
                                                        <label>Comments</label>
                                                        <asp:TextBox ID="txtgeneralComment" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                                                        <span class="error_message">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>

                                                    <div class="col-xl-3" id="divFrom" style="display: none;" runat="server">
                                                        <label>From</label>
                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="input__control"></asp:TextBox>

                                                        <span class="error_message">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>
                                                    <div class="col-xl-3" id="divTo" style="display: none;" runat="server">
                                                        <label>To</label>
                                                        <asp:TextBox ID="txtTo" runat="server" CssClass="input__control"></asp:TextBox>

                                                        <span class="error_message">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>

                                                    <div class="col-lg-3">
                                                        <div style="margin-top: 9%;">
                                                            <asp:Button Text="Send Email" ID="btnSendMail" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSendMail_Click" Visible="false" />
                                                            <asp:Button Text="Submit" ID="btnGeneraltask" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnGeneraltask_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
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
