﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmashHub.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmashHub.Core.Cqrs.Comments.GetComment
{
    public class GetCommentRequestHandler : IRequestHandler<GetCommentRequest, GetCommentResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCommentRequestHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetCommentResponse> Handle(GetCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await _dbContext.Comments
                .Where(comment => comment.Id == request.CommentId)
                .Include(comment => comment.User)
                .Include(comment => comment.Reports)
                    .ThenInclude(report => report.User)
                .FirstOrDefaultAsync();

            if (comment == null)
                throw new KeyNotFoundException($"Comment with id {request.CommentId} does not exist");

            return _mapper.Map<GetCommentResponse>(comment);
        }
    }
}
