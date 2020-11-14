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
    /// Main API for Schools
    /// </summary>
    [Route("api/schools")]
    public class SchoolsController : ChadwickBaseController
    {
        public SchoolsController(ChadwickDbContext dbContext) : base (dbContext) {}
        
        /// <summary>
        /// Gets a paged list of Schools
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetSchoolsAsync))]
        [ProducesResponseType(typeof(Paged<Schools>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSchoolsAsync(int page = DefaultPage, int limit = DefaultItemCount)
        {
            if (!Paged.ValidatePage(page, limit)) return new BadRequestObjectResult(new ErrorResponse("Index out of range"));
            var schools = Db.Schools.AsQueryable();
            var totalItems = await schools.CountAsync();
            var results = await schools.Skip(page * limit).Take(limit).ToListAsync();
            var response = new Paged<Schools>(results, page, limit, totalItems, Request);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a School by schoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        [HttpGet("school/{schoolId}", Name = nameof(GetSchoolByIdAsync))]
        [ProducesResponseType(typeof(Schools), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSchoolByIdAsync(string schoolId)
        {
            var school = await Db.Schools.FirstOrDefaultAsync(s => s.SchoolId == schoolId);
            return Ok(school);
        }
    }
}