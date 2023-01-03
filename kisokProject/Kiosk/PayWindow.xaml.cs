using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kiosk
{
    /// <summary>
    /// PayWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PayWindow : Window
    {
        public PayWindow()
        {
            InitializeComponent();
            List<Food> cart = Cart.GetInstance();
            // 데이터 생성
            receipt.ItemsSource = Cart.GetInstance();
            TotalPrice.Text = getTotalPrice().ToString();
        }
        
        private int getTotalPrice()
        {
            int total = 0;
            foreach (Food food in Cart.GetInstance())
            {
                total += food.count * food.price;
            }

            return total;
        }

    }
    
}

