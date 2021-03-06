﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmashHub.Core.Cqrs.Comments.PutComment
{
    public class PutCommentRequest : IRequest<PutCommentResponse>
    {
        public int CommentId { get; set; }
        public int CurrentUserId { get; set; }
        public string Body { get; set; }
    }
}
