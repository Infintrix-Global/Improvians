<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WDemoTest.aspx.cs" Inherits="Evo.WDemoTest" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:DropDownCheckBoxes ID="DropDown1" runat="server" AddJQueryReference="true" UseButtons="True"
                UseSelectAllNode="True" AutoPostBack="false" DataTextField="CityName"
                RepeatDirection="Horizontal">
            </asp:DropDownCheckBoxes>

            <asp:DropDownCheckBoxes ID="DropDownCheckBoxes2" runat="server" Width="180px" UseSelectAllNode="false">
                <Style SelectBoxWidth="195" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="90" />
                <Items>
                    <asp:ListItem Text="Mango" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Apple" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Banana" Value="3"></asp:ListItem>
                </Items>
            </asp:DropDownCheckBoxes>


            <cc1:DropDownCheckBoxes ID="DropDownCheckBoxes1" runat="server">

                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
                <asp:ListItem Text="A"></asp:ListItem>
            </cc1:DropDownCheckBoxes>

        </div>


    </form>
</body>
</html>

