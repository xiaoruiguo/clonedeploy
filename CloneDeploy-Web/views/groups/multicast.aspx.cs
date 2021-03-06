﻿using System;
using CloneDeploy_Common;
using CloneDeploy_Web.BasePages;

namespace CloneDeploy_Web.views.groups
{
    public partial class views_groups_multicast : Groups
    {
        protected void ddlGroupImage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroupImage.Text == "Select Image") return;
            PopulateImageProfilesDdl(ddlImageProfile, Convert.ToInt32(ddlGroupImage.SelectedValue));
            try
            {
                ddlImageProfile.SelectedIndex = 1;
            }
            catch
            {
                //ignore
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) PopulateForm();
        }

        protected void PopulateForm()
        {
            PopulateImagesDdlForGroup(Group.Id, ddlGroupImage);
            PopulateClusterGroupsDdl(ddlClusterGroup);
            ddlGroupImage.SelectedValue = Group.ImageId.ToString();
            PopulateImageProfilesDdl(ddlImageProfile, Convert.ToInt32(ddlGroupImage.SelectedValue));
            ddlImageProfile.SelectedValue = Group.ImageProfileId.ToString();
            ddlClusterGroup.SelectedValue = Group.ClusterGroupId.ToString();
        }

        protected void Submit_OnClick(object sender, EventArgs e)
        {
            RequiresAuthorizationOrManagedGroup(AuthorizationStrings.UpdateGroup, Group.Id);
            var group = Group;

            group.ImageId = Convert.ToInt32(ddlGroupImage.SelectedValue);
            group.ImageProfileId = Convert.ToInt32(ddlGroupImage.SelectedValue) == -1
                ? -1
                : Convert.ToInt32(ddlImageProfile.SelectedValue);
            group.ClusterGroupId = Convert.ToInt32(ddlClusterGroup.SelectedValue);

            var result = Call.GroupApi.Put(group.Id, group);
            EndUserMessage = !result.Success ? result.ErrorMessage : "Successfully Updated Group";
        }

       

    }
}