using Recipe_App.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe_App.UI
{
    internal class Dialog
    {
        private RecipeManager recipeManager;

        public Dialog(RecipeManager manager)
        {
            recipeManager = manager;
        }

        public void StartDialog()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. List Recipes");
                Console.WriteLine("3. Get Random Recipe");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRecipe();
                        break;

                    case "2":
                        ListRecipes();
                        break;

                    case "3":
                        GetRandomRecipe();
                        break;

                    case "4":
                        /*recipeManager.SaveRecipesToFile();*/
                        Console.WriteLine("Thank you for using Recipe App. Goodbye!");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AddRecipe()
        {
            try
            {
                Console.Write("Enter Recipe Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Recipe Description: ");
                string description = Console.ReadLine();

                Console.Write("Enter Recipe Stars (1-5): ");
                int stars = Convert.ToInt32(Console.ReadLine());

                if (stars < 1 || stars > 5)
                {
                    throw new ArgumentException("Stars must be between 1 and 5.");
                }

                List<Ingredient> ingredients = new List<Ingredient>();
                Console.WriteLine("Enter Ingredients (press Enter after each ingredient, type 'done' to finish):");
                while (true)
                {
                    Console.Write("Ingredient Name: ");
                    string ingredientName = Console.ReadLine();
                    if (ingredientName.ToLower() == "done")
                        break;

                    Console.Write("Ingredient Quantity: ");
                    string ingredientQuantity = Console.ReadLine();

                    ingredients.Add(new Ingredient { Name = ingredientName, Quantity = ingredientQuantity });
                }

                Recipe newRecipe = new Recipe(name, description, ingredients, stars);
                recipeManager.AddRecipe(newRecipe);
/*
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Recipe added successfully!");
                Console.ResetColor();*/
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}. Please enter a valid number.");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void ListRecipes()
        {
            List<Recipe> recipes = recipeManager.GetRecipes();
            StringBuilder recipeDetails = new StringBuilder();

            foreach (var recipe in recipes)
            {
                recipeDetails.AppendLine($"Name: {recipe.Name}");
                recipeDetails.AppendLine($"Description: {recipe.Description}");
                recipeDetails.AppendLine("Ingredients:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    recipeDetails.AppendLine($"- {ingredient.Name}: {ingredient.Quantity}");
                }
                recipeDetails.AppendLine($"Stars: {recipe.Stars}");
                recipeDetails.AppendLine();
            }

            Console.WriteLine(recipeDetails.ToString());
        }
          
        private void GetRandomRecipe()
        {
            Recipe randomRecipe = recipeManager.GetRandomRecipe();
            StringBuilder recipeDetails = new StringBuilder();

            if (randomRecipe != null)
            {
                recipeDetails.AppendLine("Random Recipe:");
                recipeDetails.AppendLine($"Name: {randomRecipe.Name}");
                recipeDetails.AppendLine($"Description: {randomRecipe.Description}");
                recipeDetails.AppendLine("Ingredients:");
                foreach (var ingredient in randomRecipe.Ingredients)
                {
                    recipeDetails.AppendLine($"- {ingredient.Name}: {ingredient.Quantity}");
                }
                recipeDetails.AppendLine($"Stars: {randomRecipe.Stars}");
            }

            Console.WriteLine(recipeDetails.ToString());
        }
    }
}



