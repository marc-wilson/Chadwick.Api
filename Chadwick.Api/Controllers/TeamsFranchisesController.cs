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
    /// Main API for Team Franchises
    /// </summary>
    [Route("api/team-franchises")]
    public class TeamsFranchisesController : ChadwickBaseController
    {
        /// <summary>
        /// TeamFranchiseController
        /// </summary>
        /// <param name="dbContext"></param>
        public TeamsFranchisesController(ChadwickDbContext dbContext) : base (dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Teams Franchises
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetTeamsFranchisesAsync))]
        [ProducesResponseType(typeof(Paged<TeamsFranchises>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsFranchisesAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var teams = Db.TeamsFranchises.AsQueryable();
            var totalItems = await teams.CountAsync();
            var results = await teams.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<TeamsFranchises>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a Team Franchise by franchiseId
        /// </summary>
        /// <returns></returns>
        [HttpGet("franchise/{franchiseId}", Name = nameof(GetTeamsFranchiseByFranchiseIdAsync))]
        [ProducesResponseType(typeof(TeamsFranchises), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamsFranchiseByFranchiseIdAsync(string franchiseId)
        {
            var team = await Db.TeamsFranchises.FirstOrDefaultAsync(t => t.FranchiseId == franchiseId);
            return Ok(team);
        }
    }
}