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
    /// Main API for Batting Stats
    /// </summary>
    [Route("api/batting")]
    public class BattingController : ChadwickBaseController
    {
        /// <summary>
        /// BattingController
        /// </summary>
        /// <param name="dbContext"></param>
        public BattingController(ChadwickDbContext db) : base(db) {}

        /// <summary>
        /// Gets the given players batting stats
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{playerId}")]
        [ProducesResponseType(typeof(List<Batting>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBattingStatsByPlayerAsync(string playerId)
        {
            var battingStats = await Db.Batting.Where(b => b.PlayerId == playerId).ToListAsync();
            return Ok(battingStats);
        }
    }
}