using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore_Chili.Models.Database
{
    public class OrderRow
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int MovieId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [NotMapped]
        public virtual int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Movie  Movie { get; set; }
    }
}
