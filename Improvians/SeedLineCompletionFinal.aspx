<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SeedLineCompletionFinal.aspx.cs" Inherits="Evo.SeedLineCompletionFinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function checkTray() {
            var txt1 = document.getElementById('<%= txtActualTraysNo.ClientID %>').value;
         <%--   var value = document.getElementById('<%=txtActualTraysNo.ClientID%>').value;--%>
            var txt2 = document.getElementById('<%= txtRequestedTrays.ClientID %>').value;
            //alert(value);
            if (txt1 != txt2)
                return confirm('Seeded Trays are not equal to Requested Trays. Are you are sure you want to submit this job?');
            else
                return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Sc1" runat="server"></asp:ScriptManager>
    <div class="site__container">
        <h2 class="head__title-icon mb-4">Seedline Planner Task Completion Form</h2>

        <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label>
       
        <asp:GridView ID="gvGerm" runat="server" AllowPaging="True"  AutoGenerateColumns="False"
            AllowSorting="true" PageSize="10" OnRowDataBound="gvGerm_RowDataBound"
            GridLines="None"
            ShowHeaderWhenEmpty="True" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table class="w-auto data__table">
                            <tr>
                                <th>Job No.</th>
                                <td>
                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("jobcode")%>'></asp:Label>
                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("jobcode","~/JobReports.aspx?JobCode={0}")%>' Text='<%#Eval("jobcode") %>' Font-Underline="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>Item</th>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemno")  %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>No. Of Tray</th>
                                <td>
                                    <asp:Label ID="lblNoofTray" runat="server" Text='<%# Eval("trays_plan")  %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Tray Size</th>
                                <td>
                                    <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("TraySize")  %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Number Of Plants</th>
                                <td>
                                    <asp:Label ID="lblNoOfPlants" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <th>Seeded Due Date</th>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("SoDate","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Planned Due Date</th>
                                <td>
                                    <asp:Label ID="lbldue_date" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}")  %>'></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <th>Plant Type</th>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("itemdescp")  %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <PagerStyle CssClass="paging" HorizontalAlign="Right" />
            <PagerSettings Mode="NumericFirstLast" />
            <EmptyDataTemplate>
                No Record Available
            </EmptyDataTemplate>
        </asp:GridView>

        <div class="dashboard__block dashboard__block--asign">
            <div id="userinput" runat="server" class="assign__task">
                <asp:Panel runat="server" ID="pnldetail">
                    <div class="row">
                        <div class="col-12" runat="server" visible="false">
                            <label># OF SEEDS REQUIRED TO FULFILL ORDER:</label><br />
                            <h4>
                                <asp:Label ID="lblJobID" runat="server" Visible="false"></asp:Label>

                                <asp:Label ID="lblduedate" runat="server" Visible="false"></asp:Label>
                                
                                <asp:Label ID="lblSeedingDate" runat="server" Visible="false"></asp:Label>
                            </h4>
                        </div>
                        <div class="col col-sm-6 col-md-4 col-lg-3 col-xl-auto mb-3">
                            <label>Change Tray Size?</label>

                            <div class="d-flex flex-wrap align-items-center">
                                <%-- <label>
                                        Is the tray size [<asp:Label ID="lbltraysizecon" runat="server"></asp:Label>
                                        ] correct?</label>--%>

                                <span class="custom-control custom-radio pt-2">
                                    <asp:RadioButtonList ID="radtraysize" Width="120px" RepeatDirection="Horizontal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radtraysize_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </span>

                                <span class="d-block d-xl-inline-block">
                                    <asp:TextBox ID="txtTrayChange" class="input__control input__control-auto mb-2" runat="server" TextMode="Number" Width="140px" placeholder="Tray Size" Visible="false"></asp:TextBox>
                                </span>
                            </div>
                        </div>

                        <div class="col col-sm-6 col-md-4 col-lg-3 col-xl-auto">
                            <label class="d-block">Seeding Date</label>
                            <asp:TextBox ID="txtSeedingDate" ClientIDMode="Static" TextMode="Date" runat="server" class="input__control input__control-auto"></asp:TextBox>
                            <span class="error_message d-block">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSeedingDate" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Seeding Date" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </span>
                        </div>

                        <div class="col col-sm-6 col-md-4 col-lg-3 col-xl-auto mb-3">
                            <label class="d-block">Fill Order</label>
                            <span class="custom-control custom-radio pt-2">
                                <asp:RadioButtonList ID="radOrder" runat="server" Width="180px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radOrder_SelectedIndexChanged">
                                    <asp:ListItem Text="Complete" Value="Complete" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Partial" Value="Partial"></asp:ListItem>
                                </asp:RadioButtonList>
                            </span>
                        </div>

                        <div class="col col-sm-6 col-md-4 col-lg-3 col-xl-auto">
                            <label class="d-block">No Of Trays To Be Seeded</label>
                            <asp:Label ID="lblTrays" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="txtTrays" class="pt-2" runat="server" Text=""></asp:Label>
                           
                        </div>

                        <div class="col-12 mt-3">
                            <h4 class="mb-1">Seed Lot Usage: <asp:Label ID="lblTraySize" runat="server" Visible="false"></asp:Label></h4>

                            <h6 class="mb-2">
                                NO. OF SEEDS REQUIRED TO FULFILL ORDER:
                                <asp:Label ID="lblSeedRequired" runat="server"></asp:Label>
                            </h6>
                        </div>
                        
                        <div class="col-12 my-3">
                            <div class="data__table">
                                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" class="striped"
                                    GridLines="None" OnRowDataBound="gvDetails_RowDataBound" OnRowDeleting="gvDetails_RowDeleting"
                                    ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seedlot">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLotName" Text='<%# Eval("l2")  %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="# of Seed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblactualseed" Text='<%# Eval("QTY","{0:####}")  %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actual # of tray Seeded">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblactualseed" Text='<%# Bind("#ActualTray")  %>' runat="server" ></asp:Label>--%>
                                                <asp:TextBox ID="txtActualTray" class="input__control" Text="" runat="server" AutoPostBack="true" OnTextChanged="txtActualTray_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Calculated # of Seed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeed" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-Width="18%" HeaderText="Initial Seed Lot Weight">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInitialSeedLotWeightLB" Width="90px" class="input__control" placeholder="Lb"  runat="server" Text=""></asp:TextBox>
                                                <asp:TextBox ID="txtInitialSeedLotWeightOZ" Width="90px" class="input__control" placeholder="Oz"  runat="server" Text=""></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="18%" HeaderText="Final Seed Lot Weight">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFinalSeedLotWeightLB" Width="90px" class="input__control" runat="server" placeholder="Lb" Text=""></asp:TextBox>
                                                  <asp:TextBox ID="txtFinalSeedLotWeightOZ" Width="90px" class="input__control" runat="server" placeholder="Oz" Text=""></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%--  <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                 <asp:DropDownList ID="ddlUnit" runat="server" class="custom__dropdown robotomd">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Pounds " Value="Pounds "></asp:ListItem>
                                                    <asp:ListItem Text="Oz" Value="Oz"></asp:ListItem>
                                                 
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Lot Comments">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlLotComments" runat="server" class="custom__dropdown robotomd">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Doubles" Value="Doubles"></asp:ListItem>
                                                    <asp:ListItem Text="Misses" Value="Misses"></asp:ListItem>
                                                    <asp:ListItem Text="Bad Quality Seed " Value="Bad Quality Seed "></asp:ListItem>
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="Delete" ID="btnRemove" runat="server" CssClass="bttn bttn-primary bttn-action" />

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
                    
                    <h4 class="pt-3">Seeding Details:</h4>

                    <div class="row">
                        <div class="col-12 col-sm-6 col-md-4 col-lg mb-3">
                            <label>Requested Trays:</label>
                            <asp:TextBox ID="txtRequestedTrays" ReadOnly="true" class="input__control robotomd" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-md-4 col-lg mb-3">
                            <label>Seeded Trays:</label>
                            <asp:TextBox ID="txtActualTraysNo" ReadOnly="true" class="input__control robotomd" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-md-4 col-lg mb-3">
                            <label>Remaining Trays:</label>
                            <asp:TextBox ID="txtSeedsAllocated" ReadOnly="true" runat="server" class="input__control robotomd"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-md-4 col-lg-auto mb-3">
                            <label>Job Completion:</label>

                            <span class="custom-control custom-radio pt-2">
                                <asp:RadioButtonList ID="radJobCompletion" runat="server" Width="160px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radJobCompletion_SelectedIndexChanged">
                                    <asp:ListItem Text="Full" Value="Full" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Partial" Value="Partial"></asp:ListItem>
                                </asp:RadioButtonList>
                            </span>

                            <asp:TextBox ID="txtCompletedTrays" runat="server" TextMode="Number" Width="190px" class="input__control input__control-auto" placeholder="Enter # Completed" Visible="false"></asp:TextBox>
                        </div>

                        <div class="col-12 col-sm-6 col-md-4 col-lg mb-3">
                            <label>Job Comments</label>

                            <asp:DropDownList ID="ddlJobComments" runat="server" class="custom__dropdown robotomd">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Too Wet" Value="Too Wet"></asp:ListItem>
                                <asp:ListItem Text="Too Dry" Value="Too Dry"></asp:ListItem>
                                <asp:ListItem Text="Heavy Soil" Value="Heavy Soil"></asp:ListItem>
                                <asp:ListItem Text="Too much Perlite" Value="Too much Perlite"></asp:ListItem>
                                <asp:ListItem Text="Extra fine Perlite" Value="Extra fine Perlite"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-12 mt-3">
                            <span class="custom-control custom-checkbox">
                                <asp:CheckBox ID="chkSeedReturn" runat="server" />
                                <label for="ContentPlaceHolder1_chkSeedReturn">Seed Returns Complete: Partial and Unused Seeds Boxed Up and Labeled, Scanned</label>
                            </span>
                        </div>

                        <div class="col-lg-12 mt-3 text-center">
                            <asp:Button Text="Submit" ID="btnSubmit" CssClass="bttn bttn-primary bttn-action mr-3" runat="server" OnClientClick="return checkTray();" OnClick="btnSubmit_Click" ValidationGroup="e" />

                            <asp:Button Text="Reset" ID="btnReset" runat="server" CssClass="bttn bttn-primary bttn-action" OnClick="btnReset_Click" />
                        </div>

                        <div class="col-12 text-center mt-4">
                            <asp:Label ID="lblsubmsg" runat="server" Text="Job sent to grower for put-away location once submitted." Font-Italic="true"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>

        </div>
    </div>
</asp:Content>
