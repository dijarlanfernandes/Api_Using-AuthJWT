using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        public int categoryid { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }       
        public Collection<Product> products { get; set; } = new Collection<Product>();
    }
}
