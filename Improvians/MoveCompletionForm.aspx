<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MoveCompletionForm.aspx.cs" Inherits="Improvians.MoveCompletionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="site__container">
            <h2>Move Completion</h2>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvMove" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
                                    GridLines="None" PageSize="10" OnPageIndexChanging="gvMove_PageIndexChanging"
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

                                      <%--  <asp:TemplateField HeaderText="Put Away Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("PutAwayLocation")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("PutAwayMainLocation")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("#Tray")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

<%--                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seed Lot" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("SeedLots")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From Facility" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("FacilityFrom")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Facility" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("FacilityTo")  %>'></asp:Label>
                                                <asp:Label ID="lblToFacilityID" runat="server" Visible="false" Text='<%# Eval("FacilityToID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="To GreenHouse" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("GreenHouseName")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Request Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("RequestDate", "{0:dd MMM yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Trays To be Moved" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrayRequest" runat="server" Text='<%# Eval("TraysRequest")  %>'></asp:Label>
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
                <h3>Move Completion</h3>
                <div id="userinput" runat="server" class="assign__task d-flex">
                    <asp:Panel ID="pnlint" runat="server">
                        <div class="row">
                            <div class="col">
                                <label>Move Date </label>
                                <asp:TextBox ID="txtMoveDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMoveDate" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Move Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col">
                                <label>Put Away Location</label>
                                <asp:Label ID="lblToFacility" runat="server" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlLocation" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLocation" ValidationGroup="md"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Facility" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col">
                                <label># Trays Moved</label>
                                <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                <asp:Label ID="lblRemainingTrays" Visible="false" runat="server"></asp:Label>
                                <span class="error_message">
                                    <asp:Label ID="lblerrmsg" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTrays" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Trays Moved" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-auto align-self-center">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="col-auto align-self-center">
                                <asp:Button Text="Reset" ID="btnReset" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
