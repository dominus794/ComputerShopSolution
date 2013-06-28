using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.MVC;
using ComputerShop.Interfaces;
using ComputerShop.Domain;

namespace ComputerShop.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			bool quit = false;
			string input;

			List<Customer> customers = new List<Customer>();

			ComputerShopManager shopManager = new ComputerShopManager();
			
			while (!quit)
			{
				DisplayMenu();
				input = System.Console.ReadLine();

				input = input.ToUpper();

				switch (input)
				{
					case "C":
						CreateComputer(shopManager);
						break;
					case "N":
						CreateCustomer(customers);
						break;
					case "S":
						Search(shopManager);
						break;
					case "M":
						Match(customers, shopManager);
						break;
					case "O":
						ListComputers(shopManager);
						break;
					case "L":
						ListCustomers(customers);
						break;
					case "D":
						shopManager.DisplayMainMenu();
						break;
					case "Q":
						quit = true;
						break;
					default:
						{
							System.Console.ForegroundColor = ConsoleColor.Yellow;
							System.Console.WriteLine("Invalid input please try again");
							System.Console.ResetColor();
						}
						break;
				}
			}						
		}

		static void DisplayMenu()
		{			
			System.Console.Clear();
			System.Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine("---- Computer Shop ----");
			System.Console.ResetColor();
			
			System.Console.Write("Please select a "); System.Console.ForegroundColor = ConsoleColor.Green; System.Console.Write("command "); System.Console.ResetColor(); System.Console.WriteLine("below:");
						
			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("C"); System.Console.ResetColor(); System.Console.WriteLine(": - Create a new Computer");

			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("N"); System.Console.ResetColor(); System.Console.WriteLine(": - Create a new Customer");

			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("S"); System.Console.ResetColor(); System.Console.WriteLine(": - Search");

			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("M"); System.Console.ResetColor(); System.Console.WriteLine(": - Match");

			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("O"); System.Console.ResetColor();	System.Console.WriteLine(": - Order or otherwise sort computers");

			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("L"); System.Console.ResetColor();	System.Console.WriteLine(": - List or otherwise sort customers");

			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("D"); System.Console.ResetColor();	System.Console.WriteLine(": - Manage Database");

			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("Q"); System.Console.ResetColor();	System.Console.WriteLine(": - Quit");
		}

		static void Search(ComputerShopManager shopManager)
		{
			//list to store the search results
			List<Computer> results = new List<Computer>();

			//let the user choose between a desktop and a laptop.
			string computerType = string.Empty;
			ComputerShop.MVC.Utilities.UserInputValidator.GetUserInput<string>(ref computerType, new List<string> { "Desktop", "Laptop" }, "Please select the type of computer you want:", "Invalid choice, please try again");
			//let user input a maximum price
			decimal maxPrice = 0.00M;
            ComputerShop.MVC.Utilities.UserInputValidator.GetUserInput(ref maxPrice, "Please enter a maximum price:", "Price must be between 0.00 and 1000.00", 0M, 1000M);
			//let the user input a minimum cpu speed
			float minimumCPUSpeed = 0.0F;
            ComputerShop.MVC.Utilities.UserInputValidator.GetUserInput(ref minimumCPUSpeed, "Please enter a  minimum cpu speed:", "Speed must be between 1.0 and 4.0", 1.0F, 4.0F);
			//let the user input a minimum ram amount.
			ushort minimumRamAmount = 0;
            ComputerShop.MVC.Utilities.UserInputValidator.GetUserInput(ref minimumRamAmount, "Please enter a minimum ram amount:", "Ram amount must be between 2 and 16", 2, 16);
			//let the user input a minimum hdd capacity.
			ushort minimumHddCapacity = 0;
            ComputerShop.MVC.Utilities.UserInputValidator.GetUserInput(ref minimumHddCapacity, "Please enter a minimum harddrive capacity:", "Hdd capacity must be between 60 and 3072", 60, 3072);

			switch (computerType)
			{
				case "Desktop":
					{
						List<Desktop> desktops = new List<Desktop>();
						desktops = shopManager.SearchDesktops(c => c.Cpu.ClockSpeed >= minimumCPUSpeed &&
																		   c.Price <= maxPrice &&
																		   c.TotalRam >= minimumRamAmount &&
																		   c.TotalHDDCapacity >= minimumHddCapacity).ToList<Desktop>();

						foreach (Desktop d in desktops) results.Add(d);
					}
					break;
				case "Laptop":
					{
						List<Laptop> laptops = new List<Laptop>();
						laptops = shopManager.SearchLaptops(c => c.Cpu.ClockSpeed >= minimumCPUSpeed &&
																		   c.Price <= maxPrice &&
																		   c.TotalRam >= minimumRamAmount &&
																		   c.TotalHDDCapacity >= minimumHddCapacity).ToList<Laptop>();

						foreach (Laptop l in laptops) results.Add(l);
					}
					break;
			}		

			//display the search results.
			if (results.Count > 0)
			{
				System.Console.Clear();
				System.Console.WriteLine("You specified:");
				System.Console.WriteLine("A {0} with a max price of {1:c}, and a minimum cpu speed of {2}Ghz, and at least {3}GB of ram, and a hdd capacity of at least {4}MB", computerType, maxPrice, minimumCPUSpeed, minimumRamAmount, minimumHddCapacity);
				System.Console.WriteLine();
				System.Console.WriteLine("\nWe found:");
				foreach (Computer c in results)
				{
					System.Console.WriteLine(c);
				}
				System.Console.WriteLine();
				System.Console.WriteLine("Press enter to Go Back");
				System.Console.ReadLine();
			}
			else
			{
				System.Console.Clear();
				System.Console.WriteLine("A {0} with a max price of {1:c}, and a minimum cpu speed of {2}Ghz, and at least {3}GB of ram, and a hdd capacity of at least {4}MB", computerType, maxPrice, minimumCPUSpeed, minimumRamAmount, minimumHddCapacity);
				System.Console.WriteLine();
				System.Console.WriteLine("\nNo Matches met your requirements");
				System.Console.WriteLine();
				System.Console.WriteLine("Press enter to Go Back");
				System.Console.ReadLine();
			}
		}

		static void Match(List<Customer> customers, ComputerShopManager shopManager)
		{
			//loop thru each customer
			//loop thru each computer
			//if computer spec matches cmr spec, link them in a map

			Dictionary<Customer, Computer> matched = new Dictionary<Customer, Computer>();			

			//get all the desktops
			IList<Desktop> desktops = new List<Desktop>();
			desktops = shopManager.GetAllDesktops();
			//get all the laptops
			IList<Laptop> laptops = new List<Laptop>();
			laptops = shopManager.GetAllLaptops();

			foreach (Customer cmr in customers)
			{
				switch (cmr.ComputerType)
				{
					case ComputerType.Desktop:
						{
							List<Desktop> matches = (from d in desktops
													 where d.Cpu.ClockSpeed >= cmr.MinimumCpuSpeed &&
														d.Price <= cmr.MaxPrice &&
														d.TotalRam >= cmr.MinimumRamAmount &&
														d.TotalHDDCapacity >= cmr.MinimumHDDCapacity
													 orderby d.Price descending
													 select d).ToList<Desktop>();

							//if we found some matches
							if (matches.Count > 0)
							{
								//add the most expensive pc to our matched dictionary.
								matched.Add(cmr, matches[0]);
								//remove the matched desktop from the list of desktops.
								desktops.Remove(matches[0]);
							}
						}
						break;
					case ComputerType.Laptop:
						{
							List<Laptop> matches = (from d in laptops
													 where d.Cpu.ClockSpeed >= cmr.MinimumCpuSpeed &&
														d.Price <= cmr.MaxPrice &&
														d.TotalRam >= cmr.MinimumRamAmount &&
														d.TotalHDDCapacity >= cmr.MinimumHDDCapacity
													 orderby d.Price descending
													 select d).ToList<Laptop>();

							//if we found some matches
							if (matches.Count > 0)
							{
								//add the most expensive pc to our matched dictionary.
								matched.Add(cmr, matches[0]);
								//remove the matched desktop from the list of desktops.
								laptops.Remove(matches[0]);
							}
						}
						break;
				}
			}

			//print matches:
			foreach (KeyValuePair<Customer, Computer> match in matched)
			{
				Customer cmr = match.Key;
				Computer pc = match.Value;

				System.Console.WriteLine("Customer: {0}", cmr);
				System.Console.WriteLine();
				System.Console.WriteLine("Matches with:");
				System.Console.WriteLine(pc);
				System.Console.WriteLine();
			}

			System.Console.WriteLine();
			System.Console.WriteLine("Press enter to Go Back");
			System.Console.ReadLine();
		}

		// computer methods
		static void CreateComputer(ComputerShopManager shopManager)
		{
			string input = String.Empty;
			ushort choice = 0;
			bool finished = false;
	
			while (!finished)
			{				
				System.Console.WriteLine("Please select the type of computer you wish to create:");
				System.Console.WriteLine("1. Desktop");
				System.Console.WriteLine("2. Laptop");
				System.Console.WriteLine("3. Go Back");

				input = System.Console.ReadLine();
				choice = 0;

				if (ushort.TryParse(input, out choice))
				{
					choice = ushort.Parse(input);

					switch (choice)
					{
						case 1:
							shopManager.DesktopFactory.CreateDesktop();
							break;
						case 2:
							shopManager.LaptopFactory.CreateLaptop();
							break;
						case 3:
							finished = true;
							break;
						default:
							break;
					}
				}
				else
					continue;
			}

		}

		static void ListComputers(ComputerShopManager shopManager)
		{
			//get all the desktops
			IList<Desktop> desktops = new List<Desktop>();
			desktops = (from d in shopManager.GetAllDesktops()
					   orderby d.Price ascending
						select d).ToList<Desktop>();

			//get all the laptops
			IList<Laptop> laptops = new List<Laptop>();
			laptops = shopManager.GetAllLaptops();

			System.Console.Clear();

			System.Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine("Desktops:");
			System.Console.ResetColor();
			foreach (Desktop d in desktops) System.Console.WriteLine(d);

			System.Console.WriteLine();

			System.Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine("Laptops:");
			System.Console.ResetColor();
			foreach (Laptop l in laptops) System.Console.WriteLine(l);

			System.Console.WriteLine();
			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.WriteLine("Press enter to Go Back");
			System.Console.ResetColor();
			System.Console.ReadLine();
		}

		// customer methods
		static void CreateCustomer(List<Customer> customers)
		{
			System.Console.WriteLine("Please enter your first name:");
			string firstName = System.Console.ReadLine();

			System.Console.WriteLine("Please enter your last name:");
			string lastName = System.Console.ReadLine();

			System.Console.WriteLine("Please enter your invoice address:");
			IAddress invAddress = CreateAddress();

			System.Console.WriteLine("Please enter your delivery address:");
			IAddress delAddress = CreateAddress();

			ComputerType computerType = ComputerType.Desktop;
            ComputerShop.MVC.Utilities.UserInputValidator.GetUserInput<ComputerType>(ref computerType, "Please select what type of computer you require:", "Invalid Selection, please try again");

			System.Console.WriteLine("Please enter your maximum price:");
			string maxPriceString = System.Console.ReadLine();
			decimal maxPrice;

			if (decimal.TryParse(maxPriceString, out maxPrice))
			{
				maxPrice = decimal.Parse(maxPriceString);
			}

			System.Console.WriteLine("Please enter your minimum cpu speed:");
			string minCpuString = System.Console.ReadLine();
			float minCpu;

			if (float.TryParse(minCpuString, out minCpu))
			{
				minCpu = float.Parse(minCpuString);
			}

			System.Console.WriteLine("Please enter your minimum hdd capacity:");
			string minHddString = System.Console.ReadLine();
			ushort minHdd;

			if (ushort.TryParse(minHddString, out minHdd))
			{
				minHdd = ushort.Parse(minHddString);
			}

			System.Console.WriteLine("Please enter your minimum hdd capacity:");
			string minRamString = System.Console.ReadLine();
			ushort minRam;

			if (ushort.TryParse(minRamString, out minRam))
			{
				minRam = ushort.Parse(minRamString);
			}

			Customer newCustomer = new Customer(firstName, lastName, invAddress, delAddress, computerType, maxPrice, minCpu, minHdd, minRam);

			customers.Add(newCustomer);

		}

		private static IAddress CreateAddress()
		{
			System.Console.WriteLine("Please enter the 1st Address Line:");
			string addressLine1 = System.Console.ReadLine();

			System.Console.WriteLine("Please enter the 2nd Address Line:");
			string addressLine2 = System.Console.ReadLine();

			System.Console.WriteLine("Please enter your city:");
			string city = System.Console.ReadLine();

			System.Console.WriteLine("Please enter your country:");
			string country = System.Console.ReadLine();

			System.Console.WriteLine("Please enter your postcode:");
			string postcode = System.Console.ReadLine();

			return new Address(addressLine1, addressLine2, city, country, postcode);
		}
		
		static void ListCustomers(List<Customer> customers)
		{
			foreach (Customer c in customers)
			{
				System.Console.WriteLine(c);
			}

			System.Console.WriteLine();
			System.Console.WriteLine("Press enter to Go Back");
			System.Console.ReadLine();
		}
		
	}
}
