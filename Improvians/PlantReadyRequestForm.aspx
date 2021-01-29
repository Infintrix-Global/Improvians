<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="PlantReadyRequestForm.aspx.cs" Inherits="Improvians.PlantReadyRequestForm" %>

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

            <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>
            <div class="data__table">
                <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                    GridLines="None" OnRowCommand="gvPlantReady_RowCommand"
                    ShowHeaderWhenEmpty="True" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>

                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("JobID")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblPutAwayMainLocation" runat="server" Text='<%# Eval("PutAwayMainLocation")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Put Away Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblPutAwayLocation" runat="server" Text='<%# Eval("PutAwayLocation")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblTray" runat="server" Text='<%# Eval("#Tray")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblSeedLots" runat="server" Text='<%# Eval("SeedLots")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:dd MMM yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                          <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblPlantType" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Plant Height" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                               <asp:Label ID="lblPlantHeight" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Plant Ready Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                               <asp:Label ID="lblPlantReadyDate" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>
                                <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Container.DataItemIndex  %>'></asp:Button>

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
                            <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                                <label class="d-block">Assign task to Greenhouse Supervisor</label>

                                <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
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
