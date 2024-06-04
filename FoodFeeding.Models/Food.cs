using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFeeding.Models
{
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodId { get; set; }
        public string Name { get; set; }       
        public DateTime Createddate { get; set; }
        public DateTime Modifieddate { get; set; }
        public bool Isdeleted { get; set; }
    }
}
