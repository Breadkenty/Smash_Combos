﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smash_Combos.Core.Cqrs.Votes.PostCommentVote
{
    public class PostCommentVoteRequest : IRequest<PostCommentVoteResponse>
    {
        public int CommentId { get; set; }
        public int CurrentUserId { get; set; }
        public bool IsUpVote { get; set; }
    }
}
