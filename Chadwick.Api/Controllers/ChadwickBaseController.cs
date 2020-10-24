using Chadwick.Database;
using Microsoft.AspNetCore.Mvc;

namespace Chadwick.Api.Controllers
{
    /// <summary>
    /// Base Controller for API
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class ChadwickBaseController : Controller
    {
        /// <summary>
        /// Default Page for paged responses
        /// </summary>
        public const int DefaultPage = 0;
        
        /// <summary>
        /// Default Item Count for paged responses
        /// </summary>
        public const int DefaultItemCount = 25;
        
        /// <summary>
        /// Database Context
        /// </summary>
        public ChadwickDbContext Db { get; }

        /// <summary>
        /// Base Controller with DbContext
        /// </summary>
        /// <param name="db"></param>
        public ChadwickBaseController(ChadwickDbContext db)
        {
            Db = db;
        }
    }
}