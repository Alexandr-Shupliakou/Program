using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Library
{
	public class VendingMachine
	{
		public int Id { get; set; }
		public string Product { get; set; }
		public int Price { get; set; }
		public int Quantity { get; set; }
		
	}
}
