<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="PlantReadyAssignmentForm.aspx.cs" Inherits="Evo.PlantReadyAssignmentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready">
            Plant Ready
        </h2>

        <div class="filter__row d-flex">
            <div class="row">
                <div class="col m3">
                    <label>Customer </label>
                    <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
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
        </div>

        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="PlantReadyId,CropHealth"
                                class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                GridLines="None" OnRowCommand="gvGerm_RowCommand" OnRowDataBound="gvGerm_RowDataBound1"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>

                                            <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                            <asp:Label ID="lblCropHealth" Visible="false" runat="server" Text='<%# Eval("CropHealth")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--   <asp:TemplateField HeaderText="Main Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGreenHouseID" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plant Ready Work Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlanDate" runat="server" Text='<%# Eval("PlanDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                            <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Assign" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
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
