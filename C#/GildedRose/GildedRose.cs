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
                if (!IsAgedBrie(Items[i].Name) && !IsBackstagePasses(Items[i].Name))
                {
                    if (IsAboveQualityLowLimit(Items[i].Quality))
                    {
                        if (!IsSulfurasItem(Items[i].Name))
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (IsBellowQualityHighLimit(Items[i].Quality))
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (IsBackstagePasses(Items[i].Name))
                        {
                            if (Items[i].SellIn < SellInLimitDropQualityByTwo)
                            {
                                if (IsBellowQualityHighLimit(Items[i].Quality))
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < SellInLimitDropQualityByThree)
                            {
                                if (IsBellowQualityHighLimit(Items[i].Quality))
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (!IsSulfurasItem(Items[i].Name))
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < SellInLowLimit)
                {
                    if (!IsAgedBrie(Items[i].Name))
                    {
                        if (!IsBackstagePasses(Items[i].Name))
                        {
                            if (IsAboveQualityLowLimit(Items[i].Quality))
                            {
                                if (!IsSulfurasItem(Items[i].Name))
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
                        if (IsBellowQualityHighLimit(Items[i].Quality))
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
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
