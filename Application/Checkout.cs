using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class Checkout
    {
		private decimal _runningTotal;
		private List<string>_scannedItems;
		private readonly Dictionary<string, decimal> _priceList;
		private readonly List<Discount> _discounts;

		public Checkout(Dictionary<string, decimal> priceList, List<Discount> discounts)
		{
			_priceList = priceList;
			_discounts = discounts ?? new List<Discount>();
			_scannedItems = new List<string>();
		}

		public void Scan(string scanItem)
		{
			if (string.IsNullOrEmpty(scanItem)) return;

			_scannedItems.Add(scanItem);
		}

		public decimal GetTotal()
		{
			_runningTotal = _scannedItems.Sum(x=>GetPrice(x));

			if (_discounts.Any())
			_runningTotal = _runningTotal - CalculatedDiscount(_scannedItems);
			
			return _runningTotal;
		}

		private decimal CalculatedDiscount(List<string> scannedItems)
		{
			decimal discount = 0;
			var discountsToApply = new Dictionary<string, int>();

			var itemsToCheck = scannedItems.Distinct();

			foreach (var item in itemsToCheck)
			{
				if (!_discounts.Exists(x => x.Name == item)) continue;
				discountsToApply.Add(item, scannedItems.Count(x => x == item));
			}

			if (!discountsToApply.Any()) return discount;

			foreach (var itemDiscount in discountsToApply)
			{
				var itemName = itemDiscount.Key.ToString();
				var discountItem = _discounts.Single(x => x.Name == itemName);
				int discountMultiplier = scannedItems.Count(x => x == itemName) / discountItem.Quantity;

				if (discountMultiplier < 1) continue;

				var remainder = scannedItems.Count(x => x == itemName) % discountItem.Quantity;
				var currentCharge = scannedItems.Where(y=>y == itemName).Sum(x => GetPrice(x));

				discount += currentCharge - ((discountMultiplier * discountItem.DiscountPrice) + (remainder * GetPrice(itemName)));
			}

			return discount;
		}

		private decimal GetPrice(string itemName)
		{
			decimal price = 0;
			if (string.IsNullOrEmpty(itemName) || !_priceList.TryGetValue(itemName, out price)) return default(decimal);

			return _priceList[itemName];
		}
		public void Clear()
		{
			_scannedItems.Clear();
		}
	}
}
