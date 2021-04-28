<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSeedlinePlannerReport.aspx.cs" Inherits="Evo.PrintSeedlinePlannerReport" %>

<!DOCTYPE html>
<html lang="en">
<head>
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
    <link rel="icon" type="image/png" sizes="192x192" href="images/favicon/android-icon-192x192.png">
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

                <%--  <div class="row mt-4">
                        <div class="d-flex align-items-center mb-1 col-12">
                            <label class="d-block inline__fields mr-3 mb-0">Creation Date:</label>
                            <div class="field__blank"><asp:DropDownList runat="server" ID="ddlDate" ></asp:DropDownList></div>
                        </div>
                        </div>--%>
                <asp:Repeater ID="repReport" runat="server" OnItemDataBound="repReport_ItemDataBound" >
                    <ItemTemplate>
                        <div class="page__us--header">
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
                                    <div class="field__blank">SEEDLINE-<asp:Label runat="server" ID="lblFacility" Text='<%# Eval("loc_seedline") %>' /></div>
                                </div>
                                <div class="d-flex align-items-center mb-1 col-12">
                                    <label class="d-block inline__fields mr-3 mb-0">Submit Date:</label>
                                    <div class="field__blank">
                                        <asp:Label runat="server" ID="lblDate" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>' /></div>
                                </div>
                                <div class="d-flex align-items-center mb-1 col-12">
                                    <label class="d-block inline__fields mr-3 mb-0">Operator:</label>
                                    <div class="field__blank"></div>
                                </div>
                            </div>
                        </div>

                        <div class="page-break">
                            <div class="data__table">
                                <asp:GridView ID="DGJob" runat="server" AutoGenerateColumns="False"
                                    class="striped"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">

                                    <Columns>
                                        <asp:TemplateField HeaderText="DATE" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="SrNo" runat="server" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JOB" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("jobcode") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CUSTOMER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("cname") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ITEM DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itemdescp") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="65px" HeaderText="LOC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Seedline" Text='<%# Eval("loc_seedline") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TRAY SIZE" HeaderStyle-Width="65px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("traysize") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="65px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrays" runat="server" Text='<%# Eval("trays_actual") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SOIL" HeaderStyle-Width="65px">
                                            <ItemTemplate>
                                                <%-- <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("trays_actual") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GOING OUT" HeaderStyle-Width="65px">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BACK" HeaderStyle-Width="65px">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GH-SIGNATURE" HeaderStyle-Width="65px">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SEEDLINE SIGNATURE" HeaderStyle-Width="65px">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="COMMENTS" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="page__us--footer">
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

                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </div>

        <script src="js/jquery.min.js"></script>
        <script defer="defer" src="js/popper.min.js"></script>
        <script defer="defer" src="js/bootstrap.min.js"></script>
        <script defer="defer" src="js/custom.js"></script>

    </form>
</body>


</html>
