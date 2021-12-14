using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Domain.Entity
{
    public class GroceriesTasks : Tasks
    {
        public IngredientsEnum IngredientName { get; set; }
        public double Price { get; set; }

        public int Amount { get; set; }

        //private double TotalPay = Price * Convert.ToDouble(Amount);
        //Do sprawdzenia
        //why cannot we reference properties in private variable definition?
    }
}
