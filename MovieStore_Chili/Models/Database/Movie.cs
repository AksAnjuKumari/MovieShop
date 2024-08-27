using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations;

namespace MovieStore_Chili.Models.Database
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Director { get; set; }
        [Required]

        public int ReleaseYear { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Image { get; set; }
       
        public virtual ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
       
    }
}
