<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="CustomerDashBoard.aspx.cs" Inherits="Evo.CustomerDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header__bottom">
        <div class="d-flex align-items-center justify-content-center">
            <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown input__control-auto robotomd" AutoPostBack="true" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <div class="main__header">
        <div class="site__container">
             <h2> <asp:Label ID="lblCustName" runat="server" /></h2>
            <div class="row">
                <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                    <label>Job No </label>
                    <asp:DropDownList ID="ddlJobNo" runat="server" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>

                <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                    <label>From Plant Due Date</label>
                    <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                </div>
                <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                    <label>To Plant Due Date </label>
                    <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-6 mb-3 align-self-end">
                    <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSearch_Click" />
                    <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="mr-2 bttn bttn-primary bttn-action mb-3 mb-md-0" OnClick="btnSearchRest_Click" />

                </div>
            </div>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="gvGerm_RowCommand"
                                    class="striped" AllowSorting="true" OnPageIndexChanging="gvGerm_PageIndexChanging" PageSize="15"
                                    GridLines="None" DataKeyNames="BenchLocation,JobNo,TaskRequestType" OnRowDataBound="gvGerm_RowDataBound"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Bench Location" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblBenchLocation" runat="server" Text='<%# Eval("BenchLocation")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("JobNo","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("JobNo") %>' Font-Underline="true" />
                                                <asp:Label ID="lblJobNo" Visible="false" runat="server" Text='<%# Eval("JobNo")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("Customer")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlantType" runat="server" Text='<%# Eval("PlantType")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalTray" runat="server" Text='<%# Eval("TotalTray")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--<asp:TemplateField HeaderText="TaskRequestType" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblplan_date" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Planned Due Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldue_date" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Task Request Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaskRequestType" runat="server" Text='<%# Eval("TaskRequestType")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Work Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblWorkDate" runat="server" Text='<%# Eval("WorkDate","{0:MM/dd/yyyy}")  %>'></asp:Label>

                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Completion Status" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblTaskStatus" runat="server" Text='<%# Eval("TaskStatus")  %>'></asp:Label>


                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lbldue_date" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}")  %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Button ID="btnStart" runat="server" Text="Task Report" CommandName="GStart" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>


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
</asp:Content>
