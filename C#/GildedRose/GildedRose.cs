using System.Collections.Generic;

namespace GildedRose
{
    public class GildedRose
    {
        private const string AGEDBRIE_ITEM = "Aged Brie";
        private const string BACKSTAGE_PASSES_ITEM = "Backstage passes to a TAFKAL80ETC concert";
        private const string SULFURAS_ITEM = "Sulfuras, Hand of Ragnaros";

        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != AGEDBRIE_ITEM && Items[i].Name != BACKSTAGE_PASSES_ITEM)
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != SULFURAS_ITEM)
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == BACKSTAGE_PASSES_ITEM)
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != SULFURAS_ITEM)
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != AGEDBRIE_ITEM)
                    {
                        if (Items[i].Name != BACKSTAGE_PASSES_ITEM)
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != SULFURAS_ITEM)
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
