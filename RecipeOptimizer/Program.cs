using System;
using System.Collections.Generic;
using System.Linq;
using RecipeOptimizer.Models;
using RecipeOptimizer.Services;

namespace RecipeOptimizer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient("Cucumber", 2),
                new Ingredient("Olives", 2),
                new Ingredient("Lettuce", 3),
                new Ingredient("Meat", 6),
                new Ingredient("Tomato", 6),
                new Ingredient("Cheese", 8),
                new Ingredient("Dough", 10)
            };

            var recipes = new List<Recipe>
            {
                new Recipe("Burger", 1, new Dictionary<string, int>
                {
                    {"Meat", 1},
                    {"Lettuce", 1},
                    {"Tomato", 1},
                    {"Cheese", 1},
                    {"Dough", 1}
                }),

                new Recipe("Pie", 1, new Dictionary<string, int>
                {
                    {"Dough", 2},
                    {"Meat", 2}
                }),
                new Recipe("Salad", 3, new Dictionary<string, int>
                {
                    {"Lettuce", 2},
                    {"Tomato", 2},
                    {"Cucumber", 1},
                    {"Cheese", 2},
                    {"Olives", 1}
                }),
                new Recipe("Sandwich", 1, new Dictionary<string, int>
                {
                    {"Dough", 1},
                    {"Cucumber", 1}
                }),
                new Recipe("Pizza", 4, new Dictionary<string, int>
                {
                    {"Dough", 3},
                    {"Tomato", 2},
                    {"Cheese", 3},
                    {"Olives", 1}
                }),
                new Recipe("Pasta", 2, new Dictionary<string, int>
                {
                    {"Dough", 2},
                    {"Tomato", 1},
                    {"Cheese", 2},
                    {"Meat", 1}
                })
            };

            var optimizer = new RecipeOptimizerService(recipes, ingredients);
            var (bestCombo, totalServings) = optimizer.GetOptimalCombination();

            Console.WriteLine("Optimal Recipe Combination:");
            foreach (var kvp in bestCombo)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} time(s)");
            }
            Console.WriteLine($"Total People Fed: {totalServings}");
        }
    }
}

