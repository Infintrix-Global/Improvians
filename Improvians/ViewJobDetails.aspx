<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="ViewJobDetails.aspx.cs" Inherits="Evo.ViewJobDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="site__container">
        <h2 class="text-left mb-3">Chemical Task View </h2>
        <div class="row">
            <div class="col-lg-6">
                <h3 class="robotobold">
                    <label>Bench Location</label><br />
                    <asp:Label ID="lblBenchLocation" runat="server" Text=""></asp:Label>
                </h3>
            </div>
        </div>
        <br />
        <div class="row">
            <div class=" col m12">
                <div class="portlet light ">
                    <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                    <div class="portlet-body">
                        <div class="data__table">
                        </div>
                    </div>

                </div>
            </div>

        </div>
        <div class="dashboard__block dashboard__block--asign">


            <div id="userinput" runat="server">
                <asp:Panel ID="PanelCropHealth" Visible="false" runat="server">
                    <br />
                    <h2 class="text-left">Crop Health Report </h2>
                    <div class="portlet-body">
                        <div class="data__table">
                        </div>
                        <div class="row">

                            <div class="col-lg-12">
                                <asp:Label ID="lblCommment" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlint" runat="server">

                    <div class="portlet-body">
                        <div class="data__table">
                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>


        <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
</asp:Content>
