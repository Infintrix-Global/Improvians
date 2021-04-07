<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NotificationPreference.aspx.cs" Inherits="Evo.Admin.NotificationPreference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SrciptManager1" runat="server"></asp:ScriptManager>
    <div class="admin__content">
                    <div class="container-fluid">
                        <h1 class="text-center text-sm-left">Notification Preference</h1>

                        <hr />

                        <form class="admin__form" name="add-employee" action="#" method="POST">
                            <div class="row">
                                <div class="col-md-auto col-md-4 col-xl-3">
                                    <label>
                                        <h3>Task Type</h3>
                                        <select class="custom__dropdown robotomd">
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
                                    <div class="control__box control__box-auto pr-3 mb-4 w-100">
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="customCheck1">
                                            <label class="custom-control-label" for="customCheck1">Assistant Grower</label>
                                        </div>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="customCheck2">
                                            <label class="custom-control-label" for="customCheck2">Miguel Ramos - Sup</label>
                                        </div>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="customCheck3">
                                            <label class="custom-control-label" for="customCheck3">Sylvia Reyes - Sup</label>
                                        </div>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="customCheck4">
                                            <label class="custom-control-label" for="customCheck4">Supervisor</label>
                                        </div>
                                        <div class="custom-control custom-checkbox mr-3">
                                            <input type="checkbox" class="custom-control-input" id="customCheck5">
                                            <label class="custom-control-label" for="customCheck5">Irrigator</label>
                                        </div>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="customCheck6">
                                            <label class="custom-control-label" for="customCheck6">Crew Lead</label>
                                        </div>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="customCheck7">
                                            <label class="custom-control-label" for="customCheck8">Sprayer</label>
                                        </div>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="customCheck8">
                                            <label class="custom-control-label" for="customCheck8">Alex</label>
                                        </div>
                                    </div>

                                    <label class="mb-0">
                                        <h3>Send Notification Via:</h3>
                                    </label>

                                    <div class="custom-control custom-checkbox mr-3">
                                        <input type="checkbox" class="custom-control-input" id="customCheckApp">
                                        <label class="custom-control-label mb-2" for="customCheckApp">App</label>
                                    </div>
                                    <div class="custom-control custom-checkbox">
                                        <input type="checkbox" class="custom-control-input" id="customCheckMail">
                                        <label class="custom-control-label" for="customCheckMail">Mail</label>
                                    </div>

                                    <button type="submit" class="submit-bttn bttn bttn-primary w-auto">
                                        Update Preferences
                                    </button>
                                </div>

                                <div class="col-md-auto col-md-8 col-xl-9 mt-4 mt-md-0">
                                    <h3>Assigned Notifications</h3>
                                    <div class="data__table">
                                        <table>
                                            <tr>
                                                <th>Task Type</th>
                                                <th>Send To</th>
                                                <th>Send Via</th>
                                            </tr>
                                            <tr>
                                                <td>Fertilization</td>
                                                <td>
                                                    <div class="mb-2">Assistant Grower</div>
                                                    <div class="mb-2">Miguel Ramos - Sup</div>
                                                    <div class="mb-2">Sprayer</div>
                                                </td>
                                                <td>App</td>
                                            </tr>
                                            <tr>
                                                <td>Fertilization</td>
                                                <td>
                                                    <div class="mb-2">Assistant Grower</div>
                                                    <div class="mb-2">Miguel Ramos - Sup</div>
                                                    <div class="mb-2">Sprayer</div>
                                                </td>
                                                <td>App, Email</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>


</asp:Content>
