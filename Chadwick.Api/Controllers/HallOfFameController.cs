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
    /// Main API for Hall of Fame stats
    /// </summary>
    [Route("api/hall-of-fame")]
    public class HallOfFameController : ChadwickBaseController
    {
        public HallOfFameController(ChadwickDbContext dbContext) : base(dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Hall of Fame stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetHallOfFameAsync))]
        [ProducesResponseType(typeof(Paged<HallOfFame>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHallOfFameAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var hallOfFame = Db.HallOfFame.AsQueryable();
            var totalItems = await hallOfFame.CountAsync();
            var results = await hallOfFame.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<HallOfFame>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of Hall of Fame stats by yearId
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{yearId:int}", Name = nameof(GetHallOfFameByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<HallOfFame>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHallOfFameByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var hallOfFame = Db.HallOfFame.Where(h => h.YearId == yearId);
            var totalItems = await hallOfFame.CountAsync();
            var results = await hallOfFame.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<HallOfFame>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a list of Hall of Fame stats by playerId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}", Name = nameof(GetHallOfFameByPlayerIdAsync))]
        [ProducesResponseType(typeof(Paged<HallOfFame>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHallOfFameByPlayerIdAsync(string playerId)
        {
            var hallOfFame = await Db.HallOfFame.Where(h => h.PlayerId == playerId).ToListAsync();
            return Ok(hallOfFame);
        }
    }
}