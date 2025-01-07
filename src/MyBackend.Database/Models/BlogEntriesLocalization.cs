using LinqToDB;
using LinqToDB.Mapping;

namespace MyBackend.Database.Models;

[Table(Schema = DbConstants.Schema, Name = DbConstants.BlogEntriesLocalization)]
public sealed class BlogEntriesLocalization
{
    /// <summary>
    /// Blog entry identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull]
    public int BlogEntryId { get; set; }

    /// <summary>
    /// Language identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull]
    public int LanguageId { get; set; }

    /// <summary>
    /// Blog entry localized title.
    /// </summary>
    [Column(DataType = DataType.NVarChar), NotNull]
    public string? Title { get; set; }

    /// <summary>
    /// Blog entry localized text.
    /// </summary>
    [Column(DataType = DataType.NVarChar), NotNull]
    public string? Text { get; set; }
}
