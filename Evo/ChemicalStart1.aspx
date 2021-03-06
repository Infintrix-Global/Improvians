﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="ChemicalStart1.aspx.cs" Inherits="Evo.ChemicalStart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Chemical">
            Chemical
        </h2>


        <div class="portlet light ">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">


                <div class="data__table data__table-height">
                    <asp:GridView ID="gvJobHistory" runat="server" AutoGenerateColumns="False"
                        class="striped"
                        GridLines="None"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                    <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbljid" runat="server" Text='<%# Eval("jid")  %>' Visible="false"></asp:Label>

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


        <div class="dashboard__block dashboard__block--asign">
            <div id="userinput" runat="server" class="assign__task d-flex">
                <asp:Panel ID="pnlint" runat="server">
                    <h3>Assign Task</h3>
                    <div class="row">


                        <div class="col-sm-6 col-md-4 col-lg-3 mb-3">
                            <label>
                                <asp:Label ID="lbltype" runat="server" Text="Chemical"></asp:Label>
                            </label>

                            <asp:DropDownList ID="ddlFertilizer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col-sm-6 col-md-4 col-lg-3 mb-3">
                            <label class="d-block">Method</label>
                            <asp:DropDownList ID="ddlMethod" class="custom__dropdown robotomd" runat="server" RepeatDirection="Horizontal" />
                        </div>

                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>Chemical Spray Date </label>

                            <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="Label3" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDate" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Date" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>

                        <div class="col-sm-6 col-md-4 col-lg-3 mb-3">
                            <label>Trays</label>
                            <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtTrays" Enabled="false" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <label>SQFT of Bench </label>

                            <asp:TextBox ID="txtSQFT" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                            <span class="error_message">
                                <asp:Label ID="Label2" runat="server" ForeColor="red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSQFT" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter SQFT" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                        </div>

                        <div class="col-sm-6 col-md-4 col-lg-3 mb-3">
                            <label>Minimum Days Until Next Chemical</label>
                            <asp:TextBox ID="txtResetSprayTaskForDays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="w-100"></div>

                        <div class="col-sm-6 col-md-4 col-lg-3 mb-3">
                            <label>Comments</label>
                            <asp:TextBox ID="txtComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="col-12">
                            <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnSubmit_Click" />

                            <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
