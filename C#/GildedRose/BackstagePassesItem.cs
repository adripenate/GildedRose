namespace GildedRose
{
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

            if (CanIncreaseByTwo(item)) IncreaseQuality(item);
            
            if (CanIncreaseByThree(item)) IncreaseQuality(item);
            
            if (IsBellowSellInLowLimit(item.SellIn)) item.Quality = 0;
        }

        private bool CanIncreaseByThree(Item item)
        {
            return IsBellowSellInLimitDropByThree(item.SellIn) && IsBellowQualityHighLimit(item.Quality);
        }

        private bool CanIncreaseByTwo(Item item)
        {
            return IsBellowSellInLimitDropByTwo(item.SellIn) && IsBellowQualityHighLimit(item.Quality);
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
}