namespace Smash_Combos.Domain.Models
{
    public class CommentVote
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }
        public string upOrDown { get; set; }
    }
}