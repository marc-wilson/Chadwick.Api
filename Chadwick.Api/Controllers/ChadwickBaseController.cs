using Chadwick.Database;
using Microsoft.AspNetCore.Mvc;

namespace Chadwick.Api.Controllers
{
    /// <summary>
    /// Base Controller for API
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class ChadwickBaseController : ControllerBase
    {
        /// <summary>
        /// Default Page for paged responses
        /// </summary>
        public const int DefaultPage = 0;
        
        /// <summary>
        /// Default Item Count for paged responses
        /// </summary>
        public const int DefaultItemCount = 25;
        
        public ChadwickDbContext Db { get; }

        public ChadwickBaseController(ChadwickDbContext db)
        {
            Db = db;
        }
    }
}