using System;
namespace RecipeOptimizer.Models
{
    public class Recipe
    {
        public string Name { get; }
        public int Servings { get; }
        public Dictionary<string, int> RequiredIngredients { get; }

        public Recipe(string name, int servings, Dictionary<string, int> ingredients)
        {
            Name = name;
            Servings = servings;
            RequiredIngredients = ingredients;
        }

        public bool CanMake(Dictionary<string, Ingredient> availableIngredients, int count)
        {
            return RequiredIngredients.All(req =>
                availableIngredients.ContainsKey(req.Key) &&
                availableIngredients[req.Key].Quantity >= req.Value * count);
        }
    }
}

