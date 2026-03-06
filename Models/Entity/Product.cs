using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "Varchar")]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "Varchar")]
        public string Category { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "Varchar")]
        public string Description { get; set; }

        public bool Status { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
    }
}
