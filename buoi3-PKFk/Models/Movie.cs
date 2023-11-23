namespace buoi3_PKFk.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public DateTime PlayingDate { get; set;}
        public double TicketPrice { get; set;}
        public double Rating { get; set;}
        public virtual ICollection<Order>? Order { get; set; }
    }
}
