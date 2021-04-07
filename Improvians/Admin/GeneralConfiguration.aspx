<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GeneralConfiguration.aspx.cs" Inherits="Evo.Admin.GeneralConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">General Configuration</h1>

            <hr />
            <!-- BEGIN FORM-->
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <div class="card_grid">
                <div class="row">
                    <div class="col-12 col-lg-5 col-xl-6">
                        <div class="card mb-3">
                            <div class="card-header">
                                <h4 class="mb-0">Germination Timeframe</h4>
                            </div>
                            <div class="card-body">
                                <div class="form-group">
                                    <label class="mr-2 d-block robotomd">First Germination</label>
                                    <asp:TextBox ID="txtGerm1" class="input__control input__control-auto input__control-sm text-center mr-2" Text="8" Width="50px" runat="server"></asp:TextBox>
                                    <span>days from seeding</span>
                                </div>

                                <div class="form-group">
                                    <label class="mr-2 d-block robotomd">Second Germination</label>
                                    <asp:TextBox ID="txtGerm2" class="input__control input__control-auto input__control-sm text-center mr-2" Text="15" Width="50px" runat="server"></asp:TextBox>
                                    <span>days from seeding</span>
                                </div>

                                <div class="form-group">
                                    <label class="mr-2 d-block robotomd">Third Germination</label>
                                    <asp:TextBox ID="txtGerm3" class="input__control input__control-auto input__control-sm text-center mr-2" Text="0" Width="50px" runat="server"></asp:TextBox>
                                    <span>days from seeding</span>
                                </div>

                                <asp:Button ID="btnUpdateConfig" runat="server" OnClick="ButtonUpdateConfig_Click" CausesValidation="true" ValidationGroup="e"
                                    Text="Update" CssClass="bttn bttn-sm mt-3" />

                            </div>
                        </div>
                    </div>

                    <div class="col-12 col-lg-7 col-xl-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="data__table  data__table-500">
                                    <asp:GridView ID="GridProfile" runat="server" ShowFooter="true" Width="100%" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Crop">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCrop" Text='<%# Eval("Crop")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Germination 1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGerm1" class="input__control input__control-auto input__control-sm text-center" Text='<%# Eval("Germination1")%>' Width="50px" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Germination 2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGerm2" class="input__control input__control-auto input__control-sm text-center" Text='<%# Eval("Germination2")%>' Width="50px" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Germination 3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGerm3" class="input__control input__control-auto input__control-sm text-center" Text='<%# Eval("Germination3")%>' Width="50px" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>


                                    </asp:GridView>

                                </div>
                                <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" CausesValidation="true" ValidationGroup="e"
                                    Text="Update" CssClass="bttn bttn-sm mt-3" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
