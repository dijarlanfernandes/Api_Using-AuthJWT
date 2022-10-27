using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        public string name { get; set; }
        public string description { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal price { get; set; }
        public int quantity { get; set; }
        public int categoryid { get; set; }
        public Category category { get; set; }          

    }
}
