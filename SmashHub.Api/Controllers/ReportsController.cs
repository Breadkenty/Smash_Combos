﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SmashHub.Core.Cqrs.Reports.DeleteReport;
using SmashHub.Core.Cqrs.Reports.GetReport;
using SmashHub.Core.Cqrs.Reports.GetReportsByUser;
using SmashHub.Core.Cqrs.Reports.GetReports;
using SmashHub.Core.Cqrs.Reports.PostComboReport;
using SmashHub.Core.Cqrs.Reports.PostCommentReport;
using SmashHub.Core.Cqrs.Reports.DismissReport;
using SmashHub.Domain.Models;
using Hellang.Middleware.ProblemDetails;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ReportsController>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<GetReportsResponse>>> GetReports() => Ok(await _mediator.Send(new GetReportsRequest { CurrentUserId = GetCurrentUserId() }));

        // GET api/<ReportsController>/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GetReportResponse>> GetReport([FromRoute] int id) => Ok(await _mediator.Send(new GetReportRequest { ReportId = id, CurrentUserId = GetCurrentUserId() }));

        // Get api/<ReportsController/displayName
        [HttpGet("user/{userName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GetReportsByUserResponse>> GetReportsByUser([FromRoute] string userName) => Ok(await _mediator.Send(new GetReportsByUserRequest { DisplayName = userName, CurrentUserId = GetCurrentUserId() }));

        // POST api/<ReportsControler>/combo/5
        [HttpPost("combo/{comboId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostComboReportResponse>> PostComboReport([FromRoute] int comboId, [FromBody] PostComboReportRequest request)
        {
            if (comboId != request.ComboId)
                return BadRequest(new StatusCodeProblemDetails(400) { Detail = "Id in URL and Combo don't match" });

            var response = await _mediator.Send(request);

            if (response != null)
                return CreatedAtAction("GetReport", new { id = response.Id }, response);
            else
                return StatusCode(500);
        }

        // POST api/<ReportsControler>/comment/5
        [HttpPost("comment/{commentId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostCommentReportResponse>> PostCommentReport([FromRoute] int commentId, [FromBody] PostCommentReportRequest request)
        {
            if (commentId != request.CommentId)
                return BadRequest(new StatusCodeProblemDetails(400) { Detail = "Id in URL and Comment don't match" });

            var response = await _mediator.Send(request);

            if (response != null)
                return CreatedAtAction("GetReport", new { id = response.Id }, response);
            else
                return StatusCode(500);
        }

        // PUT api/<ReportsController>/5
        [HttpPut("dismiss/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<DismissReportResponse>> DismissReport([FromRoute] int id, [FromBody] DismissReportRequest request)
        {
            if (id != request.ReportId) // If the ID in the URL does not match the ID in the supplied request body, return a bad request
                return BadRequest(new StatusCodeProblemDetails(400) { Detail = "Id in URL and Report don't match" });

            request.CurrentUserId = GetCurrentUserId();

            var response = await _mediator.Send(request);

            if (response != null)
                return Ok();
            else
                return StatusCode(500);
        }

        // DELETE api/<ReportsController>/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteReport([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteReportRequest { ReportId = id, CurrentUserId = GetCurrentUserId() });

            if (response != null)
                return Ok();
            else
                return StatusCode(500);
        }

        private int GetCurrentUserId()
        {
            // Get the User Id from the claim and then parse it as an integer.
            return int.Parse(User.Claims.SingleOrDefault(claim => claim.Type == "Id").Value);
        }
    }
}
