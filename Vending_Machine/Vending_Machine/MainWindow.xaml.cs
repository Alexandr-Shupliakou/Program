using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library;
namespace Vending_Machine
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		int sum = 0;
		VendingMachineContext db;

		public MainWindow()
        {
			
            InitializeComponent();
			db = new VendingMachineContext();



			Sum.Text = $"{sum}";
			Sum.TextAlignment = TextAlignment.Right;

			// Загрузка из БД в форму.

			var moneyMachine = db.MachineWallets;
			    MachineWallet walletMachine = moneyMachine.Find(1);
			    Machine1.Text = $"{walletMachine.Quantity}";
			    walletMachine = moneyMachine.Find(2);
			    Machine2.Text = $"{walletMachine.Quantity}";                            // Кошелёк автомата
			    walletMachine = moneyMachine.Find(3);
			    Machine5.Text = $"{walletMachine.Quantity}";
			    walletMachine = moneyMachine.Find(4);
			    Machine10.Text = $"{walletMachine.Quantity}";
			 
			var moneyBuyer = db.BuyerWallets;
				BuyerWallet walletBuyer = moneyBuyer.Find(1);
				Buyer1.Text = $"{walletBuyer.Quantity}";
			    walletBuyer = moneyBuyer.Find(2);
				Buyer2.Text = $"{walletBuyer.Quantity}";                                 // Кошелёк пользователя
			    walletBuyer = moneyBuyer.Find(3);
				Buyer5.Text = $"{walletBuyer.Quantity}";
			    walletBuyer = moneyBuyer.Find(4);
				Buyer10.Text = $"{walletBuyer.Quantity}";
			 
			var products = db.VendingMachines;
				VendingMachine product = products.Find(1);
			    Label1.Content = $"{product.Product}, {product.Price}руб.";
				TeaPortion.Text = $"{product.Quantity}";
				product = products.Find(2);
			    Label2.Content = $"{product.Product}, {product.Price}руб.";
			    CoffeePortion.Text = $"{product.Quantity}";                                // Количество продуктов
				product = products.Find(3);
			    Label3.Content = $"{product.Product}, {product.Price}руб.";
			    CoffeeMilkPortion.Text = $"{product.Quantity}";
				product = products.Find(4);
			    Label4.Content = $"{product.Product}, {product.Price}руб.";
			    JuicePortion.Text = $"{product.Quantity}";
			
		}
		public void WalletChange(TextBox a, TextBox b, int c)
		{
			int x = Convert.ToInt32(a.Text)-1;
			int y = Convert.ToInt32(b.Text)+1;    
			a.Text = $"{x}";
			b.Text = $"{y}";
			if (c < 0)
			{
				c *= -1;
				int temp = x;                                        // Метод переноса купюр из одного кошелька в другой
				x = y;
				y = temp;
			}
			var moneyBuyer = db.BuyerWallets;
			BuyerWallet walletBuyer = moneyBuyer.Find(c);
			walletBuyer.Quantity = x;
			var money = db.MachineWallets;
			MachineWallet wallet = money.Find(c);
			wallet.Quantity = y;
			db.SaveChanges();
		}

		public void Purchase(TextBox balance, int IdProduct)
		{
			int x = Convert.ToInt32(balance.Text);
			var products = db.VendingMachines;
			VendingMachine product = products.Find(IdProduct);
			if (x == 0)
				MessageBox.Show($"{product.Product} закончился.");
			else
			{
				
				if (product.Price > Convert.ToInt32(Sum.Text))                    // Метод покупки
					MessageBox.Show("Пополните баланс.");
				else
				{
					x -= 1;
					sum -= product.Price;
					Sum.Text = $"{sum}";
					Sum.TextAlignment = TextAlignment.Right;
					balance.Text = $"{x}";
					product.Quantity = Convert.ToInt32(balance.Text);
					db.SaveChanges();
					MessageBox.Show("Спасибо.");
				}
			}
		}
		public void Deposit(TextBox a, TextBox b, int IdWallet)
		{
			if (Convert.ToInt32(a.Text) == 0)
				MessageBox.Show("Купюры этого номинала у Вас закончились.");
			else
			{                                                                              //Метод переноса купюр
				var moneyMachine = db.MachineWallets;
				MachineWallet walletMachine = moneyMachine.Find(IdWallet);
				sum += walletMachine.CoinValue;
				Sum.Text = $"{sum}";
				Sum.TextAlignment = TextAlignment.Right;
				WalletChange(a, b, IdWallet);
			}
		}
		public void CashBack(TextBox a, TextBox b, int c)
		{
			var moneyMachine = db.MachineWallets;
			MachineWallet walletMachine = moneyMachine.Find(c);
			while (sum >= walletMachine.CoinValue)
			{                                                                             //Метод возврата денег
				if (Convert.ToInt32(a.Text) != 0)
				{
					sum -= walletMachine.CoinValue;
					WalletChange(Machine10, Buyer10, -c);
				}
				else break;
			}
		}
		// Покупки
		private void ButtonTea_Click(object sender, RoutedEventArgs e)
		{
			Purchase(TeaPortion, 1);			
		}
		private void ButtonCoffee_Click(object sender, RoutedEventArgs e)
		{
			Purchase(CoffeePortion, 2);
		}
		private void ButtonCoffeeMilk_Click(object sender, RoutedEventArgs e)
		{
			Purchase(CoffeeMilkPortion, 3);
		}
		private void ButtonJuice_Click(object sender, RoutedEventArgs e)
		{
			Purchase(JuicePortion, 4);
		}

		//Внесение денег
		private void Button1_Click(object sender, RoutedEventArgs e)
		{
				Deposit(Buyer1, Machine1, 1);
		}
		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			Deposit(Buyer2, Machine2, 2);
		}
		private void Button5_Click(object sender, RoutedEventArgs e)
		{
			Deposit(Buyer5, Machine5, 3);
		}
		private void Button10_Click(object sender, RoutedEventArgs e)
		{
			Deposit(Buyer10, Machine10, 4);
		}

		//Возврат денег
		
		private void ButtonCashBack_Click(object sender, RoutedEventArgs e)
		{
			CashBack(Machine10, Buyer10, 4);
		    CashBack(Machine5, Buyer5, 3);
			CashBack(Machine2, Buyer2, 2);
			CashBack(Machine1, Buyer1, 1);

			Sum.Text = $"{sum}";
			Sum.TextAlignment = TextAlignment.Right;
			MessageBox.Show("Заберите Ваши деньги.");
		}
	}
}