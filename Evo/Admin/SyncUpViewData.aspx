<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SyncUpViewData.aspx.cs" Inherits="Evo.Admin.SyncUpViewData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">View Sync Up Data</h1>

            <hr />

            <form class="admin__form" name="bench-automation" action="#">
                <div class="row">
                    <div class="col-12 mb-4">
                        <label class="mb-0">
                            <h3>Facility</h3>
                            <asp:DropDownList ID="ddlFacility" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown input__control-auto" runat="server">
                            </asp:DropDownList>
                        </label>
                    </div>
                    <div class="col-12 ">
                        <div class="data__table">
                              <asp:Label runat="server" Text="" ID="count"></asp:Label>
                            <asp:GridView ID="GridSyncUpdate" runat="server" PageSize="10" OnPageIndexChanging="GridSyncUpdate_PageIndexChanging" AllowPaging="True"
                                AutoGenerateColumns="false">
                                <Columns>

                                    <asp:TemplateField HeaderText="Bench Location" ItemStyle-Width="160px" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBatchlocation1" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="60px" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>

                                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />


                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomer" data-head="Customer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays","{0:####}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeedDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlantDueDate" runat="server" Text='<%# Eval("PlantDueDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>





                                </Columns>
                            </asp:GridView>




                        </div>
                    </div>

                </div>
            </form>
        </div>
    </div>
</asp:Content>
