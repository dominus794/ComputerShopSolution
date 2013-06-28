using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

namespace ComputerShop.DataMappers.XML
{
	public class XMLManufacturerDataMapper : XMLDataMapperBase<Manufacturer>
	{
		protected static XElement source;
		protected static int lastID;

		static XMLManufacturerDataMapper()
		{
			source = XElement.Load("Manufacturers.xml");
			XElement lastElement = source.Elements().Last<XElement>();
			lastID = int.Parse(lastElement.Attribute("ID").Value);
		}

		public override void Insert(Manufacturer data)
		{
			//Create the element.
			XElement newManufacturer =
				new XElement("Manufacturer", new XAttribute("ID", ++lastID),
					new XElement("Name", data.Name));

			//add to the xml document.
			source.Add(newManufacturer);
			source.Save("Manufacturers.xml");
		}

		public override void Delete(Manufacturer data)
		{
			//get the right XElement
			XElement manufacturer = (from m in source.Descendants("Manufacturer")
									 where int.Parse(m.Attribute("ID").Value) == data.ID
									 select m).Single<XElement>();

			//detach it from the xml element tree.
			manufacturer.Remove();
			//save the source file.
			source.Save("Manufacturers.xml");
		}

		public override Manufacturer Select(int id)
		{
			//find and instantiate the manufacturer
			Manufacturer manufacturer = (from m in source.Descendants("Manufacturers")
										 where int.Parse(m.Attribute("ID").Value) == id
										 select new Manufacturer(id, m.Element("Name").Value)).Single<Manufacturer>();

			return manufacturer;
		}

		public override void Update(Manufacturer data)
		{
			//get the right XElement
			XElement manufacturer = (from m in source.Descendants("Manufacturer")
									 where int.Parse(m.Attribute("ID").Value) == data.ID
									 select m).Single<XElement>();

			manufacturer.Attribute("ID").Value = data.ID.ToString();
			manufacturer.Element("Name").Value = data.Name;
			source.Save("Manufacturers.xml");
		}

		public override IList<Manufacturer> GetAll()
		{
			IList<Manufacturer> manufacturers = (from m in source.Descendants("Manufacturer")
												 select new Manufacturer(int.Parse(m.Attribute("ID").Value), m.Element("Name").Value)).ToList<Manufacturer>();

			return manufacturers;
		}
	}
}
