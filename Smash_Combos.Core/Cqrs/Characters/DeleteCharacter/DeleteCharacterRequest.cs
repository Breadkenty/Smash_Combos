﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smash_Combos.Core.Cqrs.Characters.DeleteCharacter
{
    public class DeleteCharacterRequest : IRequest<DeleteCharacterResponse>
    {
        public int CurrentUserId { get; set; }
        public int CharacterId { get; set; }
    }
}
