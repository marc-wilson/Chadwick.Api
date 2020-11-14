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
    /// Main API for Pitching stats
    /// </summary>
    [Route("api/pitching")]
    public class PitchingController : ChadwickBaseController
    {
        /// <summary>
        /// PitchingController
        /// </summary>
        /// <param name="dbContext"></param>
        public PitchingController(ChadwickDbContext dbContext) : base (dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Pitching stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPitchingAsync))]
        [ProducesResponseType(typeof(Paged<Pitching>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var pitching = Db.Pitching.AsQueryable();
            var totalItems = await pitching.CountAsync();
            var results = await pitching.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Pitching>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Pitching stats by yearId
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetPitchingByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<Pitching>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var pitching = Db.Pitching.Where(p => p.YearId == yearId);
            var totalItems = await pitching.CountAsync();
            var results = await pitching.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Pitching>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Pitching stats by teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetPitchingByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<Pitching>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var pitching = Db.Pitching.Where(p => p.TeamId == teamId);
            var totalItems = await pitching.CountAsync();
            var results = await pitching.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Pitching>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Pitching stats by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetPitchingByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<Pitching>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var pitching = Db.Pitching.Where(p => p.LeagueId == leagueId);
            var totalItems = await pitching.CountAsync();
            var results = await pitching.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Pitching>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Pitching stats by playerId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("player/{playerId}", Name = nameof(GetPitchingByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<Pitching>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingByPlayerIdAsync(string playerId)
        {
            var pitching = await Db.Pitching.Where(p => p.PlayerId == playerId).ToListAsync();
            return Ok(pitching);
        }
    }
}