<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="Evo.CreateTask" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function Confirm() {
            var value = ConfirmFerDateCheck();
         //   alert('value ' + value);
            if (value == true)
            {
                var txt1 = document.getElementById('<%= lblDayOfShip.ClientID %>').value;
                 //  alert('txt1' + txt1);
                 var txt2 = document.getElementById('<%= lblDateOfShip.ClientID %>').value;
                 var txtToday = document.getElementById('<%= ToDaydate.ClientID %>').value;
                 var txttoSDate = document.getElementById('<%= SprayTaskForDaysDate.ClientID %>').value;
                if (txttoSDate > txtToday)
                    return confirm("Fertilization Reset of " + txt1 + " days has been applied on this bench location on " + txt2 + ". Are you sure you want to proceed.?");
                else
                    return true;
            }
            else
                return false;
        }

        //Fertilizer Date Check
        function ConfirmFerDateCheck() {
            var Fer_Date = document.getElementById('<%= FerDate.ClientID %>').value;
            var txt2 = document.getElementById('<%= txtFDate.ClientID %>').value;
            //alert('txt2' + txt2);
            //alert('Fer_Date' + Fer_Date);
            if (Fer_Date == txt2) {
                $('#confirmModal').modal('show');
                return false;
            }//return confirm("Fertilization Reset of " + txt1 + " days has been applied on this bench location on " + txt2 + ". Are you sure you want to proceed.?");
            else
                return true;

        }


        function ConfirmChemica() {
            var value = ConfirmCheDateCheck();
            //   alert('value ' + value);
            if (value == true)
            {
                 var txt1 = document.getElementById('<%= lblDayOfShip.ClientID %>').value;
                 var txt2 = document.getElementById('<%= lblDateOfShip.ClientID %>').value;
                  var txtToday = document.getElementById('<%= ToDaydateCem.ClientID %>').value;
                  var txttoSDate = document.getElementById('<%= SprayTaskForDaysDateCem.ClientID %>').value;
                  if (txttoSDate > txtToday)
                            return confirm("Chemical Reset of " + txt1 + " days has been applied on this bench location on " + txt2 + ". Are you sure you want to proceed.?");
                   else
                return true;
            }
            else
                return false;
        }



        //Chemica Date Check
        function ConfirmCheDateCheck() {
            var Cem_Date = document.getElementById('<%= CemDate.ClientID %>').value;
            var txt2 = document.getElementById('<%= txtChemicalSprayDate.ClientID %>').value;

            if (Cem_Date == txt2)
            {
                $('#confirmModalCem').modal('show');
                return false;
            }//return confirm("Fertilization Reset of " + txt1 + " days has been applied on this bench location on " + txt2 + ". Are you sure you want to proceed.?");
            else
                return true;

        }


        function ConfirmIrrigation() {

            var value = ConfirmIrrigationDateCheck();
            //   alert('value ' + value);
            if (value == true)
            {

                var txt1 = document.getElementById('<%= lblDayOfShip.ClientID %>').value;
                 var txt2 = document.getElementById('<%= lblDateOfShip.ClientID %>').value;
                 var txtToday = document.getElementById('<%= ToDaydateIrr.ClientID %>').value;
                 var txttoSDate = document.getElementById('<%= SprayTaskForDaysDateirr.ClientID %>').value;
                 if (txttoSDate > txtToday)
                       return confirm("Irrigation Reset of " + txt1 + " days has been applied on this bench location on " + txt2 + ". Are you sure you want to proceed.?");
                 else
                    return true;
            }
            else
                return false;

        }

        //Irrigation Date Check
        function ConfirmIrrigationDateCheck() {
            var Irr_Date = document.getElementById('<%= IrrDate.ClientID %>').value;
            var txt2 = document.getElementById('<%= txtirrigationSprayDate.ClientID %>').value;

            if (Irr_Date == txt2)
            {
                $('#confirmModalIrr').modal('show');
                return false;
            }//return confirm("Fertilization Reset of " + txt1 + " days has been applied on this bench location on " + txt2 + ". Are you sure you want to proceed.?");
            else
                return true;

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="lblDayOfShip" runat="server" />
    <asp:HiddenField ID="lblDateOfShip" runat="server" />
    <asp:HiddenField ID="SprayTaskForDaysDate" runat="server" />
    <asp:HiddenField ID="ToDaydate" runat="server" />

    <asp:HiddenField ID="SprayTaskForDaysDateCem" runat="server" />
    <asp:HiddenField ID="ToDaydateCem" runat="server" />

    <asp:HiddenField ID="SprayTaskForDaysDateirr" runat="server" />
    <asp:HiddenField ID="ToDaydateIrr" runat="server" />

    <asp:HiddenField ID="FerDate" runat="server" />
    <asp:HiddenField ID="CemDate" runat="server" />
    <asp:HiddenField ID="IrrDate" runat="server" />
    <div class="site__container">
        <h2 class="head__title-icon mb-4"><%--img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization / Chemical">--%> Create Task </h2>

        <div class="row align-items-end">

            <div class="col-12 col-sm-6 col-md-4 col-lg-3 mb-3">

                <label>Job No</label>
                <asp:TextBox ID="txtSearchJobNo" runat="server" class="input__control robotomd"></asp:TextBox>


                <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="txtSearchJobNo"
                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                </cc1:AutoCompleteExtender>
            </div>

            <div class="col-12 col-md-4 col-lg-3 mb-3">

                <asp:Button Text="Search" ID="btnSearchDet" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchDet_Click" />

                <asp:Button Text="Reset" ID="Button4" runat="server" CssClass="ml-2 bttn bttn-primary bttn-action" OnClick="Button4_Click" />

            </div>

            <div class="w-100"></div>

            <div class="col-12 col-sm-6 col-md-4 col-lg-3 mb-3">
                <label>Bench Location </label>
                <asp:TextBox ID="txtBatchLocation" runat="server" class="input__control robotomd"></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="SearchBenchLocation"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="txtBatchLocation"
                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                </cc1:AutoCompleteExtender>
            </div>

            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <asp:Button Text="Search" ID="btlSearchBenchLocation" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btlSearchBenchLocation_Click" />
                <asp:Button Text="Reset" ID="btnResetBenchLocation" runat="server" CssClass="ml-2 bttn bttn-primary bttn-action" OnClick="btnResetBenchLocation_Click" />
            </div>
        </div>
        <div class="row align-items-end mt-3">
            <div class="col-12 col-md-4 col-lg mb-3">
                <label>Bench Location </label>

                <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>


                <%--<span class="error_message">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                            SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                    </span>--%>
            </div>
            <div class="col-12 col-md-4 col-lg mb-3">
                <label>Job No </label>
                <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-12 col-md-4 col-lg mb-3">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-12 col-md mb-3">
                <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" Visible="false" ValidationGroup="x" />
                <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />
            </div>
        </div>

        <asp:Panel ID="Panel_Bench" Visible="false" runat="server">
            <div class="row pt-3 align-items-center">
                <div class="col-auto col-sm-auto">
                    <asp:RadioButtonList ID="RadioBench" Width="100%" runat="server" AutoPostBack="true" ValidationGroup="x" OnSelectedIndexChanged="RadioBench_SelectedIndexChanged" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Bench (A/B)" Value="1" class="custom-control custom-radio mr-3 mt-2"></asp:ListItem>
                        <asp:ListItem Text="Benches in house" Value="2" class="custom-control custom-radio mr-3 mt-2"></asp:ListItem>
                        <asp:ListItem Text="House" Value="3" class="custom-control custom-radio mt-2"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <div class="col-12 col-sm-auto">
                    <asp:Button ID="Button1" ValidationGroup="x" Visible="false" runat="server" CssClass="bttn bttn-primary bttn-action mr-2" OnClick="btnSearch_Click" Text="Search" />
                    <asp:Button Text="Reset" ID="btnResetSearch" ValidationGroup="x" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-auto">
                    <asp:Panel ID="PanelBench" Visible="false" runat="server">
                        <asp:Label ID="lblBench1" Visible="false" runat="server" Text="Label"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="PanelBenchesInHouse" Visible="false" runat="server">

                        <asp:ListBox Height="150px" ID="ListBoxBenchesInHouse" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ListBoxBenchesInHouse_SelectedIndexChanged"  runat="server"></asp:ListBox>
                    </asp:Panel>
                    <asp:Panel ID="PanelHouse" Visible="false" runat="server">
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>

        <div class="row pt-3">
            <div class="col">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body col-lg-12 p-0">
                        <div class="data__table data__table-height">
                            <asp:GridView ID="gvFer" runat="server" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" OnRowDataBound="gvFer_RowDataBound"
                                GridLines="None" OnRowCommand="gvFer_RowCommand"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="5%">
                                        <HeaderTemplate>
                                            <div class="custom-control custom-checkbox mr-3">
                                                <asp:CheckBox ID="CheckBoxall" class="custom-control custom-checkbox" Text=" " AutoPostBack="true" OnCheckedChanged="chckchanged1" runat="server" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="custom-control custom-checkbox mr-3">
                                                <asp:CheckBox runat="server" class="custom-control custom-checkbox" Text=" " OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" ID="chkSelect"></asp:CheckBox>
                                            </div>
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
                                            <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblJIdPU" runat="server" Text='<%# Eval("jid")  %>' Visible="false"></asp:Label>
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

                                       <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>

                                            <asp:Label ID="lblPlantDueDate" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}")  %>'></asp:Label>


                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Plant Ready Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>

                                            <asp:Label ID="lblPlantReadyDate" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
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

        <h4 class="mt-4">Task Requests:</h4>

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
                                <img src="./images/dashboard_chemical.png" width="137" height="136" alt="Chemical">
                                Chemical
                            </span>
                </asp:LinkButton>


                <asp:LinkButton runat="server" ID="btnIrrigation" ForeColor="Black" class="request__block-head collapsed" OnClick="btnIrrigation_Click">
                            <span class="">
                                <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation" />
                                Irrigation
                            </span>
                </asp:LinkButton>

                <asp:LinkButton runat="server" ID="btnCropHealthReport" ForeColor="Black" class="request__block-head collapsed" OnClick="btnCropHealthReport_Click">
                            <span class="">
                                <img src="./images/dashboard_crop-health-report.png" width="137" height="132" alt="Plant Ready" />
                               Crop Health Report
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
                <div id="germination_count" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">Germination Count</h2>
                        <div class="row">
                            <div class="col-md-4 col-xl-3 mb-3">
                                <label class="d-block">Assignment</label>
                                <asp:DropDownList ID="ddlgerminationSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlgerminationSupervisor" ValidationGroup="GA"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-md-4 col-xl-3 mb-3">
                                <label class="d-block">Germination Count Date</label>
                                <asp:TextBox ID="txtGerDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <div class="col-md-4 col-xl-3 mb-3">
                                <label class="d-block">Number Of Trays To Inspect</label>
                                <asp:TextBox ID="txtTGerTrays" TextMode="Number" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <div class="col-sm-auto col-xl-4 mb-3">
                                <label>Comments </label>
                                <asp:TextBox ID="txtGcomments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-12">
                                <div class="bttn-group">
                                    <asp:Button Text="Assign" ID="btngerminationSumit" ValidationGroup="GA" CssClass="bttn bttn-primary bttn-action mb-3" OnClick="btngerminationSumit_Click" runat="server" />
                                    <asp:Button Text="Save for later" CausesValidation="true" ID="btnBSaveSubmit" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" OnClick="btnBSaveSubmit_Click" />
                                    <asp:Button Text="Start" CausesValidation="true" ID="btnStartGermination" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnStartGermination_Click" />
                                    <asp:Button Text="Reset" ID="btngerminationReset" runat="server" OnClick="btngerminationReset_Click" CssClass="bttn bttn-primary bttn-action mb-3" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="fertilization_count" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">Fertilization</h2>
                        <div class="row">
                            <div class="col-lg-4 col-xl-3 mb-3">
                                <label class="d-block">Assignment</label>
                                <asp:DropDownList ID="ddlFertilizationSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFA" runat="server" ControlToValidate="ddlFertilizationSupervisor" ValidationGroup="FA"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>

                            <div class="col-lg-4 col-xl-3 mb-3">
                                <label class="d-block">Fertilizer Spray Date</label>
                                <asp:TextBox ID="txtFDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>

                            <div class="col-lg-4 col-xl-3 mb-3">
                                <label class="d-block">
                                    Fertilizer
                                </label>
                                <asp:DropDownList ID="ddlFertilizer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            </div>

                            <div class="col-lg-4 col-xl-3 mb-3">
                                <label class="d-block">Concentration [ppm]</label>
                                <asp:TextBox ID="txtQty" TextMode="Number" Text="150" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-xl-3 mb-3">
                                <label class="d-block">Trays</label>
                                <asp:TextBox ID="txtFTrays" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-xl-3 mb-3">
                                <label class="d-block">SQFT of Bench</label>
                                <asp:TextBox ID="txtSQFT" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-xl-3 mb-3">
                                <label>Minimum Days Until Next Fertilization</label>
                                <asp:TextBox ID="txtResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                <asp:TextBox ID="txtBenchIrrigationFlowRate" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                <asp:TextBox ID="txtBenchIrrigationCoverage" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                <asp:TextBox ID="txtSprayCoverageperminutes" Visible="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                            </div>
                            <div class="w-100"></div>

                            <div class="mb-3 col-xl-3 col-md-6 col-12">
                                <label>Comments </label>
                                <asp:TextBox ID="txtFComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>

                            <div class="col-12">
                                <div class="bttn-group">
                                    <asp:Button Text="Assign" CausesValidation="true" ValidationGroup="FA" ID="btnFSubmit" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" OnClientClick="return Confirm();" OnClick="btnFSubmit_Click" />
                                    <asp:Button Text="Save for later" CausesValidation="true" ID="btnSaveFLSubmit" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClientClick="return Confirm();" OnClick="btnSaveFLSubmit_Click" />
                                    <asp:Button Text="Start" CausesValidation="true" ID="btnStartFertilization" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClientClick="return Confirm();" OnClick="btnStartFertilization_Click" />
                                    <asp:Button Text="Reset" ID="btnFReset" runat="server" CssClass="bttn bttn-primary bttn-action mb-3" OnClick="btnFReset_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>


                <div id="Chemical_count" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">Chemical</h2>
                        <%-- <div class="row align-items-end">--%>

                        <asp:Panel ID="pnlint" runat="server">
                            <div class="row">
                                <div class="col-lg-4 col-xl-3 mb-3">
                                    <label class="d-block">Assignment </label>
                                    <asp:DropDownList ID="ddlChemical_supervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                    <span class="error_message">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCA" runat="server" ControlToValidate="ddlChemical_supervisor" ValidationGroup="CA"
                                            SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="col-lg-4 col-xl-3 mb-3">
                                    <label>
                                        <asp:Label ID="lbltype" runat="server" Text="Chemical"></asp:Label>
                                    </label>
                                    <br />
                                    <asp:DropDownList ID="ddlChemical" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="col-lg-4 col-xl-3 mb-3">
                                    <label class="d-block">Method</label>
                                    <asp:DropDownList ID="ddlMethod" class="custom__dropdown robotomd" runat="server" RepeatDirection="Horizontal" />
                                </div>
                                <div class="col-lg-4 col-xl-3 mb-3">
                                    <label>Chemical Spray Date </label>

                                    <asp:TextBox ID="txtChemicalSprayDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                    <span class="error_message"></span>
                                </div>
                                <div class="col-lg-4 col-xl-3 mb-3">
                                    <label>Trays</label>
                                    <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtChemicalTrays" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-xl-3 mb-3">
                                    <label>SQFT of Bench </label>

                                    <asp:TextBox ID="txtChemicalSQFTofBench" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                                    <span class="error_message">
                                        <asp:Label ID="Label2" runat="server" ForeColor="red"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSQFT" ValidationGroup="e"
                                            SetFocusOnError="true" ErrorMessage="Please Enter SQFT" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="col-lg-4 col-xl-3 mb-3">
                                    <label>Minimum Days Until Next Chemical</label>
                                    <asp:TextBox ID="txtResetChemicalSprayTask" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="w-100"></div>
                                <div class="col-lg-4 col-xl-3 mb-3">
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtCComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="w-100"></div>
                                <div class="col-auto">
                                    <div class="bttn-group">
                                        <asp:Button Text="Assign" CausesValidation="true" ValidationGroup="CA" ID="btnChemicalSubmit" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" OnClientClick="return ConfirmChemica();" OnClick="btnChemicalSubmit_Click" />
                                        <asp:Button Text="Save for later" CausesValidation="true" ID="btnChemicalSFLSubmit" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClientClick="return ConfirmChemica();" OnClick="btnChemicalSFLSubmit_Click" />
                                        <asp:Button Text="Start" CausesValidation="true" ID="btnStartChemical" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClientClick="return ConfirmChemica();" OnClick="btnStartChemical_Click" />
                                        <asp:Button Text="Reset" ID="btnChemicalReset" runat="server" CssClass="bttn bttn-primary bttn-action mb-3" OnClick="btnChemicalReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%--</div>--%>
                    </div>
                </div>

                <div id="irrigation_count" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">Irrigation</h2>
                        <div class="row">
                            <div class="col-xl-3 col-lg-4 col-sm-6 mb-3">
                                <label class="d-block">Assignment</label>
                                <asp:DropDownList ID="ddlirrigationSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorIA" runat="server" ControlToValidate="ddlirrigationSupervisor" ValidationGroup="IA"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 mb-3">
                                <label class="d-block"># of passes </label>
                                <asp:TextBox ID="txtWaterRequired" CssClass="input__control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 mb-3">
                                <label class="d-block">Spray Date: </label>
                                <asp:TextBox ID="txtirrigationSprayDate" CssClass="input__control" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 mb-3">
                                <label class="d-block">Minimum Days Until Next Irrigation</label>
                                <asp:TextBox ID="txtirrigationResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-3 col-lg-4 mb-3">
                                <label>Comments </label>

                                <asp:TextBox ID="txtIrrComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="bttn-group">
                                    <asp:Button Text="Assign" ID="btnirrigationSubmit" ValidationGroup="IA" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" OnClientClick="return ConfirmIrrigation();" OnClick="btnirrigationSubmit_Click" />
                                    <asp:Button Text="Save for later" CausesValidation="true" ID="btnSaveirrigation" CssClass="submit-bttn bttn bttn-primary mb-3" OnClientClick="return ConfirmIrrigation();" runat="server" OnClick="btnSaveirrigation_Click" />
                                    <asp:Button Text="Start" CausesValidation="true" ID="btnStartirrigation" CssClass="submit-bttn bttn bttn-primary mb-3" OnClientClick="return ConfirmIrrigation();" runat="server" OnClick="btnStartirrigation_Click" />
                                    <asp:Button Text="Reset" ID="btnirrigationReset" runat="server" CssClass="bttn bttn-primary bttn-action mb-3" OnClientClick="return ConfirmIrrigation();" OnClick="btnirrigationReset_Click1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="plant_ready_count" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">Plant Ready</h2>
                        <div class="row">
                            <%--<div class="mb-3 mb-md-0 col-12 col-md-auto">
                                        <label class="d-block">Job No.</label>
                                        <input type="readonly" value="JB033372" size="10" class="input__control w-100 input__control-auto" />
                                    </div>--%>
                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                <label class="d-block">Assignment</label>
                                <asp:DropDownList ID="ddlplant_readySupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPA" runat="server" ControlToValidate="ddlplant_readySupervisor" ValidationGroup="PA"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                <label class="d-block">Plant Ready Work Date</label>
                                <asp:TextBox ID="txtPlantDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="d-none d-sm-block w-100"></div>
                            <div class="mb-3 col-12 col-md-auto">
                                <label>Comments </label>

                                <asp:TextBox ID="txtPlantComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="d-none d-sm-block w-100"></div>
                            <div class="col-12">
                                <div class="bttn-group">
                                    <asp:Button Text="Assign" ID="btnplant_readySubmit" ValidationGroup="PA" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" OnClick="btnplant_readySubmit_Click" />
                                    <asp:Button Text="Save for later" CausesValidation="true" ID="btnSavePlantReady" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnSavePlantReady_Click" />
                                    <asp:Button Text="Start" CausesValidation="true" ID="btnStartPlantReady" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnStartPlantReady_Click" />
                                    <asp:Button Text="Reset" ID="btnplant_readyReset" runat="server" CssClass="bttn bttn-primary bttn-action mb-3" OnClick="btnplant_readyReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="move_request" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">Move Request</h2>
                        <asp:Panel ID="Panel3" runat="server">

                            <div class="row">
                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>Assignment </label>

                                    <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                    <asp:DropDownList ID="ddlLogisticManager" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                    <span class="error_message">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlLogisticManager" ValidationGroup="e"
                                            SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Enter Request Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>To Facility Location </label>
                                    <asp:DropDownList ID="ddlToFacility" runat="server" class="custom__dropdown robotomd" AutoPostBack="true" OnSelectedIndexChanged="ddlToFacility_SelectedIndexChanged"></asp:DropDownList>
                                    <span class="error_message">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlToFacility" ValidationGroup="md"
                                            SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select To Facility" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>To Bench Location </label>
                                    <asp:DropDownList ID="ddlToGreenHouse" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                    <span class="error_message">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlToGreenHouse" ValidationGroup="md"
                                            SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Greenhouse" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="d-none d-sm-block w-100"></div>
                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>Number Of Trays </label>

                                    <asp:TextBox ID="txtMoveNumberOfTrays" runat="server" CssClass="input__control"></asp:TextBox>
                                    <span class="error_message"></span>
                                </div>
                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>Date </label>

                                    <asp:TextBox ID="txtMoveDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                    <span class="error_message"></span>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtMoveComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="bttn-group">
                                        <asp:Button Text="Assign" ValidationGroup="e" ID="btnMoveSubmit" OnClick="btnMoveSubmit_Click" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" />
                                        <asp:Button Text="Save for later" CausesValidation="true" ID="btnSaveMove" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnSaveMove_Click" />
                                        <asp:Button Text="Start" CausesValidation="true" ID="btnStartMove" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnStartMove_Click" />
                                        <asp:Button Text="Reset" ID="MoveReset" runat="server" OnClick="MoveReset_Click" CssClass="bttn bttn-primary bttn-action mb-3" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

                <div id="dump_request" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">Dump</h2>
                        <div class="row">
                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                <label>Assignment </label>

                                <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                <asp:DropDownList ID="ddlDumptAssignment" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDA" runat="server" ControlToValidate="ddlDumptAssignment" ValidationGroup="DA"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                <label class="d-block">Dump Date</label>
                                <asp:TextBox ID="txtDumpDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                <label>Quantity of Tray </label>
                                <asp:TextBox ID="txtQuantityofTray" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="d-none d-sm-block w-100"></div>
                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                <label>Comments </label>

                                <asp:TextBox ID="txtCommentsDump" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="col-12">
                                <div class="bttn-group">
                                    <asp:Button Text="Assign" ID="btnDumpSumbit" ValidationGroup="DA" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" OnClick="btnDumpSumbit_Click" />
                                    <asp:Button Text="Save for later" CausesValidation="true" ID="btnSaveDump" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnSaveDump_Click" />
                                    <asp:Button Text="Start" CausesValidation="true" ID="btnStartDumpDetails" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnStartDumpDetails_Click" />
                                    <asp:Button Text="Reset" ID="btnDumpReset" runat="server" CssClass="bttn bttn-primary bttn-action mb-3" OnClick="btnDumpReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="general_task_request" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">General Task</h2>
                        <div id="divcomments" runat="server">
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

                                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                                <label>Assignment</label>
                                                <asp:DropDownList ID="ddlAssignments" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

                                                <%--                                                        <asp:DropDownList ID="ddlAssignments" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignments_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>--%>
                                                <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>

                                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                                <label>Task Type</label>

                                                <asp:DropDownList ID="ddlTaskType" runat="server" OnSelectedIndexChanged="ddlTaskType_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                                                 <%--   <asp:ListItem Text="--Select--" Value="0" />
                                                    <asp:ListItem Text="Add Bird Netting" Value="1" />
                                                    <asp:ListItem Text="Remove Bird Netting" Value="2" />
                                                    <asp:ListItem Text="Move" Value="3" />
                                                    <asp:ListItem Text="Other" Value="4" />--%>
                                                </asp:DropDownList>

                                                <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>

                                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                                <label>General Task Date</label>
                                                <asp:TextBox ID="txtgeneralDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                                                <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtgeneralDate" ValidationGroup="x"
                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>
                                            <div class="d-none d-sm-block w-100"></div>
                                            <div class="col-12 col-md-4 col-lg-3 mb-3">
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

                                            <div class="col-12">
                                                <div class="bttn-group">
                                                    <asp:Button Text="Send Email" ID="btnSendMail" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnSendMail_Click" Visible="false" />
                                                    <asp:Button Text="Assign" ID="btnGeneraltask" ValidationGroup="x" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnGeneraltask_Click" />
                                                    <asp:Button Text="Save for later" CausesValidation="true" ID="btnSaveGeneral" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" OnClick="btnSaveGeneral_Click" />
                                                    <asp:Button Text="Start" CausesValidation="true" ID="btnGeneralStart" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnGeneralStart_Click" />
                                                    <asp:Button Text="Reset" ID="btnGeneralReset" runat="server" CssClass="bttn bttn-primary bttn-action mb-3" OnClick="btnGeneralReset_Click" />
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


                <div id="CropHealthReportID" runat="server" class="collapse dashboard__block request__block-collapse mb-4" data-parent="#task_request-group">
                    <div class="request__body">
                        <h2 class="text-left mb-3">Crop Health Report</h2>
                        <div class="row">

                            <div class="col-12 col-md-4 col-lg-3 mb-3">
                                <label class="d-block">Assignment</label>

                                <asp:DropDownList ID="ddlCropHealthAssignment" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCropHealthAssignment" ValidationGroup="CorpA"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                <label class="d-block">Crop Health Work  Date</label>
                                <asp:TextBox ID="txtCropHealthDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="d-none d-sm-block w-100"></div>
                            <div class="mb-3 col-12 col-md-auto">
                                <label>Comments </label>

                                <asp:TextBox ID="txtCropHealthComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="d-none d-sm-block w-100"></div>
                            <div class="col-12">
                                <div class="bttn-group">
                                    <asp:Button Text="Assign" ID="btnCropHealthSubmit" ValidationGroup="CorpA" CssClass="bttn bttn-primary bttn-action mb-3" runat="server" OnClick="btnCropHealthSubmit_Click" />
                                    <asp:Button Text="Save for later" CausesValidation="true" ID="btnCropHealthSave" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnCropHealthSave_Click" />
                                    <asp:Button Text="Start" CausesValidation="true" ID="btnCropHealthStart" CssClass="submit-bttn bttn bttn-primary mb-3" runat="server" OnClick="btnCropHealthStart_Click" />
                                    <asp:Button Text="Reset" ID="btnCropReset" runat="server" CssClass="bttn bttn-primary bttn-action mb-3" OnClick="btnCropReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Button trigger modal -->
        <%--  <button type="button" class="bttn bttn-primary"  data-toggle="modal" data-target="#confirmModal">
            Assign
        </button>--%>

        <!-- Modal -->
        <div class="modal" id="confirmModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                   You already have system assigned fertilization task for this reset period under your My Task. On proceeding, those task will be removed from you My Task.?
                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="bttn bttn-primary">Proceed</button>
                        --%>
                        <asp:Button ID="btnChekFSubmit" runat="server" OnClick="btnChekFSubmit_Click" class="bttn bttn-primary" Text="Proceed" />

                         <asp:Button ID="btnChekFCancel" runat="server" OnClick="btnChekFCancel_Click"  data-dismiss="modal" class="bttn bttn-secondary" Text="Cancel" />

                     
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Ends -->


        <div class="modal" id="confirmModalCem">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                      You already have system assigned Chemical task for this reset period under your My Task. On proceeding, those task will be removed from you My Task.?
                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="bttn bttn-primary">Proceed</button>
                        --%>
                        <asp:Button ID="btnChekCemSubmit" runat="server" OnClick="btnChekCemSubmit_Click" class="bttn bttn-primary" Text="Proceed" />

                         <asp:Button ID="btnChekCemCancel" runat="server" OnClick="btnChekCemCancel_Click"  data-dismiss="modal" class="bttn bttn-secondary" Text="Cancel" />

                     
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="confirmModalIrr">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                     You already have system assigned Irrigation task for this reset period under your My Task. On proceeding, those task will be removed from you My Task.?
                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="bttn bttn-primary">Proceed</button>
                        --%>
                        <asp:Button ID="btnChekIrrigationSubmit_Click" runat="server" OnClick="btnChekIrrigationSubmit_Click_Click" class="bttn bttn-primary" Text="Proceed" />

                         <asp:Button ID="btnChekIrrigationCancel_Click" runat="server" OnClick="btnChekIrrigationCancel_Click_Click"  data-dismiss="modal" class="bttn bttn-secondary" Text="Cancel" />

                     
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
