namespace ReviewAPP.Models
{
    public class Place
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }

        public ICollection<PlaceCategory> PlaceCategories { get; set; }   
        public ICollection<Review> Reviews { get; set; }


    }
}
