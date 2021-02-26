<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="temp.aspx.cs" Inherits="Improvians.temp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:Button Text="Post & Send" ID="btnSubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server" OnClick="btnSubmit_Click" />
          
                 <asp:GridView ID="DGJob" runat="server" AutoGenerateColumns="False"
                                    class="striped" Visible="false"
                                    GridLines="None" OnRowDataBound="DGJob_RowDataBound"
                                    ShowHeaderWhenEmpty="True" Width="100%">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="SrNo" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seedline Facility">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeedline" runat="server" Text='<%# Eval("loc") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljobcode" runat="server" Text='<%# Eval("jobcode") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("itmdescp") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("cname") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sales Order Seed Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSODate" runat="server" HtmlEncode="false" Text='<%# Eval("sodate","{0:MM/dd/yyyy}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sales Order Trays">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSOTrays" runat="server" Text='<%# Eval("sotrays","{0:####}") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tray Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraySize" runat="server" Text='<%# Eval("ts") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Putaway Facility">
                                            <ItemTemplate>
                                                <%-- <asp:TextBox ID="txtSeedline" runat="server" Text='<%# Eval("loc") %>' Width="50"></asp:TextBox>--%>
                                                <asp:Label ID="lbl_Seedline" Visible="false" Text='<%# Eval("loc") %>' runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlBenchLocation" class="custom__dropdown robotomd" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work order Trays">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Txtgtrays" Width="50" Text='<%# Eval("wotrays","{0:####}") %>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Scheduled Seed Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Txtgplantdt" TextMode="Date" Text='<%# Eval("wodate","{0:yyyy-MM-dd}") %>' Width="150px" runat="server"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seeds Allocated">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAllocated" runat="server" Text='<%# Eval("alloc") %>'></asp:Label>
                                                <asp:HiddenField ID="HiddenFieldsotrays" Value='<%# Eval("sotrays") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFielditm" Value='<%# Eval("itm") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFieldcusno" Value='<%# Eval("cusno") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFieldsodate" Value='<%# Eval("sodate","{0:yyyy-MM-dd}") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFieldduedate" Value='<%# Eval("duedate","{0:yyyy-MM-dd}") %>' runat="server" />
                                                <asp:HiddenField ID="HiddenFieldwo" Value='<%# Eval("wo") %>' runat="server" />
                                                   <asp:HiddenField ID="HiddenFieldGenusCode" Value='<%# Eval("GenusCode") %>' runat="server" />

                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>/>
</asp:Content>
