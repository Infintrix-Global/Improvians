<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PullData.aspx.cs" Inherits="Evo.Admin.PullData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Reset and Pull Data</h1>

            <hr />


            <asp:Panel ID="pnlAdd" runat="server">


                <div class="filter__row d-flex">
                    <div class="row">





                        <div class="col-12 text-center order-md-1">
                            <asp:Button ID="btAdd" runat="server" Text="Reset and Pull Data" class="submit-bttn bttn bttn-primary" OnClick="btAdd_Click" />

                            <asp:Button ID="btnPullData" runat="server" Text="Sync up Data" class="submit-bttn bttn bttn-primary" OnClick="btnPullData_Click" />

                            <asp:Button ID="Button1" runat="server" Text="Pull Data Test" Visible="false" class="submit-bttn bttn bttn-primary" OnClick="Button1_Click" />

                        </div>

                    </div>


                </div>
            </asp:Panel>










        </div>
        <div class="filter__row d-flex">
            <div class="row justify-content-lg-center">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="count"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvSyncUpData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" OnRowCommand="gvSyncUpData_RowCommand" DataKeyNames="CreateDate"
                                    GridLines="None" PageSize="10" OnPageIndexChanging="gvSyncUpData_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="Job No."  HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Create Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Label ID="lblSeededDate1" runat="server" Text='<%# Eval("CreateDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="View Sync up Data" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>

                                                <asp:Button ID="btnSelect" runat="server" Text="View Sync up Data" CssClass="bttn bttn-primary bttn-action w-auto" CommandName="Job" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
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
        </div>
    </div>
    </div>
</asp:Content>
