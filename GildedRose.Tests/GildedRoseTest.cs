using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose.Tests
{
    [TestClass]
    public class GildedRoseTest
    {

        private GildedRose CreateGildedRose(List<Item> items)
        {
            return new GildedRose(items);
        }

        [TestMethod]
        public void RustyDaggerWithQuality1AndPositiveSellDateShouldDegradeTo0()
        {
            var items = new List<Item>
            {
                new Item {Name = "Rusty Dagger", Quality = 1, SellIn = 1}
            };

            var gildedRose = CreateGildedRose(items);
            gildedRose.UpdateQuality();

            var expectedQuality = 0;
            Assert.AreEqual(expectedQuality, items[0].Quality);
        }

        [TestMethod]
        public void TwoItemsWithPositiveSellInDatesShouldBothDegradeByOne()
        {
            var items = new List<Item>
            {
                new Item {Name = "Rusty Longsword", Quality = 5, SellIn = 2},
                new Item {Name = "Sacred Candle", Quality = 2, SellIn = 100}
            };

            var gildedRose = CreateGildedRose(items);
            gildedRose.UpdateQuality();

            var expectedQuality1 = 4;
            var expectedQuality2 = 1;
            Assert.AreEqual(expectedQuality1, items[0].Quality);
            Assert.AreEqual(expectedQuality2, items[1].Quality);
        }

        [TestMethod]
        public void OneItemWithNegativeSellDateShouldDegradeByTwo()
        {
            var items = new List<Item>
            {
                new Item{Name = "Chain Shirt", Quality = 10, SellIn = -1}
            };

            var gildedRose = CreateGildedRose(items);
            gildedRose.UpdateQuality();

            var expectedQuality = 8;
            Assert.AreEqual(expectedQuality, items[0].Quality);
        }

        [TestMethod]
        public void OneItemWithZeroSellDateShouldDegradeByTwo()
        {
            var items = new List<Item>
            {
                new Item{Name = "Dirty Water", Quality = 15, SellIn = 0}
            };

            var gildedRose = CreateGildedRose(items);
            gildedRose.UpdateQuality();

            var expectedQuality = 13;
            Assert.AreEqual(expectedQuality, items[0].Quality);
        }

        [TestMethod]
        public void ItemQualityShouldNeverGoToNegative()
        {
            var items = new List<Item>
            {
                new Item{Name = "Holy Water", Quality = 0, SellIn = 1},
                new Item{Name = "Heartseeker", Quality = 0, SellIn = -2},
                new Item{Name = "Perdition's Blade", Quality = 0, SellIn = 0}
            };

            var gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();

            var expectedQuality = 0;
            items.ForEach(item => Assert.AreEqual(expectedQuality, item.Quality));
        }

        [TestMethod]
        public void UpdateQualityCalledMultipleTimesShouldContinueToUpdateQuality()
        {
            var items = new List<Item>
            {
                new Item{Name = "Truthseeker's Vestments", Quality = 4, SellIn = 1}
            };

            var gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();

            var expectedQuality = 1;
            Assert.AreEqual(expectedQuality, items[0].Quality);
        }



    }
}
