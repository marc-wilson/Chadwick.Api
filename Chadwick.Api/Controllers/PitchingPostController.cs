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
    [Route("api/pitching-post")]
    public class PitchingPostController : ChadwickBaseController
    {
        /// <summary>
        /// PitchingPostController
        /// </summary>
        /// <param name="dbContext"></param>
        public PitchingPostController(ChadwickDbContext dbContext) : base (dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Pitching stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPitchingPostAsync))]
        [ProducesResponseType(typeof(Paged<PitchingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingPostAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var pitching = Db.PitchingPost.AsQueryable();
            var totalItems = await pitching.CountAsync();
            var results = await pitching.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<PitchingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Pitching stats by yearId
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetPitchingPostByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<PitchingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingPostByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var pitching = Db.PitchingPost.Where(p => p.YearId == yearId);
            var totalItems = await pitching.CountAsync();
            var results = await pitching.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<PitchingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Pitching stats by teamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetPitchingPostByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<PitchingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingPostByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var pitching = Db.PitchingPost.Where(p => p.TeamId == teamId);
            var totalItems = await pitching.CountAsync();
            var results = await pitching.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<PitchingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Pitching stats by leagueId
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("league/{leagueId}", Name = nameof(GetPitchingPostByLeagueIdAsync))]
        [ProducesResponseType(typeof(Paged<PitchingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingPostByLeagueIdAsync(string leagueId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var pitching = Db.PitchingPost.Where(p => p.LeagueId == leagueId);
            var totalItems = await pitching.CountAsync();
            var results = await pitching.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<PitchingPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of Pitching stats by playerId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("player/{playerId}", Name = nameof(GetPitchingPostByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<PitchingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPitchingPostByPlayerIdAsync(string playerId)
        {
            var pitching = await Db.PitchingPost.Where(p => p.PlayerId == playerId).ToListAsync();
            return Ok(pitching);
        }
    }
}