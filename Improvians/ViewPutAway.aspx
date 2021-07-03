<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="ViewPutAway.aspx.cs" Inherits="Evo.ViewPutAway" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="text-left mb-3">Put Away</h2>
        
        <br />
        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                          
                            <asp:GridView ID="gvPutAway" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                                GridLines="None" 
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                          
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemno" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
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
                    </div>

                </div>
            </div>

        </div>
        <div class="dashboard__block dashboard__block--asign">


            <div id="userinput" runat="server">
                
                <asp:Panel ID="pnlint" runat="server">
                    <h3 class="text-left dark_txt mb-2">Task Request Flow</h3>

                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="GridViewDetails" 
                                AutoGenerateColumns="false" runat="server">
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
                                      <asp:TemplateField HeaderText="Work Date">
                                        <ItemTemplate>

                                            <asp:Label ID="lblWorkDate" runat="server" Text='<%#Bind("CreateOn","{0:MM/dd/yyyy}") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Facility">
                                        <ItemTemplate>

                                            <asp:Label ID="lblFacilityID" runat="server" Text='<%#Bind("FacilityID") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bench Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGreenHouseID" runat="server" Text='<%#Bind("GreenHouseID") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Tray">
                                        <ItemTemplate>

                                            <asp:Label ID="lblTray" runat="server" Text='<%#Bind("Trays") %>'></asp:Label>


                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bench Occupancy">
                                        <ItemTemplate>

                                            <asp:Label ID="lblBenchOccupancy" runat="server" Text='<%#Bind("BenchOccupancy") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Slot Position Start">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSlotPositionStart" runat="server" Text='<%#Bind("SlotPositionStart") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Slot Position End">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSlotPositionEnd" runat="server" Text='<%#Bind("SlotPositionEnd") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                    </div>

                </asp:Panel>
                  <br />
                <asp:Panel ID="PanlTaskComplition" Visible="false" runat="server">
                    <h3 class="text-left dark_txt mb-2">Completion</h3>
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="GridViewCompletion" 
                                AutoGenerateColumns="false" runat="server">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  

                                      <asp:TemplateField HeaderText="Completed By">
                                        <ItemTemplate>

                                            <asp:Label ID="lblCAassignedby" runat="server" Text='<%#Bind("Aassignedby") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Completion Date">
                                        <ItemTemplate>

                                            <asp:Label ID="lblMoveDate" runat="server" Text='<%#Bind("MoveDate","{0:MM/dd/yyyy}") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Moved Trays">
                                        <ItemTemplate>

                                            <asp:Label ID="lblTraysMoved" runat="server" Text='<%#Bind("TraysMoved") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Barcode">
                                        <ItemTemplate>

                                            <asp:Label ID="lblBarcode" runat="server" Text='<%#Bind("Barcode") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                    </div>

                </asp:Panel>


                
            </div>
        </div>


        <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
</asp:Content>
