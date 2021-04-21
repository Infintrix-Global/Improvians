<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GeneralTaskCompletion.aspx.cs" Inherits="Evo.GeneralTaskCompletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
            <h2 class="head__title-icon">
                <img src="images/dashboard_general-task.png" width="137" height="132" alt="General Task" />
                General Task Completion
            </h2>

            <div class="data__table">
                <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                <asp:GridView ID="gvTask" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    class="striped" AllowSorting="true"
                    GridLines="None" PageSize="10" OnPageIndexChanging="gvTask_PageIndexChanging"
                    ShowHeaderWhenEmpty="True" Width="100%">
                    <Columns>

                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                                <%--                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblGreenHouseID" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Seed Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                         
                                 <asp:Label ID="Label20" runat="server" Text='<%# Eval("SeedDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("TaskType")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Move From" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label21" runat="server" Text='<%# Eval("MoveFrom")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Move To" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label22" runat="server" Text='<%# Eval("MoveTo")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="General Task Date" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("GeneralTaskDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
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
                <asp:Panel ID="PanelCropHealth" Visible="false" runat="server">
                    <br />
                    <h2 class="text-left">Crop Health Report </h2>

                    <br />
                    <div class="portlet-body">
                        <div class="data__table">
                            <asp:GridView ID="gvCropHealth" runat="server" AutoGenerateColumns="False"
                                class="striped" AllowSorting="true"
                                GridLines="None" DataKeyNames="chid"
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Type of Problem" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("typeofProblem")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cause of Problem" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("Causeofproblem")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Severity of Problem" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitem" runat="server" Text='<%# Eval("Severityofproblem")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. of Trays" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("NoTrays")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="% of Damage" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("PerDamage")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="New Estimated Ship Date" HeaderStyle-CssClass="autostyle2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("Date","{0:MM/dd/yyyy}")  %>'></asp:Label>
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
                    <div class="row">

                        <div class="col-lg-12">
                            <asp:Label ID="lblCommment" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </asp:Panel>

                 <br />
                <br />
                <form class="web__form pt-2">
                    <div class="row justify-content-center">
                        <div class="col-12">

                            <div class="row">
                                <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                                    <label class="d-block">General Task Date</label>

                                    <asp:TextBox ID="txtGeneralDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-4 col-lg-3 mb-3">
                                    <label>Task Type</label>

                                    <asp:DropDownList ID="ddlTaskType" runat="server" OnSelectedIndexChanged="ddlTaskType_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd">
                                        <asp:ListItem Text="--Select--" Value="0" />
                                        <asp:ListItem Text="Add Bird Netting" Value="1" />
                                        <asp:ListItem Text="Remove Bird Netting" Value="2" />
                                        <asp:ListItem Text="Move" Value="3" />
                                        <asp:ListItem Text="Other" Value="4" />
                                    </asp:DropDownList>

                                </div>

                                <%--  <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                                    <label class="d-block">Quantity Of Tray</label>
                                    <asp:TextBox ID="txtQuantityOfTray" TextMode="Number" class="input__control" runat="server"></asp:TextBox>
                                </div>--%>

                                <div class="col-12 col-sm-6 col-lg-3">
                                    <label class="d-block">Comments</label>
                                    <asp:TextBox ID="txtComment" TextMode="MultiLine" class="input__control" runat="server"></asp:TextBox>
                                </div>


                                <div class="d-none d-sm-block w-100"></div>
                                <div class="col-xl-3" id="divFrom" style="display: none;" runat="server">
                                    <label>From</label>
                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="input__control"></asp:TextBox>
                                </div>

                                <div class="col-xl-3" id="divTo" style="display: none;" runat="server">
                                    <label>To</label>
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="input__control"></asp:TextBox>
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
</asp:Content>
