namespace GildedRose
{
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
}