﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Improvians.DashBoard1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="header__bottom">
            <div class="d-flex align-items-center justify-content-center">
                <select class="custom__dropdown robotomd">
                    <option>Encinal 1</option>
                    <option>Encinal 2</option>
                    <option>Encinal 3</option>
                </select>                
            </div>
        </div>


      <div class="main">
            <div class="site__container">
                <div class="dashboard__matrixlist d-flex justify-content-between">
                    
                    <div class="dashboard__block dashboard__matrix">
                        <div class="dashborad__stats stats--germination-seed">
                            <i class="rxicon-icon-germination-seed"></i>
                        </div>

                        <div class="dashboard__matrixdetail">
                            <h3 class="robotobold"><asp:Literal runat="server" ID="ltrGerminationRate" Text="0"></asp:Literal>%</h3>
                            <span>Germination Rate</span>
                        </div>
                    </div>
                    
                    <div class="dashboard__block dashboard__matrix">
                        <div class="dashborad__stats stats--plant-quality">
                            <i class="rxicon-icon-plant-quality"></i>
                        </div>

                        <div class="dashboard__matrixdetail">
                            <h3 class="robotobold">4.2</h3>
                            <span>Avg. Plant Ready Quality(1-5)</span>
                        </div>
                    </div>
                    
                    <div class="dashboard__block dashboard__matrix">
                        <div class="dashborad__stats stats--facility-probability">
                            <i class="rxicon-icon-facility-probability"></i>
                        </div>

                        <div class="dashboard__matrixdetail">
                            <h3 class="robotobold">$12</h3>
                            <span>Contribution Margin<br /> per ft<sup>2</sup> per day</span>
                        </div>
                    </div>
                    
                    <div class="dashboard__block dashboard__matrix">
                        <div class="dashborad__stats stats--labor-efficiency">
                            <i class="rxicon-icon-labor-efficiency"></i>
                        </div>

                        <div class="dashboard__matrixdetail">
                            <h3 class="robotobold">72%</h3>
                            <span>Labor Efficiency</span>
                        </div>
                    </div>
                    
                    <div class="dashboard__block dashboard__matrix">
                        <div class="dashborad__stats stats--site-capacity">
                            <i class="rxicon-icon-site-capacity"></i>
                        </div>

                        <div class="dashboard__matrixdetail">
                            <h3 class="robotobold">81%</h3>
                            <span>Site Occupancy</span>
                        </div>
                    </div>

                </div>
                
                <div class="dashboard__row">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="dashboard__block">
                                <div class="dashboard__links">
                                    <a runat="server" id="amytask" href="#" class="dashboard__task">
                                        <img src="images/link-my-task.svg" width="184" height="184" alt="My Tasks" />
                                        <span>My Task</span>
                                    </a>
                                    <a href="#" class="dashboard__task">
                                        <img src="images/link-site-tasks.svg" width="184" height="184" alt="Site Tasks" />
                                        <span>Site Task</span>
                                    </a>
                                    <a href="#" class="dashboard__task">
                                        <img src="images/link-assign-task.svg" width="184" height="184" alt="Assign a Task" />
                                        <span>Assign Task</span>
                                    </a>
                                    <a href="#" class="dashboard__task">
                                        <img src="images/link-track-tasks.svg" width="184" height="184" alt="Track Tasks" />
                                        <span>Track Task</span>
                                    </a>
                                </div>
                            </div>
                        </div>
                       <div class="col-lg-6">
                            <div class="dashboard__block">
                                <div class="block__head">
                                    <h2>Current Activities</h2>
                                </div>

                                <div class="activity__graph d-flex">
                                    <div class="graph__yaxis graph__bar">
                                        <div class="graph__mark">100%</div>
                                        <div class="graph__mark">80%</div>
                                        <div class="graph__mark">60%</div>
                                        <div class="graph__mark">40%</div>
                                        <div class="graph__mark">20%</div>
                                        <div class="graph__mark">0%</div>
                                        <div class="graph__mark"></div>
                                    </div>

                                    <div class="graph__bar graph__bar--planing">
                                        <div class="graph__bar--line">
                                            <span class="graph__bar--today" style="height:58%;"></span>
                                            <span class="graph__bar--week" style="height:22%;"></span>
                                        </div>
                                        
                                        <div class="graph__xaxis">Planning</div>
                                    </div>

                                    <div class="graph__bar graph__bar--seeding">
                                        <div class="graph__bar--line">
                                            <span class="graph__bar--today" style="height:84%;"></span>
                                            <span class="graph__bar--week" style="height:28%;"></span>
                                        </div>

                                        <div class="graph__xaxis">Seeding</div>
                                    </div>

                                    <div class="graph__bar graph__bar--growing">
                                        <div class="graph__bar--line">
                                            <span class="graph__bar--today" style="height:44%;"></span>
                                            <span class="graph__bar--week" style="height:18%;"></span>
                                        </div>

                                        <div class="graph__xaxis">Growing</div>
                                    </div>

                                    <div class="graph__bar graph__bar--transplanting">
                                        <div class="graph__bar--line">
                                            <span class="graph__bar--today" style="height:63%;"></span>
                                            <span class="graph__bar--week" style="height:26%;"></span>
                                        </div>
                                        
                                        <div class="graph__xaxis">Transplanting</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

</asp:Content>
