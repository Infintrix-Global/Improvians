<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskSpray.aspx.cs" Inherits="Evo.MyTaskSpray" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon">My Tasks</h2>

        <p class="pt-3 mb-0">The list of tasks below are items for you to complete.</p>

        <div class="dashboard__grid">
            <a runat="server" id="Put" href="MyTaskShippingCoordinator.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_put-away.png" width="137" height="140" alt="Put-Away" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_green_txt robotobold">
                        <asp:Label ID="lblPutAway" runat="server" Text="0"></asp:Label>

                    </div>
                    <h3 class="dashboard__box-title robotomd">Put-Away</h3>
                    <p>A list of put away location for a job.</p>
                </div>
            </a>
            <a  runat="server" id="Ger" href="GerminationCompletionForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_germination-count.png" width="137" height="136" alt="Germination Count" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblGerm" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Germination Count</h3>
                    <p>A list of germination count tasks to complete.</p>
                </div>
            </a>
            <a  runat="server" id="Fer" href="SprayTaskRequest.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_fertilization.png" width="137" height="136" alt="Fertilization" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_green_txt robotobold">
                        <asp:Label ID="lblFer" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Fertilization</h3>
                    <p>A list of spray task to complete</p>
                </div>
            </a>
            <a  runat="server" id="Chem" href="ChemicalTaskRequest.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_chemical.png" width="137" height="136" alt="Chemical" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblChemical" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Chemical</h3>
                    <p>A list of spray task to complete</p>
                </div>
            </a>
            <a runat="server" id="Irr" href="IrrigationCompletionForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_irrigation.png" width="137" height="142" alt="Irrigation" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_green_txt robotobold">
                        <asp:Label ID="lblIrr" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Irrigation</h3>
                    <p>A list of irrigation tasks to complete</p>
                </div>
            </a>
            <a  runat="server" id="Crop" href="CropReportRequestForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_crop-health-report.png" width="137" height="131" alt="Crop Health Report" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblCropHealthReport" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Crop Health Report</h3>
                    <p>Assign Crop Health Report Request</p>
                </div>
            </a>
            <a  runat="server" id="PR" href="PlantReadyCompletionForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_green_txt robotobold">
                        <asp:Label ID="lblpr" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Plant Ready</h3>
                    <p>A list of Plant Ready Reporting tasks to complete</p>
                </div>
            </a>
            <a  runat="server" id="Mov" href="MoveReqAsssignment.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_move-request.png" width="137" height="134" alt="Move Request" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblMove" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Move Request</h3>
                    <p>Review and assign move tasks</p>
                </div>
            </a>
            <a  runat="server" id="Dum" href="DumpCompletionForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_dump-request.png" width="137" height="136" alt="Dump Request" />
                </div>
                <div class="dashboard__box-desc">
                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblDumpCount" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">Dump</h3>
                    <p>Review and Assign Dump Tasks</p>
                </div>
            </a>
            <a  runat="server" id="Gen" href="GeneralTaskCompletionForm.aspx" class="dashboard__box">
                <div class="dashboard__box-img">
                    <img src="./images/dashboard_general-task.png" width="137" height="124" alt="General Task" />
                </div>
                <div class="dashboard__box-desc">

                    <div class="dashboard__box-count dash_blue_txt robotobold">
                        <asp:Label ID="lblGeneralCount" runat="server" Text="0"></asp:Label>
                    </div>
                    <h3 class="dashboard__box-title robotomd">General Task</h3>
                    <p>Review and Assign Tasks</p>
                </div>
            </a>
        </div>
    </div>
  <%--  <!-- Floating QR Code Button -->
    <button title="Scan QR Code" type="button" class="floating__qrcode">
        <i class="fas fa-qrcode"></i>
    </button>--%>
</asp:Content>
