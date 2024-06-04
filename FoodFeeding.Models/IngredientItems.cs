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
    public class IngredientItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientItemsId { get; set; }

        [ForeignKey("Ingredients")]
        public int IngredientsId { get; set; }
        public Ingredients Ingredients { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int Quantity { get; set; }        
        public DateTime Createddate { get; set; }
        public DateTime Modifieddate { get; set; }
        public bool Isdeleted { get; set; }
    }
}
