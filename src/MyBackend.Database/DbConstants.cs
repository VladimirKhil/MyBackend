namespace MyBackend.Database;

/// <summary>
/// Provides well-known MyBackend database constants.
/// </summary>
public static class DbConstants
{
    public const string Schema = "mybackend";
    public const string DbName = "mybackend";

    public const string News = nameof(News);
    public const string Tags = nameof(Tags);
    public const string BlogEntries = nameof(BlogEntries);
    public const string BlogEntriesTags = nameof(BlogEntriesTags);
    public const string Languages = nameof(Languages);
    public const string NewsLocalization = nameof(NewsLocalization);
    public const string TagsLocalization = nameof(TagsLocalization);
    public const string BlogEntriesLocalization = nameof(BlogEntriesLocalization);
}
