using System;
using System.Collections.Generic;
using System.IO;

namespace Recipe_App.Data
{ 
    internal class RecipeManager
    {
        private readonly string filePath;
        private List<Recipe> recipes;

        public RecipeManager(string filePath)
        {
            this.filePath = filePath;
            recipes = LoadRecipesFromFile();
        }

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            SaveRecipesToFile();
        }

        public List<Recipe> GetRecipes()
        {
            return recipes; 
        }

        public Recipe GetRandomRecipe()
        {
            if (recipes.Count == 0)
            {
                throw new InvalidOperationException("There are no recipes available.");
            }

            Random random = new Random();
            return recipes[random.Next(recipes.Count)];
        }

        public void SaveRecipesToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var recipe in recipes)
                    {
                        writer.WriteLine($"Name:{recipe.Name}");
                        writer.WriteLine($"Description:{recipe.Description}");
                        writer.WriteLine($"Stars:{recipe.Stars}");
                        writer.WriteLine("Ingredients:");
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            writer.WriteLine($"{ingredient.Name}:{ingredient.Quantity}");
                        }
                        writer.WriteLine();
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Saved recipes successfully.");
                    Console.ResetColor();
                }
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
                Console.ResetColor();
            }
        }

        public List<Recipe> LoadRecipesFromFile()
        {
            List<Recipe> loadedRecipes = new List<Recipe>();

            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        Recipe currentRecipe = null;
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("Name:"))
                            {
                                currentRecipe = new Recipe(line.Substring(5), "", new List<Ingredient>(), 0);
                            }
                            else if (line.StartsWith("Description:"))
                            {
                                currentRecipe.Description = line.Substring(12);
                            }
                            else if (line.StartsWith("Stars:"))
                            {
                                currentRecipe.Stars = int.Parse(line.Substring(6));
                            }
                            else if (line.StartsWith("Ingredients:"))
                            {
                                // Skip this line, as we're going to read ingredients in the next iteration.
                            }
                            else if (line.StartsWith("-"))
                            {
                                string[] parts = line.Split(':');
                                currentRecipe.Ingredients.Add(new Ingredient { Name = parts[0].Substring(2), Quantity = parts[1].Trim() });
                            }
                            else if (line == "")
                            {
                                loadedRecipes.Add(currentRecipe);
                            }
                        }

                        if (currentRecipe != null)
                        {
                            loadedRecipes.Add(currentRecipe);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while loading the file: {ex.Message}");
                Console.ResetColor();
            }

            return loadedRecipes;
        }
    }
}



