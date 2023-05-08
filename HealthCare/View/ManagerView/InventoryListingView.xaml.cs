using HealthCare.Context;
using HealthCare.Service;
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
    public partial class InventoryListingView : Window
    {
        private Window _parent;
        private InventoryListingViewModel _model;

        public InventoryListingView(Window parent, Hospital hospital)
        {
            InitializeComponent();
            _parent = parent;
            _parent.IsEnabled = false;

            _model = new InventoryListingViewModel(hospital);
            DataContext = _model;
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
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

        private void Filter_View(object sender, EventArgs e)
        {
            _model.Filter(
                searchBar.Text.ToLower().Trim(),
                new bool[] { Checked(rbNone), Checked(rbLittle), Checked(rbLot) },
                new bool[] { Checked(cbExaminationalET), Checked(cbOperationalET),
                            Checked(cbFurnitureET), Checked(cbHallwayET) },
                new bool[] { Checked(cbExaminationalRT), Checked(cbOperationalRT),
                            Checked(cbPatientCareRT), Checked(cbReceptionRT),
                            Checked(cbWarehouseRT) }
            );
        }

        private bool Checked(ToggleButton button)
        {
            return button.IsChecked is bool b && b;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _parent.IsEnabled = true;
            Close();
        }
    }
}
