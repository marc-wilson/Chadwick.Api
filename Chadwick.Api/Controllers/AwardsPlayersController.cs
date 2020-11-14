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
    /// Main API for Player Awards
    /// </summary>
    [Route("player-awards")]
    public class AwardsPlayersController : ChadwickBaseController
    {
        public AwardsPlayersController(ChadwickDbContext dbContext) : base(dbContext) {}

        /// <summary>
        /// Gets a paged list of Player Awards
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAwardsPlayersAsync))]
        [ProducesResponseType(typeof(Paged<AwardsPlayer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsPlayersAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsPlayers = Db.AwardsPlayers.AsQueryable();
            var totalItems = await awardsPlayers.CountAsync();
            var results = await awardsPlayers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsPlayer>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Player Awards by year
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{yearId:int}", Name = nameof(GetAwardsPlayersByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<AwardsPlayer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsPlayersByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsPlayers = Db.AwardsPlayers.Where(a => a.YearId == yearId);
            var totalItems = await awardsPlayers.CountAsync();
            var results = await awardsPlayers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsPlayer>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a pages list of Player Awards by leagueId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetAwardsPlayersByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<AwardsPlayer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsPlayersByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsPlayers = Db.AwardsPlayers.Where(a => a.LeagueId == leagueId);
            var totalItems = await awardsPlayers.CountAsync();
            var results = await awardsPlayers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsPlayer>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets the given Players awards
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetAwardsPlayersByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<AwardsPlayer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsPlayersByPlayerIdAsync(string playerId)
        {
            var awardsPlayers = await Db.AwardsPlayers.Where(a => a.PlayerId == playerId).ToListAsync();
            return Ok(awardsPlayers);
        }
    }
}