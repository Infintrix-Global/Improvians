<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Customer/CustomerMaster.Master" CodeBehind="ContactSalesRepresentative.aspx.cs" Inherits="Evo.Customer.ContactSalesRepresentative" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="main">
            <div class="site__container">
                <h2 class="text-center pt-4">Contact Our Sales Representative</h2>

                <div class="row justify-content-center mt-5 mb-5">
                    <div class="col-12 col-sm-10 col-md-9 col-lg-7 col-xl-6">
                        <div class="card card-respresentative mb-3">
                            <div class="row no-gutters">
                                <div class="col-12 col-sm-auto">
                                    <img runat="server" id="ImageProfile" class="img-fluid" src="images/support-passport-photo.jpg" alt="Support Photo" width="406" height="462">
                                </div>

                                <div class="col-12 col-sm">
                                    <div class="card-body">
                                        <h3 class="card-title"><asp:Label runat="server" ID="lblName"></asp:Label></h3>
                                        <p class="card-text">This is <asp:Label runat="server" ID="lblName1" /> from Growers Transplanting Team, let me know how may I assist you.</p>
                                        <div class="sales__contact">
                                            <p class="mb-1">
                                                <a class="bttn-link" runat="server" id="lnkPhone" href="tel:+14564564565">
                                                    <span><i class="fas fa-phone-alt pr-1"></i></span>
                                                    <span><asp:Label runat="server" ID="lblPhone" /></span>
                                                </a>
                                            </p>
                                            <p>
                                                <a class="bttn-link" runat="server" id="lnkEmail" href="mailto:janedoe@growerstransplanting.com">
                                                    <span><i class="fas fa-envelope pr-1"></i></span>
                                                    <span><asp:Label runat="server" ID="lblEmail" /></span>
                                                </a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="sales__message" class="sales__message">
                            <label class='d-block robotomd'>Message:</label>
                            <textarea required class="w-100 input__control mb-3"></textarea>
                            <button type="submit" class="bttn bttn-primary">Send Mail</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
