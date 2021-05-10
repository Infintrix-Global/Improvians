<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSeedlineReport.aspx.cs" MasterPageFile="~/EvoMaster.Master" Inherits="Evo.PrintSeedlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container d-print-none">
        <h2 class="text-center">Print Reports</h2>

        <div class="print__reports text-center mt-5">
            <div class="card d-inline-block">
                <div class="card-body">
                    <div class="row text-left">
                        <div class="col-auto">
                            <label>Select Date</label>
                            <asp:DropDownList runat="server" ID="ddlDate" DataTextField="createon" DataTextFormatString="{0:MM/dd/yyyy}" DataValueField="createon" class="custom__dropdown">
                            </asp:DropDownList>
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" OnClick="btnSubmit_Click" runat="server" Text="Print" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="d-none d-print-block">
        <div class="page__us py-4 px-2">
            <div class="container-fluid">

                <div class="page__us--header">
                    <div class="row align-items-center">
                        <div class="col-3">
                            <img class="page__logo" alt="Growers Transplanting Logo" src="images/logo-vertical.svg" width="180" height="179" />
                        </div>
                        <div class="col-6">
                            <h1 class="h4 robotobold text-center mb-0">Performance Program Log Sheet</h1>
                        </div>
                        <div class="col-3"></div>
                    </div>
                </div>

                <asp:Panel runat="server" ID="Panel1">
                    <asp:Repeater ID="repReport" runat="server" OnItemDataBound="repReport_ItemDataBound">
                        <ItemTemplate>


                            <div class="page-break">
                                <div class="row mt-4">
                                    <div class="d-flex align-items-center mb-1 col-12">
                                        <label class="d-block inline__fields mr-3 mb-0">Deptartment:</label>
                                        <div class="field__blank">SEEDLINE-<asp:Label runat="server" ID="lblFacility" Text='<%# Eval("loc_seedline") %>' /></div>
                                    </div>
                                    <div class="d-flex align-items-center mb-1 col-12">
                                        <label class="d-block inline__fields mr-3 mb-0">Submit Date:</label>
                                        <div class="field__blank">
                                            <asp:Label runat="server" ID="lblDate" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>' />
                                        </div>
                                    </div>
                                    <div class="d-flex align-items-center mb-1 col-12">
                                        <label class="d-block inline__fields mr-3 mb-0">Operator:</label>
                                        <div class="field__blank"></div>
                                    </div>
                                </div>

                                <div class="data__table">
                                    <asp:GridView ID="DGJob" runat="server" AutoGenerateColumns="False"
                                        class="striped"
                                        GridLines="None" HeaderStyle-BackColor="#489d48"   HeaderStyle-ForeColor="#ffffff"
                                        ShowHeaderWhenEmpty="True" Width="100%">

                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="DATE"  HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="SrNo" runat="server" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="JOB" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("jobcode") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CUSTOMER">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("cname") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ITEM DESCRIPTION">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itemdescp") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderStyle-Width="60px" HeaderText="LOC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Seedline" Text='<%# Eval("loc_seedline") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="TRAY SIZE" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("traysize") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrays" runat="server" Text='<%# Eval("trays_actual") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SOIL" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSoil" runat="server" Text='<%# Eval("Soil") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GOING OUT" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BACK" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GH-SIGNATURE" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SEEDLINE SIGNATURE" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COMMENTS" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="page__us--footer">
                                <div class="d-flex align-items-center">
                                    <label class="d-block inline__fields inline__fields-signs mr-3 mb-0">Seed Deliever Sign.</label>
                                    <div class="field__blank"></div>
                                </div>
                                <div class="d-flex align-items-center">
                                    <label class="d-block inline__fields inline__fields-signs mr-3 mb-0">Seed Line Sign.</label>
                                    <div class="field__blank"></div>
                                </div>
                                <div class="d-flex align-items-center">
                                    <label class="d-block inline__fields inline__fields-signs mr-3 mb-0">Seed Office Sign.</label>
                                    <div class="field__blank"></div>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>
