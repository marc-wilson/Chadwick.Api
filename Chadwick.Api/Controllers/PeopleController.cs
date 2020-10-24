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
        /// Gets all people
        /// </summary>
        /// <param name="country"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPeopleAsync))]
        [ProducesResponseType(typeof(Paged<People>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleAsync(string country = null, string state = null, string city = null, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var people = Db.People.AsQueryable();
            if (!string.IsNullOrWhiteSpace(country))
                people = people.Where(p => p.BirthCountry == country);
            if (!string.IsNullOrWhiteSpace(state))
                people = people.Where(p => p.BirthState == state);
            if (!string.IsNullOrWhiteSpace(city))
                people = people.Where(p => p.BirthCity == city);
            var totalCount = await people.CountAsync();
            var results = await people.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<People>(results, page, limit, totalCount, Request);
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
            return await battingController.GetBattingStatsByPlayerIdAsync(playerId);
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
        
        /// <summary>
        /// Gets a persons post season fielding stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}/fielding-post", Name = nameof(GetFieldingPostStatsAsync))]
        [ProducesResponseType(typeof(List<FieldingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingPostStatsAsync(string playerId)
        {
            var fieldingPostController = new FieldingPostController(Db);
            return await fieldingPostController.GetFieldingPostStatsByPlayerAsync(playerId);
        }
    }
}