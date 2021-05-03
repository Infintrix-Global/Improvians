<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportSeedlinePlanner.aspx.cs" MasterPageFile="~/EvoMaster.Master" Inherits="Evo.ReportSeedlinePlanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="site__container">
                <h2 class="text-center">Reports</h2>

                <div class="dashboard__grid justify-content-center">
                    <a href="JobReports.aspx?PageType=Reports" class="dashboard__box">
                        <div class="dashboard__box-img">
                            <img src="./images/job-report.png" width="140" height="132" alt="Job Reports">
                        </div>
                        <div class="dashboard__box-desc">
                            <h3 class="dashboard__box-title robotomd">Job Reports</h3>
                        </div>
                    </a>
                    <a href="PrintSeedlineReport.aspx" class="dashboard__box">
                        <div class="dashboard__box-img">
                            <img src="./images/print-report.png" width="140" height="132" alt="Print Reports">
                        </div>
                        <div class="dashboard__box-desc">
                            <h3 class="dashboard__box-title robotomd">Print Reports</h3>
                        </div>
                    </a>
                </div>
            </div>

</asp:Content>
