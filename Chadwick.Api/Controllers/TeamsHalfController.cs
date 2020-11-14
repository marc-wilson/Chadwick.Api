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
    /// Main API for Teams
    /// </summary>
    [Route("api/teams-half")]
    public class TeamsHalfController : ChadwickBaseController
    {
        /// <summary>
        /// TeamsHalfController
        /// </summary>
        /// <param name="dbContext"></param>
        public TeamsHalfController(ChadwickDbContext dbContext) : base(dbContext)
        {
        }
        
        /// <summary>
        /// Gets a paged list of Teams
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetTeamsHalfAsync))]
        [ProducesResponseType(typeof(Paged<TeamsHalf>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsHalfAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var teams = Db.TeamsHalf.AsQueryable();
            var totalItems = await teams.CountAsync();
            var results = await teams.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<TeamsHalf>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Teams by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetTeamsHalfByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<TeamsHalf>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsHalfByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var teams = Db.TeamsHalf.Where(t => t.LeagueId == leagueId);
            var totalItems = await teams.CountAsync();
            var results = await teams.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<TeamsHalf>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Teams by divisionId
        /// </summary>
        /// <param name="divisionId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("division/{divisionId}", Name = nameof(GetTeamsHalfByDivisionIdAsync))]
        [ProducesResponseType(typeof(Paged<Teams>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsHalfByDivisionIdAsync(string divisionId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var teams = Db.TeamsHalf.Where(t => t.DivisionId == divisionId);
            var totalItems = await teams.CountAsync();
            var results = await teams.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<TeamsHalf>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
    }
}