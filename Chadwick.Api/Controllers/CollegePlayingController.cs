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
    /// Main API for College Playing stats
    /// </summary>
    [Route("college-playing")]
    public class CollegePlayingController : ChadwickBaseController
    {
        /// <summary>
        /// CollegePlayingController
        /// </summary>
        /// <param name="dbContext"></param>
        public CollegePlayingController(ChadwickDbContext dbContext) : base(dbContext) {}
        
        /// <summary>
        /// Gets a paged list of College Playing stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetCollegePlayingAsync))]
        [ProducesResponseType(typeof(Paged<CollegePlaying>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCollegePlayingAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var collegePlaying = Db.CollegePlaying.AsQueryable();
            var totalItems = await collegePlaying.CountAsync();
            var results = await collegePlaying.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<CollegePlaying>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of College Playing stats by yearId
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetCollegePlayingByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<CollegePlaying>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCollegePlayingByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var collegePlaying = Db.CollegePlaying.Where(c => c.YearId == yearId);
            var totalItems = await collegePlaying.CountAsync();
            var results = await collegePlaying.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<CollegePlaying>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a list of College Playing stats by playerId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetCollegePlayingByPlayerIdAsync))]
        [ProducesResponseType(typeof(List<CollegePlaying>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCollegePlayingByPlayerIdAsync(string playerId)
        {
            var collegePlaying = Db.CollegePlaying.Where(c => c.SchoolId == playerId);
            var results = await collegePlaying.ToListAsync();
            return Ok(results);
        }
        
    }
}