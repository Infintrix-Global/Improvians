<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GrowersTrans.aspx.cs" Inherits="Evo.GrowersTrans" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Home - Growerstrans</title>

    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap" rel="stylesheet">

    <style>
        * { box-sizing: border-box; }
        body {
            font-family: 'Roboto', sans-serif;
            position: relative;
            border: 0;
            margin: 0;
            min-height: 100vh;
            color: #505050;
            background: #f4f6fa;
            overflow: auto;
        }

        .growerstrans img {
            width: 100%;
            max-width: 100%;
            height: auto;
            vertical-align: top;
        }

        .growerstrans a {
            position: fixed;
            top: 5vh;
            right: 2vw;
            font-size: 1.1vw;
            text-decoration: none;
            padding: 0;
            background: white;
            color: #558b2f;
            font-weight: 500;
            width: 10vw;
            height: 2.8vw;
            line-height: 2.8vw;
            text-align: center;
            border-radius: 15px;
        }
    </style>
</head>

<body>
    <div class="d-flex growerstrans">
        <img class="img-fluid" src="images/growerstrans.png" width="1920" height="903" alt="Growerstrans" />
        <a href="CustomerLogin.aspx">
            Customer Portal
        </a>
    </div>
</body>
</html>
