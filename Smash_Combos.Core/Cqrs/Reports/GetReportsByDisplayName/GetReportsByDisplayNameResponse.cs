﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Smash_Combos.Core.Cqrs.Reports.GetReportsByDisplayName
{
    public class GetReportsByDisplayNameResponse
    {
        public int Id { get; set; }

        public UserDto User { get; set; }

        public UserDto Reporter { get; set; }

        public CommentDto Comment { get; set; }

        public ComboDto Combo { get; set; }

        public string Body { get; set; }

        public DateTime DateReported { get; set; }

        public bool Dismiss { get; set; }
    }
}