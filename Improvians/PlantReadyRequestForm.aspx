<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="PlantReadyRequestForm.aspx.cs" Inherits="Evo.PlantReadyRequestForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main__header">
        <div class="site__container">
            <h2 class="head__title-icon">
                <img src="./images/dashboard_plant-ready.png" width="137" height="132" alt="Plant Ready">
                Plant Ready </h2>



            <div class="row">

                <div class="col-lg-3">
                    <label>Bench Location </label>

                    <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    <span class="error_message">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                            SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                    </span>
                </div>
                <div class="col-lg-3">
                    <label>Job No </label>
                    <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <div class="col-lg-3">
                    <label>Customer </label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>


            </div>
            <div class="row">
                <div class="col-lg-2">
                    <label>Job Source </label>
                    <asp:DropDownList ID="RadioButtonListSourse" runat="server" OnSelectedIndexChanged="RadioButtonListSourse_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                        <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Navision" Value="Manual"></asp:ListItem>
                        <asp:ListItem Text="App" Value="App"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-3">
                    <label>From Date</label>
                    <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    <label>To Date </label>
                    <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" class="input__control robotomd"></asp:TextBox>
                </div>
                <div class="col-lg-4">
                    <br />
                    <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearch_Click" />
                    <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnResetSearch_Click" />

                    <%--<asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" ValidationGroup="x" />--%>
                    <asp:Button ID="btnManual" runat="server" Visible="false" Text="Manual Request" CssClass="bttn bttn-primary bttn-action" OnClick="btnManual_Click" />
                    <%-- <asp:Button ID="btnJob" runat="server" Text="JobBuildUp" CssClass="bttn bttn-primary bttn-action" OnClick="btnJob_Click" />--%>
                </div>


            </div>


            <br />
            <%-- <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>--%>
            <div class="row">
                <div class=" col m12">
                    <div class="data__table">
                        <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                            GridLines="None" OnRowCommand="gvPlantReady_RowCommand" DataKeyNames="wo,jobcode,GrowerPutAwayId,PRRID,jid,IsAssistant,SeededDate,GreenHouseID,Trays,itemdescp"
                            ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>




                                <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>

                                        <asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Labeitemno" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Plant Ready Work Date" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                    <ItemTemplate>


                                        <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                          <asp:Button ID="btnStart" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1 mx-auto d-block w-100" CommandName="GStart" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

                                        <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                                        <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Eval("wo")  %>'></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>


                            <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <EmptyDataTemplate>
                                No Record Available
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="text-left dashboard__block my-4">

                <div id="userinput" runat="server" visible="false" class="row justify-content-center">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-6 col-sm-4 col-lg-3">
                                <label>Job No.</label><br />
                                <h4 class="robotobold">
                                   <asp:Label ID="lblJobID"  runat="server"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-6 col-sm-4 col-lg-3">
                                <label>Bench location</label><br />
                                <h4 class="robotobold">
                                    <asp:Label ID="lblBenchlocation" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-6 col-sm-4 col-lg-3">
                                <label>Total Trays</label><br />
                                <h4 class="robotobold">
                                    <asp:Label ID="lblTotalTrays" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-auto col-lg-3">
                                <label>Description </label>
                                <h4 class="robotobold">
                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                </h4>
                            </div>
                        </div>
                        <div class="row">


                            <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                <label class="d-block">Assignment</label>

                                <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>

                            </div>


                            <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                <label class="d-block">Plant Ready Work Date</label>
                                <asp:TextBox ID="txtPlantDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                            </div>

                            <div class="mb-3 mb-md-0 col-12 col-md-auto">
                                <label>Comments </label>

                                <asp:TextBox ID="txtPlantComments" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>

                            </div>
                            <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">

                                <h3 class="robotobold">
                                   </h3>
                                <asp:Label ID="lblGrowerID" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblPRRId" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblJid" Visible="false" runat="server"></asp:Label>
                                 <asp:Label ID="lblIsAssistant" Visible="false" runat="server"></asp:Label>


                            </div>
                            <div class="mb-3 mb-md-0 col-12 col-md-auto align-self-end">

                                <asp:Button Text="Submit" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />
                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>
    </div>

</asp:Content>
