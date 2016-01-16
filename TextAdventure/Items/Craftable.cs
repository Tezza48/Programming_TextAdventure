using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Craftable : Item
    {
        // a craftable is the main ingredient of a crafting recipie
        private Recipe recipie;

        public Craftable()
        {
            itemName = "";
            itemDescription = "";
            recipie = null;
        }

        public Craftable(string name, string description)
        {
            itemName = name;
            itemDescription = description;
            recipie = null;
        }

        public Craftable(string name, string description, Recipe _recipe)
        {
            itemName = name;
            itemDescription = description;
            recipie = _recipe;
        }

        public Item Craft(List<Item> givenIngredients)
        {
            Tuple<List<Item>, Item> recipeChecklist = recipie.getRecipie();
            // if the given list of items isn't the same length as the recipe, return null
            if (!(givenIngredients.Count() == recipeChecklist.Item1.Count()))
                return null;
            foreach (Item ingredient in recipeChecklist.Item1)
            {
                // if the current ingredient isn't in the recipe then return null
                if (!givenIngredients.Contains(ingredient))
                    return null;
            }
            // once we're here we are confident that the given ingredients are correct
            return recipeChecklist.Item2;
        }

    }
}
