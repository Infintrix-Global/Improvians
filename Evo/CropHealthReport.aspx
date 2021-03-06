﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="CropHealthReport.aspx.cs" Inherits="Evo.CropHealthReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="js/jquery.min.js"></script>--%>
    <script type="text/javascript" src="scripts/jquery-1.3.2.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>

    <script src="js/jquery.uploadify.js"></script>
    <script>
        $(document).ready(function () {
            $('[id$=takePictureField]').on("change", gotPic);


            if (window.File && window.FileList && window.FileReader) {
                $("#files").on("change", function (e) {
                    var files = e.target.files,
                        filesLength = files.length;
                    for (var i = 0; i < filesLength; i++) {
                        var f = files[i]
                        var fileReader = new FileReader();
                        fileReader.onload = (function (e) {
                            var file = e.target;
                            $("<span class=\"pip\">" +
                                "<img class=\"img-fluid imageThumb\" src=\"" + e.target.result + "\" title=\"" + file.name + "\"/>" +
                                "<br/><span class=\"remove small d-block\">Remove image</span>" +
                                "</span>").insertAfter("#files");
                            $(".remove").click(function () {
                                $(this).parent(".pip").remove();
                            });

                            // Old code here
                            /*$("<img></img>", {
                              class: "imageThumb",
                              src: e.target.result,
                              title: file.name + " | Click to remove"
                            }).insertAfter("#files").click(function(){$(this).remove();});*/

                        });
                        fileReader.readAsDataURL(f);
                    }
                });
                $("#takePictureField").on("change", function (e) {
                    var files = e.target.files,
                        filesLength = files.length;
                    for (var i = 0; i < filesLength; i++) {
                        var f = files[i]
                        var fileReader = new FileReader();
                        fileReader.onload = (function (e) {
                            var file = e.target;
                            $("<span class=\"pip\">" +
                                "<img class=\"imageThumb\" src=\"" + e.target.result + "\" title=\"" + file.name + "\"/>" +
                                "<br/><span class=\"remove\">Remove image</span>" +
                                "</span>").insertAfter("#takePictureField");
                            $(".remove").click(function () {
                                $(this).parent(".pip").remove();
                            });

                            // Old code here
                            /*$("<img></img>", {
                              class: "imageThumb",
                              src: e.target.result,
                              title: file.name + " | Click to remove"
                            }).insertAfter("#files").click(function(){$(this).remove();});*/

                        });
                        fileReader.readAsDataURL(f);
                    }
                });
            } else {
                alert("Your browser doesn't support to File API")
            }

        });

        function gotPic(event) {
            if (event.target.files.length == 1 &&
                event.target.files[0].type.indexOf("image/") == 0) {
                $('[id$=yourimage]').attr("src", URL.createObjectURL(event.target.files[0]));
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_crop-health-report.png" width="137" height="136" alt="Fertilization / Chemical">
            Crop Health Report
        </h2>
        <asp:Panel ID="PanelList" runat="server">
            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table data__table-height">
                                <asp:Label ID="lblJid" Visible="false" runat="server" Text=""></asp:Label>
                                <asp:GridView ID="gvFer" runat="server" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBoxall" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" />
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
                                                <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                                <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblj_id" runat="server" Text='<%# Eval("jid")  %>' Visible="false"></asp:Label>


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

        </asp:Panel>
        <asp:Panel ID="PanelView" runat="server">
            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="Label4"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table data__table-height">
                                <asp:GridView ID="GridViewView" runat="server" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
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
                                                <asp:Label ID="lblJid" runat="server" Visible="false" Text='<%# Eval("jid")  %>'></asp:Label>
                                                <asp:Label ID="lblGenusCode1" runat="server" Visible="false" Text='<%# Eval("jid")  %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomer1" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitem1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotTray1" runat="server" Text='<%# Eval("Trays","{0:####}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraySize1" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeededDate1" runat="server" Text='<%# Eval("SeedDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemdesc1" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
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
        </asp:Panel>
        <div class="dashboard__block dashboard__block--asign">
            <div id="userinput" runat="server" class="assign__task d-flex" visible="true">
                <asp:Panel ID="pnlint" runat="server">
                    <div class="row">
                        <div class="col-lg-3">
                            <label>Type of problem</label><span style="color: red">*</span>
                            <asp:DropDownList ID="ddlpr" runat="server" class="custom__dropdown robotomd">
                            </asp:DropDownList>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlpr" ValidationGroup="x"
                                    SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>

                        <div class="col-lg-3">
                            <label>Cause of problem</label><span style="color: red">*</span>
                            <asp:DropDownList ID="DropDownListCause" runat="server" class="custom__dropdown robotomd">
                                <%--   <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Cause1</asp:ListItem>
                                <asp:ListItem Value="2">Cause2</asp:ListItem>
                                <asp:ListItem Value="3">Cause3</asp:ListItem>--%>
                            </asp:DropDownList>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownListCause" ValidationGroup="x"
                                    SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>

                        <div class="col-lg-3">
                            <label>Severity of problem</label><span style="color: red">*</span>
                            <asp:DropDownList ID="DropDownListSv" runat="server" class="custom__dropdown robotomd">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>

                            </asp:DropDownList>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownListSv" ValidationGroup="x"
                                    SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>


                    </div>


                    <div class="row">
                        <div class="col-lg-3">
                            <label>No. of Trays</label>
                            <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="lblerrmsg" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTrays" ValidationGroup="md"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Trays" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="col-lg-3">
                            <label>% of Damage</label>
                            <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="percentageDamage" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="Label2" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTrays" ValidationGroup="md"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Trays" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="col-lg-3">
                            <label>New Estimated Ship Date </label>

                            <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="Label3" runat="server" ForeColor="red"></asp:Label>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDate" ValidationGroup="e"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Date" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </span>

                        </div>
                        <div class="col-lg-12">

                            <span class="d-flex align-items-center" id="divLaptop" runat="server" visible="false">
                                <input type="file" id="files" name="files" multiple class="custom-file-input" />
                                <label for="files" class="mb-3">
                                    <span class="custom-file-bttn bttn bttn-sm bttn-primary">
                                        <i class="fas fa-upload"></i>
                                        <span class="ml-2">Upload file</span>
                                    </span>
                                </label>
                            </span>


                            <asp:Label ID="lblMessage" ForeColor="Green" runat="server" />
                            <div id="divMobile" runat="server" visible="false">
                                <input type="file" accept="image/*;capture=camera" id="takePictureField" name="takePictureField" multiple />
                                <div class="row">
                                    <div class="col m6">
                                        <img id="yourimage" runat="server" width="320" height="240" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3">
                            <label>Comments </label>

                            <asp:TextBox ID="txtcomments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                        </div>

                        <div class="col-lg-3">
                        </div>

                        <div class="col-lg-3">
                        </div>


                    </div>
                    <br />

                </asp:Panel>


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
                            <img src="./images/dashboard_chemical.png" width="137" height="136" alt="Chemical">
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
                                <div class="col-md-4 col-xl-3 mb-3">
                                    <label class="d-block">Assignment</label>
                                    <asp:DropDownList ID="ddlgerminationSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 col-xl-3 mb-3">
                                    <label class="d-block">Germination Count Date</label>
                                    <asp:TextBox ID="txtGerDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                                </div>
                                <div class="col-md-4 col-xl-3 mb-3">
                                    <label class="d-block">Number Of Trays To Inspect</label>
                                    <asp:TextBox ID="txtTGerTrays" TextMode="Number" runat="server" class="input__control robotomd"></asp:TextBox>
                                </div>

                            </div>

                            <br />
                            <div class="row">
                                <div class="mb-xl-3 col-12 col-xl-6 align-self-end">
                                    <asp:Button Text="Assign" ID="btngerminationSumit" CssClass="bttn bttn-primary bttn-action" OnClick="btngerminationSumit_Click" runat="server" />
                                    <asp:Button Text="Save for later" CausesValidation="true" Visible="false" ID="btnBSaveSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnBSaveSubmit_Click" />
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
                                <div class="mb-3 col-xl-2 col-md-6 col-12">
                                    <label class="pr-2 pr-lg-0 d-lg-block"># of passes</label>
                                    <asp:TextBox ID="txtNoOfPasses" CssClass="input__control" placeholder="" runat="server"></asp:TextBox>
                                </div>

                                <%--<div class="mb-3 col-xl-3 col-md-6 col-12">
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

                                    </div>--%>
                                <div class="mb-3 col-xl-3 col-md-6 col-12">
                                    <label>Minimum Days Until Next Fertilization</label>
                                    <asp:TextBox ID="txtResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>
                                <%-- <div class="mb-3 col-xl-3 col-md-6 col-12">
                                        <label>Comments </label>

                                        <asp:TextBox ID="txtFComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                                    </div>--%>
                            </div>

                            <div class="col-auto">


                                <asp:Button Text="Assign" CausesValidation="true" ID="btnFSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnFSubmit_Click" />
                                <asp:Button Text="Save for later" CausesValidation="true" Visible="false" ID="btnSaveFLSubmit" CssClass="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSaveFLSubmit_Click" />
                                <asp:Button Text="Reset" ID="btnFReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnFReset_Click" />



                            </div>

                        </div>
                    </div>

                    <div id="Chemical_count" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <br />
                            <h2 class="text-left">Chemical</h2>
                            <br />
                            <%--   <div class="row align-items-end">--%>
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="row">
                                    <div class="col-lg-4 col-xl-3 mb-3">
                                        <label class="d-block">Assignment </label>
                                        <asp:DropDownList ID="ddlChemical_supervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
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
                                        <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtChemicalTrays" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4 col-xl-3 mb-3">
                                        <label>SQFT of Bench </label>

                                        <asp:TextBox ID="txtChemicalSQFTofBench" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                                        <span class="error_message">
                                            <asp:Label ID="Label6" runat="server" ForeColor="red"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSQFT" ValidationGroup="e"
                                                SetFocusOnError="true" ErrorMessage="Please Enter SQFT" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="col-lg-4 col-xl-3 mb-3">
                                        <label>Minimum Days Until Next Chemical</label>
                                        <asp:TextBox ID="txtResetChemicalSprayTask" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                    </div>
                                    <div class="w-100"></div>
                                    <%-- <div class="col-lg-4 col-xl-3 mb-3">
                                        <label>Comments </label>

                                        <asp:TextBox ID="txtCComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                                    </div>--%>
                                    <div class="w-100"></div>
                                    <div class="col-auto">
                                        <asp:Button Text="Assign" CausesValidation="true" ID="btnChemicalSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnChemicalSubmit_Click" />
                                        <asp:Button Text="Save for later" Visible="false" CausesValidation="true" ID="btnChemicalSFLSubmit" CssClass="submit-bttn bttn bttn-primary" runat="server" OnClick="btnChemicalSFLSubmit_Click" />
                                        <asp:Button Text="Reset" ID="btnChemicalReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnChemicalReset_Click" />
                                    </div>
                                </div>
                            </asp:Panel>

                            <%-- </div>--%>
                        </div>
                    </div>



                    <div id="irrigation_count" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <h2 class="text-left mb-3">Irrigation</h2>
                            <div class="row">
                                <div class="col-xl-3 col-lg-4 col-sm-6 mb-3">
                                    <label class="d-block">Assignment</label>
                                    <asp:DropDownList ID="ddlirrigationSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
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
                            <%-- <div class="row">
                                <div class="col-xl-3 col-lg-4 mb-3">
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtIrrComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                            </div>--%>
                            <%-- <div class="row">
                                    <div class="col-xl-3">

                                        <label class="d-block">Comments:</label>
                                        <asp:TextBox ID="txtirrigationNotes" TextMode="MultiLine" class="w-100 input__control" placeholder="Notes" runat="server"></asp:TextBox>
                                    </div>

                                </div>--%>
                            <div class="row">

                                <div class="mb-xl-3 col-12 col-xl-6 align-self-end">

                                    <asp:Button Text="Assign" ID="btnirrigationSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnirrigationSubmit_Click" />
                                    <asp:Button Text="Save for later" Visible="false" CausesValidation="true" ID="btnSaveirrigation" CssClass="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSaveirrigation_Click" />
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
                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label class="d-block">Assignment</label>

                                    <asp:DropDownList ID="ddlplant_readySupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                </div>
                                <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                    <label class="d-block">Plant Ready Work Date</label>
                                    <asp:TextBox ID="txtPlantDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>
                                <div class="d-none d-sm-block w-100"></div>
                                <%-- <div class="mb-3 col-12 col-md-auto">
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtPlantComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>--%>
                                <div class="d-none d-sm-block w-100"></div>
                                <div class="mb-3 mb-md-0 col-12 col-md-auto align-self-end">
                                    <asp:Button Text="Assign" ID="btnplant_readySubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnplant_readySubmit_Click" />
                                    <asp:Button Text="Save for later" Visible="false" CausesValidation="true" ID="btnSavePlantReady" CssClass="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSavePlantReady_Click" />
                                    <asp:Button Text="Reset" ID="btnplant_readyReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnplant_readyReset_Click" />

                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="move_request" class="collapse request__block-collapse" data-parent="#task_request-group">
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
                                <%-- <div class="row">
                                    <div class="col-12 col-md-4 col-lg-3 mb-3">
                                        <label>Comments </label>

                                        <asp:TextBox ID="txtMoveComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div class="row">
                                    <div class="col-auto">
                                        <asp:Button Text="Assign" ValidationGroup="e" ID="btnMoveSubmit" OnClick="btnMoveSubmit_Click" CssClass="bttn bttn-primary bttn-action" runat="server" />
                                        <asp:Button Text="Save for later" Visible="false" CausesValidation="true" ID="btnSaveMove" CssClass="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSaveMove_Click" />
                                        <asp:Button Text="Reset" ID="MoveReset" runat="server" OnClick="MoveReset_Click" CssClass="bttn bttn-primary bttn-action" />
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>

                    <div id="dump_request" class="collapse request__block-collapse" data-parent="#task_request-group">
                        <div class="request__body">
                            <h2 class="text-left mb-3">Dump</h2>
                            <div class="row">
                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>Assignment </label>

                                    <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                    <asp:DropDownList ID="ddlDumptAssignment" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
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
                                <%-- <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>Comments </label>

                                    <asp:TextBox ID="txtCommentsDump" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>--%>
                                <div class="mb-3 col-12 align-self-end">
                                    <asp:Button Text="Assign" ID="btnDumpSumbit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnDumpSumbit_Click" />
                                    <asp:Button Text="Save for later" Visible="false" CausesValidation="true" ID="btnSaveDump" CssClass="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSaveDump_Click" />
                                    <asp:Button Text="Reset" ID="btnDumpReset" runat="server" CssClass="bttn bttn-primary bttn-action" />
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
                                                        <%--<asp:DropDownList ID="ddlAssignments" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlAssignments_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>--%>
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
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtgeneralComment" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Enter comments" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>



                                                    <div class="col-xl-3" id="divFrom" style="display: none;" runat="server">
                                                        <label>From</label>
                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="input__control"></asp:TextBox>

                                                        <span class="error_message">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtFrom" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Enter From" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>
                                                    <div class="col-xl-3" id="divTo" style="display: none;" runat="server">
                                                        <label>To</label>
                                                        <asp:TextBox ID="txtTo" runat="server" CssClass="input__control"></asp:TextBox>

                                                        <span class="error_message">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTo" ValidationGroup="x"
                                                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Enter To" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>

                                                    <div class="mb-3 mb-md-0 col-12 col-md-auto align-self-end">
                                                        <div style="margin-top: 5%;">
                                                            <asp:Button Text="Send Email" ID="btnSendMail" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSendMail_Click" Visible="false" />
                                                            <asp:Button Text="Assign" ID="btnGeneralSubmit" type="submit" CssClass="bttn bttn-primary bttn-action" OnClick="btnGeneralSubmit_Click" runat="server" />
                                                            <asp:Button Text="Save for later" Visible="false" CausesValidation="true" ID="btnSaveGeneral" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSaveGeneral_Click" />
                                                            <asp:Button Text="Reset" ID="btnGeneralReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnGeneralReset_Click" />




                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <div class="row">
                <div class="col-auto">

                    <asp:Button Text="Submit" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnSubmit_Click" />

                    <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                </div>
            </div>
        </div>

        <%--  <div class="form__action mt-4">
                    <button type="reset" class="mr-2 bttn bttn-primary mb-2">
                        Reset
                    </button>
                    <button type="button" class="mr-2 bttn bttn-primary mb-2">
                        Save for Later
                    </button>
                    <button type="submit" class="bttn bttn-primary mb-2">
                        Submit
                    </button>
                </div>--%>
    </div>
</asp:Content>
