<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" MasterPageFile="~/EvoMaster.Master" Inherits="Evo.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main main__header">
        <div class="site__container">
            <h2 class="text-center">Change Password</h2>
            <div class="print__reports text-center mt-5">
                <div class="card d-inline-block">
                    <div class="card-body py-5">
                        <div class="row text-left">
                            <div class="col-auto">
                                <asp:Label ID="lblmsg" runat="server" Font-Bold="True"
                                    ForeColor="Red" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row text-left">
                            <div class="col-12">
                                <label class="d-block">
                                    <h3>Old Password</h3>
                                    <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" placeholder="Old Password" class="input__control input__control-icon password w-100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Old Password" ForeColor="Red"></asp:RequiredFieldValidator>
                                </label>
                            </div>
                        </div>
                        <div class="row text-left">
                            <div class="col-12">
                                <label>
                                    <h3>New Password</h3>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="New Password" CssClass="input__control input__control-icon password" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                        SetFocusOnError="true" ErrorMessage="Please Enter New Password" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please use one upper case letter, one lower case letter and one number"
                                        ValidationExpression="^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$" ForeColor="Red" ControlToValidate="txtPassword" />
                                </label>
                            </div>
                        </div>
                        <div class="row text-left">
                            <div class="col-auto">
                                <label>
                                    <h3>Confirm Password</h3>
                                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" placeholder="Confirm Password" CssClass="input__control input__control-icon password" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Confirm Password" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please use one upper case letter, one lower case letter and one number"
                                        ValidationExpression="^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$" ForeColor="Red" ControlToValidate="txtConfirmPassword" />
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Password and confirm password does not match">
                                    </asp:CompareValidator>
                                </label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnChange" class="bttn bttn-primary bttn-action" runat="server" Text="Change Password" OnClick="btnChange_Click" />
                            </div>
                            <div class="col-auto">
                                <asp:Button Text="Cancel" ID="btnReset" CssClass="bttn bttn-primary bttn-action" runat="server" OnClientClick="history.back();" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
