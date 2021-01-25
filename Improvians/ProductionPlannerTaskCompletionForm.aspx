<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="ProductionPlannerTaskCompletionForm.aspx.cs" Inherits="Improvians.ProductionPlannerTaskCompletionForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Sc1" runat="server"></asp:ScriptManager>
     <div class="main">
        <div class="site__container">
            <h2>Production Planner Task Completion Form</h2>

            <div class="filter__row d-flex">
                <div class="row">
                    <div class="col m3">
                        <label>Customer </label>
                        <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                  
                    <div class="col m3">
                        <label>Job No </label>
                        <asp:DropDownList ID="ddlJobNo" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    
                </div>
            </div>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
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
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("#Tray")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Seed Lot" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("SeedLots")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:dd MMM yyyy}")  %>'></asp:Label>
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
                        </div>

                    </div>
                </div>

            </div>
            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>

               
            <div class="dashboard__block dashboard__block--asign">
              
                <div id="userinput" runat="server" class="assign__task d-flex" >
                    <asp:Panel ID="pnlint" runat="server">
                          <h3></h3>
                        <div class="row align-items-end">
                            <div class="col-auto m6" runat="server" visible="false">
                                <label>Job No.</label><br />
                                <h3 class="robotobold"><asp:Label ID="lblJobID" runat="server"></asp:Label></h3>
                                 <h3 class="robotobold"><asp:Label ID="lblSeedRequired" runat="server"></asp:Label></h3>
                            </div>
                            <div class="col m6">
                                <label>SeedLine Facility</label>
                                <asp:DropDownList ID="ddlSeedlineFacility" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                 <span class="error_message">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSeedlineFacility" ValidationGroup="md"
                                                SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select SeedLine Facility" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                            </div>
                            <div class="col m6">
                                <label>Put Away Facility</label>
                                     <asp:DropDownList ID="ddlLocation" runat="server" class="custom__dropdown robotomd" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                              <div class="col m6">
                                <label>Put Away BenchLocation</label>
                                  <asp:CheckBox ID="chkBenchLocation" runat="server" OnCheckedChanged="chkBenchLocation_CheckedChanged" AutoPostBack="true" />
                                       <asp:DropDownList ID="ddlBenchLocation" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            </div>
                            <div class="col m6">
                                <label>Seeding Due Date</label>
                                <asp:TextBox ID="txtSeedingDueDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                                  <span class="error_message">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSeedingDueDate" ValidationGroup="md"
                                                SetFocusOnError="true"  ErrorMessage="Please Enter Seeding Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                            </div>

                              <div class="col m6">
                                <label></label>
                                <asp:RadioButtonList ID="radOrder"  runat="server" class="input__control robotomd" AutoPostBack="true" OnSelectedIndexChanged="radOrder_SelectedIndexChanged">
                                    <asp:ListItem Text="Fill Complete Order" Value="Complete" ></asp:ListItem>
                                    <asp:ListItem Text="Fill Partial Order" Value="Partial" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                                  <asp:Label ID="lblTrays" runat="server" Visible="false" ></asp:Label>
                                  <asp:TextBox ID="txtTrays" runat="server" TextMode="Number" placeholder="No of trays to be seeded" ></asp:TextBox>
                            </div>
                            
                            <div class="col m6">
                                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false"   class="striped" 
                                    GridLines="None" 
                                    ShowHeaderWhenEmpty="True" Width="100%" >
                                    <Columns>
                                   
                                        <asp:TemplateField HeaderText="Lot" ItemStyle-Width="10%" >
                                            <ItemTemplate>
                                                  <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("SeedLotName")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="# Seed" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeed" runat="server" Text='<%# Eval("NoOfSeed")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                   
                                        <asp:TemplateField HeaderText="" >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkselect" runat="server" OnCheckedChanged="chkselect_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
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
                            <div class="col m6">
                                <label>Total Seed Allocated</label>
                                <asp:TextBox ID="txtSeedsAllocated"  Enabled="false" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <%--<div class="clearfix"></div>--%>

                            <div class="col-auto m6">
                                <br />
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="col-auto m6">
                                <br />
                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

                     </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
