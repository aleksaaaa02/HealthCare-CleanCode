using HealthCare.Context;
using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class InventoryItemListingView : Window
    {
        private InventoryItemListingVIewModel _model;
        private Window _loginWindow;
        public InventoryItemListingView(Window loginWindow, Hospital hospital)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
            _model = new InventoryItemListingVIewModel(hospital);
            DataContext = _model;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            cbExaminationalRT.IsChecked = false;
            cbOperationalRT.IsChecked = false;
            cbPatientCareRT.IsChecked = false;
            cbReceptionRT.IsChecked = false;
            cbWarehouseRT.IsChecked = false;

            cbExaminationalET.IsChecked = false;
            cbOperationalET.IsChecked = false;
            cbFurnitureET.IsChecked = false;
            cbHallwayET.IsChecked = false;

            rbNone.IsChecked = false;
            rbLittle.IsChecked = false;
            rbLot.IsChecked = false;

            searchBar.Text = string.Empty;

            _model.LoadAll();
        }
        /*
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Application.Current.Shutdown();
        }
        */
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            _loginWindow.Show();
            Close();
        }

        private void FilterView(object sender, EventArgs e)
        {
            _model.Filter(
                searchBar.Text.ToLower().Trim(),
                new bool[] { _checked(rbNone), _checked(rbLittle), _checked(rbLot) },
                new bool[] { _checked(cbExaminationalET), _checked(cbOperationalET),
                            _checked(cbFurnitureET), _checked(cbHallwayET) },
                new bool[] { _checked(cbExaminationalRT), _checked(cbOperationalRT),
                            _checked(cbPatientCareRT), _checked(cbReceptionRT),
                            _checked(cbWarehouseRT) }
            );
        }

        private bool _checked(ToggleButton button)
        {
            return button.IsChecked is bool b && b;
        }
    }
}
