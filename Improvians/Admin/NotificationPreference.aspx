<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NotificationPreference.aspx.cs" Inherits="Evo.Admin.NotificationPreference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Notification Preference</h1>

            <hr />

            <div class="admin__form">
                <div class="row">
                    <div class="col-lg-6 col-12">
                        <label>
                            <h3>Task Type</h3>

                            <%--    <select class="custom__dropdown input__control-auto robotomd">
                            <option disabled selected>Select Task Type</option>
                            <option value="Germination">Germination Count</option>
                            <option value="Fertilizer">Fertilization</option>
                            <option value="Chemical">Chemical</option>
                            <option value="Irrigation">Irrigation</option>
                            <option value="PlantReady">Plant Ready</option>
                            <option value="Move">Move Request</option>
                            <option value="Dump">Dump</option>
                            <option value="GeneralTask">General Task</option>
                        </select>--%>

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

                        <label class="mb-0">
                            <h3>Send Notification to:</h3>
                        </label>

                        <div class="data__table data__table-height">
                            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" OnRowDataBound="gvUsers_RowDataBound"
                                GridLines="None" OnRowCommand="gvUsers_RowCommand"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="User" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="50%">
                                        <%--  <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBoxall" AutoPostBack="true" OnCheckedChanged="chckchanged1" runat="server" />
                                            </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <%--  <div class="custom-control custom-checkbox mr-3">--%>
                                            <%--                                                <asp:CheckBox runat="server" ID="chkSelect" ></asp:CheckBox>--%>
                                            <asp:CheckBox runat="server" ID="chkSelect"></asp:CheckBox>
                                            <asp:Label ID="userRoleNames" runat="server" Text='<%# Eval("UserName")   %>'></asp:Label>
                                            <%--                                            </div>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Send Notification Via" HeaderStyle-CssClass="autostyle2">

                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkApp"></asp:CheckBox>
                                            <asp:Label ID="appName" runat="server" Text="App"></asp:Label>

                                            <asp:CheckBox runat="server" ID="chkEmail"></asp:CheckBox>
                                            <asp:Label ID="emailName" runat="server" Text="Email"></asp:Label>
                                            <%--  <div class="custom-control custom-checkbox mr-3">
                                                <asp:CheckBox runat="server" ID="chkApp"  CssClass="custom-control-input"></asp:CheckBox>
                                                <asp:Label ID="appName" runat="server" CssClass="custom-control-label" Text="App"></asp:Label>
                                            </div>--%>
                                            <%--  <div class="custom-control custom-checkbox mr-3">
                                                <asp:CheckBox runat="server" ID="chkEmail" CssClass="custom-control-input"></asp:CheckBox>
                                                <asp:Label ID="emailName" runat="server" CssClass="custom-control-label" Text="Email"></asp:Label>
                                            </div>--%>
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

                        <asp:Button runat="server" ID="submitPreference" Text="Update Preferences" disabled="disabled" CssClass="submit-bttn bttn bttn-primary w-auto" OnClick="submitPreference_Click" />
                        <asp:Label runat="server" ID="submitErrorMsg" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </div>

                    <div class="col-lg-6 col-12 mt-0 mt-lg-0">
                        <h3>Assigned Notifications</h3>

                        <div class="data__table data__table-height">
                            <asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" OnRowDataBound="gvUserDetails_RowDataBound"
                                GridLines="None" OnRowCommand="gvUserDetails_RowCommand"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="20%">

                                        <ItemTemplate>
                                            <asp:Label ID="lblTask" runat="server" Text='<%# Eval("TaskType")   %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Send To" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="20%">

                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName")   %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Send Via" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="20%">

                                        <ItemTemplate>
                                            <asp:Label ID="lblSendVia" runat="server" Text='<%# Eval("SendType")   %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="20%" ItemStyle-CssClass="text-center">

                                        <ItemTemplate>
                                           <asp:LinkButton ID="btnEdit" runat="server" CssClass="bttn bttn__icons bttn__icons--edit mr-2" CommandName="Edit" CommandArgument='<%# Eval("ID")  %>'><i class='fa fa-edit'></i> </asp:LinkButton>
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

                        <%--<div class="data__table">
                            <table>
                                <tr>
                                    <th>Task Type</th>
                                    <th>Send To</th>
                                    <th>Send Via</th>
                                    <th>Actions</th>
                                </tr>
                                <tr>
                                    <td>Fertilization</td>
                                    <td>
                                        <div class="mb-2">Assistant Grower</div>
                                    </td>
                                    <td>App</td>
                                    <td class="text-center">
                                        <button type="button" class="bttn bttn__icons bttn__icons--edit mr-2">
                                            <i class="fa fa-edit"></i>
                                        </button>
                                        <button type="button" class="bttn bttn__icons bttn__icons--delete">
                                            <i class="fa fa-trash"></i>
                                        </button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Irrigation</td>
                                    <td>
                                        <div class="mb-2">Sprayer</div>
                                    </td>
                                    <td>App, Email</td>
                                    <td class="text-center">
                                        <button type="button" class="bttn bttn__icons bttn__icons--edit mr-2">
                                            <i class="fa fa-edit"></i>
                                        </button>
                                        <button type="button" class="bttn bttn__icons bttn__icons--delete">
                                            <i class="fa fa-trash"></i>
                                        </button>
                                    </td>
                                </tr>
                            </table>
                        </div>--%>
                        <!--
                                    <div class="assignment__filter my-2">
                                        Actions: 
                                        <button>
                                            <i class="fa fa-edit"></i> Edit All
                                        </button>
                                        <button>
                                            <i class="fa fa-trash"></i> Delete All
                                        </button>
                                    </div>
                                    -->
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
