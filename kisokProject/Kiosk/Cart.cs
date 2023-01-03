using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk
{
    public class Cart : INotifyPropertyChanged
    {
        public static Cart cart = new Cart();

        public static Cart getCart()
        {
            return cart;
        }

        private static List<Food> foods;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public static List<Food> GetInstance()
        {
            if (foods == null)
                foods = new List<Food>();

            return foods;
        }
        
        public int _totalPrice;

        public int TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                this._totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }
        
        // public int totalPrice { get; set; }

        
    }
}
