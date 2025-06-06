using System;
using RecipeOptimizer.Models;

namespace RecipeOptimizer.Services
{
    public class RecipeOptimizerService
    {
        private readonly List<Recipe> _recipes;
        private readonly Dictionary<string, Ingredient> _availableIngredients;

        public RecipeOptimizerService(List<Recipe> recipes, List<Ingredient> availableIngredients)
        {
            _recipes = recipes;
            _availableIngredients = availableIngredients.ToDictionary(i => i.Name, i => i.Clone());
        }

        public (Dictionary<string, int> bestCombination, int maxServings) GetOptimalCombination()
        {
            return GetBestCombination(0, new Dictionary<string, int>(), _availableIngredients, 0);
        }

        private (Dictionary<string, int>, int) GetBestCombination(int index, Dictionary<string, int> currentCombo,
            Dictionary<string, Ingredient> currentIngredients, int currentServings)
        {
            if (index >= _recipes.Count)
                return (new Dictionary<string, int>(currentCombo), currentServings);

            Recipe recipe = _recipes[index];
            int maxPossible = int.MaxValue;

            foreach (var req in recipe.RequiredIngredients)
            {
                if (!currentIngredients.ContainsKey(req.Key))
                {
                    maxPossible = 0;
                    break;
                }

                int possible = currentIngredients[req.Key].Quantity / req.Value;
                maxPossible = Math.Min(maxPossible, possible);
            }

            Dictionary<string, int> bestCombo = null;
            int maxServings = currentServings;

            for (int i = 0; i <= maxPossible; i++)
            {
                var tempIngredients = currentIngredients.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new Ingredient(kvp.Value.Name, kvp.Value.Quantity)
                );

                foreach (var req in recipe.RequiredIngredients)
                {
                    tempIngredients[req.Key].Quantity -= req.Value * i;
                }

                var newCombo = new Dictionary<string, int>(currentCombo);
                if (i > 0) newCombo[recipe.Name] = i;

                var (combo, servings) = GetBestCombination(index + 1, newCombo, tempIngredients, currentServings + i * recipe.Servings);

                if (servings > maxServings)
                {
                    bestCombo = combo;
                    maxServings = servings;
                }
            }

            return (bestCombo ?? new Dictionary<string, int>(currentCombo), maxServings);
        }
    }
}

