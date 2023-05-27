using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.AppointmentView;
using HealthCare.View.DoctorView;
using HealthCare.View.ManagerView;
using HealthCare.View.PatientView;
using System.ComponentModel;
using System.Windows;

namespace HealthCare
{
    public partial class MainWindow : Window
    {
        private readonly NotificationService _notificationService;
        private readonly LoginService _loginService;
        public MainWindow()
        {
            InitializeComponent();

            ServiceProvider.BuildServices();
            Schedule.Load(Global.appointmentPath);
            
            _notificationService = (NotificationService)ServiceProvider.services["NotificationService"]; 
            _loginService = (LoginService)ServiceProvider.services["LoginService"];
        }

        private void btnQuitApp_Click(object sender, RoutedEventArgs e)
        {
            Schedule.Save(Global.appointmentPath);
            ExitApp();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Password;

            try
            {
                switch (_loginService.Login(username, password))
                {
                    case Role.Manager:
                        new ManagerMenu(this).Show();
                        break;
                    case Role.Doctor:
                        ShowNotifications();
                        new DoctorMainView(this).Show();
                        break;
                    case Role.Nurse:
                        new NurseMenu(this).Show();
                        break;
                    case Role.Patient:
                        ShowNotifications();
                        new AppointmentMainView().Show();
                        break;
                }

                txtUserName.Clear();
                txtPassword.Clear();
                Hide();
            }
            catch (LoginException ex)
            {
                Utility.ShowWarning(ex.Message);
            }
        }

        private void ShowNotifications()
        {
            if (Hospital.Current is null)
                return;

            var notificationService = (NotificationService)ServiceProvider.services["NotificationService"];
            foreach (var notification in notificationService.GetForUser(Hospital.Current.JMBG))
            {
                Utility.ShowInformation(notification.Display());
                _notificationService.Update(notification);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ExitApp();
        }

        public void ExitApp()
        {
            Application.Current.Shutdown();
        }
    }
}
