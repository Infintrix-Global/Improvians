<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="FerJobBuildUp.aspx.cs" Inherits="Improvians.FerJobBuildUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main__header">
        <div class="site__container">
            <h2 class="head__title-icon">
                <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Fertilization / Chemical">
                Fertilization/Chemical 


            </h2>
            <div class="row">


                <div class="col-lg-3">
                    <label>Bench Location </label>
                    <asp:Label ID="lblbench" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <h3>Selected Jobs</h3>
                                <asp:GridView ID="gvFer" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" PageSize="10"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <%--  <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBoxall" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox runat="server" ID="chkSelect"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
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
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--   <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                 <asp:Button ID="btnReschdule" runat="server" Text="Reschdule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                 <asp:Button ID="btnDismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>

                                    <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <EmptyDataTemplate>
                                        No Record Available
                                    </EmptyDataTemplate>
                                </asp:GridView>

                                <br />
                                <h3>Other Jobs</h3>

                                <asp:GridView ID="gvJobHistory" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" PageSize="10"
                                    GridLines="None" Visible="false"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <%--  <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBoxall" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox runat="server" ID="chkSelect"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
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
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--   <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                 <asp:Button ID="btnReschdule" runat="server" Text="Reschdule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                                 <asp:Button ID="btnDismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>

                                    <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <EmptyDataTemplate>
                                        No Record Available
                                    </EmptyDataTemplate>
                                </asp:GridView>

                                <%--    <asp:GridView ID="GridViewDetails" class="table table-bordered table-hover"
                                                        AutoGenerateColumns="false" runat="server">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fertilizer" HeaderStyle-Width="40%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFertilizer" runat="server" Text='<%#Bind("Fertilizer") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%#Bind("Unit") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tray" HeaderStyle-Width="15%">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblTray" runat="server" Text='<%#Bind("Tray") %>'></asp:Label>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SQFT" HeaderStyle-Width="10%">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblSQFT" runat="server" Text='<%#Bind("SQFT") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                                        <PagerSettings Mode="NumericFirstLast" />
                                                        <EmptyDataTemplate>
                                                            No Record Available
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>--%>
                            </div>
                        </div>

                    </div>
                </div>

            </div>


            <div class="dashboard__block dashboard__block--asign">


                <div id="userinput" runat="server" class="assign__task d-flex">

                    <asp:Panel ID="pnlint" runat="server">
                        <div class="row">

                            <div class="col-lg-3">
                                <label class="d-block">Assignment </label>
                                <asp:DropDownList ID="ddlsupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsupervisor" ValidationGroup="e"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Supervisor" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>



                            <div class="col-lg-3">
                                <label>Type of Request</label>

                                <asp:RadioButtonList ID="radtype" Width="250px" runat="server" OnSelectedIndexChanged="radtype_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Chemical" Value="Chemical" class="custom-control custom-radio mr-2"></asp:ListItem>
                                    <asp:ListItem Text="Fertilizer" Value="Fertilizer" class="custom-control custom-radio" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">

                                <label>
                                    <asp:Label ID="lbltype" runat="server" Text="Fertilizer"></asp:Label></label><br />
                                <asp:DropDownList ID="ddlFertilizer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlFertilizer" ValidationGroup="md"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Fertilizer" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-lg-3">
                                <label>Concentration [ppm]</label>
                                <asp:TextBox ID="txtQty" AutoPostBack="true" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQty" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <%-- <div class="col">
                                <label>Unit </label>
                                <asp:DropDownList ID="ddlUnit" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUnit" ValidationGroup="md"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Unit" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>--%>

                            <div class="col-lg-3">
                                <label>Trays</label>
                                <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtTrays" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                <%-- <span class="error_message">
                                    <asp:Label ID="lblerrmsg" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTrays" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Trays" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>--%>
                            </div>

                            <div class="col-lg-3">
                                <label>SQFT of Bench </label>

                                <asp:TextBox ID="txtSQFT" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                                <span class="error_message">
                                    <asp:Label ID="Label2" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSQFT" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter SQFT" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-lg-3">
                                <label>Bench Irrigation Flow Rate [Gallons/min]</label>
                                <asp:TextBox ID="txtBenchIrrigationFlowRate" TextMode="Number"  runat="server" CssClass="input__control"></asp:TextBox>

                            </div>
                            <div class="col-lg-3">
                                <label>Bench Irrigation Coverage [Gallons/Sqft]</label>
                                <asp:TextBox ID="txtBenchIrrigationCoverage"  TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                            </div>
                            <div class="col-lg-3">
                                <label>Spray Coverage per minutes [sqft/min]</label>
                                <asp:TextBox ID="txtSprayCoverage"  TextMode="Number"  runat="server" CssClass="input__control"></asp:TextBox>

                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                        <%--  <div class="col align-self-center">
                                <asp:Button ID="btnAddTray" OnClick="btnAddTray_Click" class="submit-bttn bttn bttn-primary mb-0" runat="server" Text="Add" TabIndex="13" ValidationGroup="md" />
                            </div>--%>
                        <%-- <div class="col-12">
                                <div class="data__table">
                                    <asp:Panel ID="pnlPoints" runat="server" CssClass="pnlpoint">
                                        <asp:GridView runat="server" ID="gvFerDetails" AutoGenerateColumns="false" class="Grid1 mb-3"
                                            GridLines="None" CaptionAlign="NotSet" Width="801px" ForeColor="Black"
                                            ShowHeaderWhenEmpty="true">
                                            <Columns>


                                                <asp:TemplateField HeaderText="Fertilizer" ItemStyle-Width="10%">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblFertilizer" runat="server" Text='<%# Bind("[Fertilizer]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="QTY" ItemStyle-Width="10%">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblQTY" runat="server" Text='<%# Bind("[Quantity]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Unit of Measurement" ItemStyle-Width="10%">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("[Unit]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tray" ItemStyle-Width="10%">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblTray" runat="server" Text='<%# Bind("[Tray]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SQFT" ItemStyle-Width="10%">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblSQFT" runat="server" Text='<%# Bind("[SQFT]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Button class="submit-bttn bttn bttn-primary mb-0" ID="deletebtn" runat="server" CommandName="Delete"
                                                            Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>--%>

                        <br />
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnSubmit_Click" />

                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
