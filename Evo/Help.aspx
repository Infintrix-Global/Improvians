<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" MasterPageFile="~/EvoMaster.Master" Inherits="Evo.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main main__header">
        <div class="site__container">

            <section class="section__help help__reources">
                <h2 class="h2 robotobold">Training Videos & Presentations</h2>

                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-12">
                                <ul class="help__list">
                                    <li>
                                        <span class="help__list-title robotobold h5 mb-0">Description</span>
                                        <span class="help__list-action d-flex align-items-center">
                                            <span class="help__list-cell">View<br />
                                                Video
                                            </span>
                                            <span class="help__list-cell">View<br />
                                                Presentation
                                            </span>
                                        </span>
                                    </li>
                                    <asp:Repeater runat="server" ID="repDocuments" OnItemDataBound="repDocuments_ItemDataBound">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' class="help__list-title"></asp:Label>
                                                <span class="help__list-action d-flex align-items-center">
                                                    <asp:HyperLink title="Download Video" runat="server" id="lnkVideo" NavigateUrl='<%# Eval("VideoLink") %>'  class="help__list-link help__list-video">
                                                        <img src="images/icons/icon-video.png" width="84" height="64" />
                                                    </asp:HyperLink>
                                                    <asp:HyperLink title="Download Presentation" runat="server" id="lnkDocument" NavigateUrl='<%# Eval("DocumentLink") %>'  class="help__list-link help__list-ppt">
                                                        <img src="images/icons/icon-ppt.svg" width="36" height="36" />
                                                    </asp:HyperLink>
                                                </span>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section class="help__contact text-center">
                <h2 class="h2 robotobold" data-toggle="modal" data-target="#help__contact">
                    <i class="mr-1 fas fa-phone-square-alt"></i>
                    Contact Support
                </h2>

                <div class="modal fade modal--auto modal__help" id="help__contact">
                    <div class="modal-dialog">
                        <div class="modal-header">
                            <button aria-label="Close" type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-content">
                            <div class="modal-body p-0">
                                <div class="card d-inline-block">
                                    <div class="card-body text-left">
                                        <div class="d-sm-flex justify-content-center flex-sm-nowrap">
                                            <div class="contact__pic">
                                                <img alt="Profile Picture" src="customer/images/support-passport-photo.png" runat="server" id="ImageProfile" width="406" height="462" />
                                            </div>
                                            <div class="contact__detail mt-4 mt-sm-0">
                                                <div class="contact__item">
                                                    <label>Name:</label>
                                                    <asp:Label runat="server" ID="lblName" />
                                                </div>
                                                <div class="contact__item">
                                                    <label>Email:</label>
                                                    <span>
                                                        <a class="d-flex" runat="server" id="lnkEmail" href="mailto:johndoe@growerstrans.com">
                                                           <asp:Label runat="server" ID="lblEmail" />
                                                         </a>
                                                    </span>
                                                </div>
                                                <div class="contact__item">
                                                    <label>Phone:</label>
                                                    <span>
                                                        <a runat="server" id="lnkPhone" href="tel:+19876543210">
                                                           <asp:Label runat="server" ID="lblPhone" />
                                                        </a>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section class="section__help help__faqs">
                <h2 class="h2 robotobold">FAQ's</h2>

                <div class="accordion accordion__help" id="faqswrapper">
                    <asp:Repeater runat="server" ID="repFAQ">
                        <ItemTemplate>

                            <!-- FAQ 1 -->
                            <div class="card">
                                <div class="card-header" data-toggle="collapse" data-target="#faq1">
                                    <h3 class="mb-0"><asp:Literal  runat="server" ID="ltrTitle" Text='<%# Eval("Title") %>' ></asp:Literal>
                                    </h3>
                                </div>

                                <div id="faq1" class="collapse show" data-parent="#faqswrapper">
                                    <div class="card-body">
                                     <asp:Literal  runat="server" ID="ltrDescription" Text='<%# Eval("Description") %>' ></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </section>

        </div>
    </div>
</asp:Content>
