using HealthCare.Context;
using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace HealthCare.View.ManagerView
{
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

        private void Button_Reset(object sender, RoutedEventArgs e)
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
