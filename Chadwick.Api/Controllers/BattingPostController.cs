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
    /// Main API for Batting Post Stats
    /// </summary>
    [Route("api/batting-post")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BattingPostController : ChadwickBaseController
    {
        /// <summary>
        /// BattingPostController
        /// </summary>
        /// <param name="db"></param>
        public BattingPostController(ChadwickDbContext db) : base(db) {}

        /// <summary>
        /// Gets the given players batting post stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}")]
        [ProducesResponseType(typeof(List<BattingPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingPostStatsByPlayerIdAsync(string playerId)
        {
            var battingPostStats = await Db.BattingPost.Where(b => b.PlayerId == playerId).ToListAsync();
            return Ok(battingPostStats);
        }
    }
}