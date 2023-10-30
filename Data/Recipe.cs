using System;
using System.Collections.Generic;

namespace Recipe_App.Data 
{
    internal class Recipe : IRecipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int Stars { get; set; }

        public Recipe(string name, string description, List<Ingredient> ingredients, int stars)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Stars = stars;
        }
    }
}

 


