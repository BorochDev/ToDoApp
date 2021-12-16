using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Domain.Entity
{
    public class CookingTasks : Tasks
    {
        //public string DishName { get; set; }
        //zawiera się w title
        private const double cookingTime = 20;

        private List<IngredientsEnum> ingredients;
        public List<IngredientsEnum> Ingredients
        {
            get
            {
                ingredients = GetIngredients(Recipe);
                return ingredients;
            }
            //set { Ingredients = GetIngredients(Recipe); }
            //czy tak mozna ustawic Ingredients odwolujac sie do Inputu Recipe, czy trzeba jakies recipe=Recipe najpierw?

        }
        //jeśli Ingredients mają się generować na podstawie Recipe, to chyba powinniśmy dać tylko gettera (bez settera)
        public string? Recipe { get; set; }
        //można zrobić free text, który szuka po słowach kluczowych z enuma IngredientsEnum i jeśli występują to automatycznie dodaje je do property Ingredients
        //public int CookingTime { get; set; }

        //public CookingTasks(string recipe)
        //{
        //    Recipe = recipe;
        //    //jak w konstruktorze odebrać wartość inputu Recipe zeby ustawić ją dla prywatnego pola używanego w metodzie GetIngredients?
        //    Ingredients = GetIngredients(recipe);
        //}

        public List<IngredientsEnum> GetIngredients(string recipe)
        {
            List<IngredientsEnum> returnedIngredients = new();

            foreach (var ingredient in Enum.GetNames<IngredientsEnum>())
            {
                if (recipe.Contains(ingredient))
                {
                    returnedIngredients.Add((IngredientsEnum)Enum.Parse(typeof(IngredientsEnum), ingredient));
                }
            }
            return returnedIngredients;
        }
        public override double TaskPerformanceTime
        {
            get { return TaskPerformanceTime; }
            set { TaskPerformanceTime = cookingTime + double.Parse((Ingredients.Count*8).ToString()); }
        }
    }
}
