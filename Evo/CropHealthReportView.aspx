<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="CropHealthReportView.aspx.cs" Inherits="Evo.CropHealthReportView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="site__container">
        <h2>Crop Health Report  Completion</h2>

        <div class="data__table">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>

            <asp:Label ID="lbljid" Visible="false" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lblChid" Visible="false" runat="server" Text=""></asp:Label>
            <asp:GridView ID="gvCorpHelthDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                class="striped" AllowSorting="true"
                GridLines="None" PageSize="10"
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




                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblGreenHouseID" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Crop Health Report Date " HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>

                            <asp:Label ID="Label121" runat="server" Text='<%# Eval("CropHealthReportDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
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
            <form class="web__form pt-2">
                <div class="row justify-content-center">
                    <div class="col-12">
                        <h3 class="text-left dark_txt mb-2">Task Request Flow</h3>

                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="GridViewDumpView" runat="server" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assigned By">
                                            <ItemTemplate>

                                                <asp:Label ID="lblAassignedTo" runat="server" Text='<%#Bind("Aassignedby") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assigned To">
                                            <ItemTemplate>

                                                <asp:Label ID="lblAassignedby" runat="server" Text='<%#Bind("AassignedTo") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                      <%--  <asp:TemplateField HeaderText="Quantity of Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantityOfTray" runat="server" Text='<%# Eval("QuantityOfTray")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Work Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDumpDate" runat="server" Text='<%# Eval("CropHealthReportDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments")  %>'></asp:Label>
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
                        <br />

                    </div>
                </div>
            </form>

            <asp:Panel ID="PanelCropHealth" Visible="false" runat="server">
                <h3 class="text-left dark_txt mb-2">Crop Health Report </h3>
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
                                     <asp:TemplateField HeaderText="Commment" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCropHealthCommit" runat="server" Text='<%# Eval("CropHealthCommit")  %>'></asp:Label>
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
                 <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:DataList ID="CropePhotos" runat="server"  BorderStyle="None" RepeatDirection="Horizontal" RepeatColumns="4">
                                    <ItemTemplate>
                                        <div>

                                            <asp:Image Width="100" Height="150" ID="Image1" ImageUrl='<%# Bind("Imagepath") %>' runat="server" />


                                        </div>
                                    </ItemTemplate>


                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" />
                                </asp:DataList>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </div>
</asp:Content>
