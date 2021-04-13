<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NotificationPreference.aspx.cs" Inherits="Evo.Admin.NotificationPreference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
        <div class="container-fluid">
            <h1 class="text-center text-sm-left">Notification Preference</h1>

            <hr />

            <div class="admin__form">
                <div class="row">
                                <div class="col-lg-6 col-12">
                                    <label>
                                        <h3>Task Type</h3>
                                        <select class="custom__dropdown input__control-auto robotomd">
                                            <option disabled selected>Select Task Type</option>
                                            <option>Germination Count</option>
                                            <option>Fertilization</option>
                                            <option>Chemical</option>
                                            <option>Irrigation</option>
                                            <option>Plant Ready</option>
                                            <option>Move Request</option>
                                            <option>Dump</option>
                                            <option>General Task</option>
                                        </select>
                                    </label>

                                    
                                    <label class="mb-0">
                                        <h3>Send Notification to:</h3>
                                    </label>

                                    <div class="data__table">
                                        <table>
                                            <tr>
                                                <th>User</th>
                                                <th colspan="2" class="text-center">Send Notification Via</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="customCheck1">
                                                        <label class="custom-control-label" for="customCheck1">Assistant Grower</label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="app1">
                                                        <label class="custom-control-label" for="app1">App</label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="email1">
                                                        <label class="custom-control-label" for="email1">Email</label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="customCheck2">
                                                        <label class="custom-control-label" for="customCheck2">Miguel Ramos - Sup</label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="app2">
                                                        <label class="custom-control-label" for="app2">App</label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="email2">
                                                        <label class="custom-control-label" for="email2">Email</label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="customCheck3">
                                                        <label class="custom-control-label" for="customCheck3">Sylvia Reyes - Sup</label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="app3">
                                                        <label class="custom-control-label" for="app3">App</label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="email3">
                                                        <label class="custom-control-label" for="email3">Email</label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="customCheck4">
                                                        <label class="custom-control-label" for="customCheck4">Supervisor</label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="app4">
                                                        <label class="custom-control-label" for="app4">App</label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="custom-control custom-checkbox mr-3">
                                                        <input type="checkbox" class="custom-control-input" id="email4">
                                                        <label class="custom-control-label" for="email4">Email</label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <button type="submit" class="submit-bttn bttn bttn-primary w-auto">
                                        Update Preferences
                                    </button>   
                                </div>

                                <div class="col-lg-6 col-12 mt-0 mt-lg-0">
                                    <h3>Assigned Notifications</h3>
                                    <div class="data__table">
                                        <table>
                                            <tr>
                                                <th>Task Type</th>
                                                <th>Send To</th>
                                                <th>Send Via</th>
                                                <th>Actions</th>
                                            </tr>
                                            <tr>
                                                <td>Fertilization</td>
                                                <td>
                                                    <div class="mb-2">Assistant Grower</div>
                                                </td>
                                                <td>App</td>
                                                <td class="text-center">
                                                    <button type="button" class="bttn bttn__icons bttn__icons--edit mr-2">
                                                        <i class="fa fa-edit"></i>
                                                    </button>
                                                    <button type="button" class="bttn bttn__icons bttn__icons--delete">
                                                        <i class="fa fa-trash"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Irrigation</td>
                                                <td>
                                                    <div class="mb-2">Sprayer</div>
                                                </td>
                                                <td>App, Email</td>
                                                <td class="text-center">
                                                    <button type="button" class="bttn bttn__icons bttn__icons--edit mr-2">
                                                        <i class="fa fa-edit"></i>
                                                    </button>
                                                    <button type="button" class="bttn bttn__icons bttn__icons--delete">
                                                        <i class="fa fa-trash"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!--
                                    <div class="assignment__filter my-2">
                                        Actions: 
                                        <button>
                                            <i class="fa fa-edit"></i> Edit All
                                        </button>
                                        <button>
                                            <i class="fa fa-trash"></i> Delete All
                                        </button>
                                    </div>
                                    -->
                                </div>
                            </div>
            </div>
        </div>
    </div>


</asp:Content>
