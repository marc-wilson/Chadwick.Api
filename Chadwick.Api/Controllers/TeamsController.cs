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
    [Route("api/teams")]
    public class TeamsController : ChadwickBaseController
    {
        public TeamsController(ChadwickDbContext dbContext) : base(dbContext)
        {
        }
        
        /// <summary>
        /// Gets a paged list of Teams
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetTeamsAsync))]
        [ProducesResponseType(typeof(Paged<Teams>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var teams = Db.Teams.AsQueryable();
            var totalItems = await teams.CountAsync();
            var results = await teams.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Teams>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Teams by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetTeamsByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<Teams>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var teams = Db.Teams.Where(t => t.LeagueId == leagueId);
            var totalItems = await teams.CountAsync();
            var results = await teams.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Teams>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Teams by franchiseId
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("franchise/{franchiseId}", Name = nameof(GetTeamsByFranchiseIdAsync))]
        [ProducesResponseType(typeof(Paged<Teams>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsByFranchiseIdAsync(string franchiseId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var teams = Db.Teams.Where(t => t.FranchiseId == franchiseId);
            var totalItems = await teams.CountAsync();
            var results = await teams.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Teams>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Teams by divisionId
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("division/{divisionId}", Name = nameof(GetTeamsByDivisionIdAsync))]
        [ProducesResponseType(typeof(Paged<Teams>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsByDivisionIdAsync(string divisionId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var teams = Db.Teams.Where(t => t.DivisionId == divisionId);
            var totalItems = await teams.CountAsync();
            var results = await teams.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Teams>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
    }
}