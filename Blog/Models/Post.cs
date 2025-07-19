using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Content { get; set; } = default!;   // HTML from CKEditor
        public string? CoverImagePath { get; set; }
        public DateTime PublishedUtc { get; set; }
        public bool IsDraft { get; set; }
        public ICollection<PostTag> PostTags { get; set; } = [];
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }

    public class PostTag { [ForeignKey("Post")] public int PostId; public Post Post = default!; [ForeignKey("Tag")] public int TagId; public Tag Tag = default!; }

}
