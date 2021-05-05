<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpDocument.aspx.cs" MasterPageFile="~/Admin/AdminMaster.Master" Inherits="Evo.Admin.HelpDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Help Documents</h1>
            <hr />
            <div class="admin__form">
                <div class="row">
                    <asp:Label ID="lblmsg" runat="server" CssClass="col-md-6 col-xl-5 order-md-1"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-12 col-md-8 col-xl-6">
                        <label class="mb-0">
                            <h3>Document Title (Description)</h3>
                        </label>
                        <asp:TextBox ID="txtName" class="input__control" placeholder="Enter Document Title" TabIndex="3" runat="server"></asp:TextBox>
                        <span class="error_message">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName" ValidationGroup="e"
                                SetFocusOnError="true" ErrorMessage="Please Enter Document Title" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </span>
                        <label class="mb-0 mt-4">
                            <h3>Add Video Document</h3>
                        </label>

                        <div class="d-flex flex-wrap align-items-center faq__addfile">
                            <div class="faq__upload">
                                <label class="mb-0">
                                    <asp:FileUpload id="VideoUpload" class="custom-file-input" runat="server" />

                                    <span class="custom-file-bttn bttn bttn-sm bttn-primary">
                                        <i class="fas fa-upload"></i>
                                        <span class="ml-2">Select file</span>
                                    </span>
                                     <asp:Button ID="btnVideoUpload" CssClass="bttn bttn-primary bttn-action mb-2" CausesValidation="False"
                                runat="server" Text="Upload" OnClick="btnVideoUpload_Click" />
                                </label>
                            </div>
                            <span class="px-2">OR</span>
                            <div class="faq__link">
                                <asp:TextBox ID="txtVideoLink" class="input__control" placeholder="Enter Video Link" TabIndex="4" runat="server"></asp:TextBox>

                            </div>
                        </div>

                        <label class="mb-0 mt-4">
                            <h3>Add Presentation Document</h3>
                        </label>

                        <div class="d-flex flex-wrap align-items-center faq__addfile">
                            <div class="faq__upload">
                                <label class="mb-0">
                                    <asp:FileUpload id="DocumentUpload" class="custom-file-input" runat="server" />
                                    
                                    <span class="custom-file-bttn bttn bttn-sm bttn-primary">
                                        <i class="fas fa-upload"></i>
                                        <span class="ml-2">Select file</span>
                                    </span>
                                    <asp:Button ID="btnProfile" CssClass="bttn bttn-primary bttn-action mb-2" CausesValidation="False"
                                runat="server" Text="Upload" OnClick="btnProfile_Click" />
                                </label>
                            </div>
                            <span class="px-2">OR</span>
                            <div class="faq__link">
                                <asp:TextBox ID="txtDocumentLink" class="input__control" placeholder="Enter Document Link" TabIndex="4" runat="server"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Button ID="btnAdd" runat="server" onClick="btnAdd_Click" Text="Submit" ValidationGroup="e" CssClass="submit-bttn bttn bttn-primary" />
                    </div>

                    <div class="col-12 mt-5">
                        <div class="data__table data__table--faq">
                            <asp:GridView ID="gvDocument" runat="server" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" OnRowCommand="gvDocument_RowCommand" OnRowDataBound="gvDocument_RowDataBound"
                                GridLines="None" ShowHeaderWhenEmpty="True" Width="100%">
                                <columns>

                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="60px" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Document Title"  HeaderStyle-CssClass="autostyle2" >
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("Title")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                               <asp:TemplateField HeaderText="Document Type" ItemStyle-Width="180px" HeaderStyle-CssClass="autostyle2" >
                                <ItemTemplate>
                                     <span class="d-flex align-items-center justify-content-center">
                                            <asp:HyperLink ID="lnkVideo" runat="server" Text="View Video" NavigateUrl='<%# Eval("VideoLink")  %>' class="help__list-link help__list-video mr-2">
                                                <img src="images/icons/icon-video.png" width="84" height="64" />
                                            </asp:HyperLink>

                                            <asp:HyperLink ID="lnkDocument" runat="server" Text="View Presentation" NavigateUrl='<%# Eval("DocumentLink")  %>' class="help__list-link help__list-ppt">
                                                <img src="images/icons/icon-ppt.svg" width="36" height="36">
                                            </asp:HyperLink>
                                        </span>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="autostyle2">
                                <ItemTemplate>
                                    <div class="d-flex align-items-center">
                                        <asp:ImageButton class="mr-2" ID="imgEdit" runat="server" CommandArgument='<%# Eval("ID")  %>' CommandName="EditProfile" ImageUrl="~/Admin/images/edit.png" AlternateText="edit" ToolTip="edit"></asp:ImageButton>
                                        <asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<%# Eval("ID")  %>' CommandName="RemoveProfile" ImageUrl="~/Admin/images/delete.png" AlternateText="delete" ToolTip="delete" OnClientClick="return confirm('Are you sure you want to remove this Document?');"></asp:ImageButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>

                                <pagerstyle cssclass="paging" horizontalalign="Right" />
                                <pagersettings mode="NumericFirstLast" />
                                <emptydatatemplate>
                            No Record Available
                        </emptydatatemplate>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
