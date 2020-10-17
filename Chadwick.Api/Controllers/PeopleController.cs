using System.Collections.Generic;
using System.IO;
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
    /// Main API for Players and Managers
    /// </summary>
    [Route("api/people")]
    public class PeopleController : ChadwickBaseController
    {
        /// <summary>
        /// PeopleController
        /// </summary>
        /// <param name="db"></param>
        public PeopleController(ChadwickDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Get All People
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPeopleAsync))]
        [ProducesResponseType(typeof(Paged<People>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            var totalCount = await Db.People.CountAsync();
            var offset = page * limit;
            if (offset > totalCount) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var people = await Db.People.Skip(page * limit).Take(limit).ToListAsync();
            var url = Path.Join(Request.Scheme, Request.Host.ToString(), Request.Path.ToString());
            var response = new Paged<People>(people, page, limit, totalCount, url);
            return Ok(response);
        }

        /// <summary>
        /// Gets a person by their PlayerId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetPersonByPlayerIdAsync))]
        [ProducesResponseType(typeof(People), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersonByPlayerIdAsync(string playerId)
        {
            var person = await Db.People.FirstOrDefaultAsync(p => p.PlayerId == playerId);
            if (person == null) return new NotFoundObjectResult(new ErrorResponse("Player not found"));
            return Ok(person);
        }

        /// <summary>
        /// Gets a persons batting stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}/batting", Name = nameof(GetBattingStatsAsync))]
        [ProducesResponseType(typeof(List<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingStatsAsync(string playerId)
        {
            var battingController = new BattingController(Db);
            return await battingController.GetBattingStatsByPlayerAsync(playerId);
        }
        
        /// <summary>
        /// Gets a persons batting post stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}/batting-post", Name = nameof(GetBattingPostStatsAsync))]
        [ProducesResponseType(typeof(List<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingPostStatsAsync(string playerId)
        {
            var battingPostController = new BattingPostController(Db);
            return await battingPostController.GetBattingPostStatsByPlayerIdAsync(playerId);
        }
        
        /// <summary>
        /// Gets a persons fielding stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}/fielding", Name = nameof(GetFieldingStatsAsync))]
        [ProducesResponseType(typeof(List<Fielding>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingStatsAsync(string playerId)
        {
            var fieldingController = new FieldingController(Db);
            return await fieldingController.GetFieldingStatsByPlayerAsync(playerId);
        }
    }
}