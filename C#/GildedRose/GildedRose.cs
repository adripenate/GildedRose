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
        private const int SellinLimitDropQualityByTwo = 11;
        private const int SellinLimitDropQualityByThree = 6;
        private const int SellinLowLimit = 0;

        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (!isAgedBrie(Items[i].Name) && !isBackstagePasses(Items[i].Name))
                {
                    if (Items[i].Quality > QualityLowLimit)
                    {
                        if (Items[i].Name != SulfurasItem)
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < QualityHighLimit)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (isBackstagePasses(Items[i].Name))
                        {
                            if (Items[i].SellIn < SellinLimitDropQualityByTwo)
                            {
                                if (Items[i].Quality < QualityHighLimit)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < SellinLimitDropQualityByThree)
                            {
                                if (Items[i].Quality < QualityHighLimit)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != SulfurasItem)
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < SellinLowLimit)
                {
                    if (!isAgedBrie(Items[i].Name))
                    {
                        if (!isBackstagePasses(Items[i].Name))
                        {
                            if (Items[i].Quality > QualityLowLimit)
                            {
                                if (Items[i].Name != SulfurasItem)
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
                        if (Items[i].Quality < QualityHighLimit)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }

        private bool isBackstagePasses(string itemName)
        {
            return itemName == BackstagePassesItem;
        }

        private bool isAgedBrie(string itemName)
        {
            return itemName == AgedBrieItem;
        }
    }
}
