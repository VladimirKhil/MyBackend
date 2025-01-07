using FluentMigrator;
using MyBackend.Database.Models;

namespace MyBackend.Database.Migrations;

[Migration(202412070000, "Add blogs and localization")]
public sealed class AddBlogsAndLocalization : Migration
{
    public override void Up()
    {
        Create.Table(DbConstants.Languages)
            .WithColumn(nameof(LanguageModel.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(LanguageModel.Code)).AsString().NotNullable().Unique();

        Create.Table(DbConstants.NewsLocalization)
            .WithColumn(nameof(NewsLocalization.NewsId)).AsInt32().PrimaryKey().ForeignKey(nameof(DbConstants.News), nameof(NewsModel.Id))
            .WithColumn(nameof(NewsLocalization.LanguageId)).AsInt32().PrimaryKey().ForeignKey(nameof(DbConstants.Languages), nameof(LanguageModel.Id))
            .WithColumn(nameof(NewsLocalization.Text)).AsString().NotNullable();

        Create.Table(DbConstants.BlogEntries)
            .WithColumn(nameof(BlogEntriesModel.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(BlogEntriesModel.DateTime)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(BlogEntriesModel.Title)).AsString().NotNullable()
            .WithColumn(nameof(BlogEntriesModel.Text)).AsString().NotNullable();

        Create.Table(DbConstants.BlogEntriesLocalization)
            .WithColumn(nameof(BlogEntriesLocalization.BlogEntryId)).AsInt32().PrimaryKey().ForeignKey(nameof(DbConstants.BlogEntries), nameof(BlogEntriesModel.Id))
            .WithColumn(nameof(BlogEntriesLocalization.LanguageId)).AsInt32().PrimaryKey().ForeignKey(nameof(DbConstants.Languages), nameof(LanguageModel.Id))
            .WithColumn(nameof(BlogEntriesLocalization.Title)).AsString().NotNullable()
            .WithColumn(nameof(BlogEntriesLocalization.Text)).AsString().NotNullable();

        Create.Table(DbConstants.Tags)
            .WithColumn(nameof(TagsModel.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(TagsModel.Value)).AsString().NotNullable().Unique();

        Create.Table(DbConstants.TagsLocalization)
            .WithColumn(nameof(TagsLocalization.TagId)).AsInt32().PrimaryKey().ForeignKey(nameof(DbConstants.Tags), nameof(TagsModel.Id))
            .WithColumn(nameof(TagsLocalization.LanguageId)).AsInt32().PrimaryKey().ForeignKey(nameof(DbConstants.Languages), nameof(LanguageModel.Id))
            .WithColumn(nameof(TagsLocalization.Value)).AsString().NotNullable();

        Create.Table(DbConstants.BlogEntriesTags)
            .WithColumn(nameof(BlogEntryTag.BlogEntryId)).AsInt32().PrimaryKey().ForeignKey(nameof(DbConstants.BlogEntries), nameof(BlogEntriesModel.Id))
            .WithColumn(nameof(BlogEntryTag.TagId)).AsInt32().PrimaryKey().ForeignKey(nameof(DbConstants.Tags), nameof(TagsModel.Id));
    }

    public override void Down()
    {
        Delete.Table(DbConstants.BlogEntriesTags);
        Delete.Table(DbConstants.TagsLocalization);
        Delete.Table(DbConstants.Tags);
        Delete.Table(DbConstants.BlogEntriesLocalization);
        Delete.Table(DbConstants.BlogEntries);
        Delete.Table(DbConstants.NewsLocalization);
        Delete.Table(DbConstants.Languages);
    }
}
