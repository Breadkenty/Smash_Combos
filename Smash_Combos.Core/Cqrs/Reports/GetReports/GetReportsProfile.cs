﻿using AutoMapper;
using Smash_Combos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smash_Combos.Core.Cqrs.Reports.GetReports
{
    public class GetReportsProfile : Profile
    {
        public GetReportsProfile()
        {
            CreateMap<Report, GetReportsResponse>();
            CreateMap<User, UserDto>();
        }
    }
}
