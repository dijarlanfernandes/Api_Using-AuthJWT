using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Dtos
{
    public class CategoryDto
    {
        public int categoryid { get; set; }       
        public string name { get; set; }
    }
}
