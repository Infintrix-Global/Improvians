<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="JobReport.aspx.cs" Inherits="Evo.JobReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main__header">
        <div class="site__container">
            <div class="row">
                <div class="col m12">
                    JOB:
                    <asp:TextBox ID="Txtjob" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="BTrun" runat="server" Text="Run" CssClass="bt" OnClick="BTrun_Click" />
                    <br />
                    <b>JOB SUMMARY:</b>
                    <br />
                    <asp:GridView ID="DGHead01" runat="server" AutoGenerateColumns="false" DataKeyNames="">
                        <Columns>
                            <asp:BoundField HeaderText="Cust Name" DataField="cname" />
                            <asp:BoundField HeaderText="SO No" DataField="sono" />
                            <asp:BoundField HeaderText="SO Line" DataField="soline" />
                            <asp:BoundField HeaderText="Item" DataField="itemno" />
                            <asp:BoundField HeaderText="Description" DataField="itemdescp" />
                            <asp:BoundField HeaderText="Tray Size" DataField="ts" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="DGHead02" runat="server" AutoGenerateColumns="false" DataKeyNames="seeddt">
                        <Columns>
                            <asp:BoundField HeaderText="Organic" DataField="org" />
                            <asp:BoundField HeaderText="Qty Seeded" DataField="trays" DataFormatString="{0:###,0}" />
                            <asp:BoundField HeaderText="Seeded Date" DataField="seeddt" DataFormatString="{0:M/d/yy}" />
                            <asp:BoundField HeaderText="Plant Age" />
                            <asp:BoundField HeaderText="Germ %" DataField="germpct" />
                            <asp:BoundField HeaderText="Overage" DataField="overage" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="DGSeeds" runat="server" AutoGenerateColumns="false" DataKeyNames="">
                        <Columns>
                            <asp:BoundField HeaderText="Seed Code" DataField="seed" />
                            <asp:BoundField HeaderText="Lot No" DataField="lot" />
                            <asp:BoundField HeaderText="Qty Used" DataField="qty" DataFormatString="{0:###,0}" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="DGHealth" runat="server" AutoGenerateColumns="false" DataKeyNames="">
                        <Columns>
                            <asp:BoundField HeaderText="Date" DataField="dt" DataFormatString="{0:M/d/yy}" />
                            <asp:BoundField HeaderText="Category" DataField="cat" />
                            <asp:BoundField HeaderText="Description" DataField="descp" />
                            <asp:BoundField HeaderText="Remark" DataField="Remark" />
                        </Columns>
                    </asp:GridView>
                    <div style="height: 300px; overflow: auto">
                        <asp:GridView ID="DGTasks" runat="server" AutoGenerateColumns="false" DataKeyNames="compdate">
                            <Columns>
                                <asp:BoundField HeaderText="Task" DataField="act" />
                                <asp:BoundField HeaderText="Assign Date" DataField="assigndate" DataFormatString="{0:M/d/yy}" />
                                <asp:BoundField HeaderText="Completed Date" DataField="compdate" DataFormatString="{0:M/d/yy}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <asp:GridView ID="DGInventory" runat="server" AutoGenerateColumns="false" DataKeyNames="trays">
                        <Columns>
                            <asp:BoundField HeaderText="Facility" DataField="loc" />
                            <asp:BoundField HeaderText="Bench" DataField="bench" />
                            <asp:BoundField HeaderText="Balance" DataField="trays" DataFormatString="{0:###,0}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
</asp:Content>
