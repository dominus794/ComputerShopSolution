using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;
using ComputerShop.DataMappers.SQLDataSet;

namespace ComputerShop.MVC.Views
{
	public class EntitySelectorView<TEntity>
	{
		private string errorMessage = string.Empty;
		public string ErrorMessage
		{
			get { return errorMessage; }
			set { errorMessage = value; }
		}

		public void DisplayList(List<TEntity> entitys)
		{
			Console.Clear();
			Console.WriteLine("Please select a {0}:\n", typeof(TEntity).Name);

			for (int i = 0; i < entitys.Count; i++)
			{
				Console.WriteLine("{0}.{1}", i + 1, entitys[i].ToString());
			}

			if (ErrorMessage != string.Empty)
			{
				Console.WriteLine();
				Console.WriteLine(ErrorMessage);
			}
		}
	}
}
