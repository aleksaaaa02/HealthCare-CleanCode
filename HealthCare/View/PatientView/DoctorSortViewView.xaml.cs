using System.Windows.Controls;
using HealthCare.Core.Users.Model;
using HealthCare.ViewModel.PatientViewModell;

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for DoctorSortView.xaml
    /// </summary>
    public partial class DoctorSortView : UserControl
    {
        PatientMainWindow _mainWindow;
        DoctorSortViewModel model;

        public DoctorSortView(PatientMainWindow mainWindow)
        {
            model = new DoctorSortViewModel();
            DataContext = model;
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            model.Filter(tbFilter.Text);
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.Sort(cbSort.SelectedValue.ToString());
        }

        private void listViewRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewRecord.SelectedItems.Count == 1)
            {
                Doctor doctor = (Doctor)listViewRecord.SelectedItem;
                AppointmentMainView appointmentMainView = new AppointmentMainView(doctor);
                _mainWindow.mainContentGrid.Content = appointmentMainView;
            }
        }
    }
}