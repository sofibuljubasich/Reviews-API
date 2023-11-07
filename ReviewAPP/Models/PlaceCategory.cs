namespace ReviewAPP.Models
{
    public class PlaceCategory
    {
        public int PlaceID { get; set; }
        public int CategoryID { get; set; }
        public Place Place { get; set; }
        public Category Category { get; set; }
    }
}
