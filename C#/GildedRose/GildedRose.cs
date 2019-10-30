using System.Collections.Generic;

namespace GildedRose
{
    public class GildedRose
    {
        
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                CustomItem customItem = ItemFactory.CreateCustomItem(item);
                customItem.UpdateSellIn(item); 
                customItem.UpdateQuality(item);
            }
        }
    }
}
