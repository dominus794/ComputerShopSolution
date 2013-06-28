using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using ComputerShop.DAL.SQLDataSet;
using ComputerShop.DAL.SQLDataSet.ComputerShopDBTableAdapters;

namespace ComputerShop.DataMappers.SQLDataSet
{
	internal class SQLDataSetDatabase
	{
		private static SQLDataSetDatabase instance;

		//The dataset
		public ComputerShopDB shopDB;
		//the table adapters
		public ManufacturersTableAdapter manufacturersTableAdapter;
		public CPUsTableAdapter cpusTableAdapter;
		public RamModulesTableAdapter ramModulesTableAdapter;
		public HDDsTableAdapter hddsTableAdapter;
		public GPUsTableAdapter gpusTableAdapter;

		public DesktopsTableAdapter desktopsTableAdapter;
		public DesktopRamModulesTableAdapter desktopRamModulesTableAdapter;
		public DesktopHDDsTableAdapter desktopHDDsTableAdapter;
		public DesktopGPUsTableAdapter desktopGPUsTableAdapter;

		public LaptopsTableAdapter laptopsTableAdapter;
		public LaptopRamModulesTableAdapter laptopRamModulesTableAdapter;

		private SQLDataSetDatabase()
		{
			Debug.WriteLine("In SQLDataSetDatabase Constructor");
			shopDB = new ComputerShopDB();

			manufacturersTableAdapter = new ManufacturersTableAdapter();
			manufacturersTableAdapter.Fill(shopDB.Manufacturers);
			cpusTableAdapter = new CPUsTableAdapter();
			cpusTableAdapter.Fill(shopDB.CPUs);
			ramModulesTableAdapter = new RamModulesTableAdapter();
			ramModulesTableAdapter.Fill(shopDB.RamModules);
			hddsTableAdapter = new HDDsTableAdapter();
			hddsTableAdapter.Fill(shopDB.HDDs);
			gpusTableAdapter = new GPUsTableAdapter();
			gpusTableAdapter.Fill(shopDB.GPUs);

			desktopsTableAdapter = new DesktopsTableAdapter();
			desktopsTableAdapter.Fill(shopDB.Desktops);
			desktopRamModulesTableAdapter = new DesktopRamModulesTableAdapter();
			desktopRamModulesTableAdapter.Fill(shopDB.DesktopRamModules);
			desktopHDDsTableAdapter = new DesktopHDDsTableAdapter();
			desktopHDDsTableAdapter.Fill(shopDB.DesktopHDDs);
			desktopGPUsTableAdapter = new DesktopGPUsTableAdapter();
			desktopGPUsTableAdapter.Fill(shopDB.DesktopGPUs);

			laptopsTableAdapter = new LaptopsTableAdapter();
			laptopsTableAdapter.Fill(shopDB.Laptops);
			laptopRamModulesTableAdapter = new LaptopRamModulesTableAdapter();
			laptopRamModulesTableAdapter.Fill(shopDB.LaptopRamModules);
		}

		public static SQLDataSetDatabase Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new SQLDataSetDatabase();
				}
				return instance;
			}
		}
	}
}
