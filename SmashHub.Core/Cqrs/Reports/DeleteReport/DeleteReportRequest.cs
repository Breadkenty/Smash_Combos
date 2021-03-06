﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmashHub.Core.Cqrs.Reports.DeleteReport
{
    public class DeleteReportRequest : IRequest<DeleteReportResponse>
    {
        public int ReportId { get; set; }
        public int CurrentUserId { get; set; }
    }
}
