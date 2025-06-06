using System;
namespace RecipeOptimizer.Models
{
    public class Ingredient
    {
        public string Name { get; }
        public int Quantity { get; set; }

        public Ingredient(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public Ingredient Clone() => new Ingredient(Name, Quantity);
    }
}

