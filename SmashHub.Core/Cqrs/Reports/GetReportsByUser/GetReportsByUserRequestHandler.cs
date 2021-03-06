﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmashHub.Core.Services;
using SmashHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace SmashHub.Core.Cqrs.Reports.GetReportsByUser
{
    public class GetReportsByUserRequestHandler : IRequestHandler<GetReportsByUserRequest, IEnumerable<GetReportsByUserResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetReportsByUserRequestHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<GetReportsByUserResponse>> Handle(GetReportsByUserRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.CurrentUserId);
            if (currentUser == null)
                throw new KeyNotFoundException($"User with id {request.CurrentUserId} does not exist");

            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.DisplayName == request.DisplayName);
            if (user == null)
                throw new KeyNotFoundException($"User with name {request.DisplayName} does not exist");

            if (currentUser.UserType == UserType.Moderator || currentUser.UserType == UserType.Admin)
            {
                var reports = await _dbContext.Reports
                    .Where(report => report.User.DisplayName == request.DisplayName)
                    .Include(report => report.User)
                    .Include(report => report.Reporter)
                    .Include(report => report.Comment)
                        .ThenInclude(comment => comment.Combo)
                            .ThenInclude(combo => combo.Character)
                    .Include(report => report.Combo)
                        .ThenInclude(combo => combo.Character)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<GetReportsByUserResponse>>(reports);
            }
            else
            {
                throw new SecurityException($"Not authorized to get reports");
            }
        }
    }
}
