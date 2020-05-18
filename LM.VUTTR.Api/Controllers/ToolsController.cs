using LM.Responses;
using LM.Responses.Extensions;
using LM.VUTTR.Api.Domain.Tools.Commands;
using LM.VUTTR.Api.Domain.Tools.Commands.Handlers.Contracts;
using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Queries;
using LM.VUTTR.Api.Queries.Contratcs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class ToolsController : Controller
    {
        ICreateToolCommandHandle CreateToolCommandHandler { get; }

        IUpdateToolCommandHandle UpdateToolCommandHandler { get; }

        IRemoveToolCommandHandle RemoveToolCommandHandler { get; }

        IGetToolsQueryHandler GetToolsQueryHandler { get; }

        IGetToolQueryHandler GetToolQueryHandler { get; }

        IGetTagsQueryHandler GetTagsQueryHandler { get; }

        public ToolsController(ICreateToolCommandHandle createToolCommandHandler
        , IUpdateToolCommandHandle updateToolCommandHandler
        , IRemoveToolCommandHandle removeToolCommandHandler
        , IGetToolsQueryHandler getToolsQueryHandler
        , IGetToolQueryHandler getToolQueryHandler
        , IGetTagsQueryHandler getTagsQueryHandler)
        {
            CreateToolCommandHandler = createToolCommandHandler;
            UpdateToolCommandHandler = updateToolCommandHandler;
            RemoveToolCommandHandler = removeToolCommandHandler;
            GetToolsQueryHandler = getToolsQueryHandler;
            GetToolQueryHandler = getToolQueryHandler;
            GetTagsQueryHandler = getTagsQueryHandler;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetAsync()
        {
            var response = Response<List<GetToolsDto>>.Create();

            var tools = await GetToolsQueryHandler.HandleAsync(new GetToolsQuery());

            return Ok(response.SetValue(tools));
        }

        [HttpGet, Route("{code}")]
        public async Task<IActionResult> GetAsync(Guid code)
        {
            var response = Response<GetToolDto>.Create();

            var tool = await GetToolQueryHandler.HandleAsync(new GetToolQuery(code));

            if (tool.HasValue)
                return StatusCode(404, response.WithBusinessError(nameof(code)
                    , $"Ferramenta '{code}'não encontrada."));

            return Ok(response.SetValue(tool.Value));
        }

        [HttpGet, Route("{code}/tags")]
        public async Task<IActionResult> GetTagsAsync(Guid code)
        {
            var response = Response<GetTagsDto>.Create();

            var tags = await GetTagsQueryHandler.HandleAsync(new GetTagsQuery(code));

            return Ok(response.SetValue(tags));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateToolDto dto)
        {
            var command = new CreateToolCommand(dto.Name, dto.Link, dto.Description, dto.Tags);

            var response = await CreateToolCommandHandler.HandleAsync(command);

            if (response.HasError)
                return BadRequest(response);

            return StatusCode(201, response);
        }

        [HttpPut, Route("{code}")]
        public async Task<IActionResult> UpdateAsync(Guid code, [FromBody] UpdateToolDto dto)
        {
            var command = new UpdateToolCommand(code, dto.Name, dto.Link, dto.Description, dto.Tags);

            var response = await UpdateToolCommandHandler.HandleAsync(command);

            if (response.HasError)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete, Route("{code}")]
        public async Task<IActionResult> DeleteAsync(Guid code)
        {
            var command = new RemoveToolCommand(code);

            var response = await RemoveToolCommandHandler.HandleAsync(command);

            if (response.HasError)
                return BadRequest(response);

            return StatusCode(204, response);
        }
    }
}