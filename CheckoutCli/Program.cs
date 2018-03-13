using Application;
using System;
using System.Collections.Generic;

namespace CheckoutCli
{
	class Program
	{
		private static Dictionary<string, decimal> _priceList;
		private static List<Discount> _discounts;
		static void Main(string[] args)
		{
			SetupDefaultData();

			var checkout = new Checkout(_priceList, _discounts);
			ShowPremble();
			var input = Console.ReadLine();

			do
			{
				if (input.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
					Clear(checkout);

				if (input.StartsWith("help", StringComparison.OrdinalIgnoreCase))
					ShowHelp();

				if (input.StartsWith("scan", StringComparison.OrdinalIgnoreCase))
					Scan(input, checkout);

				ShowSubTotal(checkout);
				input = Console.ReadLine();
			} while (!input.StartsWith("exit", StringComparison.OrdinalIgnoreCase));

		}

		private static void ShowSubTotal(Checkout checkout)
		{
			Console.WriteLine($"SubTotal: {checkout.GetTotal()}");
			Console.Write("> ");
		}

		private static void ShowHelp()
		{
			Console.Clear();

			Console.WriteLine("The Following Commands are accepted:" + Environment.NewLine);
			Console.WriteLine("Scan: Takes item name. used like > Scan Bisuits");
			Console.WriteLine("Clear: clears  and restarts the checkout");
			Console.WriteLine("Exit: exit the application");
		}

		private static void ShowPremble()
		{
			Console.WriteLine("Checkout Scanner: to scan an apple type: Scan Apple");
			Console.WriteLine("for a list of commands type Help then enter.");
			Console.Write("> ");

		}
		private static void Clear(Checkout checkout)
		{
			Console.Clear();
			checkout.Clear();
		}
		private static void Scan(string input, Checkout checkout)
		{
			char space = ' ';

			if (input.Split(space).Length < 2) return;

			var itemName = input.Split(space)[1];

			if (string.IsNullOrEmpty(itemName)) return;

			itemName.Replace("\"", string.Empty);
			itemName = UppercaseFirst(itemName);

			checkout.Scan(itemName);
		}

		private static void SetupDefaultData()
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
		private static string UppercaseFirst(string s)
		{
			char[] chars = s.ToCharArray();
			chars[0] = char.ToUpper(chars[0]);
			return new string(chars);
		}
	}
}
