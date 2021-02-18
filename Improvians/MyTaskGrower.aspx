<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskGrower.aspx.cs" Inherits="Improvians.MyTask1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header__bottom">
        <div class="header__tabs">
            <ul class="d-flex align-items-center justify-content-center list-inline">
                <li><a href="#" class="bttn active" title="My Task">My Tasks</a></li>
                <li><a href="#" class="bttn" title="Job Reports">Job Reports</a></li>
            </ul>
        </div>
    </div>
    <div class="main">
        <div class="site__container">
            <h2>My Tasks</h2>

            <p class="pt-3">The list of tasks below are items for you to complete. For each task, you will either be completing the task or reviewing it and assigning it to someone else. Most of these tasks are auto-generated based on the plant's production profile schedule after it is seeded. You also have the ability to manually request or assign tasks as needed - just go into the form and choose.</p>

            <div class="dashboard__grid">
                <a class="dashboard__box" href="GrowerPutAwayForm.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_put-away.png" width="137" height="140" alt="Put-Away" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_green_txt robotobold">
                            <asp:Label ID="lblPutAway" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Put-Away</h3>
                        <p>Assign a put away location for a job</p>
                    </div>
                </a>

                <a class="dashboard__box" href="GerminationRequestForm.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Count" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_blue_txt robotobold">
                            <asp:Label ID="lblGerm" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Germination Count</h3>
                        <p>Review and assign these tasks to the Greenhouse Supervisor</p>
                    </div>
                </a>
                <a class="dashboard__box" href="FertilizerTaskReq.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Fertilization / Chemical" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_green_txt robotobold">
                            <asp:Label ID="lblFer" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Fertilization / Chemical</h3>
                        <p>Review and assign these tasks to the Sprayer</p>
                    </div>
                </a>
                <a class="dashboard__box" href="IrrigationRequestForm.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_blue_txt robotobold">
                            <asp:Label ID="lblIrr" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Irrigation</h3>
                        <p>Review and assign irrigation tasks to Greenhouse Supervisor</p>
                    </div>
                </a>
                <div class="dashboard__box">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_crop-health-report.png" width="137" height="131" alt="Crop Health Report" />
                    </div>
                    <div class="dashboard__box-desc">
                         <div class="dashboard__box-count dash_green_txt robotobold">
                            <asp:Label ID="lblCrop" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Crop Health Report</h3>
                        <p>Assign Crop Health Report Request</p>
                    </div>
                </div>
                <a class="dashboard__box" href="PlantReadyRequestForm.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_blue_txt robotobold">
                            <asp:Label ID="lblpr" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Plant Ready</h3>
                        <p>Review and assign Plant Health Reporting tasks to Greenhouse Supervisor</p>
                    </div>
                </a>
                <a class="dashboard__box" href="MoveForm.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_move-request.png" width="137" height="134" alt="Move Request" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_green_txt robotobold">
                            <asp:Label ID="lblMove" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Move Request</h3>
                        <p>Review and assign move tasks</p>
                    </div>
                </a>
            </div>
        </div>
    </div>

</asp:Content>
