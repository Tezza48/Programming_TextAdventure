using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Recipe
    {
        private List<Item> ingredients;
        private Item product;

        public Recipe(List<Item> _ingredients, Item _product)
        {
            ingredients = _ingredients;
            product = _product;
        }

        public Tuple<List<Item>, Item> getRecipie()
        {
            return Tuple.Create(ingredients, product);
        }
    }
}
