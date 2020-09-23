using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy.WebApi.Models
{
    /// <summary>
    /// List for the paginated functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Get the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int PageIndex { get; }

        /// <summary>
        /// Get the total count of pages.
        /// </summary>
        /// <value>
        /// The total pages count.
        /// </value>
        public int TotalPages { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        public PaginatedList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="count">The count.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        public bool HasPreviousPage => (PageIndex > 1);

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        public bool HasNextPage => (PageIndex < TotalPages);

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public static PaginatedList<T> CreateInstance(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var enumerable = source as T[] ?? source.ToArray();

            var count = enumerable.Length;
            var items = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}