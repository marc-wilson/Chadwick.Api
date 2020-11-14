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
    /// Main API for All Star Stats
    /// </summary>
    [Route("api/allstarfull")]
    public class AllStarFullController : ChadwickBaseController
    {
        /// <summary>
        /// AllStarFullController
        /// </summary>
        /// <param name="db"></param>
        public AllStarFullController(ChadwickDbContext db) : base(db) {}

        /// <summary>
        /// Gets a paged list of All Star stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAllStarFullAsync))]
        [ProducesResponseType(typeof(Paged<AllStarFull>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStarFullAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var allStars = Db.AllStarFull.AsQueryable();
            var totalItems = await allStars.CountAsync();
            var results = await allStars.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AllStarFull>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets the given players all star stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetAllStarFullByPlayerAsync))]
        [ProducesResponseType(typeof(List<AllStarFull>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStarFullByPlayerAsync(string playerId)
        {
            var allStarStats = await Db.AllStarFull.Where(a => a.PlayerId == playerId).ToListAsync();
            return Ok(allStarStats);
        }
        
        /// <summary>
        /// Gets a paged list of all star stats by year
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{yearId:int}", Name = nameof(GetAllStarFullByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<AllStarFull>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStarFullByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var allStar = Db.AllStarFull.Where(f => f.YearId == yearId);
            var totalItems = await allStar.CountAsync();
            var results = await allStar.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AllStarFull>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a paged list of all star stats by teamId
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("team/{teamId}", Name = nameof(GetAllStarFullByTeamIdAsync))]
        [ProducesResponseType(typeof(Paged<AllStarFull>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStarFullByTeamIdAsync(string teamId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var allStar = Db.AllStarFull.Where(f => f.TeamId == teamId);
            var totalItems = await allStar.CountAsync();
            var results = await allStar.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<AllStarFull>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
    }
}