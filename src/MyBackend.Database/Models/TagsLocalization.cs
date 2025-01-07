using LinqToDB;
using LinqToDB.Mapping;

namespace MyBackend.Database.Models;

/// <summary>
/// Provides a tags localization.
/// </summary>
[Table(Schema = DbConstants.Schema, Name = DbConstants.TagsLocalization)]
public sealed class TagsLocalization
{
    /// <summary>
    /// Tag identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull]
    public int TagId { get; set; }

    /// <summary>
    /// Language identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull]
    public int LanguageId { get; set; }

    /// <summary>
    /// Tag localized value.
    /// </summary>
    [Column(DataType = DataType.NVarChar), NotNull]
    public string? Value { get; set; }
}
