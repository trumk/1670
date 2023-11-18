using System.ComponentModel.DataAnnotations;

namespace buoi2.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string? Title { get; set; }
        [RegularExpression(@"[A-Z]+[a-zA-Z0-9""'\s]*$")]
        public string Description { get; set; }
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [Range(1, 1200)]
        public decimal Price { get; set; }
        public string? Genre { get; set; }
        [RegularExpression(@"[A-Z]+[a-zA-Z\s]*$")]
        public string? Rating { get; set; }
    }
}
