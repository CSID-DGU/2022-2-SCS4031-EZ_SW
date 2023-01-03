using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Boolean isOld = false;
        private static String soundFileName = "";

        private List<Food> lstFood = new List<Food>()
        {
            new Food(){ category = Category.RAMEN, name = "소유라멘", imagePath = @"/Assets/소유라멘.png" , price = 5000, media = @"/Sound/소유 라멘.mp3"},
            new Food(){ category = Category.RAMEN, name = "미소라멘", imagePath = @"/Assets/미소라멘.png" , price = 4500},
            new Food(){ category = Category.RAMEN, name = "돈카츠라멘", imagePath = @"/Assets/돈카츠라멘.png" , price = 5500},

            new Food(){ category = Category.BUNSIK, name = "떡볶이", imagePath = @"/Assets/떡볶이.png" , price = 3000},
            new Food(){ category = Category.BUNSIK, name = "순대", imagePath = @"/Assets/순대.png" , price = 2000},
            new Food(){ category = Category.BUNSIK, name = "새우튀김", imagePath = @"/Assets/새우튀김.png" , price = 1500},
            new Food(){ category = Category.BUNSIK, name = "우동", imagePath = @"/Assets/우동.png" , price = 2000},

            new Food(){ category = Category.BURGER, name = "와퍼", imagePath = @"/Assets/와퍼.png" , price = 3500 },
            new Food(){ category = Category.BURGER, name = "기네스와퍼", imagePath = @"/Assets/불고기와퍼.png" , price = 4000},
            new Food(){ category = Category.BURGER, name = "치즈와퍼", imagePath = @"/Assets/치즈와퍼.png" , price = 4000},

            new Food(){ category = Category.SNACK, name = "초코쿠키", imagePath = @"/Assets/초코쿠키.png" , price = 1500 },
            new Food(){ category = Category.SNACK, name = "호두파이", imagePath = @"/Assets/호두파이.png" , price = 2500},
            new Food(){ category = Category.SNACK, name = "양파칩", imagePath = @"/Assets/양파칩.png" , price = 1000},

            new Food(){ category = Category.SIDE, name = "콜라", imagePath = @"/Assets/콜라.png" , price = 1200},
            new Food(){ category = Category.SIDE, name = "사이다", imagePath = @"/Assets/사이다.png" , price = 1200},
            new Food(){ category = Category.SIDE, name = "탄산수", imagePath = @"/Assets/탄산수.png" , price = 1000},
            new Food(){ category = Category.SIDE, name = "포카리스웨트", imagePath = @"/Assets/포카리스웨트.png" , price = 2000}
        };
        public MainWindow()
        {
            InitializeComponent();
            //this.Loaded += MainWindow_Loaded;
            lbCategory.SelectedIndex = 0; //처음 실행 시 첫번째 카테고리가 선택되도록 
            // lbMenus.SelectedIndex = 0;
            Keyboard.Focus((ListBoxItem)lbCategory.FindName("burger"));
            Dispatcher.BeginInvoke((Action)(() => lbCategory.UpdateLayout()));


            modalbtn1.Visibility = Visibility.Hidden;
            leftbtn.Visibility = Visibility.Hidden;
            rightbtn.Visibility = Visibility.Hidden;
            tabbtn.Visibility = Visibility.Hidden;
            mediabtn1.Visibility = Visibility.Hidden;
            oldbtn.Visibility = Visibility.Hidden;
            youngbtn.Visibility = Visibility.Hidden;
            shifttabbtn.Visibility = Visibility.Hidden;
            addbtn.Visibility = Visibility.Hidden;
            modal();
        }

        // press default tab key
        /*
        private void pressTab()
        {
            var key = Key.Tab;                    // Key to send
            var target = Keyboard.FocusedElement;    // Target element
            var routedEvent = Keyboard.KeyDownEvent; // Event to send
            target.RaiseEvent(
                new KeyEventArgs(Keyboard.PrimaryDevice, PresentationSource.FromVisual((Visual)target), 0, key) { RoutedEvent = routedEvent });

        }
        */

        // 음악 재생
        private void Button_click(Object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer(@"C:\Users\dbehd\Desktop\kisokProject\Kiosk\Sound\" + soundFileName +".wav");
            player.Load();
            player.Play();
        }


        // count 변수 추가 메서드
        private void ImgPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Food food = findFoodByList();

                AddSelectedMenu(food);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("선택된 아이템이 없습니다.");
            }
            finally
            {
                SetLvOrderItem();
            }
        }

        private void ImgMinus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Food food = findFoodByList();


                if (Cart.GetInstance().Find(x => x.name == food.name).count > 1)
                {
                    Cart.GetInstance().Find(x => x.name == food.name).count--;
                }

                else if (Cart.GetInstance().Find(x => x.name == food.name).count == 1)
                {
                    int i = Cart.GetInstance().FindIndex(x => x.name == food.name);
                    Cart.GetInstance().RemoveAt(i);
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("음식을 선택해주세요.");
            }
            finally
            {
                SetLvOrderItem();
            }

        }


        // 버튼 포커싱 색 바꾸기
        private void OnGotFocusHandler(object sender, RoutedEventArgs e)
        {
            Button tb = e.Source as Button;
            tb.Background = Brushes.Blue;
        }
        private void OnLostFocusHandler(object sender, RoutedEventArgs e)
        {
            Button tb = e.Source as Button;     
            tb.Background = Brushes.Gray;
        }


        // 모달 동기 처리 -> 해당 속성을 버튼에 등록.
        async void Window_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Window w = new Window1();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(Window w)
        {
            await Task.Delay(3000);
            w.Close();
        }

        // 모달 창 띄우기 코드 -> c# 코드로 버튼 클릭해서 띄운다.
        private void modal()
        {
            modalbtn1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
        // f3 키 눌러서 팝업 창 띄우기
        private void PopupKeyPress(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                modal();
            }
            if(e.Key == Key.Left)
            {
                leftbtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Right)
            {
                rightbtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            
            if (e.Key == Key.W)
            {
                tabbtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Q)
            {
                shifttabbtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

        }
        async void leftClick(object sender, RoutedEventArgs e)
        {
            LeftWinodw w = new LeftWinodw();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(LeftWinodw w)
        {
            await Task.Delay(2000);
            w.Close();
        }
        async void rightClick(object sender, RoutedEventArgs e)
        {
            RightWindow w = new RightWindow();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(RightWindow w)
        {
            await Task.Delay(2000);
            w.Close();
        }
        async void tabClick(object sender, RoutedEventArgs e)
        {
            TabWindow w = new TabWindow();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(TabWindow w)
        {
            await Task.Delay(2000);
            w.Close();
        }
        async void shiftTabClick(object sender, RoutedEventArgs e)
        {
            ShiftTabWindow w = new ShiftTabWindow();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(ShiftTabWindow w)
        {
            await Task.Delay(2000);
            w.Close();
        }



        // 노인, 일반 모드
        async void oldClick(object sender, RoutedEventArgs e)
        {
            OldWindow w = new OldWindow();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(OldWindow w)
        {
            await Task.Delay(1000);
            w.Close();
        }
        async void youngClick(object sender, RoutedEventArgs e)
        {
            YoungWindow w = new YoungWindow();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(YoungWindow w)
        {
            await Task.Delay(1000);
            w.Close();
        }

        async void addClick(object sender, RoutedEventArgs e)
        {
            AddWindow w = new AddWindow();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(AddWindow w)
        {
            await Task.Delay(1000);
            w.Close();
        }

        // 결제 창
        async void payClick(object sender, RoutedEventArgs e)
        {
            PayWindow w = new PayWindow();
            Task work = Go(w);
            w.ShowDialog();
            await work; // exceptions in unawaited task are difficult to handle, so let us await it here.
        }
        async Task Go(PayWindow w)
        {
            await Task.Delay(4000);
            w.Close();
        }




        // 노인모드 키 버튼 이벤트.
        private void changeMode(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                if (isOld == true)
                {
                    youngbtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    //MessageBox.Show("일반모드입니다!");
                    isOld = false;
                    this.Resources["MyFillBrush"] = new SolidColorBrush(Color.FromRgb
                      (255, 0, 0));
                    return;
                    
                }
                else
                {
                    oldbtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    //MessageBox.Show("노인모드입니다!");
                    isOld = true;
                    this.Resources["MyFillBrush"] = new SolidColorBrush(Color.FromArgb(
                      0xFF,
                      0, 0, 255));
                }
            }
        }


        

        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategory.SelectedIndex == -1) return;

            Category category = (Category)lbCategory.SelectedIndex;
            lbMenus.ItemsSource = lstFood.Where(x => x.category == category).ToList();
            lbMenus.SelectedIndex = 0;

        }


        private void StackPanel_MouseDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                MessageBox.Show("이벤트 작성");
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                
            }
        }

        Cart cart = new Cart();
        private void enterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                /*
                lbMenus.SelectedItem.ToString();
                
                if(Cart.GetInstance().Count == 0)
                {
                    Cart.GetInstance().Add((new Food() { category = Category.RAMEN, name = "미소라멘", imagePath = @"/Assets/미소라멘.png", price = 4000 }));
                    cart.TotalPrice += 4000;
                    
                } else if (Cart.GetInstance().Count == 1)
                {
                    Cart.GetInstance().Add((new Food() { category = Category.BUNSIK, name = "새우튀김", imagePath = @"/Assets/새우튀김.png", price = 2000 }));
                    cart.TotalPrice += 2000;
                } else
                {
                    Cart.GetInstance().Add((new Food() { category = Category.SIDE, name = "사이다", imagePath = @"/Assets/사이다.png", price = 1000 }));
                    cart.TotalPrice += 1000;
                }

               
                MessageBox.Show("상품 추가 완료!");
                */
                try
                {
                    addbtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    Food food = findFoodByList();

                    AddSelectedMenu(food);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("선택된 아이템이 없습니다.");
                }
                finally
                {
                    SetLvOrderItem();
                }



                /*
                Food food = lbMenus.SelectedItem as Food;

                AddSelectedMenu(food);

                SetLvOrderItem();

                MessageBox.Show("상품 추가 완료!");
                */
            }
        }


        public void SetLvOrderItem()
        {
            lvOrder.ItemsSource = null;
            tbTotalPrice.Text = "0";

            tbTotalPrice.Text = getTotalPrice().ToString();
            lvOrder.ItemsSource = Cart.GetInstance();
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

        private void AddSelectedMenu(Food food)
        {
            if (food == null){
                MessageBox.Show("메뉴를 선택해주세요");
                return;
            }
            if (Cart.GetInstance().Exists(x => x.name == food.name))
            {
                soundFileName = food.name;
                mediabtn1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                Cart.GetInstance().Find(x => x.name == food.name).count++;
            }
            else
            {
                Food orderFood = new Food();
                orderFood.name = food.name;
                soundFileName = food.name;
                mediabtn1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                orderFood.imagePath = food.imagePath;
                orderFood.price = food.price;
                orderFood.category = food.category;
                orderFood.count = 1;
                Cart.GetInstance().Add(orderFood);
            }
            //orderLogData는 결제 할때 orderFood, count를 가져오는 식으로 만들기
            tbTotalPrice.Text = getTotalPrice().ToString();
        }

        private void LvFood_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Food food = lbMenus.SelectedItem as Food;
            
            AddSelectedMenu(food);
            
            SetLvOrderItem();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("정말 테이블의 주문 내용을 전부 삭제하시겠습니까?", "EZ_SW", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Cart.GetInstance().Clear();

                SetLvOrderItem();

                AutoClosingMessageBox.Show("삭제되었습니다.", "EZ_SW", 1200);
            }
            else
            {
                AutoClosingMessageBox.Show("취소되었습니다.", "EZ_SW", 1200);
            }
        }

        private void btnCancelByMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Food food = findFoodByList();

                int i = Cart.GetInstance().FindIndex(x => x.name == food.name);
                Cart.GetInstance().RemoveAt(i);
            }

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("음식을 선택하지 않았습니다", "EZ_SW");
            }
            finally
            {
                SetLvOrderItem();
            }
        }

        private Food findFoodByList()
        {
            Food food = lvOrder.SelectedItem as Food;

            if (food == null)
            {
                food = lbMenus.SelectedItem as Food;

            }

            return food;
        }










    }
}
