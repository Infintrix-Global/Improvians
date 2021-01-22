<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="Improvians.Admin.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main">
        <div class="site__container">
            <h2>Edit Employee</h2>
          
            <!-- BEGIN FORM-->
            <div class="filter__row d-flex">
             
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                 <div class="row justify-content-lg-center">
                     <div class="col-md-6 col-xl-5">
                        <table>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <tr id="photoupload1" runat="server">
                                        <td>Image Upload
                                    <asp:FileUpload ID="FileUpProfile" runat="server" ClientIDMode="Static" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnProfile" CssClass="bttn bttn-primary bttn-action" CausesValidation="False"
                                                runat="server" Text="Upload" OnClick="btnProfile_Click" />
                                            <asp:Label ID="Label2" runat="server" Text="(Format supported:jpeg,png,jpg)" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lblProfile" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Image :</td>
                                        <td>
                                            <asp:Image ID="ImageProfile" runat="server" Height="80px" Width="80px" GenerateEmptyAlternateText="True" ImageUrl="~/Images/no-photo.jpg" />
                                    </tr>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                         

                            <tr>
                                <td>Name :</td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Text="" Font-Bold="true" class="form-control"></asp:TextBox></td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ValidationGroup="e"
                                    ForeColor="Red" ErrorMessage="Only text is allowed" Display="Dynamic" ControlToValidate="txtName"
                                    SetFocusOnError="True" ValidationExpression="^\s*[a-zA-Z,\s]+\s*$">
                                </asp:RegularExpressionValidator>
                            </tr>
                            <tr>
                                <td>Mobile No :</td>
                                <td>
                                    <asp:TextBox ID="txtMobileNo" runat="server" Text="" Font-Bold="true"></asp:TextBox></td>
                                <asp:RequiredFieldValidator ID="RequiredMobile" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Please Enter Mobile Number" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobile" runat="server" ControlToValidate="txtMobileNo" Display="Dynamic" ErrorMessage="Please enter valid Mobile Number" ForeColor="Red" SetFocusOnError="True" ValidationExpression="[0-9]{10}" ValidationGroup="e"></asp:RegularExpressionValidator>
                            </tr>
                            <tr>
                                <td>Email ID :</td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" Text="" Font-Bold="true"></asp:TextBox></td>
                              
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="e"
                                    ForeColor="Red" ErrorMessage="Enter valid Email" Display="Dynamic" ControlToValidate="txtEmail"
                                    SetFocusOnError="True" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$">
                                </asp:RegularExpressionValidator>
                            </tr>
                            

                            <tr>
                                <td>Role:</td>
                                <td>
                                  <asp:DropDownList ID="ddlDesignation" runat="server" class="custom__dropdown robotomd" TabIndex="5"></asp:DropDownList>
                                        <span class="help-block">
                                        -
                                        </span></td>
                                         <asp:RequiredFieldValidator ID="requiredRole" runat="server" ControlToValidate="ddlDepartment" InitialValue="0" ErrorMessage="Please Select Role" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>
                            </tr>
                            <tr>
                                <td>Department:</td>
                                <td>
                                       <asp:DropDownList ID="ddlDepartment" runat="server" class="custom__dropdown robotomd" TabIndex="4"></asp:DropDownList>
                                </td>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDesignation" InitialValue="0" ErrorMessage="Please Select Role" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>
                            </tr>

                            <tr >
                                <td>UserName:</td>
                                <td>
                                    <asp:TextBox ID="txtUserName" runat="server" Text="" Font-Bold="true" Enabled="false"></asp:TextBox></td>
                            </tr>
                           
                            <tr >
                                <td>Password:</td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" Text="" Font-Bold="true"></asp:TextBox></td>
                            </tr>
                       

                        </table>

                    </div>
                
                <br />
                <div class="clearfix"></div>
              
                <div class="col-12 text-center order-md-1">
                        <asp:Button runat="server" ID="btnUpdate"  CssClass="bttn bttn-primary bttn-action" ClientIDMode="Static" OnClick="btnUpdate_Click" Text="Update" ValidationGroup="e" />
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
