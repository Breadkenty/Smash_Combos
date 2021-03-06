﻿using AutoMapper;
using SmashHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmashHub.Core.Cqrs.Infractions.PostInfraction
{
    public class PostInfractionProfile : Profile
    {
        public PostInfractionProfile()
        {
            CreateMap<Infraction, PostInfractionResponse>();
            CreateMap<User, UserDto>();
        }
    }
}
