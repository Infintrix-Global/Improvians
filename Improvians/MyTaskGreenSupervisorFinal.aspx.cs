﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Improvians
{
    public partial class MyTaskGreenSupervisorFinal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlTaskRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTaskRequest.SelectedValue == "1")
            {
                Response.Redirect("~/GerminationAssignmentForm.aspx");
            }
            if (ddlTaskRequest.SelectedValue == "2")
            {
               // Response.Redirect("~/FertilizerTaskReq.aspx");
            }
        }
        }
}