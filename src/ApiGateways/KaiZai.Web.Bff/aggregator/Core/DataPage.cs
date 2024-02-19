using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace KaiZai.Web.HttpAggregator.Core;

/// <summary>
/// Represents a paged collection of items with associated metadata.
/// </summary>
/// <typeparam name="T">The type of items in the paged collection.</typeparam>
public sealed class DataPage<T>
{
     /// <summary>
    /// Gets the metadata associated with the paged collection.
    /// </summary>
    public Metadata Metadata { get; private set; }

    /// <summary>
    /// Gets the collection of items in the current page.
    /// </summary>
    public IEnumerable<T> Items { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataPage{T}"/> class.
    /// </summary>
    /// <param name="items">The collection of items in the current page.</param>
    /// <param name="totalCount">The total number of items across all pages.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="pageSize">The maximum number of items per page.</param>
    public DataPage(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
    {
        Metadata = new Metadata
        (
            currentPage,
            (int)Math.Ceiling(totalCount / (double)pageSize),
            pageSize,
            totalCount
        );
        Items = items ?? Enumerable.Empty<T>();
    }
}