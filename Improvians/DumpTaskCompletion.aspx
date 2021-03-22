<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="DumpTaskCompletion.aspx.cs" Inherits="Evo.DumpTaskCompletion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main main__header">
        <div class="site__container">
            <h2>Dump Completion</h2>



            <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>
            <div class="data__table">
                <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    class="striped" AllowSorting="true"
                    GridLines="None" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                    ShowHeaderWhenEmpty="True" Width="100%">
                    <Columns>

                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                 <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                  <%--      <asp:TemplateField HeaderText="Main Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Dump Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                         
                                 <asp:Label ID="Label12" runat="server" Text='<%# Eval("DumpDateR","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

<%--                        <asp:TemplateField HeaderText="Planned Ship Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
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

            <div class="text-left dashboard__block my-4">
                <asp:Panel ID="PanelCropHealth" Visible="false" runat="server">
                    <br />
                    <h2 class="text-left">Crop Health Report </h2>

                    <br />
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
                    </div>
                     <div class="row">

                            <div class="col-lg-12">
                                <asp:Label ID="lblCommment" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                </asp:Panel>
                <br />
                <form class="web__form pt-2">
                    <div class="row justify-content-center">
                        <div class="col-12">
                           
                            <div class="row">



                                <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                                    <label class="d-block">Dump Date</label>

                                    <asp:TextBox ID="txtDumpDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                                    <label class="d-block">Quantity Of Tray</label>
                                    <asp:TextBox ID="txtQuantityOfTray" TextMode="Number" class="input__control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-12 col-sm-6 col-lg-3">
                                   <label class="d-block">Comments</label>
                                    <asp:TextBox ID="txtComment" TextMode="MultiLine" class="input__control" runat="server"></asp:TextBox>
                                </div>

                             

                                <div class="col-12 my-3">



                                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" class="ml-2 submit-bttn bttn bttn-primary" runat="server" Text="Submit" />
                                    <asp:Button ID="btnReset" OnClick="btnReset_Click" class="submit-bttn bttn bttn-primary" runat="server" Text="Reset" />

                                </div>
                            </div>

                        </div>
                    </div>
                </form>
            </div>

        </div>
    </div>
</asp:Content>
