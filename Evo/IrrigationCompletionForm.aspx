<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="IrrigationCompletionForm.aspx.cs" Inherits="Evo.IrrigationCompletionForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation">
            Irrigation Completion Form
        </h2>

        <%--  <div class="filter__row d-flex">
                <div class="row">
                    <div class="col m3">
                        <label>Customer </label>
                        <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>

                    <div class="col m3">
                        <label>Facility </label>
                        <asp:DropDownList ID="ddlFacility" AutoPostBack="true" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col m3">
                        <label>Job No </label>
                        <asp:DropDownList ID="ddlJobNo" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col m3">
                        <br />
                        <asp:Button Text="Reset" ID="btnResetSearch" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                    </div>
                </div>
            </div>--%>

        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IrrigationCode,TaskRequestKey"
                                class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                GridLines="None" OnRowCommand="gvGerm_RowCommand" OnRowDataBound="gvGerm_RowDataBound"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bench Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGreenHouseID" runat="server" Text='<%#Bind("BenchLocation") %>'></asp:Label>
                                            <asp:Label ID="lblIrrigationCode" Visible="false" runat="server" Text='<%#Bind("IrrigationCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Spray Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSprayDate" runat="server" Text='<%# Eval("SprayDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Number of Passes" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("WaterRequired")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                            <asp:Button ID="btnView" runat="server" Width="140px" Text="View Job Details" CssClass="bttn bttn-primary bttn-action my-1" CommandName="ViewDetails" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
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
</asp:Content>
