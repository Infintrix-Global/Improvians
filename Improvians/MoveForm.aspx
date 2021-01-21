<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MoveForm.aspx.cs" Inherits="Improvians.MoveForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
      <div class="main">
        <div class="site__container">
            <h2>Move Request</h2>
            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
<div class="filter__row d-flex">
                <div class="row">
                    <div class="col m3">
                        <label>Customer </label>
                        <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col m3">
                        <label>GreenHouse </label>
                        <asp:DropDownList ID="ddlGreenhouse" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col m3">
                        <label>Facility </label>
                        <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
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
                                <asp:GridView ID="gvMove" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvMove_PageIndexChanging"
                                    GridLines="None" OnRowCommand="gvMove_RowCommand"
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

                                        <asp:TemplateField HeaderText="Put Away Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("PutAwayLocation")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("PutAwayMainLocation")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("#Tray")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
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
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:dd MMM yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                               <%--         <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Request" CssClass="bttn bttn-primary bttn-action" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
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
              



                <div id="userinput" runat="server" class="assign__task d-flex" visible="false">
                    <div class="row">
                        <div class="col m3">
                            <label>Job ID</label>
                              <asp:Label ID="lbljobid" runat="server" ></asp:Label>
                            </div>
                        <div class="col m3">
                             <label>Remaining Trays</label>
                             <asp:Label ID="lblUnmovedTrays"  runat="server"></asp:Label>
                        </div>
                    </div>
                    <asp:Panel ID="pnlint" runat="server">
                          <h3>Move Request</h3>
                        <div class="row">
                            <div class="col m3">
                             
                                <label>From Facility</label><br />
                                <h3 class="robotobold"><asp:Label ID="lblFromFacility" runat="server"></asp:Label></h3>
                            </div>
                                 <div class="col m3">
                                <label>To Facility </label>
                                <asp:DropDownList ID="ddlToFacility" runat="server" class="custom__dropdown robotomd" AutoPostBack="true" OnSelectedIndexChanged="ddlToFacility_SelectedIndexChanged"></asp:DropDownList>
                                      <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlToFacility" ValidationGroup="md"
                                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select To Facility" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                            </div>
                            <div class="col m3">
                                <label>Greenhouse </label>
                                <asp:DropDownList ID="ddlToGreenHouse" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                  <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlToGreenHouse" ValidationGroup="md"
                                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Greenhouse" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                            </div>
                       
                            <div class="col m3">
                                <label>Number Of Trays </label>
                               
                                <asp:TextBox ID="txtTrays" TextMode="Number" runat="server"></asp:TextBox>
                                 <span class="error_message">
                                     <asp:Label ID="lblerrmsg" runat="server" ForeColor="red"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTrays" ValidationGroup="md"
                                                        SetFocusOnError="true" ErrorMessage="Please Enter Trays" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                            </div>
                            <div class="col m2">
                                    
                                        <asp:Button ID="btnAddTray" OnClick="btnAddTray_Click" class="submit-bttn bttn bttn-primary"  runat="server" Text="Add" TabIndex="13" ValidationGroup="md" />
                                    </div>

                            <div class="clearfix"></div>

                              <asp:Panel ID="pnlPoints" runat="server" CssClass="pnlpoint">
                                        <asp:GridView runat="server" ID="GridMove" AutoGenerateColumns="false" class="Grid1" 
                                            GridLines="None" CaptionAlign="NotSet" Width="801px" ForeColor="Black"
                                            OnRowDeleting="GridMove_RowDeleting" OnRowDataBound="GridMove_RowDataBound">
                                            <Columns>

                                                <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="5%" />

                                              <%--  <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <span class="auto-style1">
                                                            <asp:Label ID="Label1" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="From Facility" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                      
                                                            <asp:Label ID="lblFrFacility" runat="server" Text='<%# Bind("[FromFacility]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="To Facility" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                       
                                                            <asp:Label ID="lblToFacility" runat="server" Text='<%# Bind("[ToFacility]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                                <asp:TemplateField HeaderText="Greenhouse" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                       
                                                            <asp:Label ID="lblGreenhouse" runat="server" Text='<%# Bind("[GreenHouse]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Trays" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                      
                                                            <asp:Label ID="lblTray" runat="server" Text='<%# Bind("[Trays]")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                               <div class="clearfix"></div>
                            <div class="col m6">
                                <label>Move Request Date</label>
                                <asp:TextBox ID="txtReqDate" runat="server"  TextMode="Date" ></asp:TextBox>
                                  <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReqDate" ValidationGroup="e"
                                                        SetFocusOnError="true" ErrorMessage="Please Enter Request Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                            </div>

                              <div class="col m6">
                                <label runat="server" id="lblfacsupervisor">Logistic Manager</label>
                                <%-- <h3 class="robotobold"><asp:Label ID="lblSupervisorName" runat="server" ></asp:Label></h3>--%>
                                <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                <asp:DropDownList ID="ddlLogisticManager" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                   <span class="error_message">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLogisticManager" ValidationGroup="e"
                                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Enter Request Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                            </div>

                            <div class="col m6">
                                <br />
                                <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="col m6">
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
