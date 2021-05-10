<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GeneralTaskRequestForm.aspx.cs" Inherits="Evo.GeneralRequestForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2 class="head__title-icon">
            <img src="./images/dashboard_dump-request.png" width="137" height="132" alt="Plant Ready">
            General Task
        </h2>

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
                <div class="col-auto">
                    <br />
                    <asp:Button Text="Reset" ID="btnResetSearch" CssClass="bttn bttn-primary bttn-action" runat="server" OnClick="btnResetSearch_Click" />
                </div>
            </div>
        </div>

        <%-- <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>--%>

        <div class="data__table">
            <asp:GridView ID="gvTask" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="gvTask_RowDataBound"
                class="striped" AllowSorting="true" PageSize="10" OnPageIndexChanging="gvTask_PageIndexChanging"
                GridLines="None" OnRowCommand="gvTask_RowCommand" DataKeyNames="wo,ID,jid,jobcode,GreenHouseID"
                ShowHeaderWhenEmpty="True" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblGreenHouseID" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                            <asp:Label ID="lblGenusCode" runat="server" Text='<%# Eval("GenusCode")  %>' Visible="false"></asp:Label>
                            <%--<asp:Label ID="lblWo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>--%>
                            <asp:Label ID="lbljobID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomer" data-head="Customer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" data-head="Plant Type" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Trays" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblTrays" runat="server" Text='<%# Eval("Trays")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plant Due Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblPlantDueDate" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="General Task Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblGDate" runat="server" Text='<%# Eval("GeneralTaskDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Task Type" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskType" runat="server" Text='<%# Eval("TaskType")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Move From" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblMoveF" runat="server" Text='<%# Eval("MoveFrom")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Move To" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblMoveT" runat="server" Text='<%# Eval("MoveTo")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblComs" runat="server" Text='<%# Eval("Comments")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Job Source" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblsource" data-head="Job Source" runat="server" Text='<%# Eval("RequestType")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedBy" data-head="Assigened By" runat="server" Text='<%# Eval("AssignedBy")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Button ID="btnSelect" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                            <asp:Button ID="btnReschdule" runat="server" Text="Reschedule" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Reschdule" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                            <asp:Button ID="btndismiss" runat="server" Text="Dismiss" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Dismiss" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
                            <asp:Button ID="btnStart" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Start" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
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
                    <div class="row">
                        <div class="mb-3 col-12 col-md-auto">
                            <label>Assignment </label>
                            <asp:HiddenField ID="HiddenFieldJid" runat="server" />
                            <asp:HiddenField ID="HiddenFieldDid" runat="server" />
                            <%--<asp:Label ID="lblSupervisorID" runat="server" Visible="false"></asp:Label>--%>
                            <asp:DropDownList ID="ddlGeneralAssignment" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
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
                            <%--<span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>--%>
                        </div>

                        <div class="mb-3 col-12 col-md-auto">
                            <label class="d-block">General Task Date</label>
                            <asp:TextBox ID="txtGeneralDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="mb-3 col-12 col-md-auto">
                            <label>Comments </label>
                            <asp:TextBox ID="txtCommentsGeneral" TextMode="MultiLine" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="col-xl-3" id="divFrom" style="display: none;" runat="server">
                            <label>From</label>
                            <asp:TextBox ID="txtFrom" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>

                        <div class="col-xl-3" id="divTo" style="display: none;" runat="server">
                            <label>To</label>
                            <asp:TextBox ID="txtTo" runat="server" CssClass="input__control"></asp:TextBox>
                        </div>
                        <div class="mb-3 col-12 col-md-auto align-self-end">
                            <asp:Button Text="Submit" ID="btnGeneralSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />
                            <asp:Button Text="Reset" ID="btnGeneralReset" runat="server" CssClass="submit-bttn bttn bttn-primary" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<%--<span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlAssignments" ValidationGroup="x"
                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Assignment" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>--%>