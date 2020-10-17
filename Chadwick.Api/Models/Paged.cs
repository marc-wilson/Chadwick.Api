using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Chadwick.Api.Models
{
    /// <summary>
    /// Represents a page of items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Paged<T>
    {
        /// <summary>
        /// Results for the page
        /// </summary>
        public List<T> Results { get; }
        
        /// <summary>
        /// Page number
        /// </summary>
        public int PageNumber { get; }
        
        /// <summary>
        /// Result Count
        /// </summary>
        public int ResultCount { get; }
        
        /// <summary>
        /// Total items
        /// </summary>
        public int TotalItems { get; }
        
        /// <summary>
        /// Total pages available
        /// </summary>
        public long TotalPages { get; }
        
        /// <summary>
        /// Url to next page
        /// </summary>
        public string Next { get; }
        
        /// <summary>
        /// Url to current page
        /// </summary>
        public string Current { get; }
        
        /// <summary>
        /// Url to previous page
        /// </summary>
        public string Previous { get; }
        
        /// <summary>
        /// Builds a Paged Response of T
        /// </summary>
        /// <param name="results"></param>
        /// <param name="pageNumber"></param>
        /// <param name="totalItems"></param>
        public Paged(List<T> results, int pageNumber, int limit, int totalItems, HttpRequest request)
        {
            var resultCount = results.Count;
            var totalPages = Convert.ToInt64(Math.Ceiling(Convert.ToDouble(totalItems) / Convert.ToDouble(limit))) - 1;
            Results = results;
            PageNumber = pageNumber;
            ResultCount = resultCount;
            TotalItems = totalItems;
            TotalPages = totalPages > -1 ? totalPages : 0;
            Previous = GetUrl(pageNumber, limit, request);
            // Previous = pageNumber > 0 ? $"{url}?page={pageNumber - 1}&limit={limit}" : null;
            // Current = $"{url}?page={pageNumber}&limit={limit}";
            // Next = pageNumber < totalPages ? $"{url}?page={pageNumber + 1}&limit={limit}" : null;
        }

        private static string GetUrl(int pageNumber, int limit, HttpRequest request)
        {
            var queryString = HttpUtility.ParseQueryString(request.QueryString.Value);
            queryString.Set("page", pageNumber.ToString());
            return $"{request.Scheme}://{request.Host}{request.Path}?{queryString}";
        }
        
    }
}