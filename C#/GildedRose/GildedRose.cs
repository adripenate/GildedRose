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
                }
                else
                {
                    if (IsBellowQualityHighLimit(Items[i].Quality))
                    {
                        UpdateQuality(Items[i], 1);

                        if (IsBackstagePasses(Items[i].Name))
                        {
                            if (IsBellowSellInLimitDropByTwo(Items[i].SellIn))
                            {
                                if (IsBellowQualityHighLimit(Items[i].Quality))
                                {
                                    UpdateQuality(Items[i], 1);
                                }
                            }

                            if (IsBellowSellInLimitDropByThree(Items[i].SellIn))
                            {
                                if (IsBellowQualityHighLimit(Items[i].Quality))
                                {
                                    UpdateQuality(Items[i], 1);
                                }
                            }

                            if (IsBellowSellInLimit(Items[i].SellIn))
                            {
                                UpdateQuality(Items[i], -Items[i].Quality);
                            }
                        }
                    }
                }

                if (IsBellowSellInLimit(Items[i].SellIn))
                {
                    if (!IsAgedBrie(Items[i].Name))
                    {
                        if (!IsBackstagePasses(Items[i].Name))
                        {
                            if (IsAboveQualityLowLimit(Items[i].Quality))
                            {
                                if (!IsSulfurasItem(Items[i].Name))
                                {
                                    UpdateQuality(Items[i], -1);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (IsBellowQualityHighLimit(Items[i].Quality))
                        {
                            UpdateQuality(Items[i], 1);
                        }
                    }
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

        private bool IsBellowSellInLimit(int sellIn)
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
