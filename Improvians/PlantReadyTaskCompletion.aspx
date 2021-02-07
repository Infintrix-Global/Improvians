<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PlantReadyTaskCompletion.aspx.cs" Inherits="Improvians.PlantReadyTaskCompletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main main__header">
        <div class="site__container">
            <h2>Plant Ready Completion</h2>

         

            <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>
            <div class="data__table">
                <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                <asp:GridView ID="gvPlantReady" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    class="striped" AllowSorting="true"
                    GridLines="None" PageSize="10" OnPageIndexChanging="gvPlantReady_PageIndexChanging"
                    ShowHeaderWhenEmpty="True" Width="100%">
                    <Columns>

                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--                                        <asp:TemplateField HeaderText="Main Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("PutAwayMainLocation")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("trays_actual")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SoDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
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
                <form class="web__form pt-2">
                    <div class="row justify-content-center">
                        <div class="col-12">
                            <h3>User Inputs:</h3>
                            <div class="row">



                                <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                                    <label class="d-block">Updated Ready Date</label>

                                    <asp:TextBox ID="txtUpdatedReadyDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                                    <label class="d-block">Plant Expiration Date</label>
                                    <asp:TextBox ID="txtPlantExpirationDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-12 col-sm-6 col-lg-3">
                                    <label class="d-block">Root Quality</label>


                                    <asp:DropDownList ID="ddlRootQuality" class="custom__dropdown" runat="server">
                                        <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="3) GOOD" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2) WEAK" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="1) NO ROOT" Value="2"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>

                                <div class="col-12 col-sm-6 col-lg-3">
                                    <label class="d-block">Plant Height</label>
                                    <%--   <asp:TextBox ID="txtPlantHeight" class="input__control" runat="server"></asp:TextBox>
                                    --%>
                                    <asp:DropDownList ID="ddlPlantHeight" class="custom__dropdown" runat="server">
                                        <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="1.5" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="2.5" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="3.5" Value="6"></asp:ListItem>

                                        <asp:ListItem Text="4" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="4.5" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="5.5" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="6.5" Value="12"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                                <div class="col-12 col-sm-6 col-lg-4 col-xl-3">
                                    <label class="d-block">Notes</label>
                                    <asp:TextBox ID="txtNots" TextMode="MultiLine" class="w-100 input__control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-12 my-3">



                                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" class="ml-2 submit-bttn bttn bttn-primary" runat="server" Text="Submit" />
                                    <asp:Button ID="btnReset" OnClick="btnReset_Click" class="submit-bttn bttn bttn-primary" runat="server" Text="Reset" />
                               
                                    </div>
                            </div>

                        </div>
                    </div>
                </form>
            </div>

        </div>
    </div>

</asp:Content>
