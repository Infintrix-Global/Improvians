<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GerminationAssignmentForm.aspx.cs" Inherits="Evo.GerminationAssignmentForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function checkAll(objRef) {
            var GridView = document.getElementById('<%=gvGerm.ClientID %>');
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
            var GridView = document.getElementById('<%=gvGerm.ClientID %>');
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
            <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Count">
            Germination Count Task
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
                <label>Germination Count Type </label>
                <asp:DropDownList ID="RadioButtonListGno" runat="server" OnSelectedIndexChanged="RadioButtonListF_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                    <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                    <asp:ListItem Text="Germination 1" Value="Germination 1"></asp:ListItem>
                    <asp:ListItem Text="Germination 2" Value="Germination 2"></asp:ListItem>
                    <asp:ListItem Text="Crop Health" Value="Crop Health"></asp:ListItem>
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
            <div class="col-12 pt-3 mb-3">
                           <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSelect_Click" />
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
                            <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="TaskRequestKey,ID,wo,CropHealth"
                                class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                GridLines="None" OnRowCommand="gvGerm_RowCommand" OnRowDataBound="gvGerm_RowDataBound"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="20px" HeaderStyle-CssClass="autostyle2">
                                        <HeaderTemplate>
                                            <div class="custom-control custom-checkbox mr-3">
                                                <asp:CheckBox ID="CheckBoxall" class="custom-control custom-checkbox" Text=" " AutoPostBack="true"  onclick="checkAll(this);" runat="server" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="custom-control custom-checkbox mr-3">
                                                <asp:CheckBox runat="server" class="custom-control custom-checkbox" Text=" " AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" onclick="Check_Click(this)" ID="chkSelect"></asp:CheckBox>
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
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblTaskRequestKey" runat="server" Text='<%# Eval("TaskRequestKey")  %>' Visible="false"></asp:Label>

                                            <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            <asp:HyperLink ID="lnkJobID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:HyperLink>

                                            <%--    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode,GermNo","~/JobReports.aspx?JobCode={0}&GermNo={1}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />--%>

                                            <asp:Label ID="lblChid" Visible="false" runat="server" Text='<%# Eval("CropHealth")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

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
                                    <asp:TemplateField HeaderText="Total Trays" ItemStyle-Width="40px" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrays" data-head="Total Trays" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray Size" ItemStyle-Width="40px" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTraySize" data-head="Tray Size" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label22" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label17" runat="server" Text='<%# Eval("InspectionDueDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Germination Count Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGermDate" runat="server" Text='<%# Eval("GermDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Germination Count Type" ItemStyle-Width="70px" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGermNo" data-head="Germination Count Type" runat="server" Text='<%# Eval("GermNo")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Job Source" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsource" data-head="Job Source" runat="server" Text='<%# Eval("RequestType")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>

                                            <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Assign" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

                                            <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Start" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

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


        <div runat="server" Id="AddDetails" visible="false" class="dashboard__block dashboard__block--asign">
            <h3>Assign Task</h3>
        
            <div id="userinput" runat="server" class="assign__task d-flex">
                <asp:Panel ID="pnlint" runat="server">

                    <asp:Label ID="lblTaskRequestKey" Visible="false" runat="server" Text=""></asp:Label>
                    <div class="row">
                        <div class="col">
                            <label>Assignment</label>
                            <asp:DropDownList ID="ddlOperator" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col">
                            <label>Germination Count Date</label>
                            <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>
                        <div class="col">
                            <label>Number Of Trays To Inspect</label>
                            <asp:TextBox ID="txtTrays" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="col">
                            <label>Notes</label>
                            <asp:TextBox ID="txtNotes" TextMode="Multiline" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>



                        <div class="col-auto">
                            <br />
                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                        </div>
                        <div class="col-auto">
                            <br />
                            <asp:Button Text="Reset" ID="btnReset" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
