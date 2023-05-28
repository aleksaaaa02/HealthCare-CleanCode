using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.AppointmentView;
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

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for DoctorSortView.xaml
    /// </summary>
    public partial class DoctorSortView : UserControl
    {
        Hospital _hospital;
        DoctorSortViewModel model;
        PatientMainWindow _mainWindow;
        public DoctorSortView(PatientMainWindow mainWindow, Hospital hospital)
        {
            model = new DoctorSortViewModel(hospital);
            DataContext = model;
            _hospital = hospital;
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
                AppointmentMainView appointmentMainView = new AppointmentMainView(_hospital,doctor);
                _mainWindow.mainContentGrid.Content = appointmentMainView;

            }           
        }
    }
}
