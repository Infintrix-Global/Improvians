<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Evo.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Something Went Wrong!!!
            <br />
               <a href="javascript:history.back()" title="Go Back" class="bttn bttn-icon bttn-back"><i class="fas fa-angle-left"></i></a>
            <asp:HyperLink runat="server" ID="lnkBack" Text="Click here to go back" />
        </div>
    </form>
</body>
</html>
