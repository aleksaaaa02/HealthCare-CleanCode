using System.Windows;
using System.Windows.Controls;
using HealthCare.Application;

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

        private void btnHospitalSurvey_Click(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new SurveyHospitalView();
        }

        private void btnDoctorSurvey_Click(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new SurveyDoctorView();
        }
    }
}