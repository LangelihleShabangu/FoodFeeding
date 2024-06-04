﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFeeding.Models
{
    public class Ingredients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientsId { get; set; }
        public string Name { get; set; }       
        public DateTime Createddate { get; set; }
        public DateTime Modifieddate { get; set; }
        public bool Isdeleted { get; set; }
    }
}
