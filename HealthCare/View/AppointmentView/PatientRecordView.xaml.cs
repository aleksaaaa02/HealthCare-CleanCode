using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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

namespace HealthCare.View.AppointmentView
{
    /// <summary>
    /// Interaction logic for PatientRecordView.xaml
    /// </summary>
    public partial class PatientRecordView : Window
    {
        PatientRecordViewModel model;
        public PatientRecordView(Hospital hospital)
        {
            model = new PatientRecordViewModel(hospital);
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
            Appointment appointment = (Appointment)listViewRecord.SelectedItem;

            if (listViewRecord.SelectedItems.Count == 1)
            {
                model.ShowAnamnesis(appointment);
            }


        }
    }
}
