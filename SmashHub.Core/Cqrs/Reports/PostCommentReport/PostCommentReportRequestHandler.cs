﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmashHub.Core.Services;
using SmashHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmashHub.Core.Cqrs.Reports.PostCommentReport
{
    public class PostCommentReportRequestHandler : IRequestHandler<PostCommentReportRequest, PostCommentReportResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public PostCommentReportRequestHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PostCommentReportResponse> Handle(PostCommentReportRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.Where(user => user.Id == request.UserId).FirstOrDefaultAsync();
            if (user == null)
                throw new KeyNotFoundException($"User with id {request.UserId} does not exist");

            var currentUser = await _dbContext.Users.Where(user => user.Id == request.ReporterId).FirstOrDefaultAsync();
            if (currentUser == null)
                throw new KeyNotFoundException($"User with id {request.ReporterId} does not exist");

            var reportComment = await _dbContext.Comments.Where(comment => comment.Id == request.CommentId)
             .Include(comment => comment.Reports)
                .ThenInclude(report => report.Reporter)
            .FirstOrDefaultAsync();

            if (reportComment == null)
                throw new KeyNotFoundException($"Comment with id {request.CommentId} does not exist");

            if (reportComment.Reports.Any(report => report.Reporter.Id == request.ReporterId))
                throw new ArgumentException($"Already reported this comment");

            var report = new Report
            {
                User = user,
                Reporter = currentUser,
                Body = request.Body
            };
            _dbContext.Reports.Add(report);

            reportComment.Reports.Add(report);
            _dbContext.Entry(reportComment).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<PostCommentReportResponse>(report);
        }
    }
}
