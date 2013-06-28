using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;
using ComputerShop.DataMappers.SQLDataSet;
using ComputerShop.MVC.Views;

namespace ComputerShop.MVC.Controllers
{
	public class EntitySelectorController<TEntity>
	{
		private IRepository<TEntity> entityRepository;
		private EntitySelectorView<TEntity> view;

		public EntitySelectorController(IRepository<TEntity> entityRepository)
		{
			this.entityRepository = entityRepository;
			view = new EntitySelectorView<TEntity>();
		}

		public TEntity Select()
		{
			List<TEntity> entitys = entityRepository.GetAll().ToList<TEntity>();

			bool valid = false;
			int choice = -1;

			while (!valid)
			{
				view.DisplayList(entitys);

				//get user input
				string input = Console.ReadLine();

				//parse user input
				if (int.TryParse(input, out choice))
				{
					//choice = int.Parse(input);

					if (choice > entitys.Count)
					{
						view.ErrorMessage = "You must select a valid option from the list.";
						//Console.WriteLine("You must select a valid option from the list.");
						continue;
					}

					//convert choice to string,
					string chosenString = entitys[choice - 1].ToString();

					valid = entitys.Exists(i => i.ToString() == chosenString);

					if (!valid)
					{
						view.ErrorMessage = "You must select a valid option from the list.";
						break;
					}
					else
					{
						view.ErrorMessage = string.Empty;
					}
				}
				else
				{
					valid = false;
					view.ErrorMessage = "You must select a valid option from the list.";
					continue;
				}
			}

			return entitys[choice - 1];
		}

		/// <summary>
		/// Displays a filtered list to select from.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public TEntity Select(Expression<Func<TEntity, bool>> predicate)
		{
			List<TEntity> entitys = entityRepository.Search(predicate).ToList<TEntity>(); //entityRepository.GetAll().ToList<TEntity>();

			bool valid = false;
			int choice = -1;

			while (!valid)
			{
				view.DisplayList(entitys);

				//get user input
				string input = Console.ReadLine();

				//parse user input
				if (int.TryParse(input, out choice))
				{
					//choice = int.Parse(input);

					if (choice > entitys.Count)
					{
						view.ErrorMessage = "You must select a valid option from the list.";
						//Console.WriteLine("You must select a valid option from the list.");
						continue;
					}

					//convert choice to string,
					string chosenString = entitys[choice - 1].ToString();

					valid = entitys.Exists(i => i.ToString() == chosenString);

					if (!valid)
					{
						view.ErrorMessage = "You must select a valid option from the list.";
						break;
					}
					else
					{
						view.ErrorMessage = string.Empty;
					}
				}
				else
				{
					valid = false;
					view.ErrorMessage = "You must select a valid option from the list.";
					continue;
				}
			}

			return entitys[choice - 1];
		}
	}
}
