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
            <asp:Panel ID="PanelList" runat="server">

            <div class="filter__row d-flex">
                <div class="row">
                    <div class="col-lg-3">
                        <label>Facility Location</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlFacility" runat="server" class="custom__dropdown robotomd" AutoPostBack="true" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged"></asp:DropDownList>
                        <span class="error_message">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlFacility" ValidationGroup="x"
                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Facility Location" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                    </div>
                    <div class="col-lg-3">
                        <label>Bench Location </label>
                        <span style="color: red">*</span>
                        <asp:DropDownList ID="ddlBenchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlBenchLocation_SelectedIndexChanged" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                        <span class="error_message">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBenchLocation" ValidationGroup="x"
                                SetFocusOnError="true" InitialValue="" ErrorMessage="Please Select Bench Location" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                    </div>
                    <div class="col-lg-3">
                        <label>Job No </label>
                        <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <label>Customer </label>
                        <asp:DropDownList ID="ddlCustomer" runat="server" class="custom__dropdown robotomd"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-lg-3">
                </div>

                <div class="col-lg-3">
                </div>

                <div class="col-lg-3">
                </div>

                <div class="col-lg-3">

                    <asp:Button Text="Reset" ID="btnSearchRest" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnSearchRest_Click" />

                </div>

            </div>

            <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table data__table-height">
                                <asp:GridView ID="gvFer" runat="server" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreenHouse" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                                <asp:Label ID="lblwo" runat="server" Text='<%# Eval("wo")  %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGrowerputawayID" runat="server" Text='<%# Eval("GrowerPutAwayId")  %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitem" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Facility Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotTray" runat="server" Text='<%# Eval("Trays","{0:####}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--
                                        <asp:TemplateField HeaderText="Seeded Date" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SeededDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                    <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <EmptyDataTemplate>
                                        No Record Available
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>

            </div>

            </asp:Panel>
              <asp:Panel ID="PanelView" runat="server">
                  <div class="row">
                <div class=" col m12">
                    <div class="portlet light ">
                        <asp:Label runat="server" Text="" ID="Label4"></asp:Label>
                        <div class="portlet-body">
                            <div class="data__table data__table-height">
                                <asp:GridView ID="GridViewView" runat="server" AutoGenerateColumns="False"
                                    class="striped" AllowSorting="true"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Bench Location" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreenHouse1" runat="server" Text='<%# Eval("GreenHouseID")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job No." ItemStyle-Width="10%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID1" runat="server" Text='<%# Eval("jobcode")  %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer" ItemStyle-Width="20%" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomer1" runat="server" Text='<%# Eval("cname")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitem1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     

                                        <asp:TemplateField HeaderText="Total Tray" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotTray1" runat="server" Text='<%# Eval("Trays","{0:####}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tray Size" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraySize1" runat="server" Text='<%# Eval("TraySize","{0:####}")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                   

                                        <asp:TemplateField HeaderText="Plant Type" HeaderStyle-CssClass="autostyle2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemdesc1" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                    <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <EmptyDataTemplate>
                                        No Record Available
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
                  </asp:Panel>
            <div class="dashboard__block dashboard__block--asign">
                <div id="userinput" runat="server" class="assign__task d-flex" visible="true">
                    <asp:Panel ID="pnlint" runat="server">
                        <div class="row">
                            <div class="col-lg-3">
                                <label>Type of problem</label><span style="color: red">*</span>
                                <asp:DropDownList ID="ddlpr" runat="server" class="custom__dropdown robotomd">
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
                                <asp:DropDownList ID="DropDownListCause" runat="server" class="custom__dropdown robotomd">
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
                                <asp:DropDownList ID="DropDownListSv" runat="server" class="custom__dropdown robotomd">
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
                                <asp:Button Text="Save for later" ValidationGroup="e" CausesValidation="true" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-2" runat="server" OnClick="btnSubmit_Click" />

                                <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
