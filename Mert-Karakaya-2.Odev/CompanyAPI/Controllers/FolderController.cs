using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Core;
using CompanyAPI.Data.DTO;
using CompanyAPI.Data.Model;
using CompanyAPI.Service.Services;
using Microsoft.Extensions.Logging;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : BaseController<FolderDto, Folder>
    {
        private readonly IFolderService _service;
        private readonly ILogger<FolderController> _logger;
        public FolderController(IFolderService folderService, ILogger<FolderController> logger) : base(folderService)
        {
            _service = folderService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get Method");

            var result = await _service.GetAllAsync();

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.data is null)
                return NoContent();

            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public new async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get a Folder with Id is {id}.");

            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public new async Task<IActionResult> CreateAsync([FromBody] FolderDto folder)
        {
            _logger.LogInformation($"Created a Folder.");

            //Validasyon yazılacak.

            var insertResult = await _service.InsertAsync(folder);

            if (!insertResult.isSuccess)
                return BadRequest(insertResult);

            return StatusCode(201, insertResult);
        }

        [HttpPut("{id:int}")]
        public new async Task<IActionResult> UpdateAsync(int id, [FromBody] FolderDto folder)
        {
            _logger.LogInformation($"Update a Folder with Id is {id}.");

            return await base.UpdateAsync(id, folder);
        }


        [HttpDelete("{id:int}")]
        public new async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete a Folder with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    }
}
