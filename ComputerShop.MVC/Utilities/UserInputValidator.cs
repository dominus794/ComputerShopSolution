using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.MVC.Utilities
{
	public static class UserInputValidator
	{
		#region String Methods

		public static void GetUserInput(ref string str, string prompt, string errMsg,
										bool allowSpaces = true, bool allowDigits = true, bool allowPunctuation = true)
		{
			bool valid = false;

			while (!valid)
			{
				Console.WriteLine(prompt);
				str = Console.ReadLine();

				valid = Validate(str, allowSpaces, allowDigits, allowPunctuation);

				if (!valid)
					Console.WriteLine(errMsg);
			}
		}

		private static bool Validate(string str, bool allowSpaces, bool allowDigits, bool allowPunctation)
		{
			bool valid = false;

			if (string.IsNullOrEmpty(str))
			{
				Console.WriteLine("You must input something");
				return valid;
			}				

			//check each char is a letter or a space.
			for (int i = 0; i < str.Length; i++)
			{
				char ch = str[i];

				if (char.IsLetter(ch))
				{
					valid = true;
					continue;
				}
				else if (char.IsWhiteSpace(ch))
				{
					if (allowSpaces)
					{
						valid = true;
						continue;
					}
					else
					{
						valid = false;
						Console.WriteLine("Spaces are not allowed.");
						break;
					}
				}
				else if (char.IsDigit(ch))
				{
					if (allowDigits)
					{
						valid = true;
						continue;
					}
					else
					{
						valid = false;
						Console.WriteLine("Digits are not allowed.");
						break;
					}
				}
				else
				{
					// we have encountered an invalid character.
					valid = false;
					Console.WriteLine("Invalid Character found.");
					break;
				}
			}

			return valid;
		}

		#endregion

		#region Byte Methods
		public static void GetUserInput(ref byte data, string prompt, string errMsg,
										byte minValue, ushort maxValue)
		{
			bool valid = false;

			while (!valid)
			{
				Console.WriteLine(prompt);

				string input = Console.ReadLine();

				if (byte.TryParse(input, out data))
				{
					data = byte.Parse(input);

					valid = Validate(data, minValue, maxValue);

					if (!valid)
						Console.WriteLine(errMsg);
				}
				else
				{
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}

		public static bool Validate(byte value, byte minValue, byte maxValue)
		{
			return (value >= minValue) && (value <= maxValue);
		}
		#endregion

		#region UShort Methods
		public static void GetUserInput(ref ushort data, string prompt, string errMsg,
										ushort minValue, ushort maxValue)
		{
			bool valid = false;



			while (!valid)
			{
				Console.WriteLine(prompt);

				string input = Console.ReadLine();

				if (ushort.TryParse(input, out data))
				{
					data = ushort.Parse(input);

					valid = Validate(data, minValue, maxValue);

					if (!valid)
						Console.WriteLine(errMsg);
				}
				else
				{
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}

		public static bool Validate(ushort value, ushort minValue, ushort maxValue)
		{
			return (value >= minValue) && (value <= maxValue);
		}
		#endregion

		#region Short Methods
		public static void GetUserInput(ref short data, string prompt, string errMsg,
										short minValue, short maxValue)
		{
			bool valid = false;

			while (!valid)
			{
				Console.WriteLine(prompt);

				string input = Console.ReadLine();

				if (short.TryParse(input, out data))
				{
					data = short.Parse(input);

					valid = Validate(data, minValue, maxValue);

					if (!valid)
						Console.WriteLine(errMsg);
				}
				else
				{
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}

		public static bool Validate(short value, short minValue, short maxValue)
		{
			return (value >= minValue) && (value <= maxValue);
		}
		#endregion

		#region UInt Methods
		public static void GetUserInput(ref uint data, string prompt, string errMsg,
										uint minValue, uint maxValue)
		{
			bool valid = false;

			while (!valid)
			{
				Console.WriteLine(prompt);

				string input = Console.ReadLine();

				if (uint.TryParse(input, out data))
				{
					data = uint.Parse(input);

					valid = Validate(data, minValue, maxValue);

					if (!valid)
						Console.WriteLine(errMsg);
				}
				else
				{
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}

		public static bool Validate(uint value, uint minValue, uint maxValue)
		{
			return (value >= minValue) && (value <= maxValue);
		}
		#endregion

		#region Int Methods
		public static void GetUserInput(ref int data, string prompt, string errMsg,
										int minValue, int maxValue)
		{
			bool valid = false;

			while (!valid)
			{
				Console.WriteLine(prompt);

				string input = Console.ReadLine();

				if (int.TryParse(input, out data))
				{
					data = int.Parse(input);

					valid = Validate(data, minValue, maxValue);

					if (!valid)
						Console.WriteLine(errMsg);
				}
				else
				{
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}

		public static bool Validate(int value, int minValue, int maxValue)
		{
			return (value >= minValue) && (value <= maxValue);
		}
		#endregion

		#region Decimal Methods
		public static void GetUserInput(ref decimal data, string prompt, string errMsg,
										decimal minValue, decimal maxValue)
		{
			bool valid = false;

			while (!valid)
			{
				Console.WriteLine(prompt);

				string input = Console.ReadLine();

				if (decimal.TryParse(input, out data))
				{
					data = decimal.Parse(input);

					valid = Validate(data, minValue, maxValue);

					if (!valid)
						Console.WriteLine(errMsg);
				}
				else
				{
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}

		public static bool Validate(decimal value, decimal minValue, decimal maxValue)
		{
			return (value >= minValue) && (value <= maxValue);
		}
		#endregion

		#region Single Methods
		public static void GetUserInput(ref float data, string prompt, string errMsg,
										float minValue, float maxValue)
		{
			bool valid = false;

			while (!valid)
			{
				Console.WriteLine(prompt);

				string input = Console.ReadLine();

				if (float.TryParse(input, out data))
				{
					data = float.Parse(input);

					valid = Validate(data, minValue, maxValue);

					if (!valid)
						Console.WriteLine(errMsg);
				}
				else
				{
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}

		public static bool Validate(float value, float minValue, float maxValue)
		{
			return (value >= minValue) && (value <= maxValue);
		}
		#endregion

		#region Double Methods
		public static void GetUserInput(ref double data, string prompt, string errMsg,
										double minValue, double maxValue)
		{
			bool valid = false;

			while (!valid)
			{
				Console.WriteLine(prompt);

				string input = Console.ReadLine();

				if (double.TryParse(input, out data))
				{
					data = double.Parse(input);

					valid = Validate(data, minValue, maxValue);

					if (!valid)
						Console.WriteLine(errMsg);
				}
				else
				{
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}

		public static bool Validate(double value, double minValue, double maxValue)
		{
			return (value >= minValue) && (value <= maxValue);
		}
		#endregion

		#region Enum Methods
		public static void GetUserInput<TEnum>(ref TEnum data, string prompt, string errMsg) where TEnum : struct
		{
			bool valid = false;
			int choice = -1;

			while (!valid)
			{
				//display user input prompt
				Console.WriteLine(prompt);

				//get the enum's names.
				string[] options = Enum.GetNames(typeof(TEnum));

				//display them for user to choose from.
				for (int i = 1; i <= options.Length; i++)
				{
					Console.WriteLine(string.Format("{0}. {1}", i, options[i - 1]));
				}

				//get user input.
				string input = Console.ReadLine();

				//parse user input - did user input a number
				if (int.TryParse(input, out choice))
				{
					choice = int.Parse(input);

					input = (choice - 1).ToString();

					//try and convert the numeric input to an enum value.
					//data = (TEnum)Enum.Parse(typeof(TEnum), (choice - 1).ToString());
					valid = Enum.TryParse<TEnum>(input, out data);

					if (!valid)
					{
						Console.WriteLine(errMsg);
						break;
					}
				}
				else
				{
					valid = false;
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}
		#endregion

		#region List Selection Methods
		public static void GetUserInput<T>(ref T data, List<T> list, string prompt, string errMsg)
		{
			bool valid = false;
			int choice = -1;

			while (!valid)
			{
				//display user input prompt.
				Console.WriteLine(prompt);

				// display the list of options.
				for (int i = 1; i <= list.Count; i++)
				{
					Console.WriteLine(string.Format("{0}. {1}", i, list[i - 1].ToString()));
				}

				//get user input
				string input = Console.ReadLine();

				//parse user input
				if (int.TryParse(input, out choice))
				{
					choice = int.Parse(input);

					if (choice > list.Count)
					{
						Console.WriteLine("You must select a valid option from the list.");
						continue;
					}

					//convert choice to string,
					string chosenString = list[choice - 1].ToString();

					valid = list.Exists(i => i.ToString() == chosenString);

					if (!valid)
					{
						Console.WriteLine(errMsg);
						break;
					}

					// user must have selected a valid option.
					data = list.Find(i => i.ToString() == chosenString);

				}
				else
				{
					valid = false;
					Console.WriteLine(errMsg);
					continue;
				}
			}
		}
		#endregion
	}
}
