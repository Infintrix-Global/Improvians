<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PullData.aspx.cs" Inherits="Evo.Admin.PullData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Reset and Pull Data</h1>

            <hr />


            <asp:Panel ID="pnlAdd" runat="server" >


                <div class="filter__row d-flex">
                    <div class="row">
                      




                            <div class="col-12 text-center order-md-1">
                                <asp:Button ID="btAdd" runat="server" Text="Reset and Pull Data"  class="submit-bttn bttn bttn-primary" OnClick="btAdd_Click" />

                               <asp:Button ID="btnPullData" runat="server" Text="Sync up Data"  class="submit-bttn bttn bttn-primary" OnClick="btnPullData_Click" />

                                <asp:Button ID="Button1" runat="server" Text="Pull Data Test" Visible="false"  class="submit-bttn bttn bttn-primary" OnClick="Button1_Click" />

                            </div>

                        </div>
                    
                </div>
            </asp:Panel>

        








        </div>
    </div>
</asp:Content>
