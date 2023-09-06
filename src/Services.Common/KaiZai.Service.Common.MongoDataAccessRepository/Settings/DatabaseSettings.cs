namespace KaiZai.Service.Common.MongoDataAccessRepository.Settings;

public sealed record DatabaseSettings
{
    public string DatabaseName { get; init; }
}