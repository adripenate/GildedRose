using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class GildedRose
    {
        private const string AgedBrieItem = "Aged Brie";
        private const int QualityHighLimit = 50;

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

                if (!IsAgedBrie(Items[i].Name))
                {
                    customItem.UpdateQuality(Items[i]);
                }
                else if (IsAgedBrie(Items[i].Name) && IsBellowQualityHighLimit(Items[i].Quality))
                {
                    UpdateQuality(Items[i], 1);
                }

            }
        }

        private void UpdateQuality(Item item, int update)
        {
            item.Quality += update;
        }

        private bool IsBellowQualityHighLimit(int quality)
        {
            return quality < QualityHighLimit;
        }

        private bool IsAgedBrie(string itemName)
        {
            return itemName == AgedBrieItem;
        }
    }

    public class ItemFactory
    {
        private const string AgedBrieItem = "Aged Brie";
        private const string BackstagePassesItem = "Backstage passes to a TAFKAL80ETC concert";
        private const string SulfurasItem = "Sulfuras, Hand of Ragnaros";
        private static Dictionary<string, Func<CustomItem>> factories = new Dictionary<string, Func<CustomItem>>
        {
            {AgedBrieItem, () => new AgedBrieItem() },
            {BackstagePassesItem, () => new BackstagePassesItem() },
            {SulfurasItem, () => new SulfurasItem() }
        };

        public static CustomItem CreateCustomItem(Item item)
        {
            return factories.ContainsKey(item.Name) ? factories[item.Name].Invoke() : new RegularItem();
        }
    }

    public class RegularItem : CustomItem
    {
        private const int QualityLowLimit = 0;
        private const int SellInLowLimit = 0;

        public void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        public void UpdateQuality(Item item)
        {
            if (IsAboveQualityLowLimit(item.Quality)) DecreaseQuality(item);
            if (IsAboveQualityLowLimit(item.Quality) && IsBellowSellInLowLimit(item.SellIn)) DecreaseQuality(item);
        }

        private static void DecreaseQuality(Item item)
        {
            item.Quality -= 1;
        }

        private bool IsAboveQualityLowLimit(int quality)
        {
            return quality > QualityLowLimit;
        }

        private bool IsBellowSellInLowLimit(int sellIn)
        {
            return sellIn < SellInLowLimit;
        }
    }

    public class SulfurasItem : CustomItem
    {
        public void UpdateSellIn(Item item)
        {
            item.SellIn += 0;
        }

        public void UpdateQuality(Item item)
        {
            item.SellIn += 0;
        }
    }

    public class BackstagePassesItem : CustomItem
    {
        private const int QualityHighLimit = 50;
        private const int SellInLimitDropQualityByTwo = 11;
        private const int SellInLimitDropQualityByThree = 6;
        private const int SellInLowLimit = 0;

        public void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        public void UpdateQuality(Item item)
        {
            IncreaseQuality(item);

            if (IsBellowSellInLimitDropByTwo(item.SellIn) && IsBellowQualityHighLimit(item.Quality)) IncreaseQuality(item);
            
            if (IsBellowSellInLimitDropByThree(item.SellIn) && IsBellowQualityHighLimit(item.Quality)) IncreaseQuality(item);
            
            if (IsBellowSellInLowLimit(item.SellIn)) item.Quality = 0;
        }

        private void IncreaseQuality(Item item)
        {
            item.Quality += 1;
        }

        private bool IsBellowQualityHighLimit(int quality)
        {
            return quality < QualityHighLimit;
        }
        private bool IsBellowSellInLowLimit(int sellIn)
        {
            return sellIn < SellInLowLimit;
        }

        private bool IsBellowSellInLimitDropByThree(int sellIn)
        {
            return sellIn < SellInLimitDropQualityByThree;
        }

        private bool IsBellowSellInLimitDropByTwo(int sellIn)
        {
            return sellIn < SellInLimitDropQualityByTwo;
        }
    }

    public class AgedBrieItem : CustomItem
    {
        public void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        public void UpdateQuality(Item item)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface CustomItem
    {
        void UpdateSellIn(Item item);
        void UpdateQuality(Item item);
    }
}
