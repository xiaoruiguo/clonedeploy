﻿using System;
using System.IO;
using CloneDeploy_Common;
using CloneDeploy_Entities.DTOs;
using CloneDeploy_Web.BasePages;

namespace CloneDeploy_Web.views.computers
{
    public partial class ComputerImport : Computers
    {
        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                string csvContent;
                using (var inputStreamReader = new StreamReader(FileUpload.PostedFile.InputStream))
                {
                    csvContent = inputStreamReader.ReadToEnd();
                }

                var count = Call.ComputerApi.Import(new ApiStringResponseDTO {Value = csvContent});
                Call.GroupApi.ReCalcSmart();
                EndUserMessage = "Successfully Imported " + count + " Computers";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RequiresAuthorization(AuthorizationStrings.CreateComputer);
            if (IsPostBack) return;
        }
    }
}