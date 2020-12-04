
namespace CleanArchitecture.Application.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }
        public string DatePosted { get; set; }

        public int ForumId { get; set; }
        public string ForumName { get; set; }

        public int RepliesCount { get; set; }

        public ForumViewModel Forum { get; set; }
    }
}
