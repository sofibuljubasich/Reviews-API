namespace ReviewAPP.Models
{
    public class Reviewer
    {
        public int Id { get; set; }     
        public string Username { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
