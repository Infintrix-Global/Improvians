<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ViewPlantProductionProfile.aspx.cs" Inherits="Evo.Admin.ViewPlantProductionProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">View Plant Production Profile</h1>

            <hr />
            <!-- BEGIN FORM-->
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <div class="filter__row d-flex">
                <div class="row">
                    <div class="col m3">
                        <label>Filter Crop </label>
                        
                        <asp:DropDownList ID="ddlCrop" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col m3">
                        <label>Activity Code </label>
                        
                        <asp:DropDownList ID="ddlActivityCode" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col m3">
                        <label>Tray Code </label>
                        
                        <asp:DropDownList ID="ddlTrayCode" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                   <%-- <div class="col m3">
                        <label>Designation</label>
                        <asp:DropDownList ID="ddlDesignation" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>--%>

                    <div class="col-auto align-self-end">
                    <asp:Button ID="btnSearch" Text="Search" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSearch_Click"  />
                    </div>
                    <div class="col-auto align-self-end">
                    <asp:Button ID="btnSave" Text="Save" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSave_Click"  />
                    </div>
                    <div class="col-auto align-self-end">
                    <asp:Button ID="btnClear" Text="Clear" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnClear_Click"  />
                    </div>
                </div>
            </div>

            <div class="filter__row d-flex">
                <div class="row justify-content-lg-center">
                    <div class=" col m12">
                        <div class="portlet light ">
                            <asp:Label runat="server" Text="" ID="count"></asp:Label>
                            <div class="portlet-body">
                                <div class="data__table"> 
                                    
                                     <asp:GridView ID="gvProductionProfile" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true" OnRowCommand="gvProductionProfile_RowCommand"
                                        GridLines="None" 
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Code" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("code")  %>'></asp:Label>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("pid")  %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Tray Code" HeaderStyle-CssClass="autostyle2" SortExpression="EmployeeName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("traycode")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Crop" HeaderStyle-CssClass="autostyle2" SortExpression="EmployeeName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("crop")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Activity Code" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("activitycode")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date Shift" HeaderStyle-CssClass="autostyle2" SortExpression="RoleName">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="Label10" runat="server" Text='<%# Eval("dateshift")  %>'></asp:Label>--%>
                                                    <asp:TextBox ID="txtdateshift" runat="server" Text='<%# Eval("dateshift")  %>'></asp:TextBox>
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
            </div>
        </div>
    </div>
</asp:Content>
