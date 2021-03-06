﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmashHub.Core.Cqrs.Users.GetUser
{
    public class ReportDto
    {
        public int Id { get; set; }

        public UserDto User { get; set; }

        public UserDto Reporter { get; set; }

        public string Body { get; set; }

        public DateTime DateReported { get; set; }

        public bool Dismiss { get; set; }
    }
}
