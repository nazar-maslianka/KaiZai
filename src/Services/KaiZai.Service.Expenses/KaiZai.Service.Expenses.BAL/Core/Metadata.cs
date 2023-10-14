using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaiZai.Service.Expenses.BAL.Core;

/// <summary>
/// Represents metadata about a paginated list of items.
/// </summary>
public sealed record Metadata
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Metadata"/> record.
    /// </summary>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="totalPages">The total number of pages.</param>
    /// <param name="pageSize">The maximum number of items per page.</param>
    /// <param name="totalCount">The total number of items across all pages.</param>
    public Metadata(int currentPage, int totalPages, int pageSize, int totalCount)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int CurrentPage { get; init; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Gets the maximum number of items per page.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Gets the total number of items across all pages.
    /// </summary>
    public int TotalCount { get; init; }

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPrevious => CurrentPage > 1;

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNext => CurrentPage < TotalPages;
}