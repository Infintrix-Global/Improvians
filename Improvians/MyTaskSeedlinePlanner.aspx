<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskSeedlinePlanner.aspx.cs" Inherits="Evo.MyTaskSeedlinePlanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="site__container">
            <h2>My Tasks</h2>

             <p class="pt-3">The list of tasks below are items for you to complete. For each task, you will either be completing the task or reviewing it and assigning it to someone else. Most of these tasks are auto-generated based on the plant's production profile schedule after it is seeded. You also have the ability to manually request or assign tasks as needed - just go into the form and choose.</p>
            <div style="margin-left: 90%;">
                <asp:Button Text="Pull Data" ID="btnSubmitplanner" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" />
            </div>



            <div class="dashboard__grid">
                <a href="SeedingPlanForm.aspx" class="dashboard__box">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_seedline-planning.png" width="140" height="132" alt="Seedline Planning" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_green_txt robotobold">
                            00
                        </div>
                        <h3 class="dashboard__box-title robotomd">Seedline Planning</h3>
                        <p>Review and Assign Seeding Date, Seedline and Putaway location</p>
                    </div>
                </a>
                <a href="#" class="dashboard__box">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Report" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_blue_txt robotobold">
                            06
                        </div>
                        <h3 class="dashboard__box-title robotomd">Germination Report</h3>
                        <p>Review the Germination Report from Crop Health</p>
                    </div>
                </a>
                <a href="#" class="dashboard__box">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_put-away.png" width="137" height="140" alt="Put-Away Delay" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_green_txt robotobold">
                            01
                        </div>
                        <h3 class="dashboard__box-title robotomd">Put-Away Delay</h3>
                        <p>Review delayed putaway jobs</p>
                    </div>
                </a>
            </div>
        </div>
    </div>
    <!-- Floating QR Code Button -->
    <button title="Scan QR Code" type="button" class="floating__qrcode">
        <i class="fas fa-qrcode"></i>
    </button>

</asp:Content>
