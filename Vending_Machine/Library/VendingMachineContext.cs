using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	class VendingMachineContextInitializer : DropCreateDatabaseIfModelChanges<VendingMachineContext>
	{
		protected override void Seed(VendingMachineContext db)
		{
			VendingMachine vm1 = new VendingMachine
			{
				Price = 13,
				Product = "Чай",
				Quantity = 10
			};

			VendingMachine vm2 = new VendingMachine
			{
				Price = 18,
				Product = "Кофе",
				Quantity = 20
			};

			VendingMachine vm3 = new VendingMachine
			{
				Price = 21,
				Product = "Кофе с молоком",
				Quantity = 20
			};

			VendingMachine vm4 = new VendingMachine
			{
				Price = 35,
				Product = "Сок",
				Quantity = 15
			};

			db.VendingMachines.Add(vm1);
			db.VendingMachines.Add(vm2);
			db.VendingMachines.Add(vm3);
			db.VendingMachines.Add(vm4);

			BuyerWallet bw1 = new BuyerWallet()
			{
				CoinValue = 1,
				Quantity = 10
			};

			BuyerWallet bw2 = new BuyerWallet()
			{
				CoinValue = 2,
				Quantity = 30
			};

			BuyerWallet bw3 = new BuyerWallet()
			{
				CoinValue = 5,
				Quantity = 20
			};

			BuyerWallet bw4 = new BuyerWallet()
			{
				CoinValue = 10,
				Quantity = 15
			};

			db.BuyerWallets.Add(bw1);
			db.BuyerWallets.Add(bw2);
			db.BuyerWallets.Add(bw3);
			db.BuyerWallets.Add(bw4);

			MachineWallet mw1 = new MachineWallet()
			{
				 CoinValue = 1,
				 Quantity = 100
			};

			MachineWallet mw2 = new MachineWallet()
			{
				CoinValue = 2,
				Quantity = 100
			};

			MachineWallet mw3 = new MachineWallet()
			{
				CoinValue = 5,
				Quantity = 100
			};

			MachineWallet mw4 = new MachineWallet()
			{
				CoinValue = 10,
				Quantity = 100
			};

			db.MachineWallets.Add(mw1);
			db.MachineWallets.Add(mw2);
			db.MachineWallets.Add(mw3);
			db.MachineWallets.Add(mw4);
			
			db.SaveChanges();
		}
	}

	public class VendingMachineContext : DbContext
	{
		static VendingMachineContext()
		{
			Database.SetInitializer<VendingMachineContext>(new VendingMachineContextInitializer());
		}

		public VendingMachineContext() : base("MachineDB")
		{ }

		public DbSet<VendingMachine> VendingMachines { get; set; }
		public DbSet<BuyerWallet> BuyerWallets { get; set; }
		public DbSet<MachineWallet> MachineWallets { get; set; }
	}
}
