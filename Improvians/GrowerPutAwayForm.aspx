<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GrowerPutAwayForm.aspx.cs" Inherits="Improvians.GetGrowerPutAwayForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="site__container">
            <h2>Put Away Location Assignment</h2>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <asp:Panel ID="PanelList" runat="server">
                                <div class="data__table">
                                    <asp:GridView ID="gvGerm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" AllowSorting="true" OnRowCommand="gvGerm_RowCommand"
                                        GridLines="None" PageSize="10" OnPageIndexChanging="gvGerm_PageIndexChanging"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                                                    <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Customer">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Planned Trays">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTraysSeeded" runat="server" Text='<%# Eval("trays_plan")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Actual Trays">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActualTraySeeded" runat="server" Text='<%# Eval("ActualTraySeeded")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Planned Due Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSeededPlanDate" runat="server" Text='<%# Eval("plan_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seeding Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSeededDate" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Put Away Facility">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblloc_seedline" runat="server" Text='<%# Eval("loc_seedline")  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAssign" CommandName="Assign" CssClass="bttn bttn-primary bttn-action" Text="Start " runat="server" CommandArgument='<%# Eval("wo")  %>'></asp:Button>
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
                            </asp:Panel>
                            <asp:Panel ID="PanelAdd" Visible="false" runat="server">
                                <div class="dashboard__block dashboard__block--asign">
                                  
                                            <div class="row">

                                                <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                                                    <label>Job No.</label><br />

                                                    <asp:Label ID="lblJobID" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                                                    <label class="d-block">Seed Date</label>

                                                    <asp:Label ID="lblSeedDate" runat="server"></asp:Label>
                                                </div>

                                                <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                                                    <label class="d-block">Seeded Trays</label>
                                                    <asp:Label ID="lblSeededTrays" runat="server"></asp:Label>

                                                </div>

                                                
                                                <div class="col-12 col-sm-7 col-md-5 col-lg-4 col-xl-3">
                                                    <label class="d-block">Remaining Seeded Trays</label>
                                                    <asp:Label ID="lblRemaining" runat="server"></asp:Label>

                                                </div>
                                            </div>

                                            <div class="data__table">


                                                <asp:GridView ID="GridSplitJob" runat="server" ShowFooter="true" Width="80%"
                                                    AutoGenerateColumns="false" OnRowDataBound="GridSplitJob_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumber" HeaderText="NO." />
                                                        <asp:TemplateField HeaderText="Put Away Facility" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnWOEmployeeID" runat="server" Value='<%# Eval("ID")%>'></asp:HiddenField>

                                                                <asp:DropDownList ID="ddlMain" OnSelectedIndexChanged="ddlMain_SelectedIndexChanged" AutoPostBack="true" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>


                                                                <asp:Label ID="lblMain" Visible="false" runat="server" Text='<%# Eval("FacilityID")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bench Location">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlLocation" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>
                                                                <asp:Label ID="lblLocation" Visible="false" runat="server" Text='<%# Eval("GreenHouseID")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Trays">
                                                            <ItemTemplate>

                                                                <asp:TextBox ID="txtTrays" OnTextChanged="txtTrays_TextChanged" AutoPostBack="true" Text='<%# Eval("Trays")%>' CssClass="input__control" runat="server"></asp:TextBox>


                                                            </ItemTemplate>


                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="ButtonAdd" OnClick="ButtonAddGridInvoice_Click" runat="server" Width="300px" CausesValidation="false"
                                                                    Text="Add Put Away Location"  CssClass="bttn bttn-primary bttn-action" />
                                                            </FooterTemplate>



                                                        </asp:TemplateField>

                                                    </Columns>


                                                </asp:GridView>
                                            </div>


                                     
                                    <div class="row">



                                        <div class="col-auto">
                                            <br />
                                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action" OnClick="btnSubmit_Click" runat="server" />
                                        </div>
                                        <div class="col-auto">
                                            <br />
                                            <asp:Button Text="Reset" ID="btnReset" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" runat="server" />
                                        </div>
                                    </div>
                                   
                                    <div class="row">



                                        <div class="col-auto">
                                             <br />
                                            <asp:Label ID="Label1" runat="server" ForeColor="#488949" Text="Assigned to site move team once submitted.​"></asp:Label>
                                        </div>
                                      
                                    </div>
                                </div>

                               
                            </asp:Panel>
                        </div>

                    </div>
                </div>

            </div>


        </div>
    </div>
</asp:Content>
