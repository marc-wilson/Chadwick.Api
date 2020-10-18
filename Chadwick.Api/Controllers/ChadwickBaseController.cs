using Chadwick.Database;
using Microsoft.AspNetCore.Mvc;

namespace Chadwick.Api.Controllers
{
    /// <summary>
    /// Base Controller for API
    /// </summary>
    [Produces("application/json")]
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

        /// <summary>
        /// Validates page is greater than -1 and limit is less than 101
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool ValidatePage(int page, int limit)
        {
            return page >= 0 && limit >= 0 && limit <= 100;
        }
    }
}