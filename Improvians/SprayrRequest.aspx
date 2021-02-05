<%@ Page Title="" Language="C#" MasterPageFile="~/EvoMaster.Master" AutoEventWireup="true" CodeBehind="SprayrRequest.aspx.cs" Inherits="Improvians.SprayrRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="main main__header">
            <div class="site__container">
                <h2>Spray Request</h2>
                
                <div class="filter__row row">
                    <div class="col-xl-auto col-12">
                        <label>Job No.</label>
                        <asp:DropDownList ID="ddlJobNo" runat="server" ></asp:DropDownList>
                        <%--<select class="w-100 filter__control filter__select custom__dropdown">
                            <option>Job No.</option>
                            <option>Option 2</option>
                            <option>Option 3</option>
                            <option>Option 4</option>
                        </select>--%>
                    </div>

                    <div class="col-xl-auto col-12">
                        <label>Customer Name</label>
                        <asp:DropDownList ID="ddlcustomer" runat="server" ></asp:DropDownList>

<%--                        <select class="w-100 filter__control filter__select custom__dropdown">
                            <option>Customer Name</option>
                            <option>Option 2</option>
                            <option>Option 3</option>
                            <option>Option 4</option>
                        </select>--%>
                    </div>

                    <div class="col-xl-auto col-12">
                        <label>Facility Defaults</label>
                      <asp:DropDownList ID="ddlFacility" runat="server" ></asp:DropDownList>

<%--                        <select class="w-100 filter__control filter__select custom__dropdown">
                            <option>Facility Defaults</option>
                            <option>Option 2</option>
                            <option>Option 3</option>
                            <option>Option 4</option>
                        </select>--%>
                    </div>
                </div>

                <h4 class="mt-3 mt-md-4">Data Showed as per Filter:</h4>
                
                    <asp:GridView ID="grddetails" runat="server" CssClass="data__table">
                        <Columns>
                            <asp:TemplateField HeaderText="Job">
                                <ItemTemplate>
                                    <asp:Label ID="lbljob" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblitem" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Facility">
                                <ItemTemplate>
                                    <asp:Label ID="lblfacility" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bench Location">
                                <ItemTemplate>
                                    <asp:Label ID="lblbench" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total No. of Trays">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotaltrays" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tray Size">
                                <ItemTemplate>
                                    <asp:Label ID="lbltraysize" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seeded Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblseededdate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbldescription" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                <div class="text-left dashboard__block my-4">
                    <form class="web__form pt-2">
                        <div class="row justify-content-center">
                            <div class="col-12">
                                <h3>User Inputs:</h3>
                                <div class="row">

                                    <div class="col-12 col-sm-6 col-lg-4">
                                        <label>Select Greenhouse Supervisor</label>
                                        <asp:DropDownList ID="ddlsupervisor" runat="server"></asp:DropDownList>
                                    </div>

                                    <div class="col-12 col-sm-6 col-lg-3">
                                        <label>No. Of Trays to be Irrigated</label>
                                        <asp:TextBox ID="txtnooftray" runat="server" CssClass="input__control" placeholder="Enter No. of Trays" ></asp:TextBox>
                                        
                                    </div>

                                    <div class="col-12 col-sm-6 col-md-auto">
                                        <label class="pr-2 pr-lg-0 d-lg-block">Water Required</label>
                                        <asp:RadioButtonList ID="rdlwater" runat="server" CssClass="input__control input__control-auto">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        
                                    </div>

                                    <div class="col-12  col-sm-6 col-md-auto">
                                        <label class="pr-2 pr-lg-0 d-lg-block">Irrigation Duration</label>
                                        <asp:TextBox ID="txtduration" runat="server" CssClass="input__control" placeholder="00:00" ></asp:TextBox>
                                       
                                    </div>
                                </div>

                                <div class="row align-items-center mt-sm-3">
                                    <div class="col-12 mt-4 mb-3 my-sm-0 col-sm-auto">
                                        <h4 class="mb-0">Schedule:</h4>
                                    </div>
                                    <div class="col-auto">
                                        <label class="d-block">Spray Date</label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="input__control"  ></asp:TextBox>
                                    </div>
                                    <div class="col-auto">
                                        <label class="d-block">Spray Time</label>
                                        <asp:TextBox ID="txttime" runat="server" CssClass="input__control" ></asp:TextBox>
                                        
                                    </div>
                                </div>

                                <div class="row align-items-center mt-sm-3">
                                    <div class="col-12 col-sm-6 col-lg-4">
                                        <asp:TextBox ID="txtnotes" TextMode="MultiLine" runat="server" CssClass="input__control" placeholder="Notes" ></asp:TextBox>
                                        
                                    </div>

                                    <div class="col-12 my-3">
                                        <asp:Button ID="btnsubmit" CssClass="ml-2 submit-bttn bttn bttn-primary" runat="server"  Text="Submit"/>
                                         <asp:Button ID="btnreset" CssClass="submit-bttn bttn bttn-primary" runat="server" Text="Reset" />

                                    </div>
                                </div>

                            </div>
                        </div>
                    </form>
                </div>

            </div>
        </div>


</asp:Content>

