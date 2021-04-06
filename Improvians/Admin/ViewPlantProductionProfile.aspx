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

                <div class="portlet-body">
                    <div class="data__table data__table-height">
                        <asp:GridView ID="GridProfile" runat="server" ShowFooter="true" Width="100%" OnRowDeleting="GridProfile_RowDeleting"
                            AutoGenerateColumns="false" OnRowDataBound="GridProfile_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="RowNumber" HeaderText="No." />
                                <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCode" class="custom__dropdown robotomd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGridCode_SelectedIndexChanged"></asp:DropDownList>
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
                                <asp:TemplateField HeaderText="">
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
                            <label>Code </label>

                            <asp:DropDownList ID="ddlCode" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col m3">
                            <label>Crop </label>

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

<%--                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnSearch" Text="Search" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSearch_Click" />
                        </div>--%>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnSave" Text="Save" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSave_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnClear" Text="Reset" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnClear_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnAddProfile" Text="Add" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnAddProfile_Click" />
                        </div>
                         <div class="col-auto align-self-end">
                            <asp:Button ID="btnAddDateNo" Text="Add No" Visible="false" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnAddDateNo_Click" />
                        </div>
                         <div class="col-auto align-self-end">
                            <asp:Button ID="btnUpdateDateno" Text="Update No"  class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnUpdateDateno_Click" />
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
                                <div class="data__table data__table-height">

                                    <asp:GridView ID="gvProductionProfile" runat="server" AutoGenerateColumns="False"
                                        class="striped" OnRowCommand="gvProductionProfile_RowCommand"  
                                        GridLines="None"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Code" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcode" runat="server" Text='<%# Eval("code")  %>'></asp:Label>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("pid")  %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Crop" ItemStyle-Width="30%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcrop" runat="server" Text='<%# Eval("crop")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tray Code" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("traycode")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Activity Code" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("activitycode")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date Shift" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="Label10" runat="server" Text='<%# Eval("dateshift")  %>'></asp:Label>--%>
                                                    <asp:TextBox ID="txtdateshift" runat="server" Width="80%" Text='<%# Eval("dateshift")  %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Button Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="DeleteProfile" CommandArgument='<%# Eval("pid")  %>' ID="btnRemove" runat="server" CssClass="bttn bttn-primary bttn-action" />
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
