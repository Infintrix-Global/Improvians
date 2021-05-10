<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditCustomer.aspx.cs" Inherits="Evo.Admin.EditCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Edit Customer</h1>

            <hr />
            <!-- BEGIN FORM-->
            <div class="admin__form">




                <asp:Label ID="lblmsg" runat="server"></asp:Label>

                <div class="row justify-content-lg-center">

                    <div class="col-md-6 col-xl-5 order-md-1">

                        <label class="custom-file-label text-center">
                            <h3>Profile Image</h3>

                            <div class="profile-picture">
                                <asp:Image ID="ImageProfile" runat="server" Height="200px" Width="200px" GenerateEmptyAlternateText="True" ImageUrl="~/Admin/Images/no-photo.jpg" />
                            </div>

                            <asp:FileUpload ID="FileUpProfile" class="custom-file-input" runat="server" ClientIDMode="Static" />

                            <span class="custom-file-bttn bttn bttn-primary">
                                <i class="fas fa-upload"></i>
                                <span class="ml-2">Choose a file…</span>
                            </span>

                            <asp:Button ID="btnProfile" CssClass="bttn bttn-primary bttn-action mb-2" CausesValidation="False"
                                runat="server" Text="Upload" OnClick="btnProfile_Click" />
                            <asp:Label ID="Label2" runat="server" Text="(Format supported:jpeg,png,jpg)" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblProfile" runat="server" Visible="true"></asp:Label>
                            <div class="clearfix"></div>

                        </label>

                    </div>




                    <div class="col-md-6 col-xl-5">
                        <label>
                            <h3>Customer Name<span style="color: red">*</span></h3>
                            <asp:TextBox ID="txtName" class="input__control input__control-icon username" placeholder="Enter your user name" TabIndex="3" runat="server"></asp:TextBox>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ValidationGroup="e"
                                                ForeColor="Red" ErrorMessage="Only text is allowed" Display="Dynamic" ControlToValidate="txtName"
                                                SetFocusOnError="True" ValidationExpression="^\s*[a-zA-Z,\s]+\s*$">
                                            </asp:RegularExpressionValidator>--%>
                            </span>
                        </label>

                        <label>
                            <h3>Email ID<span style="color: red">*</span></h3>
                            <asp:TextBox ID="txtEmail" class="input__control input__control-icon email" placeholder="Enter your email here" TabIndex="4" runat="server"></asp:TextBox>
                            <asp:Label ID="lblStatus" runat="server" ForeColor="red"></asp:Label>
                            <%-- <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Email" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="e"
                                    ForeColor="Red" ErrorMessage="Enter valid Email" Display="Dynamic" ControlToValidate="txtEmail"
                                    SetFocusOnError="True" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$">
                                </asp:RegularExpressionValidator>
                            </span>--%>
                        </label>
                        <label>
                            <h3>Mobile Number</h3>

                            <asp:TextBox ID="txtMobile" runat="server" class="input__control input__control-icon phone" placeholder="Enter your Mobile No. here" TabIndex="2"></asp:TextBox>

                            <span class="error_message">
                                <%--  <asp:RequiredFieldValidator ID="RequiredMobile" runat="server" ControlToValidate="txtMobile" ErrorMessage="Please Enter Mobile Number" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobile" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="Please enter valid Mobile Number" ForeColor="Red" SetFocusOnError="True" ValidationExpression="[0-9]{10}" ValidationGroup="e"></asp:RegularExpressionValidator>
                            </span>
                        </label>
                    </div>
                    <div class="col-md-6 col-xl-5 order-md-1">
                        <label>
                            <h3>User Name<span style="color: red">*</span></h3>
                            <asp:TextBox ID="txtUserName" class="input__control input__control-icon " placeholder="Enter your user name here" TabIndex="5" runat="server"></asp:TextBox>

                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please Enter UserName" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>

                            </span>
                        </label>
                    </div>


                    <div class="col-md-6 col-xl-5 order-md-1">
                        <label>
                            <h3>Password<span style="color: red">*</span></h3>
                            <asp:TextBox ID="txtPassword" class="input__control input__control-icon " placeholder="Enter your passord here" TabIndex="6" runat="server"></asp:TextBox>
                            <span class="error_message">-    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please Enter Password" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please use one upper case letter, one lower case letter and one number"
                                    ValidationExpression="^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$" ForeColor="Red" ControlToValidate="txtPassword" />
                            </span>
                        </label>
                    </div>

                    <div class="col-md-6 col-xl-5 order-md-1">
                        <label>
                            <h3>Navision Customer ID</h3>
                            <asp:TextBox ID="txtNavisionID" runat="server" class="input__control input__control-icon" TabIndex="7"></asp:TextBox>
                            <span class="error_message">-    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNavisionID" InitialValue="0" ErrorMessage="Please Select Designation" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>
                            </span>
                        </label>
                    </div>
                    <div class="col-md-6 col-xl-5 order-md-1">
                        <label>
                            <h3>Sales Representative</h3>
                            <asp:DropDownList ID="ddlSales" runat="server" class="custom__dropdown robotomd" TabIndex="7"></asp:DropDownList>
                            <span class="error_message">-    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSales" InitialValue="0" ErrorMessage="Please Select Sales Representative" ForeColor="Red" SetFocusOnError="true" ValidationGroup="e"></asp:RequiredFieldValidator>
                            </span>
                        </label>
                    </div>


                    <div class="clearfix"></div>

                    <div class="col-12 text-center order-md-1">

                        <asp:Button runat="server" ID="btnUpdate" CssClass="bttn bttn-primary bttn-action" ClientIDMode="Static" OnClick="btnUpdate_Click" Text="Update" ValidationGroup="e" />



                    </div>
                    <%-- <div class="col m6">
                                <div class="form-group form-md-line-input ">

                                    <asp:Button ID="btclear" runat="server" Text="Clear" TabIndex="11" CssClass="bttn bttn-primary bttn-action" ClientIDMode="Static" OnClick="btclear_Click" />

                                </div>
                            </div>--%>


                    <div class="clearfix"></div>

                    <br />
                    <br />
                    <div class="clearfix"></div>



                </div>


            </div>
        </div>
    </div>
</asp:Content>
