using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Post
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Body { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}
