namespace buoi3_PKFk.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime OrderTime { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual User? User { get; set; }
    }
}
