namespace ReviewAPP.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }   

        public int Stars { get; set; }  
        public string Description { get; set; } 

        public Reviewer Reviewer { get; set; }  

        public Place Place { get; set; }    
    }
}
