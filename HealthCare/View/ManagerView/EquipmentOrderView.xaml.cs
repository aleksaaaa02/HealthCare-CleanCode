using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.ManagerViewModel;
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

namespace HealthCare.View.ManagerView
{
    /// <summary>
    /// Interaction logic for EquipmentOrderView.xaml
    /// </summary>
    public partial class EquipmentOrderView : Window
    {
        private DynamicEquipmentListingViewModel _model;
        private readonly Hospital _hospital;
        private Window _loginWindow;
        public EquipmentOrderView(Window loginWindow, Hospital hospital)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
            _hospital = hospital;
            _model = new DynamicEquipmentListingViewModel(hospital);
            DataContext = _model;
        }

        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            _model.LoadAll();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            _loginWindow.Show();
            Close();
        }

        private void Button_Order(object sender, RoutedEventArgs e)
        {
            bool madeOrders = false;
            foreach (var item in _model.Items)
            {
                int quantity;
                if (int.TryParse(item.OrderQuantity, out quantity) && quantity > 0)
                {
                    _makeOrder(item.EquipmentName, quantity);
                    madeOrders = true;
                }
            }
            if (!madeOrders)
                MessageBox.Show("Nema unetih porudžbina.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            else MessageBox.Show("Poručivanje uspešno.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            _model.LoadAll();
        }

        private void _makeOrder(string equipmentName, int quantity)
        {
            int id = _hospital.OrderService.NextId();
            DateTime scheduled = DateTime.Now + new TimeSpan(24, 0, 0);
            _hospital.OrderService.Add(new OrderItem(id, equipmentName, quantity, scheduled));
        }

        private void ValidateTextBox(object sender, TextChangedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox is null) return;

            if (textBox.Text != "" && !int.TryParse(textBox.Text, out _))
            {
                TextChange textChange = e.Changes.ElementAt(0);
                textBox.Text = textBox.Text.Remove(textChange.Offset, textChange.AddedLength);
            }
            else _highlightRows();
        }

        private void _highlightRows()
        {
            foreach (var item in lvDynamicEquipment.Items)
            {
                var row = (ListViewItem) lvDynamicEquipment.ItemContainerGenerator.ContainerFromItem(item);
                TextBox? tb = ViewUtility.FindChild<TextBox>(row, "tbQuantity");
                if (tb is not null && tb.Text.Trim() != "")
                    row.Background = ViewGlobal.CHIGH2;
                else
                    row.Background = ViewGlobal.CNEUT;
            }
        }
    }
}
