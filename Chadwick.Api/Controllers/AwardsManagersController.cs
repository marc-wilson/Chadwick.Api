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
    /// Main API for Manager Awards
    /// </summary>
    [Route("manager-awards")]
    public class AwardsManagersController : ChadwickBaseController
    {
        public AwardsManagersController(ChadwickDbContext dbContext) : base(dbContext) {}

        /// <summary>
        /// Gets a paged list of Manager Awards
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAwardsManagersAsync))]
        [ProducesResponseType(typeof(Paged<AwardsManager>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsManagersAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsManagers = Db.AwardsManagers.AsQueryable();
            var totalItems = await awardsManagers.CountAsync();
            var results = await awardsManagers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsManager>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Manager Awards by year
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{yearId:int}", Name = nameof(GetAwardsManagersByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<AwardsManager>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsManagersByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsManagers = Db.AwardsManagers.Where(a => a.YearId == yearId);
            var totalItems = await awardsManagers.CountAsync();
            var results = await awardsManagers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsManager>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a pages list of Manager Awards by leagueId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetAwardsManagersByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<AwardsManager>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsManagersByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var awardsManagers = Db.AwardsManagers.Where(a => a.LeagueId == leagueId);
            var totalItems = await awardsManagers.CountAsync();
            var results = await awardsManagers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AwardsManager>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets the given managers awards
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetAwardsManagersByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<AwardsManager>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardsManagersByPlayerIdAsync(string playerId)
        {
            var awardsManagers = await Db.AwardsManagers.Where(a => a.PlayerId == playerId).ToListAsync();
            return Ok(awardsManagers);
        }
    }
}