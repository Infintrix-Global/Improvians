<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="MyTaskGreenOperatorFinal.aspx.cs" Inherits="Evo.MyTaskGreenOperatorFinal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site__container">
        <h2>My Task</h2>

        <div class="mt-4 row">
            <%--  <div class="col m4">
                <label>Department </label>
                <asp:DropDownList ID="ddlDept" runat="server" class="custom__dropdown robotomd">
                    <asp:ListItem Text="Grower" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Greenhouse Support" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Transplanting Team" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Seedline" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>--%>
            <div class="col-md-4 col-lg-3 mb-3">
                <label>Task Request Form </label>
                <asp:DropDownList ID="ddlTaskRequest" AutoPostBack="true" runat="server" class="custom__dropdown robotomd" OnSelectedIndexChanged="ddlTaskRequest_SelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Germination Request" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Plant Ready Request" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Irrigation Request" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div> <!-- row ends -->
    </div>
</asp:Content>
