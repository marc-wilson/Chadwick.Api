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
    /// Main API for Batting Post Season Stats
    /// </summary>
    [Route("api/batting-post")]
    public class BattingPostController : ChadwickBaseController
    {
        /// <summary>
        /// BattingPostController
        /// </summary>
        /// <param name="db"></param>
        public BattingPostController(ChadwickDbContext db) : base(db) {}
        
        /// <summary>
        /// Gets a paged list of post season batting stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetBattingPostAsync))]
        [ProducesResponseType(typeof(Paged<BattingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingPostAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.BattingPost.AsQueryable();

            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<BattingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Get post season batting stats by year
        /// </summary>
        /// <param name="year"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{yearId:int}", Name = nameof(GetBattingPostByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<BattingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingPostByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.BattingPost.Where(b => b.YearId == yearId);
            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<BattingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets post season batting stats by teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetBattingPostByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<BattingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingPostByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.BattingPost.Where(b => b.TeamId == teamId);
            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<BattingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets post season batting stats by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetBattingPostByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingPostByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.BattingPost.Where(b => b.LeagueId == leagueId);
            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<BattingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets post season batting stats by playoff level
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("level/{level}", Name = nameof(GetBattingPostByPlayoffLevelAsync))]
        [ProducesResponseType(typeof(Paged<BattingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingPostByPlayoffLevelAsync(string level, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var batting = Db.BattingPost.Where(b => b.LevelOfPlayoffs == level);
            var totalItems = await batting.CountAsync();
            var results = await batting.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<BattingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets the given players batting post stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetBattingPostStatsByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<BattingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingPostStatsByPlayerIdAsync(string playerId)
        {
            var battingPostStats = await Db.BattingPost.Where(b => b.PlayerId == playerId).ToListAsync();
            return Ok(battingPostStats);
        }
    }
}