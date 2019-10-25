using System.Collections.Generic;

namespace GildedRose
{
    public class GildedRose
    {
        private const string AgedBrieItem = "Aged Brie";
        private const string BackstagePassesItem = "Backstage passes to a TAFKAL80ETC concert";
        private const string SulfurasItem = "Sulfuras, Hand of Ragnaros";
        private const int QualityLowLimit = 0;
        private const int QualityHighLimit = 50;
        private const int SellInLimitDropQualityByTwo = 11;
        private const int SellInLimitDropQualityByThree = 6;
        private const int SellInLowLimit = 0;

        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (!IsSulfurasItem(Items[i].Name))
                {
                    UpdateSellIn(Items[i], -1);
                }

                if (!IsAgedBrie(Items[i].Name) && !IsBackstagePasses(Items[i].Name) && !IsSulfurasItem(Items[i].Name))
                {
                    if (IsAboveQualityLowLimit(Items[i].Quality))
                    {
                        UpdateQuality(Items[i], -1);
                    }

                    if (IsAboveQualityLowLimit(Items[i].Quality) && IsBellowSellInLowLimit(Items[i].SellIn))
                    {
                        UpdateQuality(Items[i], -1);
                    }
                }
                else if (IsBackstagePasses(Items[i].Name))
                {
                    UpdateQuality(Items[i], 1);

                    if (IsBellowSellInLimitDropByTwo(Items[i].SellIn) && IsBellowQualityHighLimit(Items[i].Quality))
                    {
                        UpdateQuality(Items[i], 1);
                    }

                    if (IsBellowSellInLimitDropByThree(Items[i].SellIn) && IsBellowQualityHighLimit(Items[i].Quality))
                    {
                        UpdateQuality(Items[i], 1);
                    }

                    if (IsBellowSellInLowLimit(Items[i].SellIn))
                    {
                        UpdateQuality(Items[i], -Items[i].Quality);
                    }
                }
                else if (IsAgedBrie(Items[i].Name) && IsBellowQualityHighLimit(Items[i].Quality))
                {
                    UpdateQuality(Items[i], 1);
                }

            }
        }

        private void UpdateSellIn(Item item, int update)
        {
            item.SellIn += update;
        }

        private void UpdateQuality(Item item, int update)
        {
            item.Quality += update;
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

        private bool IsBellowQualityHighLimit(int quality)
        {
            return quality < QualityHighLimit;
        }

        private bool IsAboveQualityLowLimit(int quality)
        {
            return quality > QualityLowLimit;
        }

        private bool IsSulfurasItem(string itemName)
        {
            return itemName == SulfurasItem;
        }

        private bool IsBackstagePasses(string itemName)
        {
            return itemName == BackstagePassesItem;
        }

        private bool IsAgedBrie(string itemName)
        {
            return itemName == AgedBrieItem;
        }
    }
}
