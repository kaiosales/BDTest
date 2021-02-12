using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BDTest.Core.Models;
using BDTest.Data;
using BDTest.EAPI.Models;

namespace BDTest.EAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger<ProcessController> _logger;
        private IRepository<Batch> _batchRepository;

        public ProcessController(ILogger<ProcessController> logger, IRepository<Batch> batchRepository)
        {
            _logger = logger;
            _batchRepository = batchRepository;
        }

        [HttpGet]
        public IAsyncEnumerable<Batch> Get() => _batchRepository.GetAllAsyncIncluding(navigationPropertyPath: b => b.Numbers);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBatch(long id)
        {
            Batch batch = await _batchRepository.GetByIdAsyncIncluding(id, b => b.Numbers);

            if (batch == null)
                return NotFound();

            return Ok(batch);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateRequest request)
        {
            if (request.Batches < 1 || request.NumberCount < 1)
                return BadRequest();

            List<Batch> batchList = new List<Batch>(request.Batches);
            for(var i = 0; i < request.Batches; i++)
                batchList.Add(new Batch { Count = request.NumberCount });

            await _batchRepository.BulkInsertAsync(batchList);

            return Ok(batchList);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            IEnumerable<Batch> batches = _batchRepository.GetAll();
            await _batchRepository.BulkDeleteAsync(batches);

            return Ok();
        }
    }
}
