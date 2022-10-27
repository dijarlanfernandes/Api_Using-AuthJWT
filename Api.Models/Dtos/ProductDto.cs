using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }       
        public decimal price { get; set; }
        public int quantity { get; set; }
        public int categoryid { get; set; }
        public string categoryname{ get; set; }
    }
}