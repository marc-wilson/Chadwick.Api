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
    /// Main API for Post Season Fielding Stats
    /// </summary>
    [Route("api/fielding-post")]
    public class FieldingPostController : ChadwickBaseController
    {
        /// <summary>
        /// FieldingPostController
        /// </summary>
        /// <param name="dbContext"></param>
        public FieldingPostController(ChadwickDbContext dbContext) : base(dbContext) {}
        
        /// <summary>
        /// Gets a paged list of fielding post season stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetFieldingPostAsync))]
        [ProducesResponseType(typeof(Paged<FieldingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingPostAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingPost.AsQueryable();

            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of fielding post season stats by year
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetFieldingPostByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<FieldingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingPostByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingPost.Where(f => f.YearId == yearId);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of fielding post season stats by teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetFieldingPostByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<FieldingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingPostByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingPost.Where(f => f.TeamId == teamId);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a pages list of fielding post season stats by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetFieldingPostByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<FieldingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingPostByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingPost.Where(f => f.LeagueId == leagueId);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets post season fielding stats by playoff level
        /// </summary>
        /// <param name="level"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("level/{level}", Name = nameof(GetFieldingPostByPlayoffLevelAsync))]
        [ProducesResponseType(typeof(Paged<FieldingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingPostByPlayoffLevelAsync(string level, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var fielding = Db.FieldingPost.Where(f => f.LevelOfPlayoffs == level);
            var totalItems = await fielding.CountAsync();
            var results = await fielding.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<FieldingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets the given players fielding post season stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetFieldingPostStatsByPlayerAsync))]
        [ProducesResponseType(typeof(List<FieldingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingPostStatsByPlayerAsync(string playerId)
        {
            var fieldingStats = await Db.FieldingPost.Where(f => f.PlayerId == playerId).ToListAsync();
            return Ok(fieldingStats);
        }
    }
}