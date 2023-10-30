using System.Collections.Generic;

namespace Recipe_App.Data
{
    internal interface IRecipe
    {
        string Name { get; set; }
        string Description { get; set; }
        List<Ingredient> Ingredients { get; set; }
        int Stars { get; set; }
    }
}


 