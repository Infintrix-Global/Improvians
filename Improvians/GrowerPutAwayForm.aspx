﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GrowerPutAwayForm.aspx.cs" Inherits="Evo.GetGrowerPutAwayForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon mb-3">
            <img src="./images/dashboard_put-away.png" width="137" height="140" alt="Put-Away">
            Put Away Location Assignment
        </h2>

        <div class="portlet light pt-1">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
            <div class="portlet-body">
                <asp:Panel ID="PanelList" runat="server">
                    <div class="data__table">
                        <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            class="striped" AllowSorting="true" OnRowCommand="gvGerm_RowCommand"
                            GridLines="None" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging" OnRowDataBound="gvGerm_RowDataBound"
                            ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                        <asp:Label ID="lbljobcode" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Plant Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--   <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Total Trays">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTraysSeeded" runat="server" Text='<%# Eval("trays_plan")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tray Size">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblActualTraySeeded" runat="server" Text='<%# Eval("ActualTraySeeded")  %>'></asp:Label>--%>
                                        <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Seeded Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Planned Due Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeededPlanDate" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Put Away Facility">
                                    <ItemTemplate>
                                        <asp:Label ID="lblloc_seedline" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Genus Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Job Source" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsource" data-head="Job Source" runat="server" Text='<%# Eval("RequestType")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedBy" data-head="Assigened By" runat="server" Text='<%# Eval("AssignedBy")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAssign" CommandName="Assign" CssClass="bttn bttn-primary bttn-action" Text="Start " runat="server" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
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
                </asp:Panel>
                <asp:Panel ID="PanelAdd" Visible="false" runat="server">
                    <div class="dashboard__block dashboard__block--asign mt-1">
                        <div class="row">
                            <div class="col-auto col-sm-6 col-md-3 mb-3">
                                <label class="robotobold">Job No.</label><br />
                                <asp:Label ID="lblJobID" runat="server"></asp:Label>
                            </div>
                            <div class="col-auto col-sm-6 col-md-3 mb-3">
                                <label class="d-block robotobold">Seed Date</label>
                                <asp:Label ID="lblGenusCode" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblSeedDate" runat="server"></asp:Label>
                            </div>
                            <div class="col-auto col-sm-6 col-md-3 mb-3">
                                <label class="d-block robotobold">Seeded Trays</label>
                                <asp:Label ID="lblSeededTrays" runat="server"></asp:Label>

                            </div>
                            <div class="col-auto col-sm-6 col-md-3 mb-3">
                                <label class="d-block robotobold">Remaining Seeded Trays</label>
                                <asp:Label ID="lblRemaining" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="data__table">
                            <asp:GridView ID="GridSplitJob" runat="server" ShowFooter="true" OnRowDeleting="GridSplitJob_RowDeleting"
                                AutoGenerateColumns="false" OnRowDataBound="GridSplitJob_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="RowNumber" HeaderText="NO." />
                                    <asp:TemplateField HeaderText="Put Away Facility" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnWOEmployeeID" runat="server" Value='<%# Eval("ID")%>'></asp:HiddenField>
                                            <asp:DropDownList ID="ddlMain" OnSelectedIndexChanged="ddlMain_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown w-auto robotomd" runat="server"></asp:DropDownList>

                                            <asp:Label ID="lblMain" Visible="false" runat="server" Text='<%# Eval("FacilityID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bench Location">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlLocation" class="custom__dropdown w-auto robotomd" runat="server"></asp:DropDownList>
                                            <asp:Label ID="lblLocation" Visible="false" runat="server" Text='<%# Eval("GreenHouseID")%>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trays">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTrays" OnTextChanged="txtTrays_TextChanged" AutoPostBack="true" Text='<%# Eval("Trays")%>' CssClass="input__control" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Button Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="Delete" ID="btnRemove" runat="server" CssClass="bttn bttn-primary bttn-action d-block ml-auto" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="ButtonAdd" OnClick="ButtonAddGridInvoice_Click" runat="server" CausesValidation="false" Text="Add Put Away Location" CssClass="bttn bttn-primary bttn-action w-auto" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row mt-4 align-items-end">
                            <div class="col-12 col-md-auto col-lg-4 col-xl-3 mb-3">
                                <label runat="server" id="lblfacsupervisor">Assignment</label>
                                <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            </div>
                            <div class="col-12 col-md-auto col-lg-4 mb-3">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" OnClick="btnSubmit_Click" runat="server" />
                                <asp:Button Text="Reset" ID="btnReset" CssClass="ml-2 bttn bttn-primary bttn-action" OnClick="btnReset_Click" runat="server" />
                            </div>
                            <div class="col-12">
                                <asp:Label ID="Label1" Visible="false" runat="server" ForeColor="#488949" Text="Assigned to site move team once submitted.​"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
