<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="Livemap.aspx.cs" Inherits="Evo.Livemap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <div class="automation__home mb-5">
            <h2 class="text-center mt-4">Automation Dashboard</h2>

            <div class="automation__dashboardpage site__tabs mt-4">
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <a class="nav-link active"  data-toggle="tab" href="#boom-carrier-status">Boom & Carrier Status</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link"  data-toggle="tab" href="#live-map">Live Map</a>
                    </li>
                </ul>
                <div class="tab-content dashboard__block">
                    <div class="tab-pane fade show active" id="boom-carrier-status">
                                
                        <div class="d-flex align-items-center mb-3">
                            <h3 class="pt-0 mb-0 mr-3">Boom Status</h3>

                            <a class="ml-auto bttn bttn-primary" href="automation-boom-status-dashboard.html">Boom Status Dashboard</a>
                        </div>
            
                        <div class="custom_grids mt-2 mb-4">
                            <div class="data__table text-center">
                                <table>
                                    <tr>
                                        <th>Boom ID</th>
                                        <th>Live Location Bench / Row #</th>
                                        <th>Current Activity</th>
                                        <th>Communication</th>
                                        <th>Battery Level</th>
                                        <th>Manual Controls</th>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>ENC1-OUTSIDE-06-A / 2</td>
                                        <td class="disable__cell">Moving</td>
                                        <td class="disable__cell">Yes</td>
                                        <td class="disable__cell">
                                            <span class="icon-battery">
                                                <span class="icon-battery--filler" style="width:80%;"></span>
                                                <span>80%</span>
                                            </span>
                                        </td>
                                        <td>
                                            <a id="goBoomManual1" onClick="goBoomManual()" class="bttn bttn-primary bttn-sm" href="automation-boom-controls.html?id=1" title="Manual Control">
                                                <i class="fas fa-cog pr-1 align-middle"></i>
                                                <span class="align-middle">Manual Control</span>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr class="disable__row">
                                        <td>2</td>
                                        <td>ENC1-OUTSIDE-06-B / 3</td>
                                        <td>Irrigation</td>
                                        <td>No</td>
                                        <td>
                                            <span class="icon-battery">
                                                <span class="icon-battery--filler" style="width:20%;"></span>
                                                <span>20%</span>
                                            </span>
                                        </td>
                                        <td>
                                            <a id="goBoomManual2" onClick="goBoomManual()" class="bttn bttn-primary bttn-sm bttn-disabled" disabled href="automation-boom-controls.html?id=2" title="Manual Control">
                                                <i class="fas fa-cog pr-1 align-middle"></i>
                                                <span class="align-middle">Manual Control</span>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr class="disable__row">
                                        <td>3</td>
                                        <td>ENC1-OUTSIDE-07-A / 4</td>
                                        <td>Stop</td>
                                        <td>Yes</td>
                                        <td>
                                            <span class="icon-battery">
                                                <span class="icon-battery--filler" style="width:60%;"></span>
                                                <span>60%</span>
                                            </span>
                                        </td>
                                        <td>
                                            <a id="goBoomManual3" onClick="goBoomManual()" class="bttn bttn-primary bttn-sm bttn-disabled" disabled href="automation-boom-controls.html?id=3" title="Manual Control">
                                                <i class="fas fa-cog pr-1 align-middle"></i>
                                                <span class="align-middle">Manual Control</span>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr class="disable__row">
                                        <td>4</td>
                                        <td>ENC1-OUTSIDE-07-A / 5</td>
                                        <td>Fertilization</td>
                                        <td>No</td>
                                        <td>
                                            <span class="icon-battery">
                                                <span class="icon-battery--filler" style="width:100%;"></span>
                                                <span>100%</span>
                                            </span>
                                        </td>
                                        <td>
                                            <a id="goBoomManual4" onClick="goBoomManual()" class="bttn bttn-primary bttn-sm bttn-disabled" disabled href="  .html?id=4" title="Manual Control">
                                                <i class="fas fa-cog pr-1 align-middle"></i>
                                                <span class="align-middle">Manual Control</span>
                                            </a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                                
                        <div class="d-flex align-items-center mb-3">
                            <h3 class="pt-0 mb-0 mr-3">Carrier Status</h3>

                            <a class="ml-auto bttn bttn-primary" href="automation-carrier-status-dashboard.html">Carrier Status Dashboard</a>
                        </div>

                        <div class="custom_grid mt-2 mb-0">
                            <div class="data__table text-center">
                                <table>
                                    <tr>
                                        <th>Carrier ID</th>
                                        <th>Live Location Bench / Row #</th>
                                        <th>Current Activity</th>
                                        <th>Communication</th>
                                        <th>Battery Level</th>
                                        <th>Manual Controls</th>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>ENC1-OUTSIDE-06-A / 2</td>
                                        <td class="disable__cell">Moving</td>
                                        <td class="disable__cell">Yes</td>
                                        <td class="disable__cell">
                                            <span class="icon-battery">
                                                <span class="icon-battery--filler" style="width:37%;"></span>
                                                <span>37%</span>
                                            </span>
                                        </td>
                                        <td>
                                            <a id="goMoverManual1" onClick="goMoverManual()" class="bttn bttn-primary bttn-sm" href="automation-mover-controls.html?id=1" title="Manual Control">
                                                <i class="fas fa-cog pr-1 align-middle"></i>
                                                <span class="align-middle">Manual Control</span>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr class="disable__row">
                                        <td>2</td>
                                        <td>ENC1-OUTSIDE-06-B / 3</td>
                                        <td>Unloading Tray</td>
                                        <td>No</td>
                                        <td>
                                            <span class="icon-battery">
                                                <span class="icon-battery--filler" style="width:10%;"></span>
                                                <span>10%</span>
                                            </span>
                                        </td>
                                        <td>
                                            <a id="goMoverManual2" onClick="goMoverManual()" class="bttn bttn-primary bttn-sm bttn-disabled" disabled href="automation-mover-controls.html?id=2" title="Manual Control">
                                                <i class="fas fa-cog pr-1 align-middle"></i>
                                                <span class="align-middle">Manual Control</span>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr class="disable__row">
                                        <td>3</td>
                                        <td>ENC1-OUTSIDE-07-A / 4</td>
                                        <td>Loading Tray</td>
                                        <td>Yes</td>
                                        <td>
                                            <span class="icon-battery">
                                                <span class="icon-battery--filler" style="width:75%;"></span>
                                                <span>75%</span>
                                            </span>
                                        </td>
                                        <td>
                                            <a id="goMoverManual3" onClick="goMoverManual()" class="bttn bttn-primary bttn-sm bttn-disabled" disabled href="automation-mover-controls.html?id=3" title="Manual Control">
                                                <i class="fas fa-cog pr-1 align-middle"></i>
                                                <span class="align-middle">Manual Control</span>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr class="disable__row">
                                        <td>4</td>
                                        <td>ENC1-OUTSIDE-07-B / 5</td>
                                        <td>Moving Boom</td>
                                        <td>No</td>
                                        <td>
                                            <span class="icon-battery">
                                                <span class="icon-battery--filler" style="width:98%;"></span>
                                                <span>98%</span>
                                            </span>
                                        </td>
                                        <td>
                                            <a id="goMoverManual4" onClick="goMoverManual()" class="bttn bttn-primary bttn-sm bttn-disabled" disabled href="automation-mover-controls.html?id=4" title="Manual Control">
                                                <i class="fas fa-cog pr-1 align-middle"></i>
                                                <span class="align-middle">Manual Control</span>
                                            </a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="tab-pane fade" id="live-map">
                                
                        <h6 class="pt-0 text-center">Facility:</h6>
                        <h3 class="pt-0 text-center">ENC1</h3>

                        <div class="livemap__filters">
                            <div class="container">
                                <div class="row justify-content-center">
                                    <div class="col-12 col-md-3 col-lg-auto mb-3">
                                        <label class="d-block">Bench Location </label>

                                        <select name="livebench" onchange="" id="livebench" class="custom__dropdown  input__control-auto robotomd">
                                            <option selected="selected" value="">--- Select ---</option>
                                            <option disabled value="ENC1-H01-1-A">ENC1-H01-1-A</option>
                                            <option disabled value="ENC1-H01-1-B">ENC1-H01-1-B</option>
                                            <option disabled value="ENC1-H01-2-A">ENC1-H01-2-A</option>
                                            <option disabled value="ENC1-H01-2-B">ENC1-H01-2-B</option>
                                            <option disabled value="ENC1-H01-3-A">ENC1-H01-3-A</option>
                                            <option disabled value="ENC1-H01-3-B">ENC1-H01-3-B</option>
                                            <option disabled value="ENC1-H01-4-A">ENC1-H01-4-A</option>
                                            <option disabled value="ENC1-H01-4-B">ENC1-H01-4-B</option>
                                            <option disabled value="ENC1-H01-5-A">ENC1-H01-5-A</option>
                                            <option disabled value="ENC1-H01-5-B">ENC1-H01-5-B</option>
                                            <option disabled value="ENC1-H01-6-A">ENC1-H01-6-A</option>
                                            <option disabled value="ENC1-H01-6-B">ENC1-H01-6-B</option>
                                            <option disabled value="ENC1-H01-7-A">ENC1-H01-7-A</option>
                                            <option disabled value="ENC1-H01-7-B">ENC1-H01-7-B</option>
                                            <option disabled value="ENC1-H01-8-A">ENC1-H01-8-A</option>
                                            <option disabled value="ENC1-H01-8-B">ENC1-H01-8-B</option>
                                            <option disabled value="ENC1-H02-1-A">ENC1-H02-1-A</option>
                                            <option disabled value="ENC1-H02-1-B">ENC1-H02-1-B</option>
                                            <option disabled value="ENC1-H02-2-A">ENC1-H02-2-A</option>
                                            <option disabled value="ENC1-H02-2-B">ENC1-H02-2-B</option>
                                            <option disabled value="ENC1-H02-3-A">ENC1-H02-3-A</option>
                                            <option disabled value="ENC1-H02-3-B">ENC1-H02-3-B</option>
                                            <option disabled value="ENC1-H02-4-A">ENC1-H02-4-A</option>
                                            <option disabled value="ENC1-H02-4-B">ENC1-H02-4-B</option>
                                            <option disabled value="ENC1-H02-5-A">ENC1-H02-5-A</option>
                                            <option disabled value="ENC1-H02-5-B">ENC1-H02-5-B</option>
                                            <option disabled value="ENC1-H02-6-A">ENC1-H02-6-A</option>
                                            <option disabled value="ENC1-H02-6-B">ENC1-H02-6-B</option>
                                            <option disabled value="ENC1-H02-7-A">ENC1-H02-7-A</option>
                                            <option disabled value="ENC1-H02-7-B">ENC1-H02-7-B</option>
                                            <option disabled value="ENC1-H02-8-A">ENC1-H02-8-A</option>
                                            <option disabled value="ENC1-H02-8-B">ENC1-H02-8-B</option>
                                            <option disabled value="ENC1-H03-1-A">ENC1-H03-1-A</option>
                                            <option disabled value="ENC1-H03-1-B">ENC1-H03-1-B</option>
                                            <option disabled value="ENC1-H03-2-A">ENC1-H03-2-A</option>
                                            <option disabled value="ENC1-H03-2-B">ENC1-H03-2-B</option>
                                            <option disabled value="ENC1-H03-3-A">ENC1-H03-3-A</option>
                                            <option disabled value="ENC1-H03-3-B">ENC1-H03-3-B</option>
                                            <option disabled value="ENC1-H03-4-A">ENC1-H03-4-A</option>
                                            <option disabled value="ENC1-H03-4-B">ENC1-H03-4-B</option>
                                            <option disabled value="ENC1-H03-5-A">ENC1-H03-5-A</option>
                                            <option disabled value="ENC1-H03-5-B">ENC1-H03-5-B</option>
                                            <option disabled value="ENC1-H03-6-A">ENC1-H03-6-A</option>
                                            <option disabled value="ENC1-H03-6-B">ENC1-H03-6-B</option>
                                            <option disabled value="ENC1-H03-7-A">ENC1-H03-7-A</option>
                                            <option disabled value="ENC1-H03-7-B">ENC1-H03-7-B</option>
                                            <option disabled value="ENC1-H03-8-A">ENC1-H03-8-A</option>
                                            <option disabled value="ENC1-H03-8-B">ENC1-H03-8-B</option>
                                            <option disabled value="ENC1-H03-9-A">ENC1-H03-9-A</option>
                                            <option disabled value="ENC1-H03-9-B">ENC1-H03-9-B</option>
                                            <option disabled value="ENC1-OUTSIDE-01-A">ENC1-OUTSIDE-01-A</option>
                                            <option disabled value="ENC1-OUTSIDE-01-B">ENC1-OUTSIDE-01-B</option>
                                            <option disabled value="ENC1-OUTSIDE-02-A">ENC1-OUTSIDE-02-A</option>
                                            <option disabled value="ENC1-OUTSIDE-02-B">ENC1-OUTSIDE-02-B</option>
                                            <option disabled value="ENC1-OUTSIDE-03-A">ENC1-OUTSIDE-03-A</option>
                                            <option disabled value="ENC1-OUTSIDE-03-B">ENC1-OUTSIDE-03-B</option>
                                            <option disabled value="ENC1-OUTSIDE-04-A">ENC1-OUTSIDE-04-A</option>
                                            <option disabled value="ENC1-OUTSIDE-04-B">ENC1-OUTSIDE-04-B</option>
                                            <option value="ENC1-OUTSIDE-05-A">ENC1-OUTSIDE-05-A</option>
                                            <option value="ENC1-OUTSIDE-05-B">ENC1-OUTSIDE-05-B</option>
                                            <option value="ENC1-OUTSIDE-06-A">ENC1-OUTSIDE-06-A</option>
                                            <option value="ENC1-OUTSIDE-06-B">ENC1-OUTSIDE-06-B</option>
                                            <option value="ENC1-OUTSIDE-07-A">ENC1-OUTSIDE-07-A</option>
                                            <option value="ENC1-OUTSIDE-07-B">ENC1-OUTSIDE-07-B</option>
                                            <option value="ENC1-OUTSIDE-08-A">ENC1-OUTSIDE-08-A</option>
                                            <option value="ENC1-OUTSIDE-08-B">ENC1-OUTSIDE-08-B</option>
                                            <option value="ENC1-OUTSIDE-09-A">ENC1-OUTSIDE-09-A</option>
                                            <option value="ENC1-OUTSIDE-09-B">ENC1-OUTSIDE-09-B</option>
                                            <option value="ENC1-OUTSIDE-10-A">ENC1-OUTSIDE-10-A</option>
                                            <option value="ENC1-OUTSIDE-10-B">ENC1-OUTSIDE-10-B</option>
                                            <option value="ENC1-OUTSIDE-11-A">ENC1-OUTSIDE-11-A</option>
                                            <option value="ENC1-OUTSIDE-11-B">ENC1-OUTSIDE-11-B</option>
                                            <option value="ENC1-OUTSIDE-12-A">ENC1-OUTSIDE-12-A</option>
                                            <option value="ENC1-OUTSIDE-12-B">ENC1-OUTSIDE-12-B</option>
                                            <option value="ENC1-OUTSIDE-13-A">ENC1-OUTSIDE-13-A</option>
                                            <option value="ENC1-OUTSIDE-13-B">ENC1-OUTSIDE-13-B</option>
                                            <option value="ENC1-OUTSIDE-14-A">ENC1-OUTSIDE-14-A</option>
                                            <option value="ENC1-OUTSIDE-14-B">ENC1-OUTSIDE-14-B</option>
                                            <option value="ENC1-OUTSIDE-15-A">ENC1-OUTSIDE-15-A</option>
                                            <option value="ENC1-OUTSIDE-15-B">ENC1-OUTSIDE-15-B</option>
                                            <option value="ENC1-OUTSIDE-16-A">ENC1-OUTSIDE-16-A</option>
                                            <option value="ENC1-OUTSIDE-16-B">ENC1-OUTSIDE-16-B</option>
                                            <option value="ENC1-OUTSIDE-17-A">ENC1-OUTSIDE-17-A</option>
                                            <option value="ENC1-OUTSIDE-17-B">ENC1-OUTSIDE-17-B</option>
                                            <option value="ENC1-OUTSIDE-18-A">ENC1-OUTSIDE-18-A</option>
                                            <option value="ENC1-OUTSIDE-18-B">ENC1-OUTSIDE-18-B</option>
                                            <option value="ENC1-OUTSIDE-19-A">ENC1-OUTSIDE-19-A</option>
                                            <option value="ENC1-OUTSIDE-19-B">ENC1-OUTSIDE-19-B</option>
                                            <option value="ENC1-OUTSIDE-20-A">ENC1-OUTSIDE-20-A</option>
                                            <option value="ENC1-OUTSIDE-20-B">ENC1-OUTSIDE-20-B</option>
                                            <option value="ENC1-OUTSIDE-21-A">ENC1-OUTSIDE-21-A</option>
                                            <option value="ENC1-OUTSIDE-21-B">ENC1-OUTSIDE-21-B</option>
                                            <option value="ENC1-OUTSIDE-22-A">ENC1-OUTSIDE-22-A</option>
                                            <option value="ENC1-OUTSIDE-22-B">ENC1-OUTSIDE-22-B</option>
                                            <option value="ENC1-OUTSIDE-23-A">ENC1-OUTSIDE-23-A</option>
                                            <option value="ENC1-OUTSIDE-23-B">ENC1-OUTSIDE-23-B</option>
                                            <option value="ENC1-OUTSIDE-24-A">ENC1-OUTSIDE-24-A</option>
                                            <option value="ENC1-OUTSIDE-24-B">ENC1-OUTSIDE-24-B</option>
                                            <option value="ENC1-OUTSIDE-25-A">ENC1-OUTSIDE-25-A</option>
                                            <option value="ENC1-OUTSIDE-25-B">ENC1-OUTSIDE-25-B</option>
                                            <option value="ENC1-OUTSIDE-26-A">ENC1-OUTSIDE-26-A</option>
                                            <option value="ENC1-OUTSIDE-26-B">ENC1-OUTSIDE-26-B</option>
                                            <option value="ENC1-OUTSIDE-27-A">ENC1-OUTSIDE-27-A</option>
                                            <option value="ENC1-OUTSIDE-27-B">ENC1-OUTSIDE-27-B</option>
                                            <option value="ENC1-OUTSIDE-28-A">ENC1-OUTSIDE-28-A</option>
                                            <option value="ENC1-OUTSIDE-28-B">ENC1-OUTSIDE-28-B</option>
                                            <option value="ENC1-OUTSIDE-29-A">ENC1-OUTSIDE-29-A</option>
                                            <option value="ENC1-OUTSIDE-29-B">ENC1-OUTSIDE-29-B</option>
                                            <option value="ENC1-OUTSIDE-30-A">ENC1-OUTSIDE-30-A</option>
                                            <option value="ENC1-OUTSIDE-30-B">ENC1-OUTSIDE-30-B</option>
                                            <option value="ENC1-OUTSIDE-31-A">ENC1-OUTSIDE-31-A</option>
                                            <option value="ENC1-OUTSIDE-31-B">ENC1-OUTSIDE-31-B</option>
                                            <option value="ENC1-OUTSIDE-32-A">ENC1-OUTSIDE-32-A</option>
                                            <option value="ENC1-OUTSIDE-32-B">ENC1-OUTSIDE-32-B</option>
                                            <option value="ENC1-OUTSIDE-33-A">ENC1-OUTSIDE-33-A</option>
                                            <option value="ENC1-OUTSIDE-33-B">ENC1-OUTSIDE-33-B</option>
                                            <option value="ENC1-OUTSIDE-34-A">ENC1-OUTSIDE-34-A</option>
                                            <option value="ENC1-OUTSIDE-34-B">ENC1-OUTSIDE-34-B</option>
                                            <option value="ENC1-OUTSIDE-35-A">ENC1-OUTSIDE-35-A</option>
                                            <option value="ENC1-OUTSIDE-35-B">ENC1-OUTSIDE-35-B</option>
                                            <option value="ENC1-OUTSIDE-36-A">ENC1-OUTSIDE-36-A</option>
                                            <option value="ENC1-OUTSIDE-36-B">ENC1-OUTSIDE-36-B</option>
                                            <option value="ENC1-OUTSIDE-37-A">ENC1-OUTSIDE-37-A</option>
                                            <option value="ENC1-OUTSIDE-37-B">ENC1-OUTSIDE-37-B</option>
                                            <option value="ENC1-OUTSIDE-38-A">ENC1-OUTSIDE-38-A</option>
                                            <option value="ENC1-OUTSIDE-38-B">ENC1-OUTSIDE-38-B</option>
                                            <option value="ENC1-OUTSIDE-39-A">ENC1-OUTSIDE-39-A</option>
                                            <option value="ENC1-OUTSIDE-39-B">ENC1-OUTSIDE-39-B</option>
                                            <option value="ENC1-OUTSIDE-40-A">ENC1-OUTSIDE-40-A</option>
                                            <option value="ENC1-OUTSIDE-40-B">ENC1-OUTSIDE-40-B</option>
                                            <option value="ENC1-OUTSIDE-41-A">ENC1-OUTSIDE-41-A</option>
                                            <option value="ENC1-OUTSIDE-41-B">ENC1-OUTSIDE-41-B</option>
                                            <option value="ENC1-OUTSIDE-42-A">ENC1-OUTSIDE-42-A</option>
                                            <option value="ENC1-OUTSIDE-42-B">ENC1-OUTSIDE-42-B</option>
                                            <option value="ENC1-OUTSIDE-43-A">ENC1-OUTSIDE-43-A</option>
                                            <option value="ENC1-OUTSIDE-43-B">ENC1-OUTSIDE-43-B</option>
                                            <option value="ENC1-OUTSIDE-44-A">ENC1-OUTSIDE-44-A</option>
                                            <option value="ENC1-OUTSIDE-44-B">ENC1-OUTSIDE-44-B</option>
                                            <option value="ENC1-OUTSIDE-45-A">ENC1-OUTSIDE-45-A</option>
                                            <option value="ENC1-OUTSIDE-45-B">ENC1-OUTSIDE-45-B</option>
                                            <option value="ENC1-OUTSIDE-46-A">ENC1-OUTSIDE-46-A</option>
                                            <option value="ENC1-OUTSIDE-46-B">ENC1-OUTSIDE-46-B</option>
                                            <option value="ENC1-OUTSIDE-47-A">ENC1-OUTSIDE-47-A</option>
                                            <option value="ENC1-OUTSIDE-47-B">ENC1-OUTSIDE-47-B</option>
                                            <option value="ENC1-OUTSIDE-48-A">ENC1-OUTSIDE-48-A</option>
                                            <option value="ENC1-OUTSIDE-48-B">ENC1-OUTSIDE-48-B</option>
                                            <option value="ENC1-OUTSIDE-49-A">ENC1-OUTSIDE-49-A</option>
                                            <option value="ENC1-OUTSIDE-49-B">ENC1-OUTSIDE-49-B</option>
                                            <option value="ENC1-OUTSIDE-50-A">ENC1-OUTSIDE-50-A</option>
                                            <option value="ENC1-OUTSIDE-50-B">ENC1-OUTSIDE-50-B</option>
                                            <option value="ENC1-OUTSIDE-51-A">ENC1-OUTSIDE-51-A</option>
                                            <option value="ENC1-OUTSIDE-51-B">ENC1-OUTSIDE-51-B</option>
                                            <option value="ENC1-OUTSIDE-52-A">ENC1-OUTSIDE-52-A</option>
                                            <option value="ENC1-OUTSIDE-52-B">ENC1-OUTSIDE-52-B</option>
                                            <option disabled value="ENC1-OUTSIDE-53-A">ENC1-OUTSIDE-53-A</option>
                                            <option disabled value="ENC1-OUTSIDE-53-B">ENC1-OUTSIDE-53-B</option>
                                            <option disabled value="ENC1-OUTSIDE-54-A">ENC1-OUTSIDE-54-A</option>
                                            <option disabled value="ENC1-OUTSIDE-54-B">ENC1-OUTSIDE-54-B</option>
                                            <option disabled value="ENC1-OUTSIDE-55-A">ENC1-OUTSIDE-55-A</option>
                                            <option disabled value="ENC1-OUTSIDE-55-B">ENC1-OUTSIDE-55-B</option>
                                            <option disabled value="ENC1-OUTSIDE-56-A">ENC1-OUTSIDE-56-A</option>
                                            <option disabled value="ENC1-OUTSIDE-56-B">ENC1-OUTSIDE-56-B</option>
                                            <option disabled value="ENC1-OUTSIDE-57-A">ENC1-OUTSIDE-57-A</option>
                                            <option disabled value="ENC1-OUTSIDE-57-B">ENC1-OUTSIDE-57-B</option>
                                            <option disabled value="ENC1-OUTSIDE-58-A">ENC1-OUTSIDE-58-A</option>
                                            <option disabled value="ENC1-OUTSIDE-58-B">ENC1-OUTSIDE-58-B</option>
                                            <option disabled value="ENC1-OUTSIDE-W4">ENC1-OUTSIDE-W4</option>

                                        </select>
                                    </div>

                                    <div class="col-12 col-md-3 col-lg-auto mb-3">
                                        <label class="d-block">Job No</label>

                                        <select name="livejobno" onchange="" id="livejobno" class="custom__dropdown input__control-auto robotomd">
                                            <option selected="selected" value="">--- Select ---</option>
                                            <option value="JB0200004">JB0200004</option>
                                            <option value="JB031356">JB031356</option>
                                            <option value="JB031589">JB031589</option>
                                            <option value="JB031591">JB031591</option>
                                            <option value="JB033345">JB033345</option>
                                            <option value="JB034284">JB034284</option>
                                            <option value="JB037353">JB037353</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="live__map my-3">
                            <div class="sys__container">
                                <div class="sys__loadingarea">
                                    <span>Loading Area</span>
                                </div>

                                <div class="sys__carrier">
                                    <span>CARRIER</span>
                                </div>

                                <div class="sys__conveyor conveyor__unloading"></div>

                                <div class="sys__bencharea">
                                    <div class="boom__group">
                                        <div class="boom" data-x="0" data-y="0"></div>
                                    </div>
                                </div>
                                        
                                <div class="sys__conveyor conveyor__loading"></div>
                            </div>
                        </div>
                        <!--Live Map Ends -->

                    </div>
                </div>
            </div>
                    
        </div>
    </div>

    <!-- Bench Popup -->
    <div class="modal fade" id="bench-view" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdropLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                </div>
            </div>
        </div>
    </div>

</asp:Content>
