using HealthCare;
using HealthCare.Application;
using HealthCare.Service;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        MainWindow _mainWindow;
        public PatientMainWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            this.labelUsername.Text = Context.Current.Name;
            btnCrud.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            btnCrud.Focus();
            
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            mainContentGrid.Content = new AppointmentMainView();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new PriorityAppointmentView();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new PatientRecordView();
        }

        private void btnDoctors_Click(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new DoctorSortView(this);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
            _mainWindow.Show();
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new PatientNotificationsView();
        }

        private void SetWindow(UserControl control)
        {
            mainContentGrid.Content = control;
        }
    }
}
