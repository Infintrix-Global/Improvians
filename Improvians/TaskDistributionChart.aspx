<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDistributionChart.aspx.cs" Inherits="Evo.TaskDistributionChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Task Distribution - Growers Transplanting, Inc</title>
    <link rel="stylesheet" href="css/all.min.css">
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/custom.css">
</head>
<body>
    <header>
        <div class="site__container">
            <div class="d-flex align-items-center">
                <a href="login.html" title="Growers Transplanting Inc">
                    <img src="images/logo.png" alt="Growers Transplanting Inc" width="294" height="51" />
                </a>

                <div class="dropdown alert__dropdown ml-auto">
                    <div class="dropdown-toggle" data-display="static" id="alert__dropdown" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <i class="far fa-bell"></i>
                        <span class="alert__badge">2</span>
                    </div>

                    <div class="dropdown-menu dropdown-menu-left dropdown-menu-sm-right" aria-labelledby="alert__dropdown">
                        <div class="dropdown__head">
                            <div>Notifications</div>
                        </div>

                        <div class="dropdown__body">
                            <div class="alert__bar alert__bar--unread">
                                <i class="fas fa-tasks"></i>
                                <div class="alert__info">
                                    You have assigned new task. <strong>Job: #JB025437</strong>
                                </div>
                                <div class="alert__stamp">
                                    <span>16 Jan 2021 - 10:05 AM</span>
                                </div>
                            </div>
                            <div class="alert__bar alert__bar--unread">
                                <i class="fas fa-tasks"></i>
                                <div class="alert__info">
                                    You have assigned new task. <strong>Job: #JB025437</strong>
                                </div>
                                <div class="alert__stamp">
                                    <span>16 Jan 2021 - 10:05 AM</span>
                                </div>
                            </div>
                            <div class="alert__bar alert__bar">
                                <i class="fas fa-tasks"></i>
                                <div class="alert__info">
                                    You have assigned new task. <strong>Job: #JB025437</strong>
                                </div>
                                <div class="alert__stamp">
                                    <span>16 Jan 2021 - 10:05 AM</span>
                                </div>
                            </div>
                        </div>

                        <div class="dropdown__foo text-center">
                            <button>CLEAR ALL</button>
                        </div>
                    </div>
                </div>

                <div class="dropdown account__dropdown ml-3">
                    <div class="dropdown-toggle" id="account__dropdown" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <img src="images/user-profile.jpg" alt="John Smith" width="128" height="128" />
                        <span class="account__user robotomd">John Smith</span>
                    </div>

                    <div class="dropdown-menu" aria-labelledby="account__dropdown">
                        <a class="dropdown-item" href="#">Profile</a>
                        <a class="dropdown-item" href="#">Log Out</a>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <div class="main">
        <div class="site__container">
            <h2>Task Distribution</h2>

            <div class="dashboard__row mt-3">
                <div class="row">
                    <div class="col-12 mb-md-4">
                        <div class="chart__filter mt-4 mb-5">
                            <div class="row">
                                <div class="col-auto mb-3">
                                    <label class="d-block">From Date: </label>
                                    <input name="" type="date" id="" class="todaysDate input__control input__control-auto">
                                </div>
                                <div class="col-auto mb-3">
                                    <label class="d-block">To Date: </label>
                                    <input name="" type="date" id="" class="nextWeekDate input__control input__control-auto">
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
                                    <button type="submit" class="ml-2 submit-bttn bttn bttn-primary">
                                        Submit
                                    </button>
                                </div>
                            </div>
                        </div>

                        <div class="dashboard__block mb-5">
                            <div class="dashboard__chart dashboard__chart--bar pt-4">
                                <div class="chart__filter mb-3 text-center">
                                    <label class="robotomd mb-0">Date:</label>
                                    <label class="todaysDateLabel mb-0"></label>
                                    <label class="mb-0">/ Wednesday</label>
                                </div>

                                <div class="googleChart" id="task-distribution-1"></div>
                            </div>
                        </div>

                        <div class="dashboard__block mb-5">
                            <div class="dashboard__chart dashboard__chart--bar pt-4">
                                <div class="chart__filter mb-3 text-center">
                                    <label class="robotomd mb-0">Date:</label>
                                    <label class="mb-0">06-05-2021</label>
                                    <label class="mb-0">/ Thursday</label>
                                </div>

                                <div class="googleChart" id="task-distribution-2"></div>
                            </div>
                        </div>

                        <div class="dashboard__block">
                            <div class="dashboard__chart dashboard__chart--bar pt-4">
                                <div class="chart__filter mb-3 text-center">
                                    <label class="robotomd mb-0">Date:</label>
                                    <label class="mb-0">12-05-2021</label>
                                    <label class="mb-0">/ Wednesday</label>
                                </div>

                                <div class="googleChart" id="task-distribution-3"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="js/jquery.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="js/custom.js"></script>
</body>
</html>
