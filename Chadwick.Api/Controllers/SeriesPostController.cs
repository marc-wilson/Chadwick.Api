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
    /// Main API for post season Series stats
    /// </summary>
    [Route("api/series-post")]
    public class SeriesPostController : ChadwickBaseController
    {
        /// <summary>
        /// SeriesPostController
        /// </summary>
        /// <param name="dbContext"></param>
        public SeriesPostController(ChadwickDbContext dbContext) : base(dbContext) {}
        
        /// <summary>
        /// Gets a paged list of post season Series stats
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetSeriesPostAsync))]
        [ProducesResponseType(typeof(Paged<SeriesPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSeriesPostAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var series = Db.SeriesPost.AsQueryable();
            var totalItems = await series.CountAsync();
            var results = await series.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<SeriesPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a paged list of post season Series stats by yearId
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("year/{yearId:int}", Name = nameof(GetSeriesPostByYearIdAsync))]
        [ProducesResponseType(typeof(Paged<SeriesPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSeriesPostByYearIdAsync(int yearId, int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var series = Db.SeriesPost.Where(s => s.YearId == yearId);
            var totalItems = await series.CountAsync();
            var results = await series.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<SeriesPost>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
    }
}