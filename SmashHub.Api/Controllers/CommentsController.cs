using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmashHub.Domain.Models;
using SmashHub.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmashHub.Core.Cqrs.Comments.GetComments;
using SmashHub.Core.Cqrs.Comments.GetComment;
using SmashHub.Core.Cqrs.Comments.PutComment;
using SmashHub.Core.Cqrs.Comments.PostComment;
using SmashHub.Core.Cqrs.Comments.DeleteComment;
using System;
using Hellang.Middleware.ProblemDetails;

namespace SmashHub.Controllers
{
    // All of these routes will be at the base URL:     /api/Comments
    // That is what "api/[controller]" means below. It uses the name of the controller
    // in this case CommentsController to determine the URL
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        // This is the variable you use to have access to your database
        private readonly IMediator _mediator;

        // Constructor that recives a reference to your database context
        // and stores it in _context for you to use in your API methods
        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Comments
        //
        // Returns a list of all your Comments
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCommentsResponse>>> GetComments() => Ok(await _mediator.Send(new GetCommentsRequest()));

        // GET: api/Comments/5
        //
        // Fetches and returns a specific comment by finding it by id. The id is specified in the
        // URL. In the sample URL above it is the `5`.  The "{id}" in the [HttpGet("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCommentResponse>> GetComment([FromRoute] int id) => Ok(await _mediator.Send(new GetCommentRequest { CommentId = id }));

        // PUT: api/Comments/5
        //
        // Update an individual comment with the requested id. The id is specified in the URL
        // In the sample URL above it is the `5`. The "{id} in the [HttpPut("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        // In addition the `body` of the request is parsed and then made available to us as a Comment
        // variable named comment. The controller matches the keys of the JSON object the client
        // supplies to the names of the attributes of our Comment POCO class. This represents the
        // new values for the record.
        //
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutComment([FromRoute] int id, [FromBody] PutCommentRequest request)
        {
            if (id != request.CommentId) // If the ID in the URL does not match the ID in the supplied request body, return a bad request
                return BadRequest(new StatusCodeProblemDetails(400) { Detail = "Id in URL and Comment don't match" });

            request.CurrentUserId = GetCurrentUserId();

            var response = await _mediator.Send(request);

            if (response != null)
                return Ok();
            else
                return StatusCode(500);
        }

        // POST: api/Comments
        //
        // Creates a new comment in the database.
        //
        // The `body` of the request is parsed and then made available to us as a Comment
        // variable named comment. The controller matches the keys of the JSON object the client
        // supplies to the names of the attributes of our Comment POCO class. This represents the
        // new values for the record.
        //
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostComment(PostCommentRequest request)
        {
            request.CurrentUserId = GetCurrentUserId();

            var response = await _mediator.Send(request);

            if (response != null)
                return CreatedAtAction("GetComment", new { id = response.Id }, response);
            else
                return StatusCode(500);
        }

        // DELETE: api/Comments/5
        //
        // Deletes an individual comment with the requested id. The id is specified in the URL
        // In the sample URL above it is the `5`. The "{id} in the [HttpDelete("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var response = await _mediator.Send(new DeleteCommentRequest { CommentId = id, CurrentUserId = GetCurrentUserId() });

            if (response != null)
                return Ok();
            else
                return StatusCode(500);
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.Claims.SingleOrDefault(claim => claim.Type == "Id").Value);
        }
    }
}

