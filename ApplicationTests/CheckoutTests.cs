using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Tests
{
	[TestClass]
	public class CheckoutTesting
	{
		private Dictionary<string, decimal> _priceList;
		private List<Discount> _discounts;

		[TestInitialize]
		public void Setup()
		{
			_priceList = new Dictionary<string, decimal>
			{
				{ "Apple", 0.50m},
				{ "Biscuits", 0.30m},
				{ "Coffee", 1.80m},
				{ "Tissues", 0.99m},
			};

			_discounts = new List<Discount>
			{
				new Discount( "Apple", 3,  1.30m),
				new Discount( "Biscuits", 2,  0.45m),
			};

		}

		[TestMethod]
		public void CheckoutScansEmptyItem()
		{
			var expectedTotal = 0;
			var scanItem = string.Empty;

			var checkout = new Checkout(_priceList, null);

			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}

		[TestMethod]
		public void CheckoutScansSingleApple()
		{
			var expectedTotal = 0.50m;
			var scanItem = "Apple";

			var checkout = new Checkout(_priceList, null);

			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}

		[TestMethod]
		public void CheckoutScansTwoApples()
		{
			var expectedTotal = 0.50m;
			var scanItem = "Apple";

			var checkout = new Checkout(_priceList, null);

			checkout.Scan(scanItem);
			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(2 * expectedTotal, total);
		}

		[TestMethod]
		public void CheckoutScansBiscuits()
		{
			var expectedTotal = 0.30m;
			var scanItem = "Biscuits";

			var checkout = new Checkout(_priceList, null);

			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}

		[TestMethod]
		public void CheckoutScansCoffee()
		{
			var expectedTotal = 1.80m;
			var scanItem = "Coffee";

			var checkout = new Checkout(_priceList, null);

			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}

		[TestMethod]
		public void CheckoutScansTissues()
		{
			var expectedTotal = 0.99m;
			var scanItem = "Tissues";

			var checkout = new Checkout(_priceList, null);

			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}

		[TestMethod]
		public void CheckoutScansUnknownItem()
		{
			var expectedTotal = 0m;
			var scanItem = "Pepsi";

			var checkout = new Checkout(_priceList, null);

			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}

		public void ItemsMustBeAcceptedInAnyOrder_ReturnsCorrectTotal()
		{
			var scanItemApple = "Apple";
			var scanItemBiscuits = "Biscuits";
			var scanItemCoffee = "Coffee";

			var expectedTotal = (2 * _priceList[scanItemApple]) + _priceList[scanItemBiscuits] + _priceList[scanItemCoffee];

			var checkout = new Checkout(_priceList, null);

			checkout.Scan(scanItemApple);
			checkout.Scan(scanItemBiscuits);
			checkout.Scan(scanItemCoffee);
			checkout.Scan(scanItemApple);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}

		[TestMethod]
		public void CheckoutScansDiscountedApples()
		{
			var expectedTotal = 1.30m;
			var scanItem = "Apple";

			var checkout = new Checkout(_priceList, _discounts);

			checkout.Scan(scanItem);
			checkout.Scan(scanItem);
			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}

		[TestMethod]
		public void CheckoutScansDiscountedBiscuits()
		{
			var expectedTotal = 0.45m;
			var scanItem = "Biscuits";

			var checkout = new Checkout(_priceList, _discounts);

			checkout.Scan(scanItem);
			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}
		[TestMethod]
		public void CheckoutScans7DiscountedApples()
		{
			var expectedTotal = (2 * 1.30m) + 0.50m;
			var scanItem = "Apple";

			var checkout = new Checkout(_priceList, _discounts);

			checkout.Scan(scanItem);
			checkout.Scan(scanItem);
			checkout.Scan(scanItem);
			checkout.Scan(scanItem);
			checkout.Scan(scanItem);
			checkout.Scan(scanItem);
			checkout.Scan(scanItem);

			var total = checkout.GetTotal();

			Assert.AreEqual(expectedTotal, total);
		}
	}
}
