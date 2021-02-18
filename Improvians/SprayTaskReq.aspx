<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SprayTaskReq.aspx.cs" Inherits="Improvians.SprayTaskReq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/JS1/jquery.min.js") %>"></script>

    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "<%= ResolveUrl("~/Images/minus.png") %>");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "<%= ResolveUrl("~/Images/plus.png") %>");
            $(this).closest("tr").next().remove();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main">
        <div class="site__container">
            <h2 class="text-left">Spray Request </h2>
            <%-- <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>--%>

         <%--   <div class="row">
                <div class="col m3">
                    <label>Customer </label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <%--   <div class="col m3">
                        <label>GreenHouse </label>
                        <asp:DropDownList ID="ddlGreenhouse" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                <div class="col m3">
                    <label>Put away Facility </label>
                    <asp:DropDownList ID="ddlFacility" AutoPostBack="true" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
                <div class="col m3">
                    <label>Job No </label>
                    <asp:DropDownList ID="ddlJobNo" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>

                <div class="col m3">
                    <label>Bench Location </label>
                    <asp:DropDownList ID="ddlBenchLocation" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged1" AutoPostBack="true" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                </div>
            </div>--%>
            <br />

            <div class="row">
                <div class="col m3">
                </div>

                <div class="col m3">
                </div>
                <div class="col m3">
                </div>

                <div class="col m3">
                    <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />

                </div>
            </div>
            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvSpray" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" PageSize="10" DataKeyNames="wo,jobcode,GrowerPutAwayId" OnRowDataBound="gvSpray_RowDataBound"
                                    GridLines="None" OnRowCommand="gvSpray_RowCommand" OnPageIndexChanging="gvSpray_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>


                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                                   <asp:Label ID="lblFertilizationId" runat="server" Text='<%# Eval("FertilizationId")  %>' Visible="false"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Put Away Main Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

<%--                                        <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType12" runat="server" Text='<%# Eval("Type")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

                                                <img alt="" style="cursor: pointer" src="../Images/plus.png" runat="server" id="img" />


                                                <asp:Panel ID="Panel1" runat="server" Style="display: none">

                                                    <asp:GridView ID="GridViewDetails" class="table table-bordered table-hover"
                                                        AutoGenerateColumns="false" runat="server">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fertilizer">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblFertilizer" runat="server" Text='<%#Bind("Fertilizer") %>'></asp:Label>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%#Bind("Unit") %>'></asp:Label>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tray">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblTray" runat="server" Text='<%#Bind("Tray") %>'></asp:Label>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SQFT">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblSQFT" runat="server" Text='<%#Bind("SQFT") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                                        <PagerSettings Mode="NumericFirstLast" />
                                                        <EmptyDataTemplate>
                                                            No Record Available
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>

                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
                </div>

            </div>
            <div class="dashboard__block dashboard__block--asign">


                <div id="userinput" runat="server" >

                    <asp:Panel ID="pnlint" runat="server">



                        <div class="row">

                            <div class="col-md-auto">
                               <%-- <label>Job No.</label><br />--%>


                                <h3 class="robotobold">
                                    <asp:Label ID="lblJobID" runat="server"></asp:Label></h3>
                                <asp:Label ID="lblGrowerID" Visible="false" runat="server"></asp:Label>


                            </div>

                            <div class="col-md-auto">
                                <label class="d-block">Spray Date</label>

                                <asp:TextBox ID="txtSprayDate" class="input__control input__control-auto" TextMode="Date" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-md-auto">
                                <asp:TextBox ID="txtNotes" TextMode="MultiLine" class="w-100 input__control" placeholder="Notes" runat="server"></asp:TextBox>
                            </div>


                        </div>

                        <div class="row">


                            <div class="row align-items-center mt-sm-3">


                                <div class="col-12 my-3">
                                    <asp:Button Text="Submit" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />

                                    <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>


            <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>
</asp:Content>
