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
            for (var i = 0; i < Items.Count; i++)
            {
                CustomItem customItem = ItemFactory.CreateCustomItem(Items[i]);
                customItem.UpdateSellIn(Items[i]); 
                customItem.UpdateQuality(Items[i]);
            }
        }
    }
}
