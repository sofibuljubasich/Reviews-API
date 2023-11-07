namespace ReviewAPP.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlaceCategory> PlaceCategories { get; set; }

    }
}
