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
    /// Main API for Shared Player Awards
    /// </summary>
    [Route("shared-player-awards")]
    public class AwardsSharedPlayersController : ChadwickBaseController
    {
        /// <summary>
        /// AwardsSharedPlayersController
        /// </summary>
        /// <param name="dbContext"></param>
        public AwardsSharedPlayersController(ChadwickDbContext dbContext) : base(dbContext) {}

        /// <summary>
        /// Gets a paged list of Shared Player Awards
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAwardsSharedPlayersAsync))]
        [ProducesResponseType(typeof(Paged<AwardsSharePlayer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsSharedPlayersAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsSharedPlayers = Db.AwardsSharePlayers.AsQueryable();
            var totalItems = await awardsSharedPlayers.CountAsync();
            var results = await awardsSharedPlayers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsSharePlayer>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Shared Player Awards by year
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetAwardsSharedPlayersByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<AwardsSharePlayer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsSharedPlayersByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsSharedPlayers = Db.AwardsSharePlayers.Where(a => a.YearId == yearId);
            var totalItems = await awardsSharedPlayers.CountAsync();
            var results = await awardsSharedPlayers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsSharePlayer>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a pages list of Shared Player Awards by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetAwardsSharedPlayersByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<AwardsSharePlayer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsSharedPlayersByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsSharedPlayers = Db.AwardsSharePlayers.Where(a => a.LeagueId == leagueId);
            var totalItems = await awardsSharedPlayers.CountAsync();
            var results = await awardsSharedPlayers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsSharePlayer>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets the given Shared Players awards
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetAwardsSharedPlayersByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<AwardsSharePlayer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsSharedPlayersByPlayerIdAsync(string playerId)
        {
            var awardsSharedPlayers = await Db.AwardsSharePlayers.Where(a => a.PlayerId == playerId).ToListAsync();
            return Ok(awardsSharedPlayers);
        }
    }
}