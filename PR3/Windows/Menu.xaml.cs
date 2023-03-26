using PR3.Resource.Model;
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
using System.Windows.Shapes;

namespace PR3.Windows
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        Resource.Model.DatabaseEntities db = new Resource.Model.DatabaseEntities();
        bool order = true;
        public List<Service> GetServices(bool order)
        {
            if (!order) return db.Service.OrderBy(x => x.Cost - x.Discount * x.Cost).ToList();
            else return db.Service.OrderByDescending(x => x.Cost - x.Discount * x.Cost).ToList();
        }
        public List<Service> GetServices(double sale, bool order)
        {
            if (!order) return db.Service.Where(x => x.Discount == sale).OrderBy(x => x.Cost - x.Discount * x.Cost).ToList();
            else return db.Service.Where(x => x.Discount == sale).OrderByDescending(x => x.Cost - x.Discount * x.Cost).ToList();
        }
        public Menu()
        {
            InitializeComponent();
            cB.SelectedIndex = 0;
            priceMin.IsChecked = true;
            lV.ItemsSource = GetServices(order);
        }

        private void cB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox l = (ComboBox)sender;
            string name = ((Label)l.SelectedItem).Content.ToString();
            if (name != "все") lV.ItemsSource = GetServices(Convert.ToDouble(name.Replace("скидка ", "").Replace("%", "")) / 100, order);
            else lV.ItemsSource = GetServices(order);
        }

        private void priceMin_Checked(object sender, RoutedEventArgs e)
        {
            order = false;
            string name = cB.Text;
            if (name != "все") lV.ItemsSource = GetServices(Convert.ToDouble(name.Replace("скидка ", "").Replace("%", "")) / 100, order);
            else lV.ItemsSource = GetServices(order);
        }

        private void priceMax_Checked(object sender, RoutedEventArgs e)
        {
            order = true;
            string name = cB.Text;
            if (name != "все") lV.ItemsSource = GetServices(Convert.ToDouble(name.Replace("скидка ", "").Replace("%", "")) / 100, order);
            else lV.ItemsSource = GetServices(order);
        }
    }
}
