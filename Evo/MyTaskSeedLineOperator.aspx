<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskSeedLineOperator.aspx.cs" Inherits="Evo.MyTaskSeedLineOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon mb-4">Seeding Lot Fulfillment Task Completion</h2>

        <div class="row mb-4">
            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Job No.</label>
                <select class="w-100 filter__control filter__select custom__dropdown mb-0">
                    <option>Job No.</option>
                    <option>Option 2</option>
                    <option>Option 3</option>
                    <option>Option 4</option>
                </select>
            </div>

            <div class="col-12 col-md-4 col-lg-3 mb-3">
                <label>Customer Name</label>
                <select class="w-100 filter__control filter__select custom__dropdown mb-0">
                    <option>Customer Name</option>
                    <option>Option 2</option>
                    <option>Option 3</option>
                    <option>Option 4</option>
                </select>
            </div>

            <div class="col-12 col-md-4 col-lg-3 mb-3s">
                <label>Facility Defaults</label>
                <select class="w-100 filter__control filter__select custom__dropdown mb-0">
                    <option>Facility Defaults</option>
                    <option>Option 2</option>
                    <option>Option 3</option>
                    <option>Option 4</option>
                </select>
            </div>
        </div>

        <div class="data__table mb-4">
            <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                class="striped" AllowSorting="true" PageSize="10"
                GridLines="None" OnRowCommand="gvGerm_RowCommand"
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("JobID")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Item")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("#TraysSeeded")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Seed Lot" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("SeedLots")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeedingDueDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
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
</asp:Content>
