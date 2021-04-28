<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceLogSheet.aspx.cs" Inherits="Evo.PerformanceLogSheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Performance Program Log Sheets - Growers Transplanting, Inc</title>

        <!-- Cross platform favicon setup & app themeing -->
        <link rel="apple-touch-icon" sizes="57x57" href="images/favicon/apple-icon-57x57.png">
        <link rel="apple-touch-icon" sizes="60x60" href="images/favicon/apple-icon-60x60.png">
        <link rel="apple-touch-icon" sizes="72x72" href="images/favicon/apple-icon-72x72.png">
        <link rel="apple-touch-icon" sizes="76x76" href="images/favicon/apple-icon-76x76.png">
        <link rel="apple-touch-icon" sizes="114x114" href="images/favicon/apple-icon-114x114.png">
        <link rel="apple-touch-icon" sizes="120x120" href="images/favicon/apple-icon-120x120.png">
        <link rel="apple-touch-icon" sizes="144x144" href="images/favicon/apple-icon-144x144.png">
        <link rel="apple-touch-icon" sizes="152x152" href="images/favicon/apple-icon-152x152.png">
        <link rel="apple-touch-icon" sizes="180x180" href="images/favicon/apple-icon-180x180.png">
        <link rel="icon" type="image/png" sizes="192x192"  href="images/favicon/android-icon-192x192.png">
        <link rel="icon" type="image/png" sizes="32x32" href="images/favicon/favicon-32x32.png">
        <link rel="icon" type="image/png" sizes="96x96" href="images/favicon/favicon-96x96.png">
        <link rel="icon" type="image/png" sizes="16x16" href="images/favicon/favicon-16x16.png">
        <link rel="manifest" href="manifest.json">
        <meta name="msapplication-TileColor" content="#ffffff">
        <meta name="msapplication-TileImage" content="images/favicon/ms-icon-144x144.png">
        <meta name="theme-color" content="#005a01">

        <!-- SEO tags -->
        <meta name="description" content="Growers Transplanting, Inc">

        <meta property="og:locale" content="en_US" />
        <meta property="og:type" content="website" />
        <meta property="og:title" content="Dashboard - Growers Transplanting, Inc" />
        <meta property="og:description" content="Growers Transplanting, Inc" />
        <meta property="og:url" content="/" />
        <meta property="og:site_name" content="Growers Transplanting" />
        <meta property="og:image" content="growers-transplanting.jpg" />
        <meta property="og:image:secure_url" content="growers-transplanting.jpg" />
        <meta property="og:image:width" content="1104" />
        <meta property="og:image:height" content="736" />

        <meta name="twitter:card" content="summary_large_image" />
        <meta name="twitter:description" content="Growers Transplanting, Inc" />
        <meta name="twitter:title" content="Dashboard - Growers Transplanting, Inc" />
        <meta name="twitter:site" content="@growerstransplanting" />
        <meta name="twitter:image" content="growers-transplanting.jpg" />
        <meta name="twitter:creator" content="@growerstransplanting" />

        <link rel="preload" href="fonts/fa/fa-solid-900.woff2" as="font" type="font/woff2" crossorigin>
        <link rel="preload" href="fonts/fa/fa-regular-400.woff2" as="font" type="font/woff2" crossorigin>
        <link rel="preload" href="fonts/fa/fa-brands-400.woff2" as="font" type="font/woff2" crossorigin>
        <link rel="preload" href="fonts/Roboto-Bold.woff2" as="font" type="font/woff2" crossorigin>
        <link rel="preload" href="fonts/Roboto-Medium.woff2" as="font" type="font/woff2" crossorigin>
        <link rel="preload" href="fonts/Roboto-Regular.woff2" as="font" type="font/woff2" crossorigin>

        <link rel="stylesheet" href="css/all.min.css">
        <link rel="stylesheet" href="css/bootstrap.min.css">
        <link rel="stylesheet" href="css/custom.css">
