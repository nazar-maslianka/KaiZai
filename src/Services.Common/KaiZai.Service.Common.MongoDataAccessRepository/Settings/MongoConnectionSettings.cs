namespace KaiZai.Service.Common.MongoDataAccessRepository.Settings;

public sealed record MongoConnectionSettings
{
    public string Host { get; init; }

    public int Port { get; init; }

    public string ConnectionString => $"mongodb://{Host}:{Port}";
}