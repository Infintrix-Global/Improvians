<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedLineCompletionFinal.aspx.cs" Inherits="Improvians.SeedLineCompletionFinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Sc1" runat="server"></asp:ScriptManager>
    <div class="main">
        <div class="site__container">
            <h2>Seedline Planner Task Completion Form</h2>

            <%-- <div class="filter__row d-flex">
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
            </div>--%>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">

                            <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" class="data__table" AutoGenerateColumns="False"
                                AllowSorting="true" PageSize="10"
                                GridLines="None"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>Job No.</td>
                                                    <td>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")%>'></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td>Item</td>
                                                    <td>

                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td>No. Of Tray</td>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("trays_plan")  %>'></asp:Label></td>

                                                </tr>

                                                <tr>
                                                    <td>Tray Size</td>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label></td>

                                                </tr>

                                                <tr>
                                                    <td>Seeded Due Date</td>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("SoDate","{0:dd MMM yyyy}")  %>'></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td>Planned Due Date</td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("due_date","{0:dd MMM yyyy}")  %>'></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td>Plant Type</td>
                                                    <td>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label></td>

                                                </tr>
                                            </table>
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
                                    <span class="custom-control custom-radio ml-4 mr-2">
                                        <asp:RadioButtonList ID="radOrder" runat="server" Width="300px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radOrder_SelectedIndexChanged">
                                            <asp:ListItem Text="Fill Complete Order" Value="Complete" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Fill Partial Order" Value="Partial"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </span>

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


                            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                            <div class="col-lg-12 mt-3">
                                <h4>Seed Lot Usage:</h4>
                                <asp:Label ID="lblTraySize" runat="server" Visible="false"></asp:Label>
                                <h3 class="mb-2">NO. OF SEEDS REQUIRED TO FULFILL ORDER:
                                            <asp:Label ID="lblSeedRequired" runat="server"></asp:Label></h3>
                            </div>

                            <%--  <div class="row">
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
                                    <asp:Button ID="btnAddTray" OnClick="btnAddTray_Click" class="submit-bttn bttn bttn-primary" runat="server" Text="Add" TabIndex="13" ValidationGroup="md" />
                                </div>
                            </div>--%>





                            <div class="row">
                                <div class="col-lg-12 my-3">
                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" class="striped data__table w-auto"
                                        GridLines="None"
                                        ShowHeaderWhenEmpty="True">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblLotName" Text='<%# Eval("l2")  %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="# of Seed">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblactualseed" Text='<%# Eval("QTY")  %>' runat="server"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Actual # of tray Seeded">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblactualseed" Text='<%# Bind("#ActualTray")  %>' runat="server" ></asp:Label>--%>
                                                    <asp:TextBox ID="txtActualTray" class="input__control robotomd" Text="" runat="server" AutoPostBack="true" OnTextChanged="txtActualTray_TextChanged"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Calculated # of Seed">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSeed" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Used/Unused">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlType" runat="server" class="custom__dropdown robotomd">
                                                        <asp:ListItem Text="Used" Value="Used"></asp:ListItem>
                                                        <asp:ListItem Text="UnUsed" Value="UnUsed"></asp:ListItem>
                                                        <asp:ListItem Text="Partial" Value="Partial"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leftover seeds">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPartial" class="input__control robotomd" runat="server" Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Button Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="Delete" ID="btnRemove" runat="server" CssClass="bttn bttn-primary bttn-action" />

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
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
                        <br />

                        <div class="row">
                            <div class="col-md-4">
                                <h4>Seeding Details:</h4>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-4">
                                <label>Total Seed Allocated</label>
                                <asp:TextBox ID="txtSeedsAllocated" ReadOnly="true" runat="server" class="input__control robotomd"></asp:TextBox>
                            </div>
                            <div class="col-md-4">


                                <label>Requested No of Trays Seeded:</label>
                                <asp:TextBox ID="txtRequestedTrays" ReadOnly="true" class="input__control robotomd" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-md-4">

                                <label>Actual No of Trays Seeded:</label>
                                <asp:TextBox ID="txtActualTraysNo" ReadOnly="true" class="input__control robotomd" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <h4>Job Completion:</h4>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mt-4 mt-md-0">

                                <div>
                                    <span class="custom-control custom-radio ml-4 mr-2">
                                        <asp:RadioButtonList ID="radJobCompletion" runat="server" Width="300px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radJobCompletion_SelectedIndexChanged">
                                            <asp:ListItem Text="Full" Value="Full" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Partial" Value="Partial"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </span>

                                    <span>
                                        <asp:TextBox ID="txtCompletedTrays" runat="server" TextMode="Number" placeholder="Enter # Completed" Visible="false"></asp:TextBox>
                                    </span>
                                </div>
                            </div>

                        </div>
                        <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>


                        <div class="col-12">
                            <%-- <span class="custom-control custom-checkbox mx-2">--%>
                            <span>
                                <asp:CheckBox ID="chkSeedReturn" runat="server" />
                                <label for="chkSeedReturn">Seed Returns Complete: Partial and Unused Seeds Boxed Up and Labeled, Scanned</label>
                            </span>
                        </div>
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
