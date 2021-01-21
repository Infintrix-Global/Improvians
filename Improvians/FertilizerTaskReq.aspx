<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="FertilizerTaskReq.aspx.cs" Inherits="Improvians.FertilizerTaskReq" %>

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
                                                        <asp:Label ID="Label1" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
												<asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFacility" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Put Away Location" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                                <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotTray" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label10" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Seed Lot" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label11" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSelect" runat="server" Text="Request" CssClass="bttn bttn-primary bttn-action" CommandName="Select" ></asp:Button>
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




                        <div id="userinput" runat="server" class="assign__task d-flex" visible="true">

                            <asp:Panel ID="pnlint" runat="server">
                                 <div class="row">
                                    <div class="col m3">

                                        <label>GreenHouse Supervisor</label><br />
                                        <h3 class="robotobold">
                                            </h3>
                                    </div>
                                    <div class="col m9">
                                        <label>Greenhouse </label>
                                        <asp:DropDownList ID="ddlsupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                                        <span class="error_message">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsupervisor" ValidationGroup="md"
                                                SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Supervisor" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>

                                <div class="row">
                                    <div class="col m3">
                                        <label>Type of Request</label>
                                       
                                    </div>
                                    <div class="col m9">
                                       <input type="radio" id="chemical" name="Chemical" value="chemical"><label for="chemical">Chemical</label>
									   <input type="radio" id="fertilizer" name="Fertilizer" value="Fertilizer"><label for="fertilizer">Fertilizer</label>
                                    </div>
                                </div>
                               
                            
                                    <div class="row">
                                    <div class="col m12">

                                        <asp:Panel ID="pnlPoints" runat="server" CssClass="pnlpoint">
                                            <asp:GridView runat="server" ID="GridMove" AutoGenerateColumns="false" class="Grid1"
                                                GridLines="None" CaptionAlign="NotSet" Width="801px" ForeColor="Black"
                                                ShowHeaderWhenEmpty="true">
                                                <Columns>
                                                    <%--  <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <span class="auto-style1">
                                                            <asp:Label ID="Label1" runat="server" ></asp:Label></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Fertilizer" ItemStyle-Width="10%">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblFertilizer" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QTY" ItemStyle-Width="10%">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblQTY" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Unit of Measurement" ItemStyle-Width="10%">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblUOM" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Tray" ItemStyle-Width="10%">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblTray" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
													
                                                    <asp:TemplateField HeaderText="SQFT" ItemStyle-Width="10%">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblSQFT" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="5%" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>


                                  </div>
                                            <div class="col-auto">
                                                <br />
                                                <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" runat="server"  />
                                            </div>
                                            <div class="col-auto">
                                                <br />
                                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action"  />
                                            </div>
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
