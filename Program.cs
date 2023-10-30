using Recipe_App.Data;
using Recipe_App.UI;

Console.WriteLine("Please enter your name: ");
string userName = Console.ReadLine();

Console.WriteLine("Welcome " + userName + "! ");

string filePath = @"C:\Data\recipe_data.txt";

RecipeManager recipeManager = new RecipeManager(filePath);
Dialog dialog = new Dialog(recipeManager);
dialog.StartDialog();



  