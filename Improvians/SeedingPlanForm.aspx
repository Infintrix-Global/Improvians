<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedingPlanForm.aspx.cs" Inherits="Improvians.SeedingPlanForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="site__container">
            <h2>My Task</h2>

            <div class="filter__row d-flex">
                <div class="row">
                    <div class="col m3">
                        <label>Customer </label>

                        <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="form-control" placeholder="From Date"
                            ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="col m3">
                        <label>Facility </label>
                        <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" class="form-control" placeholder="To Date"
                            ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col m3">
                        <br />


                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" CssClass="bttn bttn-primary bttn-action"></asp:Button>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">


                                <asp:GridView ID="DGJob" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="20"
                                    class="striped" AllowSorting="true" OnPageIndexChanging="DGJob_PageIndexChanging"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="" />
                                        <asp:BoundField HeaderText="Seedline" DataField="loc" />
                                        <asp:BoundField HeaderText="Job" DataField="jobcode" />
                                        <asp:BoundField HeaderText="Item" DataField="itmdescp" />
                                        <asp:BoundField HeaderText="Cust Name" DataField="cname" />
                                        <asp:BoundField HeaderText="SO Date" DataField="sodate" DataFormatString="{0:M/d/yy}" HtmlEncode="false" />
                                        <asp:BoundField HeaderText="SO Trays" DataField="sotrays" DataFormatString="{0:###,#}" />
                                        <asp:BoundField HeaderText="Tray Size" DataField="ts" />
                                        <asp:TemplateField HeaderText="WO Trays">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Txtgtrays" Width="50" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Txtgplantdt" TextMode="Date" Width="80px" runat="server"></asp:TextBox>
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Seeds Allocated" DataField="alloc" />
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>
    </div>
</asp:Content>
