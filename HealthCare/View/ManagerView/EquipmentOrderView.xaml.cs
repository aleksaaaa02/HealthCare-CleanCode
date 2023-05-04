using HealthCare.Context;
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
        private Window _loginWindow;
        public EquipmentOrderView(Window loginWindow, Hospital hospital)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
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

        }

        private void ValidateTextBox(object sender, TextChangedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox == null) return;

            if (textBox.Text != "" && !int.TryParse(textBox.Text, out _))
            {
                TextChange textChange = e.Changes.ElementAt(0);
                int iAddedLength = textChange.AddedLength;
                int iOffset = textChange.Offset;
                textBox.Text = textBox.Text.Remove(iOffset, iAddedLength);
            }
            else
            {
                _highlightRows();
            }
        }

        private void _highlightRows()
        {
            foreach (var item in lvDynamicEquipment.Items)
            {
                var container = (ListViewItem) lvDynamicEquipment.ItemContainerGenerator.ContainerFromItem(item);
                if (container != null)
                {
                    var tb = ViewUtility.FindChild<TextBox>(container, "tbQuantity");
                    if (tb is TextBox t && t.Text.Trim() != "")
                        container.Background = Brushes.Yellow;
                    else
                        container.Background = Brushes.White;
                }
            }
        }
    }
}
