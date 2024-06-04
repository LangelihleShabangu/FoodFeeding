using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodFeeding.Models;

namespace FoodFeeding.Models
{
    public class RecieptItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecieptItemsId { get; set; }

        [ForeignKey("Reciept")]
        public int RecieptId { get; set; }
        public Reciept Reciept { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int Quantity { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Modifieddate { get; set; }
        public bool Isdeleted { get; set; }
    }
}
