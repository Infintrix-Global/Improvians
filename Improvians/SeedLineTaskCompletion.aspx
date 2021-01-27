<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedLineTaskCompletion.aspx.cs" Inherits="Improvians.SeedLineTaskCompletion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="main main__header">
            <div class="site__container">
                <h2>Seedline Task Completion</h2>
                
                <div class="filter__row row">
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
                </div>

                <div class="data__table">
                 <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" 
                                    GridLines="None" PageSize="10" 
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                                 <asp:Label ID="Label2" runat="server" Text='<%# Eval("JobID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Item" >
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Item")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Seeding Due Date" >
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("SeedingDueDate")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                               <asp:TemplateField HeaderText="No. Of Trays to be seeded">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("#TraysSeeded")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Tray Size">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          
                                        <asp:TemplateField HeaderText="Seed Lots" >
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("LotList")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField HeaderText="Plant Type" >
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

                <div class="text-left dashboard__block mt-4">
                    <form class="web__form pt-2">
                        <div class="row justify-content-center">
                            <div class="col-12">
                                <div class="row justify-content-center">
                                    <div class="col-12">
                                        <h4>Confirm Tray Size:</h4>
                                        <div class="d-flex">
                                            <label>Is the tray size [256] correct?</label>
                                            <span class="custom-control custom-radio ml-4 mr-2">
                                                <asp:RadioButtonList ID="radtraysize" runat="server">
                                                    <asp:ListItem  Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem  Text="N" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                               
                                            </span>
                                        </div>
                                    </div>
                                    
                                    <div class="col-xl-12">
                                        <h4>Seed Lot Usage:</h4>
                                        <div class="text-center data__table mt-2 mb-4">
                                           <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" class="striped data__table w-auto"
                                            GridLines="None"
                                            ShowHeaderWhenEmpty="True">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                      
                                                        <asp:Label ID="lblLotName" Text='<%# Eval("SeedLotName")  %>' runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actual # of tray Seeded">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtActualTray" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="# of Seed">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSeed" runat="server" Text='<%# Eval("NoOfSeed")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Used/Unused">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlType" runat="server" >
                                                            <asp:ListItem Text="Used" Value="Used"></asp:ListItem>
                                                            <asp:ListItem Text="UnUsed" Value="UnUsed"></asp:ListItem>
                                                            <asp:ListItem Text="Partial" Value="Partial"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Partial">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPartial" runat="server" ></asp:TextBox>
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

                                    <div class="col-md-6">
                                        <h4>Seeding Details:</h4>
                                        <div class="d-flex align-items-center">
                                            <label class="mb-2">Requested No of Trays Seeded:</label>
                                           <asp:TextBox ID="txtRequestedTrays" Enabled="false" runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="d-flex align-items-center mt-2">
                                            <label class="mb-2">Actual No of Trays Seeded:</label>
                                          <asp:TextBox ID="txtActualTrays" Enabled="false" runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-lg-6">
                                             <asp:TextBox ID="txtSeededDate" TextMode="Date"  runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 mt-4 mt-md-0">
                                        <h4>Job Completion:</h4>
                                        <div>
                                            <span class="custom-control custom-radio mx-2">
                                                <asp:RadioButtonList ID="radJobCompletion" runat="server" OnSelectedIndexChanged="radJobCompletion_SelectedIndexChanged">
                                                    <asp:ListItem  Text="Full" Value="Full"></asp:ListItem>
                                                    <asp:ListItem  Text="Partial" Value="Partial"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>
                                       
                                            <span>
                                                 <asp:TextBox ID="txtTrays" runat="server" TextMode="Number" placeholder="Enter # Completed" Visible="false"></asp:TextBox>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-12 mt-3">
                                        <span class="custom-control custom-checkbox mx-2">
                                           <asp:CheckBox ID="chkPutAwayLocation" runat="server" Checked="true" Enabled="false" />
                                            <label for="chkPutAwayLocation">Specify put-away location</label>
                                        </span>
                                    </div>

                                    <div class="col-12">
                                        <span class="custom-control custom-checkbox mx-2">
                                           <asp:CheckBox ID="chkSeedReturn" runat="server"  />
                                            <label for="chkSeedReturn">Seed Returns Complete: Partial and Unused Seeds Boxed Up and Labeled, Scanned</label>
                                        </span>
                                    </div>
                                    
                                    <div class="col-12 text-center mt-4">
                                         <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-3" runat="server" OnClick="btnSubmit_Click" ValidationGroup="md" />

                                        <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>

            </div>
        </div>


</asp:Content>
