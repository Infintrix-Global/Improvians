﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedingPlanForm.aspx.cs" Inherits="Improvians.SeedingPlanForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="site__container">
            <br />
            <h2>
                <asp:LinkButton ID="LinkMyTask" ForeColor="#505050" PostBackUrl="~/DashBoard.aspx" runat="server"> My Tasks </asp:LinkButton></h2>
            <h2>Seedline Planning</h2>
            <asp:Button ID="Reset" runat="server" Text="Reset All Data" OnClick="Reset_Click" CssClass="bttn bttn-primary bttn-action" />

            
                <div class="row">
                    <div class="col-lg-3">
                        <label>From Date </label>

                        <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="form-control" placeholder="From Date"
                            ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="col-lg-3">
                        <label>To Date </label>
                        <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" class="form-control" placeholder="To Date"
                            ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <label>Seedline Location</label>
                        <asp:DropDownList ID="ddlSeedlineLocation" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <label>Seeds Allocated </label>
                        <asp:DropDownList ID="ddlSeedAllocated" runat="server" class="custom__dropdown robotomd">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3">
                        <label>Item</label>
                        <asp:DropDownList ID="ddlItem" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <label>Tray Size</label>
                        <asp:DropDownList ID="ddlTraySize" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>

                    <div class="col-lg-6">
                        <br />


                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" CssClass="bttn bttn-primary bttn-action"></asp:Button>
                        <asp:Button ID="btnSearchReset" OnClick="btnSearchReset_Click" runat="server" Text="Reset" CssClass="bttn bttn-primary bttn-action"></asp:Button>
                    </div>

                    <div class="col-lg-3">
                       
                    </div>
                </div>


            
            <br />
            <div class="row">
                <div class="col m6">
                    <asp:Label ID="lblTotal" ForeColor="#488949" runat="server" Text=""></asp:Label>

                </div>

                <div class="col m3">
                </div>
                <div class="col m3">
                </div>

            </div>
            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table data__table-height">


                                <asp:GridView ID="DGJob" runat="server" AutoGenerateColumns="False"
                                    class="striped"
                                    GridLines="None" OnRowDataBound="DGJob_RowDataBound"
                                    ShowHeaderWhenEmpty="True" Width="100%">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seedline Facility">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeedline" runat="server" Text='<%# Eval("loc") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("jobcode") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itmdescp") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("cname") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sales Order Seed Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSODate" runat="server" HtmlEncode="false" Text='<%# Eval("sodate","{0:MM/dd/yyyy}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sales Order Trays">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSOTrays" runat="server" Text='<%# Eval("sotrays","{0:####}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tray Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("ts") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Putaway Facility">
                                            <ItemTemplate>
                                                <%-- <asp:TextBox ID="txtSeedline" runat="server" Text='<%# Eval("loc") %>' Width="50"></asp:TextBox>--%>
                                                <asp:Label ID="lbl_Seedline" Visible="false" Text='<%# Eval("loc") %>' runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlBenchLocation" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work order Trays">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Txtgtrays" Width="50" Text='<%# Eval("wotrays","{0:####}") %>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Scheduled Seed Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Txtgplantdt" TextMode="Date" Text='<%# Eval("wodate","{0:yyyy-MM-dd}") %>' Width="150px" runat="server"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seeds Allocated">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAllocated" runat="server" Text='<%# Eval("alloc") %>'></asp:Label>
                                                <asp:HiddenField ID="HiddenFieldsotrays" Value='<%# Eval("sotrays") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFielditm" Value='<%# Eval("itm") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFieldcusno" Value='<%# Eval("cusno") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFieldsodate" Value='<%# Eval("sodate","{0:yyyy-MM-dd}") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFieldduedate" Value='<%# Eval("duedate","{0:yyyy-MM-dd}") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFieldwo" Value='<%# Eval("wo") %>' runat="server" />


                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>

                    </div>
                </div>

            </div>

            <div class="text-left dashboard__block my-4">

                <div class="row justify-content-center">
                    <div class="col-12">

                        <div class="row">
                            <div class="col-12 my-3">

                                <asp:Button Text="Post & Send" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />
                                <asp:Button Text="Print" ID="BtnPrint" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="BtnPrint_Click" />
                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-12 my-3">
                                <asp:Label ID="Label1" runat="server" ForeColor="#488949" Text="Seedline supervisors at each facility sent jobs with WO trays and plan dates filled out once submitted.​"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
