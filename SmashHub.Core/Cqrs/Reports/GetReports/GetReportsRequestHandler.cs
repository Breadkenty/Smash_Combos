﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmashHub.Core.Services;
using SmashHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmashHub.Core.Cqrs.Reports.GetReports
{
    public class GetReportsRequestHandler : IRequestHandler<GetReportsRequest, IEnumerable<GetReportsResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetReportsRequestHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<GetReportsResponse>> Handle(GetReportsRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _dbContext.Users.Where(user => user.Id == request.CurrentUserId).SingleOrDefaultAsync();

            if (currentUser == null)
                throw new KeyNotFoundException($"User with id {request.CurrentUserId} does not exist");

            if (currentUser.UserType == UserType.Moderator || currentUser.UserType == UserType.Admin)
            {

                var reports = await _dbContext.Reports
                    .Include(report => report.User)
                    .Include(report => report.Reporter)
                    .Include(report => report.Combo)
                    .Include(report => report.Comment)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<GetReportsResponse>>(reports);
            }
            else
            {
                throw new SecurityException($"Not authorized to get reports");
            }
        }
    }
}
