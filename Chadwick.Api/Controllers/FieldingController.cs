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
    /// Main API for Fielding Stats
    /// </summary>
    [Route("api/fielding")]
    public class FieldingController : ChadwickBaseController
    {
        /// <summary>
        /// FieldingController
        /// </summary>
        /// <param name="dbContext"></param>
        public FieldingController(ChadwickDbContext db) : base(db) {}

        /// <summary>
        /// Gets a paged list of fielding stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetFieldingAsync))]
        [ProducesResponseType(typeof(Paged<Fielding>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.Fielding.AsQueryable();

            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Fielding>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a pages list of fielding stats by year
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetFieldingByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<Fielding>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.Fielding.Where(f => f.YearId == yearId);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Fielding>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a pages list of fielding stats by teamId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetFieldingByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<Fielding>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.Fielding.Where(f => f.TeamId == teamId);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Fielding>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a pages list of fielding stats by leagueId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{teamId}", Name = nameof(GetFieldingByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<Fielding>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.Fielding.Where(f => f.LeagueId == leagueId);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Fielding>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets the given players fielding stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetFieldingStatsByPlayerAsync))]
        [ProducesResponseType(typeof(List<Fielding>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingStatsByPlayerAsync(string playerId)
        {
            var fieldingStats = await Db.Fielding.Where(f => f.PlayerId == playerId).ToListAsync();
            return Ok(fieldingStats);
        }
    }
}