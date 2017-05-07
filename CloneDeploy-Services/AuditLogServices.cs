﻿using System.Collections.Generic;
using CloneDeploy_DataModel;
using CloneDeploy_Entities;
using CloneDeploy_Entities.DTOs;

namespace CloneDeploy_Services
{
    public  class AuditLogServices
    {
          private readonly UnitOfWork _uow;

        public AuditLogServices()
        {
            _uow = new UnitOfWork();
        }

        public void AddAuditLog(AuditLogEntity auditLog)
        {
            _uow.AuditLogRepository.Insert(auditLog);
            _uow.Save();
        }
    }
}