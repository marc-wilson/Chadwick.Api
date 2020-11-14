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
    /// Main API for Parks
    /// </summary>
    [Route("api/parks")]
    public class ParksController : ChadwickBaseController
    {
        /// <summary>
        /// ParksController
        /// </summary>
        /// <param name="dbContext"></param>
        public ParksController(ChadwickDbContext dbContext) : base (dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Parks
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetParksAsync))]
        [ProducesResponseType(typeof(Paged<Parks>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetParksAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var parks = Db.Parks.AsQueryable();
            var totalItems = await parks.CountAsync();
            var results = await parks.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Parks>(results, page, limit, totalItems, Request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a Park by parkId
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        [HttpGet("{parkId}", Name = nameof(GetParkByIdAsync))]
        [ProducesResponseType(typeof(Parks), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetParkByIdAsync(string parkId)
        {
            var park = await Db.Parks.FirstOrDefaultAsync(p => p.ParkKey == parkId);
            return Ok(park);
        }
    }
}