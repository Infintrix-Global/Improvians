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
                                            <span class="help__list-cell">
                                                View<br/>Video
                                            </span>
                                            <span class="help__list-cell">
                                                View<br/>Presentation
                                            </span>
                                        </span>
                                    </li>
                                    <li>
                                        <span class="help__list-title">Sample Help Resource 2</span>
                                        <span class="help__list-action d-flex align-items-center">
                                            <a title="Download Video" href="#" class="help__list-link help__list-video">
                                                <img src="images/icons/icon-video.png" width="84" height="64" />
                                            </a>
                                            <a title="Download Presentation" href="#" class="help__list-link help__list-ppt">
                                                <img src="images/icons/icon-ppt.svg" width="36" height="36" />
                                            </a>
                                        </span>
                                    </li>
                                    <li>
                                        <span class="help__list-title">Sample Help Resource 3</span>
                                        <span class="help__list-action d-flex align-items-center">
                                            <a title="Download Presentation" href="#" class="help__list-link help__list-ppt">
                                                <img src="images/icons/icon-ppt.svg" width="36" height="36" />
                                            </a>
                                        </span>
                                    </li>
                                    <li>
                                        <span class="help__list-title">Sample Help Resource 4</span>
                                        <span class="help__list-action d-flex align-items-center">
                                            <a title="Download Presentation" href="#" class="help__list-link help__list-ppt">
                                                <img src="images/icons/icon-ppt.svg" width="36" height="36" />
                                            </a>
                                        </span>
                                    </li>
                                    <li>
                                        <span class="help__list-title">Sample Help Resource 5</span>
                                        <span class="help__list-action d-flex align-items-center">
                                            <a title="Download Video" href="#" class="help__list-link help__list-video">
                                                <img src="images/icons/icon-video.png" width="84" height="64" />
                                            </a>
                                        </span>
                                    </li>
                                    <li>
                                        <span class="help__list-title">Sample Help Resource 6</span>
                                        <span class="help__list-action d-flex align-items-center">
                                            <a title="Download Video" href="#" class="help__list-link help__list-video">
                                                <img src="images/icons/icon-video.png" width="84" height="64" />
                                            </a>
                                            <a title="Download Presentation" href="#" class="help__list-link help__list-ppt">
                                                <img src="images/icons/icon-ppt.svg" width="36" height="36" />
                                            </a>
                                        </span>
                                    </li>
                                    <li>
                                        <span class="help__list-title">Sample Help Resource 6</span>
                                        <span class="help__list-action d-flex align-items-center">
                                            <a title="Download Video" href="#" class="help__list-link help__list-video">
                                                <img src="images/icons/icon-video.png" width="84" height="64" />
                                            </a>
                                            <a title="Download Presentation" href="#" class="help__list-link help__list-ppt">
                                                <img src="images/icons/icon-ppt.svg" width="36" height="36" />
                                            </a>
                                        </span>
                                    </li>
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
                                                <img alt="Profile Picture" src="customer/images/support-passport-photo.png" width="406" height="462" />
                                            </div>
                                            <div class="contact__detail mt-4 mt-sm-0">
                                                <div class="contact__item">
                                                    <label>Name:</label>
                                                    <span>John Doe</span>
                                                </div>
                                                <div class="contact__item">
                                                    <label>Email:</label>
                                                    <span>
                                                        <a class="d-flex" href="mailto:johndoe@growerstrans.com">
                                                            <span>johndoe@growerstrans.com</span>
                                                        </a>
                                                    </span>
                                                </div>
                                                <div class="contact__item">
                                                    <label>Phone:</label>
                                                    <span>
                                                        <a href="tel:+19876543210">
                                                            <span>+1-987-654-3210</span>
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
                    <!-- FAQ 1 -->
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" data-target="#faq1">
                            <h3 class="mb-0">
                                1. Question one goes here...
                            </h3>
                        </div>
                        
                        <div id="faq1" class="collapse show" data-parent="#faqswrapper">
                            <div class="card-body">
                                Some placeholder content for the first faqs panel. This panel is shown by default.
                                This could have a <a href="#">Link</a> redirecting to some page.
                            </div>
                        </div>
                    </div>

                    <!-- FAQ 2 -->
                    <div class="card">
                        <div class="card-header collapsed" data-toggle="collapse" data-target="#faq2">
                            <h3 class="mb-0">
                                2. Question two goes here...
                            </h3>
                        </div>
                        
                        <div id="faq2" class="collapse" data-parent="#faqswrapper">
                            <div class="card-body">
                                Some placeholder content for the second faqs panel.
                            </div>
                        </div>
                    </div>

                    <!-- FAQ 3 -->
                    <div class="card">
                        <div class="card-header collapsed" data-toggle="collapse" data-target="#faq3">
                            <h3 class="mb-0">
                                3. Question three goes here...
                            </h3>
                        </div>
                        
                        <div id="faq3" class="collapse" data-parent="#faqswrapper">
                            <div class="card-body">
                                Some placeholder content for the third faqs panel.
                            </div>
                        </div>
                    </div>

                </div>
            </section>

        </div>
    </div>
</asp:Content>
