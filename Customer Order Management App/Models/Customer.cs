using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_Order_Management_App.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustmerId { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(ErrorMessage = "CustomerName is must")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "CustomerAge is must")]
        [Column(TypeName = "varchar(20)")]
        public int CustomerAge { get; set; }

        [Required(ErrorMessage = "ProductName is must")]
        [Column(TypeName = "varchar(100)")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "OrderDate is must")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "OrderId is must")]
        public int OrderId { get; set; }
    }
}
