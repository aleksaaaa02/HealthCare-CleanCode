using HealthCare;
using HealthCare.Application;
using HealthCare.Application.Common;
using HealthCare.Model;
using HealthCare.Service;
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
    /// Interaction logic for PatientNotificationsView.xaml
    /// </summary>
    public partial class PatientNotificationsView : UserControl
    {
        
        private readonly PatientService _patientService;
        PatientNotificationsViewModel _viewModel;
        public PatientNotificationsView()
        {
            _patientService = Injector.GetService<PatientService>();
            _viewModel = new PatientNotificationsViewModel();
            _viewModel.LoadNotifications();
            DataContext = _viewModel;
            InitializeComponent();
            
        }

        
        private void expandButton_Click(object sender, RoutedEventArgs e)
        {
            new NotificationCreationView(_viewModel).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hoursText = hoursTextBox.Text;
            if (int.TryParse(hoursText, out int notificationHours))
            {
                if (notificationHours <= 0)
                {
                    ViewUtil.ShowWarning("Broj sati mora biti pozitivan broj!");
                    return;
                }
                else
                {
                    Patient patient = (Patient)Context.Current;
                    patient.NotificationHours = notificationHours;
                    _patientService.Update(patient);
                    _viewModel.LoadNotifications();
                }
            }
            else
            {
                ViewUtil.ShowWarning("Broj sati mora biti broj");
            }
        }
    }
}
