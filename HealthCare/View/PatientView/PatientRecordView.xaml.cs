using HealthCare.Model;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using HealthCare.ViewModel.PatientViewModell;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.AppointmentView
{
    public partial class PatientRecordView : UserControl
    {
        PatientRecordViewModel model;
        public PatientRecordView()
        {
            model = new PatientRecordViewModel();
            DataContext = model;
            InitializeComponent();           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.Sort(cbSort.SelectedValue.ToString());
        }

        private void TbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            model.Filter(tbFilter.Text);
        }

        private void ListViewRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var appointment = (AppointmentViewModel)listViewRecord.SelectedItem;

            if (listViewRecord.SelectedItems.Count == 1)
            {
                model.ShowAnamnesis(appointment.Appointment);
            }
        }
    }
}
