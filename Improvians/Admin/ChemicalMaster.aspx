<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ChemicalMaster.aspx.cs" Inherits="Evo.Admin.ChemicalMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Chemical Method Master</h1>

            <hr />


            <asp:Panel ID="pnlAdd" runat="server" Visible="false">
                  
                     <div class="filter__row d-flex">
             <div class="row">
                    <div class="col m3">
                        
                            <label>Chemical Method Name<span style="color: red">*</span></label>
                            <asp:TextBox ID="txtName" class="input__control" placeholder="Enter Chemical name" runat="server"></asp:TextBox>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Name" ForeColor="Red"></asp:RequiredFieldValidator>
                              
                            </span>
                        

                     

                        <div class="col-12 text-center order-md-1">
                            <asp:Button ID="btAdd" runat="server" Text="Submit" TabIndex="10" class="submit-bttn bttn bttn-primary"  OnClick="btAdd_Click" ValidationGroup="e" />

                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" class="submit-bttn bttn bttn-primary" ClientIDMode="Static" onclick="btnCancel_Click"  />
                        </div>
                         
                    </div>
                </div></div>
            </asp:Panel>

            <asp:Panel ID="pnlList" runat="server" Visible="true">
                <div class="filter__row d-flex">
                    <div class="row">
                        <div class="col m3">
                            <label>Chemical Method Name </label>
                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="input__control "></asp:TextBox>
                        </div>



                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnSearch" Text="Search" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnClear" Text="Clear" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnClear_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnAddFertilizer" Text="Add" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnAddFertilizer_Click" />
                        </div>
                    </div>
                </div>
                </asp:Panel>
                <div class="filter__row d-flex">
                    <div class="row justify-content-lg-center">
                        <div class=" col m12">
                            <div class="portlet light ">
                                <asp:Label runat="server" Text="" ID="count"></asp:Label>
                                <div class="portlet-body">
                                    <div class="data__table">
                                        <asp:GridView ID="gvChemical" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            class="striped" OnRowCommand="GridEmployee_RowCommand" AllowSorting="true" OnPageIndexChanging="GridEmployee_PageIndexChanging"
                                            GridLines="None" OnSorting="GridEmployee_Sorting"
                                            ShowHeaderWhenEmpty="True" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="40%" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Chemical Method Name" ItemStyle-Width="60%" HeaderStyle-CssClass="autostyle2" SortExpression="ChemicalName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("ChemicalName")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                                    <ItemTemplate>
                                                        <div class="d-flex align-items-center">
                                                            <asp:ImageButton class="mr-2" ID="imgEdit" runat="server" CommandArgument='<%# Eval("ID")  %>' CommandName="EditProfile" ImageUrl="~/Admin/images/edit.png" AlternateText="edit" ToolTip="edit"></asp:ImageButton>
                                                            <asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<%# Eval("ID")  %>' CommandName="RemoveProfile" ImageUrl="~/Admin/images/delete.png" AlternateText="delete" ToolTip="delete" OnClientClick="return confirm('Are you sure you want to remove this chemical?');"></asp:ImageButton>
                                                        </div>
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
