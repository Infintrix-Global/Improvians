<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Evo.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>500 Server Error</title>
    

    <style>
        * { box-sizing: border-box; }
        body {
            font-family: 'Tahoma', sans-serif;
            border: 0;
            margin: 0;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #505050;
            background: #f4f6fa;
        }

        .error__page {
            padding: 20px 15px 0;
            text-align: center;
        }

        .error__page h1 {
            font-size: 220px;
            line-height: 230px;
            margin: 0;
        }

        h3 {
            text-transform: uppercase;
            font-size: 30px;
            line-height: 30px;
            margin: 20px auto;
        }

        h4 {
            margin-bottom: 35px;
        }

        .bttn {
            padding: 14px 30px;
            border: 0px solid;
            font-size: 16px;
            border-radius: 8px;
            background: #488949;
            color: #ffffff;
            text-align: center;
            outline: none;
            text-decoration: none;
            margin-bottom: 30px;
            display: inline-block;
            cursor: pointer;
        }

        @media screen and (max-width: 767px) {            
            .error__page h1 {
                font-size: 120px;
                line-height: 130px;
                margin: 0;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="d-flex error__page">
            <h1>500</h1>
            <h3>Server Error</h3>
            <h4>Something Went Wrong!!!</h4>
            <a href="javascript:history.back()" class="bttn bttn-primary">Go Back</a>
            <asp:HyperLink runat="server" ID="lnkBack" CssClass="bttn bttn-primary" Text="Click here to go back" />
        </div>
    </form>
</body>
</html>
