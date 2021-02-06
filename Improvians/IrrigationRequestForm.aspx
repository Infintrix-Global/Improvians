﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="IrrigationRequestForm.aspx.cs" Inherits="Improvians.IrrigationRequestForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="main main__header">
        <div class="site__container">
            <h2>Plant Ready Request</h2>

            <div class="filter__row row">
                <div class="col-xl-auto col-12">
                    <label>Job No.</label>

                    <asp:DropDownList ID="ddlJobNo" runat="server" class="w-100 filter__control filter__select custom__dropdown"></asp:DropDownList>
                </div>

                <div class="col-xl-auto col-12">
                    <label>Customer Name</label>

                    <asp:DropDownList ID="ddlCustomer" runat="server" class="w-100 filter__control filter__select custom__dropdown"></asp:DropDownList>
                </div>

                <div class="col-xl-auto col-12">
                    <label>Facility Defaults</label>

                    <asp:DropDownList ID="ddlFacility" runat="server" class="w-100 filter__control filter__select custom__dropdown"></asp:DropDownList>
                </div>
            </div>

            <%-- <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>--%>

            <div class="data__table">
                <asp:GridView ID="GridIrrigation" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="GridIrrigation_PageIndexChanging"
                    GridLines="None" OnRowCommand="GridIrrigation_RowCommand"
                    ShowHeaderWhenEmpty="True" Width="100%">
                    <Columns>

                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>

                                <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbljobID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Labeitemno" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("trays_actual")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SoDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>


                                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                                <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                                <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Eval("wo")  %>'></asp:Button>

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

            <div class="text-left dashboard__block my-4">

                <div id="userinput" runat="server" visible="false" class="row justify-content-center">
                    <div class="col-12">
                        <h3>User Inputs:</h3>
                        <div class="row">

                            <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                                <label>Job No.</label><br />
                                <h3 class="robotobold">
                                    <asp:Label ID="lblJobID" runat="server"></asp:Label></h3>
                            </div>

                        </div>


                        <div class="row">

                            <div class="col-12 col-sm-6 col-lg-4">
                                <label>Select Greenhouse Supervisor</label>
                                <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                            </div>

                            <div class="col-12 col-sm-6 col-lg-3">
                                <label>No. Of Trays to be Irrigated</label>
                               
                                <asp:TextBox ID="txtIrrigatedNoTrays" class="input__control" placeholder="Enter No." runat="server"></asp:TextBox>

                            </div>

                            <div class="col-12 col-sm-6 col-md-auto">
                                <label class="pr-2 pr-lg-0 d-lg-block">Water Required</label>
                          
                                 <asp:RadioButtonList ID="RadioButtonWaterRequired" v runat="server" RepeatDirection="Horizontal" Width="200px">
                                     <asp:ListItem Text="Yes" Value="Yes" Selected="True">
                                     </asp:ListItem>
                                        <asp:ListItem Text="No" Value="No" Selected="True">
                                     </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-12  col-sm-6 col-md-auto">
                                <label class="pr-2 pr-lg-0 d-lg-block">Irrigation Duration</label>
                               
                                <asp:TextBox ID="txtIrrigationDuration" class="mb-0 input__control input__control-auto" placeholder="00:00" runat="server"></asp:TextBox>

                            </div>
                        </div>

                        <div class="row align-items-center mt-sm-3">
                            <div class="col-12 mt-4 mb-3 my-sm-0 col-sm-auto">
                                <h4 class="mb-0">Schedule:</h4>
                            </div>
                            <div class="col-auto">
                                <label class="d-block">Spray Date</label>
                               
                                <asp:TextBox ID="txtSprayDate" class="input__control input__control-auto" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-auto">
                                <label class="d-block">Spray Time</label>
                             
                                <asp:TextBox ID="txtSprayTime" TextMode="Time" class="input__control input__control-auto" placeholder="00:00" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center mt-sm-3">
                            <div class="col-12 col-sm-6 col-lg-4">
                              
                                <asp:TextBox ID="txtNotes" class="w-100 input__control" placeholder="Notes" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-12 my-3">
                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />

                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />

                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>
    </div>
</asp:Content>