</head>
<body>
    <form id="form1" runat="server">
     
        <div class="page__us py-4 px-2">
            <div class="container-fluid">
                <div class="row align-items-center">
                    <div class="col-3">
                        <img class="page__logo" alt="Growers Transplanting Logo" src="images/logo-vertical.svg" width="180" height="179" />
                    </div>
                    <div class="col-6">
                        <h1 class="h4 robotobold text-center mb-0">Performance Program Log Sheet</h1>
                    </div>
                    <div class="col-3"></div>
                </div>

                <div class="row mt-4">
                    <div class="d-flex align-items-center mb-1 col-12">
                        <label class="d-block inline__fields mr-3 mb-0">Deptartment:</label>
                        <div class="field__blank">SEEDLINE-GUS1</div>
                    </div>
                    <div class="d-flex align-items-center mb-1 col-12">
                        <label class="d-block inline__fields mr-3 mb-0">Submit Date:</label>
                        <div class="field__blank">1/28/2021</div>
                    </div>
                    <div class="d-flex align-items-center mb-1 col-12">
                        <label class="d-block inline__fields mr-3 mb-0">Operator:</label>
                        <div class="field__blank"></div>
                    </div>
                </div>

                <div class="data__table">

                    <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true" 
                                GridLines="None" 
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>


                                    
                                       <asp:TemplateField HeaderText="DATE" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                       
                                            <asp:Label ID="lblPudawayDate" runat="server" Text='<%# Eval("CreateOn","{0:MM/dd/yyyy}")  %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                       
                                            <asp:Label ID="lblID"  runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                        

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Work Order" ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>

                                            <asp:Label ID="lblwo" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Seedline Facility" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblloc" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="No. Of Tray" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("trays_actual")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Seeded Due Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoDate" runat="server" Text='<%# Eval("SoDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plan Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblplan_date" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Planned Due Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldue_date" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Seeding Status" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>

                                            <asp:Label ID="lblStatusValues" Visible="false" runat="server" Text='<%# Eval("jstatus")  %>'></asp:Label>


                                        </ItemTemplate>


                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Put Away Status" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>

                                            <asp:Label ID="lblPutawayStatusValues" Visible="false" runat="server" Text='<%# Eval("jstatus")  %>'></asp:Label>
                                            <asp:Label ID="lblPudawayDate" runat="server" Text='<%# Eval("CreateOn","{0:MM/dd/yyyy}")  %>'></asp:Label>


                                        </ItemTemplate>


                                    </asp:TemplateField>



                                </Columns>

                                <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>

                    <%--<table>
                        <thead>
                            <tr>
                                <th style="width: 60px;">DATE</th>
                                <th style="width: 60px;">JOB</th>
                                <th>CUSTOMER NAME</th>
                                <th>ITEM DESCRIPTION</th>
                                <th style="width: 60px;">LOC</th>
                                <th style="width: 60px;">TRAY<br/>SIZE</th>
                                <th style="width: 60px;">QTY.</th>
                                <th style="width: 55px;">SOIL</th>
                                <th style="width: 65px;">GOING<br/>OUT</th>
                                <th style="width: 60px;">BACK</th>
                                <th style="width: 65px;">GH-SIGNATURE</th>
                                <th style="width: 70px;">SEEDLINE<br/>SIGNATURE</th>
                                <th style="width: 100px;">COMMENTS</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>DONALD BEETMAN - TM</td>
                                <td>TOMATO CANNING-6428 - 339</td>
                                <td>GUS1</td>
                                <td>339</td>
                                <td>4,439.</td>
                                <td>CONV</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>01/28/21</td>
                                <td>JB032921</td>
                                <td>JIM BORCHARD FARMING - TM</td>
                                <td>TOMATO CANNING-HM5235 - 339</td>
                                <td>GUS1</td>
                                <td>230</td>
                                <td>18.</td>
                                <td>ORG</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>--%>
                </div>

                <div class="d-flex align-items-center">
                    <label class="d-block inline__fields inline__fields-signs mr-3 mb-0">Seed Deliever Sign.</label>
                    <div class="field__blank"></div>
                </div>
                <div class="d-flex align-items-center">
                    <label class="d-block inline__fields inline__fields-signs mr-3 mb-0">Seed Line Sign.</label>
                    <div class="field__blank"></div>
                </div>
                <div class="d-flex align-items-center">
                    <label class="d-block inline__fields inline__fields-signs mr-3 mb-0">Seed Office Sign.</label>
                    <div class="field__blank"></div>
                </div>
            </div>

        </div>

        <script src="js/jquery.min.js"></script>
        <script defer="defer" src="js/popper.min.js"></script>
        <script defer="defer" src="js/bootstrap.min.js"></script>
        <script defer="defer" src="js/custom.js"></script>
    </form>
</body>
</html>
