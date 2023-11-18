using Microsoft.AspNetCore.Mvc.Rendering;

namespace buoi1.Models
{
    public class TicketGenreViewModel
    {
        public List<Ticket>? Tickets { get; set; }
        public SelectList? Genres { get; set; }
        public string? TicketGenre {  get; set; }
        public string? SearchString { get; set; }
    }
}
