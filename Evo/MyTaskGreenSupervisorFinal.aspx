﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskGreenSupervisorFinal.aspx.cs" Inherits="Evo.MyTaskGreenSupervisorFinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon">My Tasks</h2>

        <p class="pt-3">The list of tasks below are items for you to complete. For each task, you will either be completing the task or reviewing it and assigning it to someone else. These tasks have been assigned to you by Grower/Assistant Grower after it is seeded. You also have the ability to manually request or assign tasks as needed - just go into the form and choose</p>

        <div class="dashboard__grid">
            <a runat="server" id="Put" href="MyTaskLogisticManager.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_put-away.png" width="137" height="140" alt="Put-Away" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_green_txt robotobold">
                        <asp:Label ID="lblPutAway" runat="server" Text="0"></asp:Label>

                    </div>
                    <h3 class="dashboard__box-title robotomd">Put-Away</h3>
                </div>
            </a>
            <a runat="server" id="Ger" href="GerminationAssignmentForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Count" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblGerm" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Germination Count</h3>
                </div>
            </a>
            <a runat="server" id="Fer" href="SprayTaskRequest.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_green_txt robotobold">
                        <asp:Label ID="lblFer" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Fertilization</h3>
                </div>
            </a>
            <a runat="server" id="Chem" href="ChemicalTaskRequest.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_chemical.png" width="137" height="136" alt="Chemical" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblChemical" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Chemical</h3>
                </div>
            </a>
            <a runat="server" id="Irr" href="IrrigationAssignmentForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_green_txt robotobold">
                        <asp:Label ID="lblIrr" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Irrigation</h3>
                </div>
            </a>
            <a runat="server" id="Crop" href="CropReportRequestForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_crop-health-report.png" width="137" height="131" alt="Crop Health Report" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblCropHealthReport" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Crop Health Report</h3>
                </div>
            </a>
            <a runat="server" id="PR" href="PlantReadyAssignmentForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_green_txt robotobold">
                        <asp:Label ID="lblpr" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Plant Ready</h3>
                </div>
            </a>
            <a runat="server" id="Mov" href="MoveRequestForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_move-request.png" width="137" height="134" alt="Move Request" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblMove" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Move Request</h3>
                </div>
            </a>
            <a runat="server" id="Dum" href="DumpAssignmentForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_dump-request.png" width="137" height="136" alt="Dump Request" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblDumpCount" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Dump</h3>
                </div>
            </a>
            <a runat="server" id="Gen" href="GeneralTaskAssignmentForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_general-task.png" width="137" height="124" alt="General Task" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblGeneralTaskCount" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">General Task</h3>
                </div>
            </a>
        </div>
    </div>
  <%--  <!-- Floating QR Code Button -->
    <button title="Scan QR Code" type="button" class="floating__qrcode">
        <i class="fas fa-qrcode"></i>
    </button>--%>
</asp:Content>
