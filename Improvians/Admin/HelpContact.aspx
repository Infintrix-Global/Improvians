<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpContact.aspx.cs" MasterPageFile="~/Admin/AdminMaster.Master" Inherits="Evo.Admin.HelpContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid admin__form">
        <h1 class="text-center text-sm-left">Contact</h1>

        <hr />
        <div class="row">
            <asp:Label ID="lblmsg" runat="server" CssClass="col-md-6 col-xl-5 order-md-1"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-6 col-xl-5 order-md-1">
                <label class="custom-file-label text-center mb-3 mb-md-0">
                    <h3>Contact Image</h3>

                    <div class="profile-picture">
                        <asp:Image ID="ImageProfile" runat="server" ImageUrl="images/support-passport-photo.png" alt="" Style="height: 200px; width: 200px;" />
                    </div>

                    <asp:FileUpload ID="FileUpProfile" class="custom-file-input" runat="server" ClientIDMode="Static" />

                    <span class="custom-file-bttn bttn bttn-primary">
                        <i class="fas fa-upload"></i>
                        <span class="ml-2">Choose a file…</span>
                    </span>

                    <asp:Button ID="btnProfile" CssClass="bttn bttn-primary bttn-action mb-2" CausesValidation="False"
                                runat="server" Text="Upload" OnClick="btnProfile_Click" />
                    <span class="d-block" id="ContentPlaceHolder1_Label2" style="color: Red;">(Format supported:jpeg,png,jpg)</span>
                    <asp:Label ID="lblProfile" runat="server" Visible="true"></asp:Label>
                </label>
            </div>

            <div class="col-md-6 col-xl-5">
                <label class="mb-3">
                    <h3>Name</h3>
                    <asp:TextBox ID="txtName" class="input__control input__control-icon username" placeholder="Enter your employee name" TabIndex="3" runat="server"></asp:TextBox>
                    <span class="error_message">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName" ValidationGroup="e"
                            SetFocusOnError="true" ErrorMessage="Please Enter Name" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                </label>

                <label class="mb-3">
                    <h3>Email</h3>
                    <asp:TextBox ID="txtEmail" class="input__control input__control-icon email" placeholder="Enter your email here" TabIndex="4" runat="server"></asp:TextBox>
                    <asp:Label ID="lblStatus" runat="server" ForeColor="red"></asp:Label>
                    <span class="error_message">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ValidationGroup="e"
                            SetFocusOnError="true" ErrorMessage="Please Enter Email" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="e"
                            ForeColor="Red" ErrorMessage="Enter valid Email" Display="Dynamic" ControlToValidate="txtEmail"
                            SetFocusOnError="True" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$">
                        </asp:RegularExpressionValidator>
                    </span>
                </label>

                <label class="mb-0 mb-md-3">
                    <h3>Phone</h3>
                    <asp:TextBox ID="txtMobile" runat="server" class="input__control input__control-icon phone" placeholder="Enter your mobile No. here" TabIndex="2"></asp:TextBox>

                    <span class="error_message">
                        <%--  <asp:RequiredFieldValidator ID="RequiredMobile" runat="server" ControlToValidate="txtMobile" ErrorMessage="Please Enter Mobile Number" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobile" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="Please enter valid Mobile Number" ForeColor="Red" SetFocusOnError="True" ValidationExpression="[0-9]{10}" ValidationGroup="e"></asp:RegularExpressionValidator>
                    </span>
                </label>
            </div>

            <div class="col-12 order-md-1">
                <asp:Button ID="btnAdd" runat="server" OnClick="btnUpdate_Click" Text="Submit" CssClass="submit-bttn bttn bttn-primary" />

            </div>
        </div>
    </div>
</asp:Content>
