<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpFAQ.aspx.cs" MasterPageFile="~/Admin/AdminMaster.Master" Inherits="Evo.Admin.HelpFAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid admin__form">
        <h1 class="text-center text-sm-left">Help FAQ's</h1>

        <hr />
        <div class="row">
            <asp:Label ID="lblmsg" runat="server" CssClass="col-md-6 col-xl-5 order-md-1"></asp:Label>
        </div>
        <div class="row">

            <div class="col-12 col-md-6 col-xl-5">
                <label class="mb-0">
                    <h3>FAQ Title</h3>
                </label>
                <asp:TextBox ID="txtName" class="input__control" placeholder="Enter FAQ Title" TabIndex="3" runat="server"></asp:TextBox>
                <span class="error_message">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName" ValidationGroup="e"
                        SetFocusOnError="true" ErrorMessage="Please Enter FAQ title" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </span>
                <label class="mb-0 mt-3">
                    <h3>FAQ Description</h3>
                </label>
                <asp:TextBox ID="txtDescription" class="input__textarea input__control" TextMode="MultiLine" placeholder="Enter FAQ Description" TabIndex="4" runat="server"></asp:TextBox>
                <span class="error_message">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription" ValidationGroup="e"
                        SetFocusOnError="true" ErrorMessage="Please Enter FAQ Description" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </span>
            </div>

            <div class="col-12">
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Submit" CssClass="submit-bttn bttn bttn-primary" />
            </div>

            <div class="col-12 mt-5">
                <div class="data__table data__table--faq">
                    <asp:GridView ID="gvFAQ" runat="server" AutoGenerateColumns="False"
                        class="striped" OnRowCommand="gvFAQ_RowCommand" AllowSorting="true"
                        GridLines="None" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="60px" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FAQ Title" ItemStyle-Width="30%" HeaderStyle-CssClass="autostyle2" SortExpression="Title">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("Title")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FAQ Description" ItemStyle-Width="60%" HeaderStyle-CssClass="autostyle2" SortExpression="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblFCode" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <div class="d-flex align-items-center">
                                        <asp:ImageButton class="mr-2" ID="imgEdit" runat="server" CommandArgument='<%# Eval("ID")  %>' CommandName="EditProfile" ImageUrl="~/Admin/images/edit.png" AlternateText="edit" ToolTip="edit"></asp:ImageButton>
                                        <asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<%# Eval("ID")  %>' CommandName="RemoveProfile" ImageUrl="~/Admin/images/delete.png" AlternateText="delete" ToolTip="delete" OnClientClick="return confirm('Are you sure you want to remove this FAQ?');"></asp:ImageButton>
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
</asp:Content>
