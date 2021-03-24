<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ViewPlantProductionProfile.aspx.cs" Inherits="Evo.Admin.ViewPlantProductionProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Plant Production Profile</h1>

            <hr />
            <!-- BEGIN FORM-->
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:Panel ID="pnlAdd" runat="server" Visible="false">

                <%--<div class="filter__row d-flex">--%>
                <%-- <div class="row">
                    <div class="col m3">
                        <label>
                            Code
                                    <label style="color: red">*</label></label>
                        <asp:TextBox ID="txtCode" class="input__control" placeholder="Enter code" runat="server"></asp:TextBox>
                        <span class="error_message">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode" ValidationGroup="e"
                                SetFocusOnError="true" ErrorMessage="Please Enter Code" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <cc1:AutoCompleteExtender ServiceMethod="SearchCode"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtCode"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                        </cc1:AutoCompleteExtender>
                    </div>
                    <div class="col m3">
                        <label>
                            Crop
                                    <label style="color: red">*</label></label>
                        <asp:TextBox ID="txtCrop" class="input__control" placeholder="Enter crop" runat="server"></asp:TextBox>
                        <span class="error_message">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCrop" ValidationGroup="e"
                                SetFocusOnError="true" ErrorMessage="Please Enter Crop" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <cc1:AutoCompleteExtender ServiceMethod="SearchCrop"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtCrop"
                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                        </cc1:AutoCompleteExtender>
                    </div>

                    <div class="col m3">
                        <label>
                            Tray Code
                                    <label style="color: red">*</label></label>
                        <asp:TextBox ID="txtTray" class="input__control" TextMode="Number" placeholder="Enter tray code" runat="server"></asp:TextBox>
                        <span class="error_message">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTray" ValidationGroup="e"
                                SetFocusOnError="true" ErrorMessage="Please Enter Tray Code" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                    </div>
                </div>
                <div class="row">
                    <div class="col m3">
                        <label>
                            Activity Code
                                    <label style="color: red">*</label></label>
                        <asp:DropDownList ID="ddlActivity" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>

                    <div class="col m3">
                        <label>Date Shift<label style="color: red">*</label></label>
                        <asp:TextBox ID="txtName" class="input__control" TextMode="Number" placeholder="Enter date shift" runat="server"></asp:TextBox>
                        <span class="error_message">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName" ValidationGroup="e"
                                SetFocusOnError="true" ErrorMessage="Please Enter Date Shift" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                    </div>

                    <div class="col-auto">
                        <br />
                        <asp:Button ID="btAdd" runat="server" Text="Submit" TabIndex="10" class="submit-bttn bttn bttn-primary" OnClick="btAdd_Click" ValidationGroup="e" />

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" class="submit-bttn bttn bttn-primary" ClientIDMode="Static" OnClick="btnCancel_Click" />
                    </div>

                </div>--%>
                <%--</div>--%>
                <div class="portlet-body">
                    <div class="data__table data__table-height">
                        <asp:GridView ID="GridProfile" runat="server" ShowFooter="true" Width="100%" OnRowDeleting="GridProfile_RowDeleting"
                            AutoGenerateColumns="false" OnRowDataBound="GridProfile_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="RowNumber" HeaderText="NO." />
                                <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCode" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCode" ValidationGroup="e"
                                            InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Enter Code" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnCode" runat="server" Value='<%# Eval("Code")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCrop" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCrop" ValidationGroup="e"
                                            InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Enter Crop" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnCrop" runat="server" Value='<%# Eval("Crop")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tray Size">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTraySize" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTraySize" ValidationGroup="e"
                                            InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Enter Tray Size" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnTraySize" runat="server" Value='<%# Eval("TraySize")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity Code" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlActivityCode" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlActivityCode" ValidationGroup="e"
                                            InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Enter Activity Code" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnActivityCode" runat="server" Value='<%# Eval("ActivityCode")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Shift" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDateShift" class="custom__dropdown robotomd" runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hdnDateShift" runat="server" Value='<%# Eval("DateShift")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Button Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="Delete" ID="btnRemove" runat="server" CssClass="bttn bttn-primary bttn-action" />

                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Button ID="ButtonAdd" OnClick="btAdd_Click" runat="server" CausesValidation="true" ValidationGroup="e"
                                            Text="Add Row" CssClass="bttn bttn-primary bttn-action" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>


                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-auto">
                        <br />
                        <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" CausesValidation="true" ValidationGroup="e" OnClick="btnSubmit_Click" runat="server" />
                    </div>
                    <div class="col-auto">
                        <br />
                        <asp:Button Text="Reset" ID="btnReset" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlList" runat="server" Visible="true">
                <div class="filter__row d-flex">
                    <div class="row">
                        <div class="col m3">
                            <label>Filter Crop </label>

                            <asp:DropDownList ID="ddlCrop" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col m3">
                            <label>Tray Code </label>

                            <asp:DropDownList ID="ddlTrayCode" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlTrayCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col m3">
                            <label>Activity Code </label>

                            <asp:DropDownList ID="ddlActivityCode" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlActivityCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>

                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnSearch" Text="Search" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnSave" Text="Save" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSave_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnClear" Text="Clear" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnClear_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnAddProfile" Text="Add" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnAddProfile_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="filter__row d-flex">
                <div class="row justify-content-lg-center">
                    <div class=" col m12">
                        <div class="portlet light ">
                            <asp:Label runat="server" Text="" ID="count"></asp:Label>
                            <div class="portlet-body">
                                <div class="data__table">

                                    <asp:GridView ID="gvProductionProfile" runat="server" AutoGenerateColumns="False"
                                        class="striped" OnRowCommand="gvProductionProfile_RowCommand"
                                        GridLines="None"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Code" ItemStyle-Width="30%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("code")  %>'></asp:Label>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("pid")  %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Crop" ItemStyle-Width="30%" HeaderStyle-CssClass="autostyle2" SortExpression="EmployeeName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("crop")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tray Code" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2" SortExpression="EmployeeName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("traycode")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Activity Code" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("activitycode")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date Shift" ItemStyle-Width="50%" HeaderStyle-CssClass="autostyle2" SortExpression="RoleName">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="Label10" runat="server" Text='<%# Eval("dateshift")  %>'></asp:Label>--%>
                                                    <asp:TextBox ID="txtdateshift" runat="server" Width="50%" Text='<%# Eval("dateshift")  %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>





                                        </Columns>


                                        <EmptyDataTemplate>
                                            No Record Available
                                        </EmptyDataTemplate>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
