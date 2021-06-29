<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="IrrigationTaskCompletion.aspx.cs" Inherits="Evo.IrrigationCompletionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2>Irrigation Completion</h2>
        <div class="data__table">
            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
           
            <asp:GridView ID="gvIrrigation" Visible="false" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                class="striped" AllowSorting="true"
                GridLines="None" PageSize="10" OnPageIndexChanging="gvIrrigation_PageIndexChanging"
                ShowHeaderWhenEmpty="True" Width="100%">
                <Columns>

                    <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <%--  <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>--%>
                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>

                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />

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
                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblSprayDate" runat="server" Text='<%# Eval("SprayDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="No of Passes" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblWaterRequired" runat="server" Text='<%# Eval("WaterRequired")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Assigned By" HeaderStyle-CssClass="autostyle2">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName")  %>'></asp:Label>
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
          
            <div class="row justify-content-center">
                <div class="col-12">
                     <div class="row">



                        <div class="col-lg-6">
                            <h3 class="robotobold">
                                <label>Bench Location</label><br />
                                 <asp:Label runat="server"  Text="" ID="lblBenchLocation"></asp:Label>
                            </h3>
                        </div>

                        <div class="col-lg-6">
                        </div>



                    </div>

                    <br />
                  
                    <div class="row">

                        <div class="col-12 col-sm-5 col-md-4 col-lg-3">
                            <label class="d-block">Spray Completion Date </label>
                            
                            <asp:TextBox ID="txtSprayDate" TextMode="Date" class="input__control" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-sm col-sm-auto">
                            <label class="d-block">No of Passes</label>

                            <asp:TextBox ID="txtNoofPasses" class="input__control" placeholder="No of Passes" runat="server"></asp:TextBox>
                        </div>


                        <div class="col-12 my-3">
                            <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" class="ml-2 submit-bttn bttn bttn-primary" runat="server" Text="Submit" />
                            <asp:Button ID="btnReset" OnClick="btnReset_Click" class="submit-bttn bttn bttn-primary" runat="server" Text="Reset" />

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
