<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NotificationPreference.aspx.cs" Inherits="Evo.Admin.NotificationPreference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Notification Preference</h1>

            <hr />
            <asp:Label ID="lblmsg" runat="server"></asp:Label>

            <asp:Panel ID="pnlAdd" runat="server" Visible="false">

                <div class="portlet-body">
                    <div class="data__table data__table-height">
                        <asp:GridView ID="gvAddUsers" runat="server" ShowFooter="true" Width="70%" OnRowDeleting="gvAddUsers_RowDeleting"
                            AutoGenerateColumns="false" OnRowDataBound="gvAddUsers_RowDataBound">
                            <Columns>
<%--                                <asp:BoundField DataField="RowNumber" HeaderText="No." />--%>
                                <asp:TemplateField HeaderText="Task Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTasks" runat="server" AutoPostBack="true" CssClass="custom__dropdown input__control-auto robotomd">
                                            <asp:ListItem Text="Select Task Type" Value="0" Selected="True" disabled="disabled" />
                                            <asp:ListItem Value="Germination" Text="Germination Count" />
                                            <asp:ListItem Value="Fertilizer" Text="Fertilization" />
                                            <asp:ListItem Value="Chemical" Text="Chemical" />
                                            <asp:ListItem Value="Irrigation" Text="Irrigation" />
                                            <asp:ListItem Value="PlantReady" Text="Plant Ready" />
                                            <asp:ListItem Value="Move" Text="Move Request" />
                                            <asp:ListItem Value="Dump" Text="Dump" />
                                            <asp:ListItem Value="GeneralTask" Text="General Task" />
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTasks" ValidationGroup="e"
                                            InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Task" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnTask" runat="server" Value='<%# Eval("TaskType")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Users">
                                    <ItemTemplate>                                        
                                        <asp:DropDownList ID="ddlUsers" runat="server" CssClass="custom__dropdown robotomd"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUsers" ValidationGroup="e"
                                            InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select User" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnUserNames" runat="server" Value='<%# Eval("UserName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Send Via" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApp" Text="App" CssClass="custom-control custom-checkbox d-inline-block mr-2" runat="server"></asp:CheckBox>
                                        <asp:CheckBox ID="chkEmail" Text="Email" CssClass="custom-control custom-checkbox d-inline-block" runat="server"></asp:CheckBox>
                                        <asp:HiddenField ID="hdnViaApps" runat="server" Value='<%# Eval("viaApp")%>' />
                                        <asp:HiddenField ID="hdnViaEmails" runat="server" Value='<%# Eval("viaEmail")%>' />


                                       <%-- <asp:Label Visible="false" ID="apps" runat="server" Text='<%# Eval("IsApp")%>'></asp:Label>
                                        <asp:Label Visible="false" ID="emails" runat="server" Text='<%# Eval("IsEmail")%>'></asp:Label>--%>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="Delete" ID="btnRemove" runat="server" CssClass="bttn bttn-primary bttn-action" />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>

                                        <asp:Button ID="ButtonAdd" OnClick="ButtonAdd_Click" runat="server" CausesValidation="true" ValidationGroup="e"
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
                            <label>Task Type </label>
                            <asp:DropDownList runat="server" ID="ddlTTypes" AutoPostBack="true" OnSelectedIndexChanged="ddlTasks_SelectedIndexChanged" CssClass="custom__dropdown input__control-auto robotomd">
                                <asp:ListItem Text="Select Task Type" Value="0" Selected="True" disabled="disabled" />
                                <asp:ListItem Value="Germination" Text="Germination Count" />
                                <asp:ListItem Value="Fertilizer" Text="Fertilization" />
                                <asp:ListItem Value="Chemical" Text="Chemical" />
                                <asp:ListItem Value="Irrigation" Text="Irrigation" />
                                <asp:ListItem Value="PlantReady" Text="Plant Ready" />
                                <asp:ListItem Value="Move" Text="Move Request" />
                                <asp:ListItem Value="Dump" Text="Dump" />
                                <asp:ListItem Value="GeneralTask" Text="General Task" />
                            </asp:DropDownList>
                        </div>

                        <div class="col-auto align-self-end">
                            <%--<asp:Button ID="Button1" Text="Reset" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnClear_Click" />--%>
                            <asp:Button runat="server" ID="submitPreference" Text="Update Preferences" disabled="disabled" CssClass="submit-bttn bttn bttn-primary w-auto" OnClick="submitPreference_Click" />

                        </div>

                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnClear" Text="Reset" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnClear_Click" />
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
                                <div class="data__table data__table-height" style="max-height:600px;overflow-y:scroll;">

                                    <asp:GridView ID="gvUsersProfile" runat="server" AutoGenerateColumns="False"
                                        class="striped" OnRowCommand="gvUsersProfile_RowCommand" OnRowDataBound="gvUsersProfile_RowDataBound"
                                        GridLines="None" OnRowDeleting="gvUsersProfile_RowDeleting"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Task Type" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTask" runat="server" Text='<%# Eval("TaskType")   %>'></asp:Label>
                                                    <asp:Label ID="NPId" runat="server" Text='<%# Eval("ID")   %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Users" ItemStyle-Width="30%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName")   %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Send Via" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>                                                    
                                                    <asp:HiddenField ID="hdnApps" runat="server" Value='<%# Eval("IsApp")%>'></asp:HiddenField>
                                                    <asp:HiddenField ID="hdnEmails" runat="server" Value='<%# Eval("IsEmail")%>'></asp:HiddenField>                                                    
                                                    <asp:CheckBox ID="viaApp" Text="App" CssClass="custom-control custom-checkbox d-inline-block mr-2" AutoPostBack="true" runat="server" OnCheckedChanged="viaApp_CheckedChanged" ></asp:CheckBox>
                                                    <asp:CheckBox ID="viaEmail" Text="Email" CssClass="custom-control custom-checkbox d-inline-block" AutoPostBack="true" runat="server" OnCheckedChanged="viaEmail_CheckedChanged" ></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Button Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="Delete" CommandArgument='<%# Eval("id")  %>' ID="btnDelete" runat="server" CssClass="bttn bttn-primary bttn-action" />
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

            <%--  <div class="admin__form">
                <div class="row">
                    <div class="col-lg-6 col-12">
                        <label>
                            <h3>Task Type</h3>

                            <asp:DropDownList runat="server" ID="ddlTasks" AutoPostBack="true" OnSelectedIndexChanged="ddlTasks_SelectedIndexChanged" CssClass="custom__dropdown input__control-auto robotomd">
                                <asp:ListItem Text="Select Task Type" Value="0" Selected="True" disabled="disabled" />
                                <asp:ListItem Value="Germination" Text="Germination Count" />
                                <asp:ListItem Value="Fertilizer" Text="Fertilization" />
                                <asp:ListItem Value="Chemical" Text="Chemical" />
                                <asp:ListItem Value="Irrigation" Text="Irrigation" />
                                <asp:ListItem Value="PlantReady" Text="Plant Ready" />
                                <asp:ListItem Value="Move" Text="Move Request" />
                                <asp:ListItem Value="Dump" Text="Dump" />
                                <asp:ListItem Value="GeneralTask" Text="General Task" />
                            </asp:DropDownList>
                        </label>

                        <div class="portlet-body">

                            <h3>Assigned Notifications</h3>
                            <asp:Button runat="server" ID="reset" Text="Reset" CssClass="submit-bttn bttn bttn-primary w-auto" OnClick="reset_Click" />

                            <div class="data__table data__table-height">
                                <asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                    class="striped" AllowSorting="true" OnRowDataBound="gvUserDetails_RowDataBound"
                                    GridLines="None" OnRowCommand="gvUserDetails_RowCommand" OnRowDeleting="gvUserDetails_RowDeleting" OnRowEditing="gvUserDetails_RowEditing"
                                    ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="20%">

                                            <ItemTemplate>

                                                <asp:Label ID="lblTask" runat="server" Text='<%# Eval("TaskType")   %>'></asp:Label>
                                                <asp:Label ID="NPId" runat="server" Text='<%# Eval("ID")   %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Send To" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="20%">

                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Via" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="20%" ItemStyle-CssClass="text-center">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="bttn bttn__icons bttn__icons--edit mr-2" CommandName="Edit" CommandArgument='<%# Eval("UserName")  %>'><i class='fa fa-edit'></i> </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="bttn bttn__icons bttn__icons--delete" CommandName="Delete" CommandArgument='<%# Eval("ID")  %>'><i class='fa fa-trash'></i></asp:LinkButton>

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

                        <asp:Button runat="server" ID="submitPreference" Text="Update Preferences" disabled="disabled" CssClass="submit-bttn bttn bttn-primary w-auto" OnClick="submitPreference_Click" />
                        <asp:Label runat="server" ID="submitErrorMsg" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>--%>
        </div>
    </div>


