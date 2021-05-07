<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PlantReadyTaskCompletion.aspx.cs" Inherits="Evo.PlantReadyTaskCompletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2>Plant Ready Completion</h2>

        <div class="data__table">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
              <asp:Label ID="lbljid" Visible="false" runat="server" Text="Label"></asp:Label>
            <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                class="striped" AllowSorting="true"
                GridLines="None" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                ShowHeaderWhenEmpty="True" Width="100%">
                <Columns>

                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblitem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <%--    <asp:TemplateField HeaderText="Main Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Plant Ready Work Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>

                            <asp:Label ID="LabelDate" runat="server" Text='<%# Eval("PlanDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:TemplateField HeaderText="Planned Ship Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
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

        <div class="text-left dashboard__block my-4">
            <asp:Panel ID="PanelCropHealth" Visible="false" runat="server">
                <br />
                <h2 class="text-left">Crop Health Report </h2>

                <br />
                <div class="portlet-body">
                    <div class="data__table">
                        <asp:GridView ID="gvCropHealth" runat="server" AutoGenerateColumns="False"
                            class="striped" AllowSorting="true"
                            GridLines="None" DataKeyNames="chid"
                            ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderText="Type of Problem" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("typeofProblem")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cause of Problem" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("Causeofproblem")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Severity of Problem" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitem" runat="server" Text='<%# Eval("Severityofproblem")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of Trays" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("NoTrays")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="% of Damage" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("PerDamage")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="New Estimated Ship Date" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("Date","{0:MM/dd/yyyy}")  %>'></asp:Label>
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
                <div class="row">

                    <div class="col-lg-12">
                        <asp:Label ID="lblCommment" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:DataList ID="CropePhotos" runat="server"  BorderStyle="None" RepeatDirection="Horizontal" RepeatColumns="4">
                                    <ItemTemplate>
                                        <div>

                                            <asp:Image Width="100" Height="150" ID="Image1" ImageUrl='<%# Bind("Imagepath") %>' runat="server" />


                                        </div>
                                    </ItemTemplate>


                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" />
                                </asp:DataList>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <div class="portlet-body">
                <br />
                <h2 class="text-left">
                  
                    Task Request Flow</h2>
                <div class="data__table">

                    <asp:GridView ID="GridViewDumpView" runat="server" AutoGenerateColumns="False"
                        class="striped" AllowSorting="true"
                        GridLines="None"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>


                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>

                                    <asp:Label ID="lblAassignedTo" runat="server" Text='<%#Bind("Aassignedby") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To">
                                <ItemTemplate>

                                    <asp:Label ID="lblAassignedby" runat="server" Text='<%#Bind("AassignedTo") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plant Ready Work Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDumpDate" runat="server" Text='<%# Eval("PlanDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate>
                                    <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments")  %>'></asp:Label>
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
            <br />
            <asp:Panel ID="PanelComplitionDetsil" Visible="false" runat="server">
                <br />
                <%-- <h2 class="text-left">Crop Health Report </h2>--%>
                <h2 class="text-left">
                   
                    Completion  </h2>
                <br />
                <div class="portlet-body">
                    <div class="data__table">
                        <asp:GridView ID="GridPlantComplition" runat="server" AutoGenerateColumns="False"
                            class="striped" AllowSorting="true"
                            GridLines="None"
                            ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>
                                
                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Completed By">
                                        <ItemTemplate>

                                            <asp:Label ID="lblCAassignedby" runat="server" Text='<%#Bind("AassignedTo") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Updated Ready Date" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdatedReadyDate" runat="server" Text='<%# Eval("UpdatedReadyDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Plant Expiration Date" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlantExpirationDate" runat="server" Text='<%# Eval("PlantExpirationDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Root Quality" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRootQuality" runat="server" Text='<%# Eval("RootQuality")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="lblPlantHeight" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlantHeight" runat="server" Text='<%# Eval("PlantHeight")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Eval("Notes")  %>'></asp:Label>
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

            </asp:Panel>



            <br />
            <div id="PantReadyAdd" runat="server" class="row justify-content-center">
                <div class="col-12">



                    <br />



                    <div class="row">




                        <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                            <label class="d-block">Actual Plant Ready Work Date</label>

                            <asp:TextBox ID="txtUpdatedReadyDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                            <label class="d-block">Plant Expiration Date</label>
                            <asp:TextBox ID="txtPlantExpirationDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-12 col-sm-6 col-lg-3">
                            <label class="d-block">Plant Quality</label>


                            <asp:DropDownList ID="ddlRootQuality" class="custom__dropdown" runat="server">
                                <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1- Poor" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2- Good" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3- Excellent" Value="3"></asp:ListItem>

                            </asp:DropDownList>
                        </div>

                        <div class="col-12 col-sm-6 col-lg-3">
                            <label class="d-block">Plant Height [inches]</label>
                            <%--   <asp:TextBox ID="txtPlantHeight" class="input__control" runat="server"></asp:TextBox>
                            --%>
                            <asp:DropDownList ID="ddlPlantHeight" class="custom__dropdown" runat="server">
                                <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="1.5" Value="2"></asp:ListItem>
                                <asp:ListItem Text="2" Value="3"></asp:ListItem>
                                <asp:ListItem Text="2.5" Value="4"></asp:ListItem>
                                <asp:ListItem Text="3" Value="5"></asp:ListItem>
                                <asp:ListItem Text="3.5" Value="6"></asp:ListItem>

                                <asp:ListItem Text="4" Value="7"></asp:ListItem>
                                <asp:ListItem Text="4.5" Value="8"></asp:ListItem>
                                <asp:ListItem Text="5" Value="9"></asp:ListItem>
                                <asp:ListItem Text="5.5" Value="10"></asp:ListItem>
                                <asp:ListItem Text="6" Value="11"></asp:ListItem>
                                <asp:ListItem Text="6.5" Value="12"></asp:ListItem>
                            </asp:DropDownList>

                        </div>

                        <%-- <div class="col-12 col-sm-6 col-lg-4 col-xl-3">
                                    <label class="d-block">Notes</label>
                                    <asp:TextBox ID="txtNots" TextMode="MultiLine" class="w-100 input__control" runat="server"></asp:TextBox>
                                </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-12 col-sm-6 col-md-4 col-lg-3">



                            <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" class="ml-2 submit-bttn bttn bttn-primary" runat="server" Text="Submit" />
                            <asp:Button ID="btnReset" OnClick="btnReset_Click" class="submit-bttn bttn bttn-primary" runat="server" Text="Reset" />

                        </div>
                    </div>
                </div>
            </div>



        </div>

        <asp:Panel ID="PanelView" runat="server">
            <h4 class="mt-4 mt-lg-3">Task Requests:</h4>

            <div class="task_request_assignments" id="task_request-group">

                <div id="GeneralTaskId" runat="server" class="task_request-buttons">


                    <asp:LinkButton runat="server" ID="btnGeneral_Task1" ForeColor="Black" class="request__block-head collapsed" OnClick="btnGeneral_Task1_Click">
                            <span class="">
                                <img src="./images/dashboard_general-task.png" width="137" height="134" alt="General Task" />
                                General Task
                            </span>
                    </asp:LinkButton>

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
                                                    <asp:ListItem Text="--Select--" Value="0" />
                                                    <asp:ListItem Text="Add Bird Netting" Value="1" />
                                                    <asp:ListItem Text="Remove Bird Netting" Value="2" />
                                                    <asp:ListItem Text="Move" Value="3" />
                                                    <asp:ListItem Text="Other" Value="4" />
                                                </asp:DropDownList>


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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtgeneralComment" ValidationGroup="x"
                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>

                                            <div class="col-xl-3" id="divFrom" style="display: none;" runat="server">
                                                <label>From</label>
                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="input__control"></asp:TextBox>

                                                <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtFrom" ValidationGroup="x"
                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>
                                            <div class="col-xl-3" id="divTo" style="display: none;" runat="server">
                                                <label>To</label>
                                                <asp:TextBox ID="txtTo" runat="server" CssClass="input__control"></asp:TextBox>

                                                <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTo" ValidationGroup="x"
                                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>

                                            <div class="col-12">
                                                <asp:Button Text="Send Email" ID="btnSendMail" CssClass="submit-bttn bttn bttn-primary mr-2" runat="server" OnClick="btnSendMail_Click" Visible="false" />


                                                <asp:Button Text="Submit" ID="btnGeneraltask" CssClass="submit-bttn bttn bttn-primary" runat="server" OnClick="btnGeneraltask_Click" />

                                                <asp:Button Text="Reset" ID="btnGeneralReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnGeneralReset_Click" />

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
        </asp:Panel>
    </div>
</asp:Content>
