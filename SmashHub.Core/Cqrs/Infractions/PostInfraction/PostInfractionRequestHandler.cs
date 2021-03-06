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

namespace SmashHub.Core.Cqrs.Infractions.PostInfraction
{
    public class PostInfractionRequestHandler : IRequestHandler<PostInfractionRequest, PostInfractionResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public PostInfractionRequestHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PostInfractionResponse> Handle(PostInfractionRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.Where(user => user.Id == request.UserId).FirstOrDefaultAsync();
            if (user == null)
                throw new KeyNotFoundException($"User with id {request.UserId} does not exist");

            var currentUser = await _dbContext.Users.Where(user => user.Id == request.CurrentUserId).FirstOrDefaultAsync();
            if (currentUser == null)
                throw new KeyNotFoundException($"User with id {request.CurrentUserId} does not exist");


            if (currentUser.UserType == UserType.Moderator || currentUser.UserType == UserType.Admin)
            {
                if (user.Id == currentUser.Id)
                    throw new ArgumentException("Moderators cannot infract themselves");

                var infraction = new Infraction
                {
                    User = user,
                    Moderator = currentUser,
                    Body = request.Body,
                    BanDuration = request.BanDuration,
                    Category = request.Category,
                    Points = DeterminePoints(request)
                };
                _dbContext.Infractions.Add(infraction);

                user.Infractions.Add(infraction);
                _dbContext.Entry(user).State = EntityState.Modified;

                var reportsForUser = await _dbContext.Reports.Where(report => report.User.Id == user.Id).ToListAsync();

                foreach (var report in reportsForUser)
                {
                    report.Dismiss = true;
                    _dbContext.Entry(report).State = EntityState.Modified;
                }

                await _dbContext.SaveChangesAsync(CancellationToken.None);
                return _mapper.Map<PostInfractionResponse>(infraction);
            }
            else
            {
                throw new SecurityException("Not authorized to create infractions");
            }
        }

        private int? DeterminePoints(PostInfractionRequest request)
        {
            if (request.Points != null)
                return request.Points;

            return request.Category switch
            {
                InfractionCategory.Spam => 1,
                InfractionCategory.Harassment => 2,
                InfractionCategory.Inappropriate => 2,
                InfractionCategory.UnauthorizedPromotion => 1,
                _ => null,
            };
        }
    }
}
