﻿using MediatR;
using SmashHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmashHub.Core.Cqrs.Reports.PostCommentReport
{
    public class PostCommentReportRequest : IRequest<PostCommentReportResponse>
    {
        public int UserId { get; set; }

        public int ReporterId { get; set; }

        public int CommentId { get; set; }

        public string Body { get; set; }
    }
}
