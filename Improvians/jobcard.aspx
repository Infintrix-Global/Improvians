<%@ Page Title="" Language="VB" MasterPageFile="~/ne.master" AutoEventWireup="false" CodeFile="jobcard.aspx.vb" Inherits="gti_jobcard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="necpholder" runat="Server">
    <table style="width: 95%;">
        <tr>
            <td colspan="3">
                <table width="95%" cellspacing="0">
                    <tr>
                        <td class="tabinact">
                            <a href="germrept.aspx">Germ Report</a>
                        </td>
                        <td style="width: 1px">&nbsp;
                        </td>
                        <td class="tabinact">
                            <a href="seedinv.aspx">Seeds</a>
                        </td>
                        <td style="width: 1px">&nbsp;
                        </td>
                        <td class="tabinact">
                            <a href="sales.aspx">GTI Sales</a>
                        </td>
                        <td style="width: 1px">&nbsp;
                        </td>
                        <td class="tabinact">
                            <a href="wo.aspx">Work Orders</a>
                        </td>
                        <td style="width: 1px">&nbsp
                        </td>
                        <td class="tabact">Job Report
                        </td>
                        <td style="width: 30%">&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td class="tabact" colspan="15">
                            <img src="N" height="4" alt="" width="1" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblerr" runat="server" CssClass="txterr" EnableViewState="false"></asp:Label>
                <asp:Label ID="lblmsg" runat="server" CssClass="txtmsg" EnableViewState="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                JOB: <asp:TextBox ID="Txtjob" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="BTrun" runat="server" Text="Run" CssClass="bt" />
            </td>
        </tr>
        <tr>
            <td>
               &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <b>JOB SUMMARY:</b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Lbljob" runat="server" CssClass="txtcomp"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
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
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td><b>SEED / LOT:</b>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="DGSeeds" runat="server" AutoGenerateColumns="false" DataKeyNames="">
                                <Columns>
                                    <asp:BoundField HeaderText="Seed Code" DataField="seed" />
                                    <asp:BoundField HeaderText="Lot No" DataField="lot" />
                                    <asp:BoundField HeaderText="Qty Used" DataField="qty" DataFormatString="{0:###,0}" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <asp:Panel ID="Pnlhealth" runat="server" Visible="false">
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td><b>HEALTH:</b>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="DGHealth" runat="server" AutoGenerateColumns="false" DataKeyNames="">
                                <Columns>
                                    <asp:BoundField HeaderText="Date" DataField="dt" DataFormatString="{0:M/d/yy}" />
                                    <asp:BoundField HeaderText="Category" DataField="cat" />
                                    <asp:BoundField HeaderText="Description" DataField="descp" />
                                    <asp:BoundField HeaderText="Remark" DataField="Remark" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
            </asp:Panel>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td><b>TASKS:</b>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <div style="height: 300px; overflow: auto">
                            <asp:GridView ID="DGTasks" runat="server" AutoGenerateColumns="false" DataKeyNames="compdate">
                                <Columns>
                                    <asp:BoundField HeaderText="Task" DataField="act" />
                                    <asp:BoundField HeaderText="Assign Date" DataField="assigndate" DataFormatString="{0:M/d/yy}" />
                                    <asp:BoundField HeaderText="Completed Date" DataField="compdate" DataFormatString="{0:M/d/yy}" />
                                </Columns>
                            </asp:GridView>
                                </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td><b>INVENTORY:&nbsp;<asp:Label ID="Lblinvct" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="DGInventory" runat="server" AutoGenerateColumns="false" DataKeyNames="trays">
                                <Columns>
                                    <asp:BoundField HeaderText="Facility" DataField="loc" />
                                    <asp:BoundField HeaderText="Bench" DataField="bench" />
                                    <asp:BoundField HeaderText="Balance" DataField="trays" DataFormatString="{0:###,0}" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

