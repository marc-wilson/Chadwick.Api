using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chadwick.Api.Models;
using Chadwick.Database;
using Chadwick.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chadwick.Api.Controllers
{
    /// <summary>
    /// Main API for Outfield Position Stats
    /// </summary>
    [Route("api/fielding-of")]
    public class FieldingOFController : ChadwickBaseController
    {
        /// <summary>
        /// FieldingOFController
        /// </summary>
        /// <param name="db"></param>
        public FieldingOFController(ChadwickDbContext db) : base(db) {}
        
        /// <summary>
        /// Gets a paged list of outfield position stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetFieldingOFAsync))]
        [ProducesResponseType(typeof(Paged<FieldingOF>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingOFAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingOF.AsQueryable();
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingOF>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of outfield position stats by yearId
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetFieldingOFByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<FieldingOF>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingOFByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingOF.Where(f => f.YearId == yearId);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingOF>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a list of outfield position stats by playerId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetFieldingOFByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<FieldingOF>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingOFByPlayerIdAsync(string playerId)
        {
            var fieldingStats = await Db.FieldingOF.Where(f => f.PlayerId == playerId).ToListAsync();
            return Ok(fieldingStats);
        }
    }
}