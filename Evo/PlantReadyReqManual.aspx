<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="PlantReadyReqManual.aspx.cs" Inherits="Evo.PlantReadyReqManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2>Plant Ready</h2>

        <div class="filter__row d-flex">
            <div class="row">
                <div class="col m3">
                    <label>Customer </label>
                    <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <div class="col m3">
                    <label>Job No </label>
                    <asp:DropDownList ID="ddlJobNo" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <div class="col m3">
                    <br />
                    <asp:Button Text="Reset" ID="btnResetSearch" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                </div>
            </div>
        </div>

        <%-- <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>--%>

        <div class="data__table">
            <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                GridLines="None" OnRowCommand="gvPlantReady_RowCommand" DataKeyNames="wo,jobcode,GrowerPutAwayId"
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


                    <%--<asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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

                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
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

        <div class="text-left dashboard__block my-4">

            <div id="userinput" runat="server" visible="false" class="row justify-content-center">
                <div class="col-12">
                    <h3>User Inputs:</h3>
                    <div class="row">

                        <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                            <label>Job No.</label><br />
                            <h3 class="robotobold">
                                <asp:Label ID="lblJobID" runat="server"></asp:Label></h3>
                            <asp:Label ID="lblGrowerID" Visible="false" runat="server"></asp:Label>
                        </div>
                        <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                            <label class="d-block">Assign task to Greenhouse Supervisor</label>

                            <asp:DropDownList ID="ddlSupervisor" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        </div>

                        <div class="col-12 my-3">

                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />
                            <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />
                        </div>
                    </div>

                </div>
            </div>

        </div>

    </div>
</asp:Content>
