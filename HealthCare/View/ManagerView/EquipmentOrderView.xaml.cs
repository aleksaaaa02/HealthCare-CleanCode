using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.ManagerView
{
    public partial class EquipmentOrderView : Window
    {
        private EquipmentOrderViewModel _model;
        private readonly Inventory _inventory;
        private readonly OrderService _orderService;
        private Window _parent;

        public EquipmentOrderView(Window parent, Hospital hospital)
        {
            InitializeComponent();
            _parent = parent;
            _inventory = hospital.Inventory;
            _orderService = hospital.OrderService;

            _model = new EquipmentOrderViewModel(_inventory, hospital);
            DataContext = _model;
        }

        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            _model.Load();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            Close();
        }

        private void Button_Order(object sender, RoutedEventArgs e)
        {
            try {
                _validate();
            } catch (ValidationException ve) {
                Utility.ShowWarning(ve.Message);
                return;
            }

            foreach (var item in _model.Items)
                if (item.IsSelected)
                    _makeOrder(item.EquipmentId, int.Parse(item.OrderQuantity));

            Utility.ShowInformation("Poručivanje uspešno.");
            _model.Load();
        }

        private void _makeOrder(int equipmentId, int quantity)
        {
            DateTime scheduled = DateTime.Now + new TimeSpan(24, 0, 0);
            _orderService.AddWithNewId(
                new OrderItem(equipmentId, quantity, scheduled, false));
        }
        
        private void _validate()
        {
            bool someSelected = false;
            int quantity;
            foreach (var item in _model.Items)
            {
                if (item.IsSelected && int.TryParse(item.OrderQuantity, out quantity) && quantity < 0)
                    throw new ValidationException("Količina mora da bude prirodan broj.");

                someSelected |= item.IsSelected;
            }
            if (!someSelected)
                throw new ValidationException("Nema unetih porudžbina.");
        }

        public void HighlightRows(object sender, EventArgs e)
        {
            foreach (OrderItemViewModel item in lvDynamicEquipment.Items)
            {
                var row = (ListViewItem) lvDynamicEquipment.ItemContainerGenerator.ContainerFromItem(item);
                if (item.IsSelected)
                    row.Background = ViewGlobal.CHIGHROW;
                else
                    row.Background = ViewGlobal.CNEUT;
            }
        }

        private void tbQuantity_Focused(object sender, EventArgs e)
        {
            TextBox? tb = sender as TextBox;
            if (tb is null) return;
            if (tb.Text == "0")
                tb.Text = "";
        }

        private void tbQuantity_Unfocused(object sender, EventArgs e)
        {
            TextBox? tb = sender as TextBox;
            if (tb is null) return;
            if (tb.Text == "")
                tb.Text = "0";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _parent.IsEnabled = true;
            Close();
        }
    }
}
