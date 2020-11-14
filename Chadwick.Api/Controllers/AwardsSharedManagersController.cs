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
    /// Main API for Shared Manager Awards
    /// </summary>
    [Route("shared-manager-awards")]
    public class AwardsSharedManagersController : ChadwickBaseController
    {
        /// <summary>
        /// AwardsSharedManagersController
        /// </summary>
        /// <param name="dbContext"></param>
        public AwardsSharedManagersController(ChadwickDbContext dbContext) : base(dbContext) {}

        /// <summary>
        /// Gets a paged list of Shared Manager Awards
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAwardsSharedManagersAsync))]
        [ProducesResponseType(typeof(Paged<AwardsShareManager>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsSharedManagersAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsSharedManagers = Db.AwardsShareManagers.AsQueryable();
            var totalItems = await awardsSharedManagers.CountAsync();
            var results = await awardsSharedManagers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsShareManager>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Shared Manager Awards by year
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetAwardsSharedManagersByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<AwardsShareManager>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsSharedManagersByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsSharedManagers = Db.AwardsShareManagers.Where(a => a.YearId == yearId);
            var totalItems = await awardsSharedManagers.CountAsync();
            var results = await awardsSharedManagers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsShareManager>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a pages list of Shared Manager Awards by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetAwardsSharedManagersByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<AwardsShareManager>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsSharedManagersByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsSharedManagers = Db.AwardsShareManagers.Where(a => a.LeagueId == leagueId);
            var totalItems = await awardsSharedManagers.CountAsync();
            var results = await awardsSharedManagers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsShareManager>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets the given managers shared awards
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetAwardsSharedManagersByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<AwardsManager>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsSharedManagersByPlayerIdAsync(string playerId)
        {
            var awardsSharedManagers = await Db.AwardsShareManagers.Where(a => a.PlayerId == playerId).ToListAsync();
            return Ok(awardsSharedManagers);
        }
    }
}