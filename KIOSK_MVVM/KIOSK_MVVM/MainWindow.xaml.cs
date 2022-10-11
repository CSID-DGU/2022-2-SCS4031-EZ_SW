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

namespace KIOSK_MVVM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			KioskMainView kioskMainView = new KioskMainView();
			Lamens lamens = new Lamens();
			kioskMainView.InitializeComponent();
			Grid items = (Grid)kioskMainView.FindName("items");
			items.Children.Add(lamens);
		}
	}
}
