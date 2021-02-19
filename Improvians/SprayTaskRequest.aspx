<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SprayTaskRequest.aspx.cs" Inherits="Improvians.SprayTaskRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
     <div class="header__bottom">
        <div class="header__tabs">
            <ul class="d-flex align-items-center justify-content-center list-inline">
                 <li><a href="#" class="bttn active" title="My Task">My Tasks</a></li>
                <li><a href="#" class="bttn" title="Job Reports">Job Reports</a></li>
            </ul>
        </div>
    </div>
    <div class="main">
        <div class="site__container">
            <h2 class="text-left">Fertilization / Chemical Task Completion </h2>
          
            <br />

            
            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvSpray" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" PageSize="10" DataKeyNames="FertilizationCode" OnRowDataBound="gvSpray_RowDataBound"
                                    GridLines="None" OnRowCommand="gvSpray_RowCommand" OnPageIndexChanging="gvSpray_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                         <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                    <asp:Label ID="lblFertilizationCode" Visible="false" runat="server" Text='<%#Bind("FertilizationCode") %>'></asp:Label>
                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                              <asp:Panel ID="Panel11" runat="server" >

                                                    <asp:GridView ID="GridViewFShow" class="table table-bordered table-hover"
                                                        AutoGenerateColumns="false" runat="server">
                                                        <Columns>

                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblGreenHouseID" runat="server" Text='<%#Bind("GreenHouseID") %>'></asp:Label>

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
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                              <asp:Panel ID="Panel1" runat="server">

                                                    <asp:GridView ID="GridViewDetails" class="table table-bordered table-hover"
                                                        AutoGenerateColumns="false" runat="server">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fertilizer" HeaderStyle-Width="40%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFertilizer" runat="server" Text='<%#Bind("Fertilizer") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%#Bind("Unit") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tray" HeaderStyle-Width="15%">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblTray" runat="server" Text='<%#Bind("Tray") %>'></asp:Label>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SQFT" HeaderStyle-Width="10%">
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
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>


                                                
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
                </div>

            </div>
            <div class="dashboard__block dashboard__block--asign">


                <div id="userinput" runat="server" visible="false">

                    <asp:Panel ID="pnlint" runat="server">



                        <div class="row">

                            <div class="col-md-auto">
                              <%--  <label>Job No.</label><br />--%>


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
