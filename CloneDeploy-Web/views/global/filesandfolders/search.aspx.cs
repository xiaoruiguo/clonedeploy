﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CloneDeploy_Common;
using CloneDeploy_Entities;
using CloneDeploy_Web.BasePages;

namespace CloneDeploy_Web.views.global.filesandfolders
{
    public partial class views_global_filesandfolders_search : Global
    {
        protected void ButtonConfirmDelete_Click(object sender, EventArgs e)
        {
            RequiresAuthorization(AuthorizationStrings.DeleteGlobal);
            foreach (GridViewRow row in gvFiles.Rows)
            {
                var cb = (CheckBox) row.FindControl("chkSelector");
                if (cb == null || !cb.Checked) continue;
                var dataKey = gvFiles.DataKeys[row.RowIndex];
                if (dataKey == null) continue;
                Call.FileFolderApi.Delete(Convert.ToInt32(dataKey.Value));
            }

            PopulateGrid();
        }

        protected void chkSelectAll_OnCheckedChanged(object sender, EventArgs e)
        {
            ChkAll(gvFiles);
        }

        protected void gvFiles_OnSorting(object sender, GridViewSortEventArgs e)
        {
            PopulateGrid();
            var listSysprepTags = (List<FileFolderEntity>) gvFiles.DataSource;
            switch (e.SortExpression)
            {
                case "Name":
                    listSysprepTags = GetSortDirection(e.SortExpression) == "Asc"
                        ? listSysprepTags.OrderBy(s => s.Name).ToList()
                        : listSysprepTags.OrderByDescending(s => s.Name).ToList();
                    break;
            }

            gvFiles.DataSource = listSysprepTags;
            gvFiles.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            PopulateGrid();
        }

        protected void PopulateGrid()
        {
            gvFiles.DataSource = Call.FileFolderApi.Get(int.MaxValue, txtSearch.Text);
            gvFiles.DataBind();

            lblTotal.Text = gvFiles.Rows.Count + " Result(s) / " + Call.FileFolderApi.GetCount() +
                            " Total File(s) / Folder(s)";
        }

        protected void txtSearch_OnTextChanged(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}