<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDistributionChart.aspx.cs" MasterPageFile="~/EvoMaster.Master" Inherits="Evo.TaskDistributionChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2>Task Distribution</h2>

        <div class="dashboard__row mt-3">
            <div class="row">
                <div class="col-12 mb-md-4">
                    <div class="chart__filter mt-4 mb-5">
                        <div class="row">
                            <div class="col-auto mb-3">
                                <label class="d-block">From Date: </label>                                
                                <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" CssClass="input__control" ></asp:TextBox>
                            </div>
                            <div class="col-auto mb-3">
                                <label class="d-block">To Date: </label>                                                               
                              <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-lg-auto">
                                <label class="d-block">Profiles: </label>
                                <div class="control__box bg-white">
                                    <div class="custom-control custom-checkbox mr-3">
                                        <input checked type="checkbox" class="custom-control-input" id="agrower">
                                        <label class="custom-control-label" for="agrower">Assistant Grower</label>
                                    </div>
                                    <div class="custom-control custom-checkbox mr-3">
                                        <input checked type="checkbox" class="custom-control-input" id="crewLead">
                                        <label class="custom-control-label" for="crewLead">Crew Lead</label>
                                    </div>
                                    <div class="custom-control custom-checkbox mr-3">
                                        <input checked type="checkbox" class="custom-control-input" id="grower">
                                        <label class="custom-control-label" for="grower">Grower</label>
                                    </div>
                                    <div class="custom-control custom-checkbox mr-3">
                                        <input checked type="checkbox" class="custom-control-input" id="irrigator">
                                        <label class="custom-control-label" for="irrigator">Irrigator</label>
                                    </div>
                                    <div class="custom-control custom-checkbox mr-3">
                                        <input type="checkbox" class="custom-control-input" id="prodPlanner">
                                        <label class="custom-control-label" for="prodPlanner">Production Planner</label>
                                    </div>
                                    <div class="custom-control custom-checkbox mr-3">
                                        <input checked type="checkbox" class="custom-control-input" id="sprayer">
                                        <label class="custom-control-label" for="sprayer">Sprayer</label>
                                    </div>
                                    <div class="custom-control custom-checkbox mr-3">
                                        <input type="checkbox" class="custom-control-input" id="supervisor">
                                        <label class="custom-control-label" for="supervisor">Supervisor</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-auto mt-0 pt-4">
                                <asp:Button ID="btnSubmit" Text="Submit" class="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />

                            </div>
                        </div>
                    </div>

                    <asp:Repeater runat="server" ID="repChart" OnItemDataBound="repChart_ItemDataBound">
                        <ItemTemplate>
                            <div class="dashboard__block mb-5">
                                <div class="dashboard__chart dashboard__chart--bar pt-4">
                                    <div class="chart__filter mb-3 text-center">
                                        <label class="robotomd mb-0">Date:</label>
                                        <label class="mb-0">
                                            <asp:Literal runat="server" ID="ltrDate" Text='<%# Eval("WorkDate","{0:MM/dd/yyyy}")  %>' />
                                        </label>
                                        <label class="mb-0">/
                                            <asp:Literal runat="server" ID="ltrDayofWeek"></asp:Literal></label>
                                    </div>

                                    <div class="googleChart" id="task-distribution"></div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <script src="https://www.gstatic.com/charts/loader.js"></script>
</asp:Content>

