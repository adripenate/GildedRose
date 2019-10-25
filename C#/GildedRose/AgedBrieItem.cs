namespace GildedRose
{
    public class AgedBrieItem : CustomItem
    {
        private const int QualityHighLimit = 50;

        public void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        public void UpdateQuality(Item item)
        {
            if (IsBellowQualityHighLimit(item.Quality)) item.Quality += 1;
        }

        private bool IsBellowQualityHighLimit(int quality)
        {
            return quality < QualityHighLimit;
        }
    }
}