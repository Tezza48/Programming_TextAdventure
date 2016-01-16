using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Recipe
    {
        private List<Craftable> ingredients;
        private Item product;

        public Recipe(List<Craftable> _ingredients, Item _product)
        {
            ingredients = _ingredients;
            product = _product;
        }
    }
}
