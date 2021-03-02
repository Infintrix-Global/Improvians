<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="CropHealthReport.aspx.cs" Inherits="Improvians.CropHealthReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            console.log('onReady');
            $("#takePictureField").on("change", gotPic);
        });

        function gotPic(event) {
            if (event.target.files.length == 1 &&
                event.target.files[0].type.indexOf("image/") == 0) {
                $("#yourimage").attr("src", URL.createObjectURL(event.target.files[0]));

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="file" accept="image/*;capture=camera" id="takePictureField" name="takePictureField" runat="server" />
    <div class="row">
        <div class="col m6">
            <img id="yourimage" runat="server" width="320" height="240" />
        </div>
    </div>
</asp:Content>
