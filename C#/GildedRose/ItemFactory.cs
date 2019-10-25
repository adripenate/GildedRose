using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class ItemFactory
    {
        private const string AgedBrieItem = "Aged Brie";
        private const string BackstagePassesItem = "Backstage passes to a TAFKAL80ETC concert";
        private const string SulfurasItem = "Sulfuras, Hand of Ragnaros";
        private const string Conjured = "Conjured";
        private static Dictionary<string, Func<CustomItem>> factories = new Dictionary<string, Func<CustomItem>>
        {
            {AgedBrieItem, () => new AgedBrieItem() },
            {BackstagePassesItem, () => new BackstagePassesItem() },
            {SulfurasItem, () => new SulfurasItem() },
            {Conjured, () => new ConjuredItem() }
        };

        public static CustomItem CreateCustomItem(Item item)
        {
            return factories.ContainsKey(item.Name) ? factories[item.Name].Invoke() : new RegularItem();
        }
    }

    public class ConjuredItem : CustomItem
    {
        private const int QualityLowLimit = 0;

        public void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        public void UpdateQuality(Item item)
        {
            DecreaseQualityTwiceAsFast(item);
        }

        private void DecreaseQualityTwiceAsFast(Item item)
        {
            DecreaseQuality(item);
            DecreaseQuality(item);
        }


        private void DecreaseQuality(Item item)
        {
            if (IsAboveQualityLowLimit(item.Quality)) item.Quality -= 1;
        }

        private bool IsAboveQualityLowLimit(int quality)
        {
            return quality > QualityLowLimit;
        }
    }
}