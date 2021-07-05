<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskGreenOperator.aspx.cs" Inherits="Evo.MyTaskGreenhouseOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2>My Task</h2>

        <div class="mt-4 row">
            <div class="col-md-4 col-lg-3 mb-3">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>

            <div class="col-md-4 col-lg-3 mb-3">
                <label>Facility </label>
                <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-md-4 col-lg-3 mb-3">
                <label>Job No </label>
                <asp:DropDownList ID="ddlJobNo" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
        </div>

        <div class="portlet light pt-3 mb-4">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">
                <div class="data__table">
                    <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="10"
                        class="striped" AllowSorting="true" OnPageIndexChanging="gvGerm_PageIndexChanging1"
                        GridLines="None" OnRowCommand="gvGerm_RowCommand"
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="lblStage" runat="server" Text='<%# Eval("Stage")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("JobID")  %>'></asp:Label>
                                    <asp:HiddenField ID="HiddenFieldTaskID" Value='<%# Eval("TaskID")%>' runat="server" />

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
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
                <!-- data__table ends -->
            </div>
            <!-- portlet-body ends -->
        </div>
        <!-- portlet light ends -->
    </div>
    <!-- site__container ends -->
</asp:Content>
