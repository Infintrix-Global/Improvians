<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSeedlineReport.aspx.cs"  MasterPageFile="~/EvoMaster.Master"  Inherits="Evo.PrintSeedlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          <div class="site__container">
                <h2 class="text-center">Print Reports</h2>

                <div class="print__reports text-center mt-5">
                    <div class="card d-inline-block">
                        <div class="card-body">
                            <div class="row text-left">
                                <div class="col-auto">
                                    <label>Select Date</label>
                                    <asp:DropDownList runat="server" ID="ddlDate" DataTextField="createon"  DataTextFormatString="{0:MM/dd/yyyy}"  DataValueField="createon" class="custom__dropdown">                            
                                    </asp:DropDownList>
                                </div>
                                <div class="col-auto align-self-end">
                                    <asp:Button id="btnSubmit" CssClass="bttn bttn-primary bttn-action" OnClick="btnSubmit_Click" runat="server" Text="Print" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

</asp:Content>