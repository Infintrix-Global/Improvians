<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="ChemicalTaskRequest.aspx.cs" Inherits="Evo.ChemicalTaskRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>   
    <div class="main">
        <div class="site__container">
            <h2 class="head__title-icon">
                <img src="./images/dashboard_fertilization-chemical.png" width="137" height="136" alt="Fertilization / Chemical">
                Chemical Task Completion </h2>

            <br />


            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvSpray" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true" PageSize="10" DataKeyNames="ChemicalCode" 
                                    GridLines="None" OnRowCommand="gvSpray_RowCommand" OnPageIndexChanging="gvSpray_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                <asp:Label ID="lblFertilizationCode" Visible="false" runat="server" Text='<%#Bind("ChemicalCode") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bench Location">
                                            <ItemTemplate>

                                                <asp:Label ID="lblGreenHouseID" runat="server" Text='<%#Bind("GreenHouseID") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Method">
                                            <ItemTemplate>

                                                <asp:Label ID="lblMethod" runat="server" Text='<%#Bind("Method") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chemical">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFertilizer" runat="server" Text='<%#Bind("Fertilizer") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    

                                        <asp:TemplateField HeaderText="Tray">
                                            <ItemTemplate>

                                                <asp:Label ID="lblTray" runat="server" Text='<%#Bind("Tray") %>'></asp:Label>


                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SQFT of Bench">
                                            <ItemTemplate>

                                                <asp:Label ID="lblSQFT" runat="server" Text='<%#Bind("SQFT") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                         

                                        <asp:TemplateField HeaderText=""  HeaderStyle-Width="21%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>
                                                <asp:Button ID="btnView" runat="server" Width="140px" Text="View Job Details" CssClass="bttn bttn-primary bttn-action my-1" CommandName="ViewDetails" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:Button>

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
            


            <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>


</asp:Content>
