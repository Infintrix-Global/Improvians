<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="ChemicalTaskCompletion.aspx.cs" Inherits="Evo.ChemicalTaskCompletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="text-left">Chemical Task Completion </h2>

        <div class="dashboard__block dashboard__block--asign">


            <div id="userinput" runat="server">

                <asp:Panel ID="pnlint" runat="server">

                    <div class="row">
                        <div class="col-lg-12">
                            <h3 class="robotobold">
                                <label>Bench Location</label><br />
                                <asp:Label ID="lblBenchLocation" runat="server" Text=""></asp:Label>
                            </h3>
                        </div>
                    </div>

                    <asp:Panel ID="PanelCropHealth" Visible="false" runat="server">
                        <h2 class="text-left mb-3">Crop Health Report </h2>
                        <div class="portlet-body">

                            <div class="data__table">
                                <asp:GridView ID="gvCropHealth" runat="server" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
                                    GridLines="None" DataKeyNames="chid"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Type of Problem" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("typeofProblem")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cause of Problem" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("Causeofproblem")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Severity of Problem" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitem" runat="server" Text='<%# Eval("Severityofproblem")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Trays" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("NoTrays")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="% of Damage" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("PerDamage")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="New Estimated Ship Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("Date","{0:MM/dd/yyyy}")  %>'></asp:Label>
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
                            <div class="row">

                                <div class="col-lg-12">
                                    <asp:Label ID="lblCommment" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <br />

                    <div class="row">
                        <div class="col-12 mb-3">
                            <label class="d-block">Chemical Spary Date</label>

                            <asp:TextBox ID="txtSprayDate" class="input__control input__control-auto" TextMode="Date" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-12 mb-3">
                            <asp:TextBox ID="txtNotes" TextMode="MultiLine" class="input__control input__control-auto" placeholder="Notes" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-12 mb-3">
                            <%-- <label>Job No.</label><br />--%>
                            <h3 class="robotobold">
                                <asp:Label ID="lblJobID" runat="server"></asp:Label
                            </h3>
                            <asp:Label ID="lblGrowerID" Visible="false" runat="server"></asp:Label>
                        </div>

                        <div class="col-12 mb-3">
                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />

                            <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>


        <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
</asp:Content>
