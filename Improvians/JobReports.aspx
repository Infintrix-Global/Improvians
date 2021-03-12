<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="JobReports.aspx.cs" Inherits="Evo.JobReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main__header">
        <div class="site__container">
            <div class="row">
                <div class="col m12">
                    <div class="mb-2">
                        <h2>Production Traceability Report</h2>
                    </div>
                    <br />
                    <div class="row" id="divFilter" runat="server" visible="false">
                        <div class="col-lg-3">
                            <label>Bench Location </label>
                            <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <label>Job No </label>
                            <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <label>Job No</label>
                            <asp:TextBox ID="txtSearchJobNo" runat="server" Text="JB" class="input__control robotomd"></asp:TextBox>

                            <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                                MinimumPrefixLength="2"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="txtSearchJobNo"
                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                            </cc1:AutoCompleteExtender>
                        </div>
                        <div class="col-auto"> 
                            <br />
                            <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearch_Click" />
                            <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />
                        </div>
                    </div>
                    <br />
                    <div class="portlet light ">

                        <div class="portlet-body">

                            <asp:Panel ID="Panel1" runat="server">
                                <div class="data__table mb-2">
                                    <asp:GridView ID="gv1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Prod No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("wo")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <div class="data__table">
                                    <asp:GridView ID="GV2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Cust. No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                   
                                                    <asp:Label ID="lblcusno" runat="server" Text='<%# Eval("cusno")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Cust. Name" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblcname" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item. No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lbliteno" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Crop" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblitemdescp" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lbltraysize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of trays" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("trays_actual")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("actual_date")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Due Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>                                                  
                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("due_date")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <hr class="my-4">

            <div class="row">
                <div class=" col m12">
                    <div class="mb-2"><strong>Lot Details</strong></div>
                    <div class="portlet light ">

                        <div class="portlet-body">

                            <asp:Panel ID="Panel2" runat="server">
                                <div class="data__table">
                                    <asp:GridView ID="Gv3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Posting Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblSD" runat="server" Text='<%# Eval("SeededDate")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Planning Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblCD" runat="server" Text='<%# Eval("CreatedOn")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lot ID" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblLOTID" runat="server" Text='<%# Eval("LotID")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Number of seeds" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblNS" runat="server" Text='<%# Eval("NumberOfSeed")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <hr class="my-4">

            <div class="row">
                <div class=" col m12">
                    <div class="mb-2"><strong>Routing Steps</strong></div>
                    <div class="portlet light ">

                        <div class="portlet-body">

                            <asp:Panel ID="Panel3" runat="server">
                                <div class="data__table">
                                    <asp:GridView ID="GV4" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblD" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Starting Date" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblSD" runat="server" Text='<%# Eval("StartingDate")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="Operation No" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate> 
                                                    <asp:Label ID="lblON" runat="server" Text='<%# Eval("OperationNo")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         
                                            <asp:TemplateField HeaderText="RunTime" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRT" runat="server" Text='<%# Eval("RunTime")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                            <asp:TemplateField HeaderText="Completion Date " ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblED" runat="server" Text='<%# Eval("EndingDate")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Germ" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>                                                    
                                                    <asp:Label ID="lblG" runat="server" Text='<%# Eval("Germ")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <hr class="my-4">

            <div class="row">
                <div class=" col m12">
                    <div class="mb-2"><strong>Facility/House Detail</strong></div>
                    <div class="portlet light ">

                        <div class="portlet-body">

                            <asp:Panel ID="Panel4" runat="server">
                                <div class="data__table">
                                    <asp:GridView ID="GV5" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true"
                                        GridLines="None" PageSize="10"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="House/Section" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblGHD" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trays" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblFID" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
