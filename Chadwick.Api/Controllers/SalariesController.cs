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
    /// Main API for Salaries
    /// </summary>
    [Route("api/salaries")]
    public class SalariesController : ChadwickBaseController
    {
        public SalariesController(ChadwickDbContext dbContext) : base (dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Salaries
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetSalariesAsync))]
        [ProducesResponseType(typeof(Paged<Salaries>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalariesAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var salaries = Db.Salaries.AsQueryable();
            var totalItems = await salaries.CountAsync();
            var results = await salaries.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Salaries>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Salaries by yearId
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetSalariesByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<Salaries>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalariesByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var salaries = Db.Salaries.Where(s => s.YearId == yearId);
            var totalItems = await salaries.CountAsync();
            var results = await salaries.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Salaries>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Salaries by teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetSalariesByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<Salaries>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalariesByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var salaries = Db.Salaries.Where(s => s.TeamId == teamId);
            var totalItems = await salaries.CountAsync();
            var results = await salaries.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Salaries>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Salaries by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetSalariesByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<Salaries>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalariesByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var salaries = Db.Salaries.Where(s => s.LeagueId == leagueId);
            var totalItems = await salaries.CountAsync();
            var results = await salaries.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Salaries>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a list of Salaries by playerId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("player/{playerId}", Name = nameof(GetSalariesByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<Salaries>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalariesByPlayerIdAsync(string playerId)
        {
            var salaries = Db.Salaries.Where(s => s.PlayerId == playerId).ToListAsync();
            return Ok(salaries);
        }
    }
}