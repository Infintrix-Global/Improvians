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
                                    class="striped" AllowSorting="true" OnRowCommand="gvMove_RowCommand" OnRowDataBound="gvMove_RowDataBound"
                                    GridLines="None" PageSize="10" OnPageIndexChanging="gvMove_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("JobCode")  %>'></asp:Label>
                                                   <asp:Label ID="lblMoveAssignID" runat="server" Visible="false" Text='<%# Eval("Sh_Co_AssignId")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTray" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From Facility" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacilityFrom" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Facility" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblToFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="To Bench" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreenHouseName" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Request Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("CreateOn","{0:dd MMM yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Trays Left To be Moved" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                              
                                                  <asp:Label ID="lblTraysRequest" runat="server" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                     <%--   <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="bttn bttn-primary bttn-action" CommandName="Select1" CommandArgument='<%# Container.DataItemIndex %>'></asp:Button>
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
                </div>

            </div>

            <div  runat="server" id="AddDetails"  class="dashboard__block dashboard__block--asign">
                <h3>Move Completion</h3>
                <div class="row">
                    <div class="col">
                      <%--  <asp:Label ID="lblToFacility" runat="server" Visible="false"></asp:Label>--%>
                        <asp:Label ID="lblMoveAssignID" runat="server" Visible="false"></asp:Label>
                        <label>Remaining Trays:</label>
                        <asp:Label ID="lblRemainingTrays" runat="server"></asp:Label>
                    </div>
                </div>
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
                                <label>Bench Location</label>
                                <asp:TextBox ID="txtPutAwayLocation" Enabled="false" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="col">
                                <label># Trays Moved</label>
                                <asp:TextBox ID="txtTrays" TextMode="Number" OnTextChanged="txtTrays_TextChanged" AutoPostBack="true" runat="server" CssClass="input__control"></asp:TextBox>

                                <span class="error_message">
                                    <asp:Label ID="lblerrmsg" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTrays" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Trays Moved" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col">
                                <label>Barcode of Putaway Location</label>
                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="input__control"></asp:TextBox>

                                <span class="error_message">
                                    <asp:Label ID="Label1" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBarcode" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Barcode" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-auto align-self-center">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" ValidationGroup="md" OnClick="btnSubmit_Click" />
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
