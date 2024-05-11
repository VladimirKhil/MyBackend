using FluentMigrator;
using MyBackend.Database.Models;

namespace MyBackend.Database.Migrations;

[Migration(202404130000, "Initial migration")]
public sealed class Initial : Migration
{
    public override void Up() =>
        Create.Table(DbConstants.News)
            .WithColumn(nameof(NewsModel.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(NewsModel.DateTime)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(NewsModel.Text)).AsString().NotNullable();

    public override void Down() => Delete.Table(DbConstants.News);
}
