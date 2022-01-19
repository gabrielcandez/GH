namespace GH.Application.Entities;

public sealed class Post
{
    private Post()
    {
        Title = null!;
    }

    public Post(Guid id, string title)
    {
        Id = id;
        Title = title;
        PostedOn = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Title { get; set; }
    public DateTime PostedOn { get; set; }
}