﻿using AutoMapper;
using SmashHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmashHub.Core.Cqrs.Characters.PostCharacter
{
    public class PostCharacterProfile : Profile
    {
        public PostCharacterProfile()
        {
            CreateMap<Character, PostCharacterResponse>();
        }
    }
}
