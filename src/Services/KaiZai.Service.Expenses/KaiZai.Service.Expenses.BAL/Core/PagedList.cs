namespace KaiZai.Service.Expenses.BAL.Core;

/// <summary>
/// Represents a paged list of items.
/// </summary>
/// <typeparam name="T">The type of items in the paged list.</typeparam>
public sealed class PagedList<T> : List<T>
{
     /// <summary>
    /// Gets the metadata associated with the paged list.
    /// </summary>
    public Metadata Metadata { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="items">The collection of items in the current page.</param>
    /// <param name="totalCount">The total number of items across all pages.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="pageSize">The maximum number of items per page.</param>
    public PagedList(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
    {
        Metadata = new Metadata
        (
            currentPage,
            (int)Math.Ceiling(totalCount / (double)pageSize),
            pageSize,
            totalCount
        );
        AddRange(items);
    }
}