using Grpc;
using BALayerCommonTypes = KaiZai.Common.Types;
using BALayerDTOs = KaiZai.Service.Incomes.BAL.DTOs;
using BALayerCore = KaiZai.Service.Incomes.BAL.Core;
using Google.Protobuf.WellKnownTypes;

namespace KaiZai.Service.Incomes.API.Grpc;

public static class Mappings
{
    public static BALayerDTOs.AddUpdateIncomeDTO ToBALayerAddUpdateIncomeDTO(this UpdateIncomeRequest updateIncomeRequestGrpc)
        => new BALayerDTOs.AddUpdateIncomeDTO (
            updateIncomeRequestGrpc.UpdateIncomeDTO.CategoryId,
            updateIncomeRequestGrpc.UpdateIncomeDTO.IncomeDate.ToDateTimeOffset(),
            updateIncomeRequestGrpc.UpdateIncomeDTO.Description,
            updateIncomeRequestGrpc.UpdateIncomeDTO.Amount);

    public static BALayerDTOs.AddUpdateIncomeDTO ToBALayerAddUpdateIncomeDTO(this AddIncomeRequest addIncomeRequestGrpc)
        => new BALayerDTOs.AddUpdateIncomeDTO (
            addIncomeRequestGrpc.AddIncomeDTO.CategoryId,
            addIncomeRequestGrpc.AddIncomeDTO.IncomeDate.ToDateTimeOffset(),
            addIncomeRequestGrpc.AddIncomeDTO.Description,
            addIncomeRequestGrpc.AddIncomeDTO.Amount);

    public static IncomeDTO ToGrpcIncomeDTO(this BALayerDTOs.IncomeDTO incomeDTOBALayer)
        => new IncomeDTO
        {
            Id = incomeDTOBALayer.Id,
            ProfileId = incomeDTOBALayer.ProfileId,
            Category = incomeDTOBALayer.Category.ToCategoryDTOGrpc(),
            IncomeDate = Timestamp.FromDateTimeOffset(incomeDTOBALayer.IncomeDate),
            Description = incomeDTOBALayer.Description,
            Amount = incomeDTOBALayer.Amount
        };

    public static CategoryDTO ToCategoryDTOGrpc(this BALayerDTOs.CategoryDTO categoryDTOBALayer)
        => new CategoryDTO
        {
            Id = categoryDTOBALayer.Id,
            Name = categoryDTOBALayer.Name,
            CategoryType = categoryDTOBALayer.CategoryType.ToCategoryTypeGrpc()
        };

    public static BALayerCore.FilteringParams ToBALayerFilteringParams(this FilteringParams filterParamsGrpc)
        => new BALayerCore.FilteringParams
        {
            StartDate = filterParamsGrpc.StartDate.ToDateTimeOffset(),
            EndDate = filterParamsGrpc.EndDate.ToDateTimeOffset()
        };
    
    public static BALayerCore.PagingParams ToBALayerPagingParams(this PagingParams pagingParamsGrpc)
        => new BALayerCore.PagingParams
        {
            PageNumber = pagingParamsGrpc.PageNumber,
            PageSize = pagingParamsGrpc.PageSize
        };
    
     public static Metadata ToMetadataGrpc(this BALayerCore.Metadata metadataBALayer)
    {
        return new Metadata
        {
            CurrentPage = metadataBALayer.CurrentPage,
            TotalPages = metadataBALayer.TotalPages,
            PageSize = metadataBALayer.PageSize,
            TotalCount = metadataBALayer.TotalCount
        };
    }

    public static CategoryType ToCategoryTypeGrpc(this BALayerCommonTypes.CategoryType categoryTypeBALayer)
    {
        var categoryTypeGrpc = System.Enum.Parse(typeof(CategoryType), categoryTypeBALayer.ToString());
        return (CategoryType) categoryTypeGrpc;
    }
}