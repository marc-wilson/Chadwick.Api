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
    /// Main API for Home Game stats
    /// </summary>
    [Route("api/home-games")]
    public class HomeGamesController : ChadwickBaseController
    {
        public HomeGamesController(ChadwickDbContext dbContext) : base(dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Home Game stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetHomeGamesAsync))]
        [ProducesResponseType(typeof(Paged<HomeGames>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHomeGamesAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var homeGames = Db.HomeGames.AsQueryable();
            var totalItems = await homeGames.CountAsync();
            var results = await homeGames.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<HomeGames>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Home Game stats by yearId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetHomeGamesByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<HomeGames>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHomeGamesByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var homeGames = Db.HomeGames.Where(h => h.YearKey == yearId);
            var totalItems = await homeGames.CountAsync();
            var results = await homeGames.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<HomeGames>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Home Game stats by leagueId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetHomeGamesByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<HomeGames>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHomeGamesByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var homeGames = Db.HomeGames.Where(h => h.LeagueKey == leagueId);
            var totalItems = await homeGames.CountAsync();
            var results = await homeGames.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<HomeGames>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Home Game stats by teamId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetHomeGamesByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<HomeGames>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHomeGamesByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var homeGames = Db.HomeGames.Where(h => h.TeamKey == teamId);
            var totalItems = await homeGames.CountAsync();
            var results = await homeGames.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<HomeGames>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Home Game stats by parkId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("ballpark/{parkId}", Name = nameof(GetHomeGamesByParkIdAsync))]
        [ProducesResponseType(typeof(Paged<HomeGames>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHomeGamesByParkIdAsync(string parkId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var homeGames = Db.HomeGames.Where(h => h.ParkKey == parkId);
            var totalItems = await homeGames.CountAsync();
            var results = await homeGames.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<HomeGames>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
    }
}