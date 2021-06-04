<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedingPlanForm.aspx.cs" Inherits="Evo.SeedingPlanForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container  d-print-none">
        <%--  <div class="row">--%>
        <%-- <div class="col-lg-10">--%>

        <h2 class="head__title-icon mb-3 ">Seedline Planning</h2>
        <%--</div>--%>

        <div class="col-lg-2">
            <asp:Button ID="Reset" runat="server" Text="Reset All Data" OnClick="Reset_Click" Visible="false" CssClass="bttn bttn-primary bttn-action" />
        </div>



        <div class="row">
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Seedline Facility</label>
                <asp:DropDownList ID="ddlSeedlineLocation" OnSelectedIndexChanged="ddlSeedlineLocation_SelectedIndexChanged" AutoPostBack="true" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Item</label>
                <asp:DropDownList ID="ddlItem" runat="server" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Customer </label>
                <asp:DropDownList ID="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Tray Size</label>
                <asp:DropDownList ID="ddlTraySize" runat="server" OnSelectedIndexChanged="ddlTraySize_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd"></asp:DropDownList>
            </div>

            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>Seeds Allocated </label>
                <asp:DropDownList ID="ddlSeedAllocated" runat="server" OnSelectedIndexChanged="ddlSeedAllocated_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                </asp:DropDownList>

            </div>


        </div>
        <div class="row mb-1 mb-md-4 align-items-end">

            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>From Date</label>
                <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <label>To Date </label>
                <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
            </div>
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" CssClass="bttn bttn-primary bttn-action"></asp:Button>
                <asp:Button ID="btnSearchReset" OnClick="btnSearchReset_Click" runat="server" Text="Reset" CssClass="bttn bttn-primary bttn-action"></asp:Button>

            </div>

        </div>



        <div class="row">
            <div class="col-xl-2 col-md-4 col-sm-6 mb-3">
                <asp:Label ID="lblTotal" ForeColor="#488949" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table data__table-height">


                            <asp:GridView ID="DGJob" runat="server" AutoGenerateColumns="False"
                                class="striped"
                                GridLines="None" OnRowDataBound="DGJob_RowDataBound"
                                ShowHeaderWhenEmpty="True" Width="100%">

                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="autostyle2" ItemStyle-Width="5%">
                                        <HeaderTemplate>
                                            <div class="custom-control custom-checkbox mr-3">
                                                <asp:CheckBox ID="CheckBoxall" class="custom-control custom-checkbox" Text=" " Checked="true" AutoPostBack="true" OnCheckedChanged="chckchanged1" runat="server" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="custom-control custom-checkbox mr-3">
                                                <asp:CheckBox runat="server" class="custom-control custom-checkbox" Text=" " Checked="true" ID="chkSelect"></asp:CheckBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="70px" HeaderText="Seedline Facility">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeedline" runat="server" Text='<%# Eval("loc") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("jobcode") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itmdescp") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("cname") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Soil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoil" runat="server" Text='<%# Eval("Soil") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Order Seed Date" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSODate" runat="server" HtmlEncode="false" Text='<%# Eval("sodate","{0:MM/dd/yyyy}") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Sales Order Trays">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSO_Tray" runat="server" Text='<%# Eval("sotrays","{0:####}") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tray Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("ts") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="146px" HeaderText="Putaway Facility">
                                        <ItemTemplate>
                                            <%-- <asp:TextBox ID="txtSeedline" runat="server" Text='<%# Eval("loc") %>' Width="50"></asp:TextBox>--%>
                                            <asp:Label ID="lbl_Seedline" Visible="false" Text='<%# Eval("loc") %>' runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlBenchLocation" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="70px" HeaderText="Work order Trays">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Txtgtrays" class="input__control robotomd" Width="100px" Text='<%# Eval("wotrays","{0:####}") %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scheduled Seed Date" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Txtgplantdt" TextMode="Date" class="input__control robotomd" Text='<%# Eval("wodate","{0:yyyy-MM-dd}") %>' runat="server"></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="70px" HeaderText="Seeds Allocated">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllocated" runat="server" Text='<%# Eval("alloc") %>'></asp:Label>
                                            <asp:HiddenField ID="HiddenFieldsotrays" Value='<%# Eval("sotrays") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenFielditm" Value='<%# Eval("itm") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenFieldcusno" Value='<%# Eval("cusno") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenFieldsodate" Value='<%# Eval("sodate","{0:yyyy-MM-dd}") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenFieldduedate" Value='<%# Eval("duedate","{0:yyyy-MM-dd}") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenFieldwo" Value='<%# Eval("wo") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenFieldGenusCode" Value='<%# Eval("GenusCode") %>' runat="server" />


                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                </div>
            </div>

        </div>

        <div class="text-left dashboard__block my-4">

            <div class="row justify-content-center">
                <div class="col-12">

                    <div class="row">
                        <div class="col-12 my-3">

                            <asp:Button Text="Post & Send" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />
                            <asp:Button Text="Print" ID="BtnPrint" Visible="false" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="BtnPrint_Click" />
                            <asp:Button Text="Cancel" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-12 my-3">
                            <asp:Label ID="Label1" runat="server" ForeColor="#488949" Text="Seedline supervisors at each facility sent jobs with WO trays and plan dates filled out once submitted.​"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


    <div class="d-none d-print-block">
        <div class="page__us py-4 px-2">
            <div class="container-fluid">

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
                </div>

                <asp:Panel runat="server" ID="Panel1">
                    <asp:Repeater ID="repReport" runat="server" OnItemDataBound="repReport_ItemDataBound">
                        <ItemTemplate>


                            <div class="page-break">
                                <div class="row mt-4">
                                    <div class="d-flex align-items-center mb-1 col-12">
                                        <label class="d-block inline__fields mr-3 mb-0">Deptartment:</label>
                                        <div class="field__blank">SEEDLINE-<asp:Label runat="server" ID="lblFacility" Text='<%# Eval("loc_seedline") %>' /></div>
                                    </div>
                                    <div class="d-flex align-items-center mb-1 col-12">
                                        <label class="d-block inline__fields mr-3 mb-0">Submit Date:</label>
                                        <div class="field__blank">
                                            <asp:Label runat="server" ID="lblDate" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>' />
                                        </div>
                                    </div>
                                    <div class="d-flex align-items-center mb-1 col-12">
                                        <label class="d-block inline__fields mr-3 mb-0">Operator:</label>
                                        <div class="field__blank"></div>
                                    </div>
                                </div>

                                <div class="data__table">
                                    <asp:GridView ID="DGJob" runat="server" AutoGenerateColumns="False"
                                        class="striped" OnRowDataBound="DGJob_RowDataBound1"
                                        GridLines="None" HeaderStyle-BackColor="#489d48" HeaderStyle-ForeColor="#ffffff"
                                        ShowHeaderWhenEmpty="True" Width="100%">

                                        <Columns>
                                            <%-- <asp:TemplateField HeaderText="DATE"  HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="SrNo" runat="server" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="SCHEDULED SEED DATE" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblplan_date" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    <asp:Label ID="lblCreateDate" Visible="false" runat="server" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    <asp:Label ID="lbldue_date" Visible="false" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}") %>'></asp:Label>



                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="JOB" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("jobcode") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CUSTOMER" HeaderStyle-Width="180px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("cname") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ITEM DESCRIPTION" HeaderStyle-Width="180px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itemdescp") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderStyle-Width="60px" HeaderText="LOC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Seedline" Text='<%# Eval("loc_seedline") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="TRAY SIZE" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("traysize") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrays" runat="server" Text='<%# Eval("trays_actual") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SOIL" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSoil" runat="server" Text='<%# Eval("Soil") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="GOING OUT" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BACK" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GH-SIGNATURE" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                
                                                   <asp:TemplateField HeaderText="SEEDLINE SIGNATURE" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                
                                                --%>
                                            <asp:TemplateField HeaderText="DAY EARLY/LATE" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDaysEarly" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GREENHOUSE DAYS" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGreenhouseDays" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                         
                                            <asp:TemplateField HeaderText="COMMENTS" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>


                                <asp:Panel ID="PanelView" Visible="false" runat="server">
                                    <div class="page-break">
                                        <div class="row mt-4">
                                            <div class="d-flex align-items-center mb-1 col-12">
                                                <label class="d-block inline__fields mr-3 mb-0">Deptartment:</label>
                                                <div class="field__blank">SEEDLINE-<asp:Label runat="server" ID="Label1" Text='<%# Eval("loc_seedline") %>' /></div>
                                            </div>
                                            <div class="d-flex align-items-center mb-1 col-12">
                                                <label class="d-block inline__fields mr-3 mb-0">Submit Date:</label>
                                                <div class="field__blank">
                                                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>' />
                                                </div>
                                            </div>
                                            <div class="d-flex align-items-center mb-1 col-12">
                                                <label class="d-block inline__fields mr-3 mb-0">Operator:</label>
                                                <div class="field__blank"></div>
                                            </div>
                                        </div>


                                        <div class="data__table">
                                            <asp:GridView ID="DGJob1" runat="server" AutoGenerateColumns="False"
                                                class="striped" OnRowDataBound="DGJob1_RowDataBound"
                                                GridLines="None" HeaderStyle-BackColor="#489d48" HeaderStyle-ForeColor="#ffffff"
                                                ShowHeaderWhenEmpty="True" Width="100%">

                                                <Columns>
                                                    <%-- <asp:TemplateField HeaderText="DATE"  HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="SrNo" runat="server" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="SCHEDULED SEED DATE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplan_date1" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            <asp:Label ID="lblCreateDate1" Visible="false" runat="server" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            <asp:Label ID="lbldue_date1" Visible="false" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JOB" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobcode1" runat="server" Text='<%# Eval("jobcode") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CUSTOMER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustName1" runat="server" Text='<%# Eval("cname") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ITEM DESCRIPTION">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItem1" runat="server" Text='<%# Eval("itemdescp") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderStyle-Width="60px" HeaderText="LOC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Seedline" Text='<%# Eval("loc_seedline") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="TRAY SIZE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTraySize1" runat="server" Text='<%# Eval("traysize") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTrays1" runat="server" Text='<%# Eval("trays_actual") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SOIL" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSoil1" runat="server" Text='<%# Eval("Soil") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--   <asp:TemplateField HeaderText="GOING OUT" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BACK" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GH-SIGNATURE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        
                                                          <asp:TemplateField HeaderText="SEEDLINE SIGNATURE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    --%>
                                                    <asp:TemplateField HeaderText="DAY EARLY/LATE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDaysEarly1" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GREENHOUSE DAYS" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGreenhouseDays1" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="COMMENTS" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </asp:Panel>


                                <asp:Panel ID="PanelView1" Visible="false" runat="server">
                                    <div class="page-break">
                                        <div class="row mt-4">
                                            <div class="d-flex align-items-center mb-1 col-12">
                                                <label class="d-block inline__fields mr-3 mb-0">Deptartment:</label>
                                                <div class="field__blank">SEEDLINE-<asp:Label runat="server" ID="Label3" Text='<%# Eval("loc_seedline") %>' /></div>
                                            </div>
                                            <div class="d-flex align-items-center mb-1 col-12">
                                                <label class="d-block inline__fields mr-3 mb-0">Submit Date:</label>
                                                <div class="field__blank">
                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>' />
                                                </div>
                                            </div>
                                            <div class="d-flex align-items-center mb-1 col-12">
                                                <label class="d-block inline__fields mr-3 mb-0">Operator:</label>
                                                <div class="field__blank"></div>
                                            </div>
                                        </div>


                                        <div class="data__table">
                                            <asp:GridView ID="DGJob2" runat="server" AutoGenerateColumns="False"
                                                class="striped" OnRowDataBound="DGJob2_RowDataBound"
                                                GridLines="None" HeaderStyle-BackColor="#489d48" HeaderStyle-ForeColor="#ffffff"
                                                ShowHeaderWhenEmpty="True" Width="100%">

                                                <Columns>
                                                    <%-- <asp:TemplateField HeaderText="DATE"  HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="SrNo" runat="server" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="SCHEDULED SEED DATE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplan_date2" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            <asp:Label ID="lblCreateDate2" Visible="false" runat="server" Text='<%# Eval("createon","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            <asp:Label ID="lbldue_date2" Visible="false" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JOB" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobcode2" runat="server" Text='<%# Eval("jobcode") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CUSTOMER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustName2" runat="server" Text='<%# Eval("cname") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ITEM DESCRIPTION">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItem2" runat="server" Text='<%# Eval("itemdescp") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderStyle-Width="60px" HeaderText="LOC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Seedline" Text='<%# Eval("loc_seedline") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="TRAY SIZE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTraySize2" runat="server" Text='<%# Eval("traysize") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTrays2" runat="server" Text='<%# Eval("trays_actual") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SOIL" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSoil2" runat="server" Text='<%# Eval("Soil") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="GOING OUT" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BACK" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GH-SIGNATURE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        
                                                    <asp:TemplateField HeaderText="SEEDLINE SIGNATURE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        
                                                    --%>
                                                    <asp:TemplateField HeaderText="DAY EARLY/LATE" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDaysEarly2" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GREENHOUSE DAYS" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGreenhouseDays2" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="COMMENTS" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>



                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <div class="page__us--footer">
                    <div class="d-flex align-items-center">
                        <label class="d-block inline__fields inline__fields-signs mr-3 mb-0">Seed Deliver Sign.</label>
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
        </div>
    </div>


</asp:Content>
