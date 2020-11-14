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
    /// Main API for Appearance Stats
    /// </summary>
    [Route("api/appearances")]
    public class AppearancesController : ChadwickBaseController
    {
        /// <summary>
        /// AllStarFullController
        /// </summary>
        /// <param name="dbContext"></param>
        public AppearancesController(ChadwickDbContext dbContext) : base(dbContext) {}

        /// <summary>
        /// Gets a paged list of appearance stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAppearancesAsync))]
        [ProducesResponseType(typeof(Paged<Appearance>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppearancesAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var appearances = Db.Appearances.AsQueryable();
            var totalItems = await appearances.CountAsync();
            var results = await appearances.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Appearance>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of appearance stats by year
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetAppearancesByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<Appearance>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppearancesByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var appearances = Db.Appearances.Where(f => f.YearId == yearId);
            var totalItems = await appearances.CountAsync();
            var results = await appearances.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Appearance>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of appearance stats by teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>a
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetAppearancesByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<Appearance>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppearancesByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var appearances = Db.Appearances.Where(f => f.TeamId == teamId);
            var totalItems = await appearances.CountAsync();
            var results = await appearances.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Appearance>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a pages list of appearance stats by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetAppearancesByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<Appearance>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppearancesByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var appearances = Db.Appearances.Where(f => f.LeagueId == leagueId);
            var totalItems = await appearances.CountAsync();
            var results = await appearances.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Appearance>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
    }
}