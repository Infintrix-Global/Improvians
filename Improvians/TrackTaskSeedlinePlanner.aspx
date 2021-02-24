<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="TrackTaskSeedlinePlanner.aspx.cs" Inherits="Improvians.TrackTaskSeedlinePlanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="site__container">
            <h2>Track Task</h2>

            <div class="row">
                <div class="col-lg-3">
                    <label>Customer </label>
                    <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <div class="col-lg-3">
                    <label>Job No </label>
                    <asp:DropDownList ID="ddlJobNo" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>

                <div class="col-lg-3">
                    <label>Seedline Facility </label>
                    <asp:DropDownList ID="ddlFacility" AutoPostBack="true" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>

                <div class="col-lg-3">
                    <label>Seedline Status </label>
                    <span class="custom-control custom-radio ml-4 mr-2">
                        <asp:RadioButtonList ID="radJSeedlineStatus" runat="server" Width="250px" RepeatDirection="Horizontal" >
                            <asp:ListItem Value="0" Text="Pending"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Completed"></asp:ListItem>
                         
                        </asp:RadioButtonList>
                    </span>

                </div>
            </div>
             <br />
            <div class="row">
                <div class="col-lg-3">
                    <label>Put away Status </label>
                    <span class="custom-control custom-radio ml-4 mr-2">
                        <asp:RadioButtonList ID="RadioPutAwayStatus" runat="server" Width="250px" RepeatDirection="Horizontal" >
                            <asp:ListItem Value="0" Text="Pending" ></asp:ListItem>
                            <asp:ListItem Value="1" Text="Completed"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </span>

                </div>

                <div class="col m3">
                    <br />
                    <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearch_Click" />
                    <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />
                </div>

            </div>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                    GridLines="None" OnRowDataBound="gvGerm_RowDataBound1"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Work Order" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Seedline Facility" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblloc" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="No. Of Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("trays_actual")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeded Due Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSoDate" runat="server" Text='<%# Eval("SoDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblplan_date" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Planned Due Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldue_date" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeding Status" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>

                                                <asp:Label ID="lblStatusValues" Visible="false" runat="server" Text='<%# Eval("jstatus")  %>'></asp:Label>


                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Put away Status" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblPutawayStatusValues" Visible="false" runat="server" Text='<%# Eval("jstatus")  %>'></asp:Label>
                                                <asp:Label ID="lblPudawayDate" runat="server" Text='<%# Eval("CreateOn","{0:MM/dd/yyyy}")  %>'></asp:Label>


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
