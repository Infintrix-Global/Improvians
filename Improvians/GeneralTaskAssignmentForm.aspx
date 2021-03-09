<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="GeneralTaskAssignmentForm.aspx.cs" Inherits="Improvians.GeneralTaskAssignmentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="main__header">
        <div class="site__container">
            <h2 class="head__title-icon">
                <img src="./images/dashboard_general-task.png" width="137" height="132" alt="Plant Ready">
                General Task</h2>
              <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table">
                                <asp:GridView ID="gvTask" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                                    class="striped" AllowSorting="true" PageSize="20" 
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>                               
                                                                       
                                       <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("comments")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                



                                        <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>                                                
                                                <asp:Button ID="btnSelect" runat="server" Text="Start" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Select" ></asp:Button>
                                                <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="bttn bttn-primary bttn-action my-1" CommandName="Assign" ></asp:Button>                                                    
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
            </div>
          </div>
</asp:Content>


 
