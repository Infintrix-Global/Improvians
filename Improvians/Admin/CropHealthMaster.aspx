<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CropHealthMaster.aspx.cs" Inherits="Evo.Admin.CropHealthMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Crop Health Master</h1>

            <hr />


            <asp:Panel ID="pnlAdd" runat="server" Visible="false">

                <div class="filter__row d-flex">
                    <div class="row">
                        <div class="col m3">
                            <div>
                            <label>Problem Type<span style="color: red">*</span></label>
                            <asp:TextBox ID="txtProblemType" class="input__control" placeholder="Enter Problem Type" runat="server"></asp:TextBox>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProblemType" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Problem Type" ForeColor="Red"></asp:RequiredFieldValidator>

                            </span>
                                </div>
                            <div>
                            <label>Problem Cause<span style="color: red">*</span></label>
                            <asp:TextBox ID="txtProblemCause" class="input__control" placeholder="Enter Problem Cause" runat="server"></asp:TextBox>
                            <span class="error_message">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProblemCause" ValidationGroup="e"
                                    SetFocusOnError="true" ErrorMessage="Please Enter Problem Cause" ForeColor="Red"></asp:RequiredFieldValidator>

                            </span>
                                 </div>
                            <div class="col-12 text-center order-md-1">
                                <asp:Button ID="btAdd" runat="server" Text="Submit" TabIndex="10" class="submit-bttn bttn bttn-primary" OnClick="btnAdd_Click" ValidationGroup="e" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" class="submit-bttn bttn bttn-primary" ClientIDMode="Static" OnClick="btnCancel_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlList" runat="server" Visible="true">
                <div class="filter__row d-flex">
                    <div class="row">
                        <div class="col m3">
                            <label>Name </label>
                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="input__control "></asp:TextBox>
                        </div>



                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnSearch" Text="Search" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnClear" Text="Clear" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnClear_Click" />
                        </div>
                        <div class="col-auto align-self-end">
                            <asp:Button ID="btnAddCropHealth" Text="Add" class="submit-bttn bttn bttn-primary" runat="server" OnClick="btnAddCropHealth_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="filter__row d-flex">
                <div class="row justify-content-lg-center">
                    <div class=" col m12">
                        <div class="portlet light ">
                            <asp:Label runat="server" Text="" ID="count"></asp:Label>
                            <div class="portlet-body">
                                <div class="data__table">
                                    <asp:GridView ID="gvCropHealth" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        class="striped" OnRowCommand="GridEmployee_RowCommand" AllowSorting="true" OnPageIndexChanging="GridEmployee_PageIndexChanging"
                                        GridLines="None" OnSorting="GridEmployee_Sorting"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="40%" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")  %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Problem Type" ItemStyle-Width="60%" HeaderStyle-CssClass="autostyle2" SortExpression="TypeOfProblem">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeOfProblem" runat="server" Text='<%# Eval("TypeOfProblem")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Problem Cause" ItemStyle-Width="60%" HeaderStyle-CssClass="autostyle2" SortExpression="CauseOfProblem">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCauseOfProblem" runat="server" Text='<%# Eval("CauseOfProblem")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="autostyle2">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <asp:ImageButton class="mr-2" ID="imgEdit" runat="server" CommandArgument='<%# Eval("Id")  %>' CommandName="EditProfile" ImageUrl="~/Admin/images/edit.png" AlternateText="edit" ToolTip="edit"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<%# Eval("Id")  %>' CommandName="RemoveProfile" ImageUrl="~/Admin/images/delete.png" AlternateText="delete" ToolTip="delete" OnClientClick="return confirm('Are you sure you want to remove this crop health?');"></asp:ImageButton>
                                                    </div>
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
            </div>
        </div>
    </div>
</asp:Content>
