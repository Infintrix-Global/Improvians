<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AutomationBenchControls.aspx.cs" Inherits="Evo.Admin.AutomationBenchControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Benches Automated</h1>

            <hr />

            <form class="admin__form" name="bench-automation" action="#">
                <div class="row">
                    <div class="col-12 mb-4">
                        <label class="mb-0">
                            <h3>Put Away Facility</h3>
                            <asp:DropDownList ID="ddlFacility" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown input__control-auto" runat="server">
                            </asp:DropDownList>
                        </label>
                    </div>
                    <div class="col-12 col-lg-6 col-xl-5">
                        <div class="data__table">

                            <asp:GridView ID="GridBanchLocation" runat="server" PageSize="10" OnPageIndexChanging="GridBanchLocation_PageIndexChanging" AllowPaging="True" 
                                OnRowDataBound ="GridBanchLocation_RowDataBound"
                                AutoGenerateColumns="false">
                                <Columns>

                                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-Width="300px">
                                        <ItemTemplate>

                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("BenchName")%>'></asp:Label>
                                            <asp:Label ID="lblloc_seedline" Visible="false" runat="server" Text='<%# Eval("Facility")%>'></asp:Label>
                                            <asp:Label ID="lblAutomationBenchControlsId"  Visible="false" runat="server" Text='<%# Eval("AutomationBenchControlsId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bench Process Flow" HeaderStyle-Width="300px">
                                        <ItemTemplate>

                                            <asp:DropDownList ID="ddlMain" OnSelectedIndexChanged="ddlMain_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown" runat="server">
                                                <asp:ListItem Text="Manual" Value="Manual" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Auto" Value="Auto"></asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:Label ID="lblAutomation" Visible="false" runat="server" Text='<%# Eval("Automation")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>




                        </div>
                    </div>
                    <div class="row mt-4 align-items-end">

                        <div class="col-12 col-md-auto col-lg-4 mb-3">
                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" Visible="false"  ValidationGroup="e" OnClick="btnSubmit_Click" runat="server" />

                        </div>

                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
