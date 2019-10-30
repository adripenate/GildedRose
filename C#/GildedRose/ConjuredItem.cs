namespace GildedRose
{
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
            if(item.SellIn < 0) DecreaseQuality(item);
            if (item.SellIn < 0) DecreaseQuality(item);
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