﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="DumpRequestForm.aspx.cs" Inherits="Evo.DumpRequestForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main__header">
        <div class="site__container">
            <h2 class="head__title-icon">
                <img src="./images/dashboard_dump-request.png" width="137" height="132" alt="Plant Ready">
               Dump </h2>

            <div class="filter__row d-flex">
                <div class="row">
                    <div class="col m3">
                        <label>Customer </label>
                        <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                                       
                    <div class="col m3">
                        <label>Job No </label>
                        <asp:DropDownList ID="ddlJobNo" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                      <div class="col-auto">
                         <br />
                                <asp:Button Text="Reset" ID="btnResetSearch" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                            
                    </div>

                    <%-- <div class="col m3">
                    
                    </div>--%>
                     
                </div>
            </div>

            <%-- <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>--%>

            <div class="data__table">
                <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                    GridLines="None" OnRowCommand="gvPlantReady_RowCommand"  DataKeyNames="wo,DumpId,jid"
                    ShowHeaderWhenEmpty="True" Width="100%">
                    <Columns>

                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>

                                <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Labeitemno" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <%--<asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>


                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>

                                 <asp:Button ID="btnStart" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action" CommandName="StartDump" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

                                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
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
                      
                        <div class="row">
                                <div class="mb-3 col-12 col-md-auto">
                                    <label>Assignment </label>
                                    <asp:HiddenField ID="HiddenFieldJid" runat="server" />
                                    <asp:HiddenField ID="HiddenFieldDid" runat="server" />
                                    <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                                    <asp:DropDownList ID="ddlDumptAssignment" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

                                </div>

                                <div class="mb-3 col-12 col-md-auto">
                                    <label class="d-block">Dump Date</label>
                                    <asp:TextBox ID="txtDumpDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>

                                <div class="mb-3 col-12 col-md-auto">

                                    <label>Quantity of Tray </label>
                                    <asp:TextBox ID="txtQuantityofTray" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>

                                </div>

                                <div class="mb-3 col-12 col-md-auto">

                                    <label>Comments </label>

                                    <asp:TextBox ID="txtCommentsDump" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>


                                </div>
                                <div class="mb-3 col-12 col-md-auto align-self-end">
                                    <asp:Button Text="Submit" ID="btnDumpSumbit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />

                                    <asp:Button Text="Reset" ID="btnDumpReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />

                                </div>
                            </div>

                    </div>
                </div>

            </div>

        </div>
    </div>
</asp:Content>