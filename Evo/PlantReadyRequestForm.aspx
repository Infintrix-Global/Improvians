<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="PlantReadyRequestForm.aspx.cs" Inherits="Evo.PlantReadyRequestForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function checkAll(objRef) {
            var GridView = document.getElementById('<%=gvPlantReady.ClientID %>');
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex       
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function Check_Click(objRef) {
            //Get the reference of GridView
            var GridView = document.getElementById('<%=gvPlantReady.ClientID %>');
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = false;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>

    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready">
            Plant Ready
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
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Bench Location </label>
              <asp:ListBox ID="ddlBenchLocation" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" CssClass="SelectBox custom__dropdown robotomd"></asp:ListBox>
                
                
                <%-- <span class="error_message">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                </span>--%>
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

        <div class="row mb-2 mb-md-4 align-items-end">
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Crop </label>
                <asp:DropDownList ID="ddlCrop" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-lg-2 col-md-4 col-sm-12 mb-3">
                <label>Job Source </label>
                <asp:DropDownList ID="RadioButtonListSourse" runat="server" OnSelectedIndexChanged="RadioButtonListSourse_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Navision" Value="Manual"></asp:ListItem>
                    <asp:ListItem Text="App" Value="App"></asp:ListItem>
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

            <div class="col-xl-4 col-12 mb-3">
                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSelect_Click" />
                <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearch_Click" />
                <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnResetSearch_Click" />
                <%--<asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" ValidationGroup="x" />--%>
                <asp:Button ID="btnManual" runat="server" Visible="false" Text="Manual Request" CssClass="bttn bttn-primary bttn-action" OnClick="btnManual_Click" />
                <%-- <asp:Button ID="btnJob" runat="server" Text="JobBuildUp" CssClass="bttn bttn-primary bttn-action" OnClick="btnJob_Click" />--%>
            </div>
        </div>

        <%-- <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>--%>
        <div class="row">
            <div class=" col m12">
                  <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                <div class="data__table">
                    <asp:GridView ID="gvPlantReady" runat="server"  AutoGenerateColumns="False" OnRowDataBound="gvPlantReady_RowDataBound"
                        class="striped" AllowSorting="true"   PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging" AllowPaging="True"
                        GridLines="None" OnRowCommand="gvPlantReady_RowCommand" DataKeyNames="wo,jobcode,GrowerPutAwayId,PRRID,jid,IsAssistant,PlantReadySeedDate,GreenHouseID,Trays,itemdescp,GrowerPutAwayPlantReadyId,TaskRequestKey"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="autostyle2">
                                <HeaderTemplate>
                                    <div class="custom-control custom-checkbox mr-3">
                                        <asp:CheckBox ID="CheckBoxall" class="custom-control custom-checkbox" Text=" " onclick="checkAll(this);" runat="server" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="custom-control custom-checkbox mr-3">
                                        <asp:CheckBox runat="server" class="custom-control custom-checkbox"  AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" Text=" " onclick="Check_Click(this)" ID="chkSelect"></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblGreenHouseID" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                    <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbljid" runat="server" Text='<%# Eval("jid")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblIsAssistant" runat="server" Text='<%# Eval("IsAssistant")  %>' Visible="false"></asp:Label>
                                       <asp:Label ID="lblPRRID" runat="server" Text='<%# Eval("PRRID")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblTaskRequestKey" Visible="false" runat="server" Text='<%# Eval("TaskRequestKey")  %>'></asp:Label>
                                     <asp:Label ID="lbljobcode1" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />
                                    <asp:Label ID="lblGrowerputawayID21" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>


                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--  <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Labeitemno" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomer" data-head="Customer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" data-head="Plant Type" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeededDate" data-head="Seeded Date" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlantDueDate" runat="server" Text='<%# Eval("PlantDueDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Plant Ready Work Date" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlantDate" runat="server" Text='<%# Eval("PlantReadySeedDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <%--    <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateCountNo" runat="server" Text='<%# Eval("DateCountNo")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Job Source" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblsource" data-head="Job Source" runat="server" Text='<%# Eval("RequestType")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedBy" data-head="Assigened By" runat="server" Text='<%# Eval("AssignedBy")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2" HeaderStyle-Width="265px">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                    <asp:Button ID="btnStart" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="GStart" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                    <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschedule" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                                    <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" OnClientClick="return confirm('Are you sure you want to dismiss this ?'); " CommandName="Dismiss" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
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
        <div class="text-left dashboard__block my-4">

            <div id="userinput" runat="server" visible="false" class="row justify-content-center">
                <div class="col-12">
                    <div class="row" runat="server" id="divLabel">
                        <div class="col-6 col-sm-4 col-lg-3">
                            <label>Job No.</label><br />
                            <h4 class="robotobold">
                                <asp:Label ID="lblJobID" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="col-6 col-sm-4 col-lg-3">
                            <label>Bench location</label><br />
                            <h4 class="robotobold">
                                <asp:Label ID="lblBenchlocation" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="col-6 col-sm-4 col-lg-3">
                            <label>Total Trays</label><br />
                            <h4 class="robotobold">
                                <asp:Label ID="lblTotalTrays" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="col-auto col-lg-3">
                            <label>Description </label>
                            <h4 class="robotobold">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="mb-3 mb-md-0 col-12 col-md-auto">
                            <label class="d-block">Assignment</label>
                            <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>

                        <div class="mb-3 mb-md-0 col-12 col-md-auto">
                            <label class="d-block">Plant Ready Work Date</label>
                            <asp:TextBox ID="txtPlantDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="mb-3 mb-md-0 col-12 col-md-auto">
                            <label>Comments </label>
                            <asp:TextBox ID="txtPlantComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                            <h3 class="robotobold"></h3>
                            <asp:Label ID="lblGrowerID" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="lblPRRId" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="lblJid" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="lblIsAssistant" Visible="false" runat="server"></asp:Label>
                        </div>
                        <div class="w-100"></div>
                        <div class="col-lg-3">
                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />
                        <%--    <asp:Button Text="Submit" ID="btnMSubmit" Visible="false" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnMSubmit_Click" />--%>
                            <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="divReschedule" runat="server" class="assign__task d-flex" visible="false">
                <asp:Panel ID="Panel1" runat="server">
                    <h3>Reschedule Task</h3>
                    <div class="row">
                        <div class="col-auto">
                            <label>Job No.</label><br />
                            <h4 class="robotobold">
                                <asp:Label ID="lblRescheduleJobID" runat="server"></asp:Label>
                                <asp:Label ID="lblRescheduleID" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblOldDate" runat="server" Visible="false"></asp:Label>
                            </h4>
                        </div>
                        <div class="col mb-3">
                            <label class="d-block">New Plant Ready Work Date </label>
                            <asp:TextBox ID="txtNewDate" TextMode="Date" runat="server" class="input__control input__control-auto robotomd"></asp:TextBox>
                        </div>

                        <div class="col-12">
                            <asp:Button Text="Submit" ID="btnReschedule" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnReschedule_Click" />
                            <asp:Button Text="Reset" ID="btnRescheduleReset" runat="server" CssClass="ml-2 bttn bttn-primary bttn-action" OnClick="btnResetReschedule_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
