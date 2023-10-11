using KaiZai.Common.Types;

namespace KaiZai.Service.Categories.Contracts;

public interface IBaseCategoryContract
{
    Guid Id { get; init; }
    Guid ProfileId { get; init; }
    CategoryType CategoryType { get; init; }
}