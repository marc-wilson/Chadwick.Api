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
    /// Main API for Outfield Position Split Season Stats
    /// </summary>
    [Route("api/fielding-of-split")]
    public class FieldingOFSplitController : ChadwickBaseController
    {
        /// <summary>
        /// FieldingOFSplitController
        /// </summary>
        /// <param name="db"></param>
        public FieldingOFSplitController(ChadwickDbContext db) : base(db) {}
        
        /// <summary>
        /// Gets a paged list of outfield position split season stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetFieldingOFSplitAsync))]
        [ProducesResponseType(typeof(Paged<FieldingOFSplit>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingOFSplitAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingOFSplit.AsQueryable();
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingOFSplit>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of outfield position split season stats by yearId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{yearId:int}", Name = nameof(GetFieldingOFSplitByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<FieldingOFSplit>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingOFSplitByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingOFSplit.Where(f => f.YearId == yearId);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingOFSplit>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of outfield position split season stats by playerId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetFieldingOFSplitByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<FieldingOFSplit>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingOFSplitByPlayerIdAsync(string playerId)
        {
            var fieldingStats = await Db.FieldingOFSplit.Where(f => f.PlayerId == playerId).ToListAsync();
            return Ok(fieldingStats);
        }
    }
}