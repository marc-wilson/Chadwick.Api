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
    /// Main API for Batting Stats
    /// </summary>
    [Route("api/batting")]
    public class BattingController : ChadwickBaseController
    {
        /// <summary>
        /// BattingController
        /// </summary>
        /// <param name="dbContext"></param>
        public BattingController(ChadwickDbContext dbContext) : base(dbContext) {}

        /// <summary>
        /// Gets a paged list of batting stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns>
        /// </returns>
        [HttpGet(Name = nameof(GetBattingAsync))]
        [ProducesResponseType(typeof(Paged<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.Batting.AsQueryable();

            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Batting>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Get batting stats by year
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetBattingByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.Batting.Where(b => b.YearId == yearId);
            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Batting>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets batting stats by teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetBattingByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.Batting.Where(b => b.TeamId == teamId);
            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Batting>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets batting stats by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetBattingByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.Batting.Where(b => b.LeagueId == leagueId);
            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Batting>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets the given players batting stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetBattingStatsByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingStatsByPlayerIdAsync(string playerId)
        {
            var battingStats = await Db.Batting.Where(b => b.PlayerId == playerId).ToListAsync();
            return Ok(battingStats);
        }
    }
}