using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public class Discount
	{
		public Discount(string name, int quantity, decimal discountPrice)
		{
			Name = name;
			Quantity = quantity;
			DiscountPrice = discountPrice;
		}

		public string Name { get; }
		public int Quantity { get; }
		public decimal DiscountPrice { get; }
	}
}
