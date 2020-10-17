using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chadwick.Database;
using Chadwick.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chadwick.Api.Controllers
{
    /// <summary>
    /// Main API for Fielding Stats
    /// </summary>
    [Route("api/fielding")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FieldingController : ChadwickBaseController
    {
        /// <summary>
        /// BattingController
        /// </summary>
        /// <param name="dbContext"></param>
        public FieldingController(ChadwickDbContext db) : base(db) {}

        /// <summary>
        /// Gets the given players fielding stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}")]
        [ProducesResponseType(typeof(List<Fielding>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFieldingStatsByPlayerAsync(string playerId)
        {
            var fieldingStats = await Db.Fielding.Where(f => f.PlayerId == playerId).ToListAsync();
            return Ok(fieldingStats);
        }
    }
}