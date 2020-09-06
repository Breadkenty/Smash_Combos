﻿using Smash_Combos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smash_Combos.Core.Cqrs.Users.GetUsers
{
    public class GetUsersResponse : ResponseBase<IEnumerable<UserFullDto>>
    {
    }
}
