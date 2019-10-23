using System.Collections.Generic;
using FluentAssertions;
using GildedRose;
using NUnit.Framework;

namespace GildedRoseCSharp.Test {
    public class GlidedRoseTest {
        [Test]
        public void shouldCheckDefaultOutput()
        {
            List<Item> Items = new List<Item>()
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item {Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 15,Quality = 20},
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          };

            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>()
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 9, Quality = 19},
                                              new Item {Name = "Aged Brie", SellIn = 1, Quality = 1},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 4, Quality = 6},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 14, Quality = 21},
                                              new Item {Name = "Conjured Mana Cake", SellIn = 2, Quality = 5}
                                          };


            expectedOutput[0].Should().BeEquivalentTo(Items[0]);
            expectedOutput[1].Should().BeEquivalentTo(Items[1]);
            expectedOutput[2].Should().BeEquivalentTo(Items[2]);
            expectedOutput[3].Should().BeEquivalentTo(Items[3]);
            expectedOutput[4].Should().BeEquivalentTo(Items[4]);
            expectedOutput[5].Should().BeEquivalentTo(Items[5]);
        }



        //- Once the sell by date has passed, Quality degrades twice as fast
        [Test]
        public void shouldDecreasetheQualityTwiceIfSellByDateIsPassed()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "+5 Dexterity Vest", SellIn = -1, Quality = 18 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);
        }

        //- The Quality of an item is never negative
        [Test]
        public void qualityOfItemShouldNotBeNegative()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 0 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "+5 Dexterity Vest", SellIn = -1, Quality = 0 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);
        }

        //- "Aged Brie" actually increases in Quality the older it gets
        [Test]
        public void shouldincreasetheQualityof_AgedBrie()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "Aged Brie", SellIn = 1, Quality = 1 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);

        }

        //- The Quality of an item is never more than 50 except "Sulfuras", being a legendary item
        [Test]
        public void qualityOfanItemShouldNotExceed50_ExceptSulfras()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "Aged Brie", SellIn = 2, Quality = 50 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "Aged Brie", SellIn = 1, Quality = 50 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);

        }

        //- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        [Test]
        public void shouldNotIncreaseOrDecreaseQualityof_Sulfuras()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);

        }

        //- "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality increases by 2 when there are 10 days or less 
        //  and by 3 when there are 5 days or less but Quality drops to 0 after the concert
        [Test]
        public void shouldIncreaseInQualityBy1_IfMoreThan10Days_BackstagePasses()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 14, Quality = 21 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);

        }

        [Test]
        public void shouldIncreaseInQualityBy2_IfLessThanOrEqualTo10Days_BackstagePasses()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 9, Quality = 22 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);

        }

        [Test]
        public void shouldIncreaseInQualityBy3_IfLessThanOrEqualTo5Days_BackstagePasses()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 23 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);

        }

        [Test]
        public void shouldZerotheQualityAfterSellInDate_BackstagePasses()
        {
            List<Item> Items = new List<Item>() { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 } };
            var app = new GildedRose.GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedOutput = new List<Item>() { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 0 } };

            expectedOutput[0].Should().BeEquivalentTo(Items[0]);

        }
    }
}