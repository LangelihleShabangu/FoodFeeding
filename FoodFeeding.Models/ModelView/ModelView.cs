using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFeeding.Models.ModelView
{
    public class ModelView
    {
        public ModelView()
        {
            ModelViewList = new List<ModelView>();
        }
        public string Food_Name { get; set; }
        public string Reciept_Name { get; set; }
        public int Feeds { get; set; }
        public int Quantity { get; set; }
        public List<ModelView> ModelViewList { get; set; }
    }
}
