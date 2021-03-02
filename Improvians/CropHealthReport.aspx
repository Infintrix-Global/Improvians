<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="CropHealthReport.aspx.cs" Inherits="Improvians.CropHealthReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[id$=takePictureField]').on("change", gotPic);
        });

        function gotPic(event) {
            if (event.target.files.length == 1 &&
                event.target.files[0].type.indexOf("image/") == 0) {
                $('[id$=yourimage]').attr("src", URL.createObjectURL(event.target.files[0]));
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="main__header">
        <div class="site__container">
            <h2 class="head__title-icon">

                <img src="./images/dashboard_crop-health-report.png" width="137" height="136" alt="Fertilization / Chemical">
                Crop Health Report


            </h2>


            <div class="dashboard__block dashboard__block--asign">
                <div id="userinput" runat="server" class="assign__task d-flex" visible="true">
                    <asp:Panel ID="pnlint" runat="server">
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Type of problem</label><span style="color: red">*</span>
                                <asp:DropDownList ID="ddlpr" runat="server" class="custom__dropdown robotomd" AutoPostBack="true">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Germination</asp:ListItem>
                                    <asp:ListItem Value="2">Irrigation</asp:ListItem>
                                    <asp:ListItem Value="3">Seeding</asp:ListItem>
                                    <asp:ListItem Value="4">Soil</asp:ListItem>
                                </asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlpr" ValidationGroup="x"
                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>

                            <div class="col-lg-3">
                                <label>Cause of problem</label><span style="color: red">*</span>
                                <asp:DropDownList ID="DropDownListCause" runat="server" class="custom__dropdown robotomd" AutoPostBack="true">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Cause1</asp:ListItem>
                                    <asp:ListItem Value="2">Cause2</asp:ListItem>
                                    <asp:ListItem Value="3">Cause3</asp:ListItem>

                                </asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownListCause" ValidationGroup="x"
                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>

                            <div class="col-lg-3">
                                <label>Severity of problem</label><span style="color: red">*</span>
                                <asp:DropDownList ID="DropDownListSv" runat="server" class="custom__dropdown robotomd" AutoPostBack="true">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>

                                </asp:DropDownList>
                                <span class="error_message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownListSv" ValidationGroup="x"
                                        SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>


                        </div>


                        <div class="row">
                            <div class="col-lg-3">
                                <label>No. of Trays</label>
                                <asp:Label ID="lblUnMovedTrays" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtTrays" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                <span class="error_message">
                                    <asp:Label ID="lblerrmsg" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTrays" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Trays" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-lg-3">
                                <label>% of Damage</label>
                                <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="percentageDamage" TextMode="Number" runat="server" CssClass="input__control"></asp:TextBox>
                                <span class="error_message">
                                    <asp:Label ID="Label2" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTrays" ValidationGroup="md"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Trays" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="col-lg-3">
                                <label>Date </label>

                                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="input__control"></asp:TextBox>
                                <span class="error_message">
                                    <asp:Label ID="Label3" runat="server" ForeColor="red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDate" ValidationGroup="e"
                                        SetFocusOnError="true" ErrorMessage="Please Enter Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>

                            </div>
                            <div class="col-lg-3">
                                <label>&nbsp; </label>
                                <div id="divLaptop" runat="server" visible="false">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" Height="45px" />
                                    <%--<hr />
<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile" />
<br />--%>
                                </div>
                                <asp:Label ID="lblMessage" ForeColor="Green" runat="server" />
                                <div id="divMobile" runat="server" visible="false">
                                    <input type="file" accept="image/*;capture=camera" id="takePictureField" name="takePictureField" runat="server" />
                                    <div class="row">
                                        <div class="col m6">
                                            <img id="yourimage" runat="server" width="320" height="240" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button Text="Submit" ValidationGroup="e" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnSubmit_Click" />

                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
