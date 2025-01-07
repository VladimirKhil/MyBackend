namespace MyBackend.Service.Contract.Models;

public sealed class BlogEntriesPage
{
    public BlogEntry[] Entries { get; set; } = Array.Empty<BlogEntry>();
    
    public int TotalCount { get; set; }
}
