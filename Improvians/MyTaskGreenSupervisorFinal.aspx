<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskGreenSupervisorFinal.aspx.cs" Inherits="Evo.MyTaskGreenSupervisorFinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="main">
        <div class="site__container">
            <h2>My Tasks</h2>

            <p class="pt-3">
                The list of tasks below are items for you to complete. For each task, you will either be completing the task or reviewing it and assigning it to someone else. These tasks have been assigned to you by Grower/Assistant Grower after it is seeded. You also have the ability to manually request or assign tasks as needed - just go into the form and choose.
                       
            </p>

            <div class="dashboard__grid">
                <a class="dashboard__box" href="MyTaskLogisticManager.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_put-away.png" width="137" height="140" alt="Put-Away" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_green_txt robotobold">
                            <asp:Label ID="lblPutAway" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Put-Away</h3>
                        <p>Request a put away location for a job</p>
                    </div>
                </a>

                <a class="dashboard__box" href="GerminationAssignmentForm.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Count" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_blue_txt robotobold">
                            <asp:Label ID="lblGerm" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Germination Count</h3>
                        <p>A list of germination count tasks to complete</p>
                    </div>
                </a>

                <a class="dashboard__box" href="SprayTaskRequest.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Fertilization / Chemical" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_green_txt robotobold">
                            <asp:Label ID="lblFer" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Fertilization / Chemical</h3>
                        <p>Request a spray job</p>
                    </div>
                </a>
                <a class="dashboard__box" href="IrrigationAssignmentForm.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation" />
                    </div>
                    <div class="dashboard__box-desc">
                        <div class="dashboard__box-count dash_blue_txt robotobold">
                            <asp:Label ID="lblIrr" runat="server" Text="0"></asp:Label>
                        </div>
                        <h3 class="dashboard__box-title robotomd">Irrigation</h3>
                        <p>A list of irrigation tasks to complete</p>
                    </div>
                </a>
                <div class="dashboard__box">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_crop-health-report.png" width="137" height="131" alt="Crop Health Report" />
                    </div>
                    <div class="dashboard__box-desc">                        
                        <h3 class="dashboard__box-title robotomd">Crop Health Report</h3>
                        <p>Assign Crop Health Report Request</p>
                    </div>
                </div>
                <a class="dashboard__box" href="PlantReadyAssignmentForm.aspx">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready" />
                    </div>
                    <div class="dashboard__box-desc">

                        <div class="dashboard__box-count dash_blue_txt robotobold">
                            <asp:Label ID="lblpr" runat="server" Text="0"></asp:Label>
                        </div>

                        <h3 class="dashboard__box-title robotomd">Plant Ready</h3>
                        <p>A list of plant ready reporting tasks to complete</p>

                    </div>
                </a>
                <a class="dashboard__box" href="MoveReqAsssignment.aspx">
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
                <a href="#" class="dashboard__box">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_dump-request.png" width="137" height="136" alt="Dump Request" />
                    </div>
                    <div class="dashboard__box-desc">
                        <h3 class="dashboard__box-title robotomd">Dump</h3>
                        <p>Review and Assign Dump Tasks</p>
                    </div>
                </a>
                <a href="#" class="dashboard__box">
                    <div class="dashboard__box-img">
                        <img src="./images/dashboard_general-task.png" width="137" height="124" alt="General Task" />
                    </div>
                    <div class="dashboard__box-desc">
                        <h3 class="dashboard__box-title robotomd">General Task</h3>
                        <p>Review and Assign Tasks</p>
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
