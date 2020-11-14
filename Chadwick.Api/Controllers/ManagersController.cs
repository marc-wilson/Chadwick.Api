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
    /// Main API for Managers
    /// </summary>
    [Route("api/managers")]
    public class ManagersController : ChadwickBaseController
    {
        public ManagersController(ChadwickDbContext dbContext) : base(dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Managers
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetManagersAsync))]
        [ProducesResponseType(typeof(Paged<Managers>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetManagersAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var managers = Db.Managers.AsQueryable();
            var totalItems = await managers.CountAsync();
            var results = await managers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Managers>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Managers by yearId
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetManagersByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<Managers>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetManagersByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var managers = Db.Managers.Where(m => m.YearId == yearId);
            var totalItems = await managers.CountAsync();
            var results = await managers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Managers>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Managers by teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId:int}", Name = nameof(GetManagersByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<Managers>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetManagersByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var managers = Db.Managers.Where(m => m.TeamId == teamId);
            var totalItems = await managers.CountAsync();
            var results = await managers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Managers>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Managers by leagueId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetManagersByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<Managers>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetManagersByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var managers = Db.Managers.Where(m => m.LeagueId == leagueId);
            var totalItems = await managers.CountAsync();
            var results = await managers.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Managers>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
    }
}