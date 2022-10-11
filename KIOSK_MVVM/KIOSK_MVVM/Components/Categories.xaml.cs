using KIOSK_MVVM.Views;
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

namespace KIOSK_MVVM.Components
{
	/// <summary>
	/// Interaction logic for Categories.xaml
	/// </summary>
	public partial class Categories : UserControl
	{
		public Categories()
		{
			InitializeComponent();
		}

		private void LamenButton_Click(object sender, RoutedEventArgs e)
		{
			Lamens lamens = new Lamens();
			Grid buttons = (Grid)this.Parent;
			KioskMainView mainView = (KioskMainView)buttons.Parent;
			Grid items = (Grid)mainView.FindName("items");
			items.Children.Clear();
			items.Children.Add(lamens);

		}

		private void BeverageButton_Click(object sender, RoutedEventArgs e)
		{
			Beverages beverages = new Beverages();
			Grid buttons = (Grid)this.Parent;
			KioskMainView mainView = (KioskMainView)buttons.Parent;
			Grid items = (Grid)mainView.FindName("items");
			items.Children.Clear();
			items.Children.Add(beverages);

		}

		private void SideMenuButton_Click(object sender, RoutedEventArgs e)
		{
			SideMenus sides = new SideMenus();
			Grid buttons = (Grid)this.Parent;
			KioskMainView mainView = (KioskMainView)buttons.Parent;
			Grid items = (Grid)mainView.FindName("items");
			items.Children.Clear();
			items.Children.Add(sides);
		}

		private void BurgerButton_Click(object sender, RoutedEventArgs e)
		{
			Burgers burgers = new Burgers();
			Grid buttons = (Grid)this.Parent;
			KioskMainView mainView = (KioskMainView)buttons.Parent;
			Grid items = (Grid)mainView.FindName("items");
			items.Children.Clear();
			items.Children.Add(burgers);
		}

		private void RiceButton_Click(object sender, RoutedEventArgs e)
		{
			Rices rices = new Rices();
			Grid buttons = (Grid)this.Parent;
			KioskMainView mainView = (KioskMainView)buttons.Parent;
			Grid items = (Grid)mainView.FindName("items");
			items.Children.Clear();
			items.Children.Add(rices);
		}
	}
}
