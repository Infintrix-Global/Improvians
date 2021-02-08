<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedLineCompletionFinal.aspx.cs" Inherits="Improvians.SeedLineCompletionFinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Sc1" runat="server"></asp:ScriptManager>
    <div class="main">
        <div class="site__container">
            <h2>Seedline Planner Task Completion Form</h2>

            <div class="filter__row d-flex">
                <div class="row justify-content-center">
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
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="No. Of Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("trays_plan")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeded Due Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SoDate","{0:dd MMM yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Planned Due Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("due_date","{0:dd MMM yyyy}")  %>'></asp:Label>
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

                <div id="userinput" runat="server" class="assign__task d-flex">
                    <asp:Panel ID="pnlint" runat="server">
                        <h3></h3>
                        <div class="row">
                            <div class="col-lg-6" runat="server" visible="false">
                                <label># OF SEEDS REQUIRED TO FULFILL ORDER:</label><br />
                                <h3 class="robotobold">
                                    <asp:Label ID="lblJobID" runat="server" Visible="false"></asp:Label></h3>
                                <h3 class="robotobold"></h3>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <h4>Confirm Tray Size:</h4>
                                    <div class="d-flex">
                                        <label>
                                            Is the tray size [<asp:Label ID="lbltraysizecon" runat="server"></asp:Label>
                                            ] correct?</label>
                                        <span class="custom-control custom-radio ml-4 mr-2">
                                            <asp:RadioButtonList ID="radtraysize" Width="150px" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>

                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label>Seeding Date</label>
                                    <asp:TextBox ID="txtSeedingDate" ClientIDMode="Static" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                                    <span class="error_message">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSeedingDate" ValidationGroup="e"
                                            SetFocusOnError="true" ErrorMessage="Please Enter Seeding Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                </div>



                                <div class="col-lg-4">

                                    <br />
                                    <asp:RadioButtonList ID="radOrder" runat="server" Width="300px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radOrder_SelectedIndexChanged">
                                        <asp:ListItem Text="Fill Complete Order" Value="Complete" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Fill Partial Order" Value="Partial"></asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>


                                <div class="col-lg-4">
                                    <label>No of trays to be seeded</label>
                                    <asp:Label ID="lblTrays" runat="server" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtTrays" runat="server" TextMode="Number" class="input__control robotomd" placeholder="No of trays to be seeded"></asp:TextBox>
                                    <span class="error_message">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTrays" ValidationGroup="e"
                                            SetFocusOnError="true" ErrorMessage="Please Enter # Trays" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>

                                </div>
                            </div>


                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-lg-12 mt-3">
                                        <h4>Seed Lot Usage:</h4>
                                        <asp:Label ID="lblTraySize" runat="server" Visible="false"></asp:Label>
                                        <h3 class="mb-2">NO. OF SEEDS REQUIRED TO FULFILL ORDER:
                                            <asp:Label ID="lblSeedRequired" runat="server"></asp:Label></h3>
                                    </div>

                                    <div class="row">
                                        <div class="col m3">
                                            <label>Seed Lot</label>
                                            <asp:DropDownList ID="ddlSeedLot" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                            <span class="error_message">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSeedLot" ValidationGroup="md"
                                                    SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select SeedLot" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="col m3">
                                            <br />
                                            <asp:Button ID="btnAddTray" OnClick="btnAddTray_Click"  class="submit-bttn bttn bttn-primary" runat="server" Text="Add" TabIndex="13" ValidationGroup="md" />
                                        </div>
                                    </div>
                                    <%--      <div class="col m3">
                                        <label>Actual # of Tray </label>
                                       <asp:TextBox ID="txtActualTray" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                        <span class="error_message">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtActualTray" ValidationGroup="md"
                                                SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Enter Tray" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>

                     <div class="col m3">
                                        <label>Used/Unused</label>
                                      <asp:DropDownList ID="ddlType" runat="server" class="custom__dropdown robotomd">
                                                            <asp:ListItem Text="Used" Value="Used"></asp:ListItem>
                                                            <asp:ListItem Text="UnUsed" Value="UnUsed"></asp:ListItem>
                                                            <asp:ListItem Text="Partial" Value="Partial"></asp:ListItem>
                                                        </asp:DropDownList>
                                    </div>

                                    <div class="col m3">
                                        <label>Partial</label>
                                        <asp:TextBox ID="txtPartial" Text="0"  TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                        <span class="error_message">
                                            <asp:Label ID="lblerrmsg" runat="server" ForeColor="red"></asp:Label>
                                         
                                        </span>
                                    </div>--%>




                                    <div class="row">
                                        <div class="col-lg-12 my-3">
                                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" class="striped data__table w-auto"
                                                GridLines="None" OnRowDataBound="gvDetails_RowDataBound"  OnRowDeleting="gvDetails_RowDeleting"
                                                ShowHeaderWhenEmpty="True">
                                                <Columns>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("SeedLotID")  %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblLotName" Text='<%# Eval("SeedLot")  %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="# of Seed">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblactualseed" Text='<%# Eval("ActualSeed")  %>' runat="server"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Actual # of tray Seeded">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblactualseed" Text='<%# Bind("#ActualTray")  %>' runat="server" ></asp:Label>--%>
                                                            <asp:TextBox ID="txtActualTray" Text='<%# Eval("NoOftray")  %>' runat="server" AutoPostBack="true" OnTextChanged="txtActualTray_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Calculated # of Seed">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSeed" runat="server" Text='<%# Eval("Seed")  %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Used/Unused">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlType" runat="server" class="custom__dropdown robotomd">
                                                                <asp:ListItem Text="Used" Value="Used"></asp:ListItem>
                                                                <asp:ListItem Text="UnUsed" Value="UnUsed"></asp:ListItem>
                                                                <asp:ListItem Text="Partial" Value="Partial"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("type")  %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Leftover seeds">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPartial" runat="server" Text='<%# Eval("LeftOver")  %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                              <asp:Button Text="Remove" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="Delete" ID="btnRemove" runat="server"   CssClass="bttn bttn-primary bttn-action"  />
                                                     
                                                          
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


                                    <div class="col-lg-6 m6">
                                        <label>Total Seed Allocated</label>
                                        <asp:TextBox ID="txtSeedsAllocated" Enabled="false" runat="server" class="input__control robotomd"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <h4>Seeding Details:</h4>
                                        <div class="d-flex align-items-center">
                                            <label class="mb-2">Requested No of Trays Seeded:</label>
                                            <asp:TextBox ID="txtRequestedTrays" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="d-flex align-items-center mt-2">
                                            <label class="mb-2">Actual No of Trays Seeded:</label>
                                            <asp:TextBox ID="txtActualTraysNo" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-lg-6">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 mt-4 mt-md-0">
                                        <h4>Job Completion:</h4>
                                        <div>
                                            <span class="custom-control custom-radio mx-2">
                                                <asp:RadioButtonList ID="radJobCompletion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radJobCompletion_SelectedIndexChanged">
                                                    <asp:ListItem Text="Full" Value="Full" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Partial" Value="Partial"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>

                                            <span>
                                                <asp:TextBox ID="txtCompletedTrays" runat="server" TextMode="Number" placeholder="Enter # Completed" Visible="false"></asp:TextBox>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <%-- <span class="custom-control custom-checkbox mx-2">--%>
                                        <span>
                                            <asp:CheckBox ID="chkSeedReturn" runat="server" />
                                            <label for="chkSeedReturn">Seed Returns Complete: Partial and Unused Seeds Boxed Up and Labeled, Scanned</label>
                                        </span>
                                    </div>



                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="col-lg-12 mt-3 text-center">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-3" runat="server" OnClick="btnSubmit_Click" ValidationGroup="e" />

                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>

                            <div class="col-12 text-center mt-4">
                                <asp:Label ID="lblsubmsg" runat="server" Text="Job sent to grower for put-away location once submitted." Font-Italic="true"></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>


        </div>
</asp:Content>
