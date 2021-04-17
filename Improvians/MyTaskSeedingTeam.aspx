<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskSeedingTeam.aspx.cs" Inherits="Evo.MyTaskSeedingTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon mb-4">My Task</h2>

        <div class="row mb-4">
            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Facility </label>
                <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Job No </label>
                <asp:DropDownList ID="ddlJobNo" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
        </div>

        <div class="portlet light mb-4">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">
                <div class="data__table">
                    <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        class="striped" AllowSorting="true" OnRowCommand="gvGerm_RowCommand"
                        GridLines="None" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("JobID")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Item")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Seeding Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("SeedingDueDate")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="No. Of Trays to be seeded">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("#TraysSeeded")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tray Size">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("SeedLots")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Seed Lots">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("LotList")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Plant Type">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="bttn bttn-primary bttn-action" CommandName="Select" CommandArgument='<%# Eval("JobID")  %>'></asp:Button>
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
</asp:Content>