</asp:Content>
<%--                        <label class="mb-0">
                            <h3>Send Notification to:</h3>
                        </label>

                        <div class="data__table data__table-height">
                            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" OnRowDataBound="gvUsers_RowDataBound"
                                GridLines="None" OnRowCommand="gvUsers_RowCommand"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="User" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="50%">
                                        <asp:CheckBox ID="chkSelect" Text='<%#Bind("UserName")%>' CssClass="custom-control custom-checkbox" runat="server"></asp:CheckBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Send Notification Via" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkApp" Text="App" CssClass="custom-control custom-checkbox d-inline-block mr-2" runat="server"></asp:CheckBox>
                                            <asp:CheckBox ID="chkEmail" Text="Email" CssClass="custom-control custom-checkbox d-inline-block" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>--%>

<%-- <div class="data__table">
                            <table>
                                <tr>
                                    <th>User</th>
                                    <th colspan="2" class="text-center">Send Notification Via</th>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="customCheck1">
                                            <label class="custom-control-label" for="customCheck1">Assistant Grower</label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="app1">
                                            <label class="custom-control-label" for="app1">App</label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="email1">
                                            <label class="custom-control-label" for="email1">Email</label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="customCheck2">
                                            <label class="custom-control-label" for="customCheck2">Miguel Ramos - Sup</label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="app2">
                                            <label class="custom-control-label" for="app2">App</label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="email2">
                                            <label class="custom-control-label" for="email2">Email</label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="customCheck3">
                                            <label class="custom-control-label" for="customCheck3">Sylvia Reyes - Sup</label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="app3">
                                            <label class="custom-control-label" for="app3">App</label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="email3">
                                            <label class="custom-control-label" for="email3">Email</label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="customCheck4">
                                            <label class="custom-control-label" for="customCheck4">Supervisor</label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="app4">
                                            <label class="custom-control-label" for="app4">App</label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="email4">
                                            <label class="custom-control-label" for="email4">Email</label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>--%>