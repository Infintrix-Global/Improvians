<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedLineLotFulfillment.aspx.cs" Inherits="Improvians.SeedLineLotFulfillment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="main main__header">
            <div class="site__container">
                <h2 class="text-center">Seeding Lot Fulfillment Task Completion</h2>
                
             <%--   <div class="filter__row row justify-content-center">
                    <div class="col-xl-auto col-12">
                        <label>Job No.</label>
                        <select class="w-100 filter__control filter__select custom__dropdown">
                            <option>Job No.</option>
                            <option>Option 2</option>
                            <option>Option 3</option>
                            <option>Option 4</option>
                        </select>
                    </div>

                    <div class="col-xl-auto col-12">
                        <label>Customer Name</label>
                        <select class="w-100 filter__control filter__select custom__dropdown">
                            <option>Customer Name</option>
                            <option>Option 2</option>
                            <option>Option 3</option>
                            <option>Option 4</option>
                        </select>
                    </div>

                    <div class="col-xl-auto col-12">
                        <label>Facility Defaults</label>
                        <select class="w-100 filter__control filter__select custom__dropdown">
                            <option>Facility Defaults</option>
                            <option>Option 2</option>
                            <option>Option 3</option>
                            <option>Option 4</option>
                        </select>
                    </div>
                </div>--%>

                <div class="data__table">
                             <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" PageSize="10"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>


                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("JobID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Item")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("#TraysSeeded")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Seed Lot" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("SeedLots")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeedingDueDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
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

                <div class="text-center">
                    <div class="text-left dashboard__block mt-4">
                        <form class="web__form form__md mx-auto">
                            <div class="row justify-content-center">
                                <div class="col-lg-9 col-12">
                                    <div class="row justify-content-center">
                                        <div class="col-12">
                                            <span class="custom-control custom-checkbox">
                                               <asp:CheckBox ID="chkprntlbl" runat="server" />
                                                <label for="chkprntlbl">PRINT LABEL FROM BARTENDER</label>
                                            </span>
                                        </div>
                                        <div class="col-12">
                                            <span class="custom-control custom-checkbox">
                                              <asp:CheckBox ID="chkpulllots" runat="server" />
                                                <label for="chkpulllots">PULL SEED LOTS</label>
                                            </span>
                                        </div>
                                        <div class="col-12">
                                            <div class="text-center data__table data__table-seedinglot mt-2 mb-4">
                                          
                                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" class="striped data__table w-auto"
                                            GridLines="None"
                                            ShowHeaderWhenEmpty="True">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblLotName" Text='<%# Eval("SeedLotName")  %>' runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="# of Seed">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Label ID="lblSeed" runat="server" Text='<%# Eval("NoOfSeed")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pulled">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Textbox ID="txtBarCode"  runat="server" TextMode="Number" ></asp:Textbox>
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
                                        <div class="col-12">
                                            <span class="custom-control custom-checkbox">
                                            <asp:CheckBox ID="chkprntseed" runat="server" />
                                                <label for="chkprntseed">PRINT ABOVE SEED LOT LIST WITH JOB# & ADD TO PACKAGE</label>
                                            </span>
                                        </div>
                                        <div class="col-12">
                                            <span class="custom-control custom-checkbox">
                                                   <asp:CheckBox ID="chkShipSeed" runat="server" />
                                                <label for="chkShipSeed">SHIP SEEDS TO SEEDLINE FACILITY</label>
                                            </span>
                                        </div>

                                        <div class="col-12 text-center mt-4">
                                             <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-3" runat="server" OnClick="btnSubmit_Click"  />

                                        <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>

            </div>
        </div>


</asp:Content>
