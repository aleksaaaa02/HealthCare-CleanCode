using HealthCare.Application;
using HealthCare.Application.Common;
using HealthCare.Exceptions;
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

            _notificationService = Injector.GetService<NotificationService>();
            _loginService = Injector.GetService<LoginService>();
        }

        private void btnQuitApp_Click(object sender, RoutedEventArgs e)
        {
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
                ViewUtil.ShowWarning(ex.Message);
            }
        }

        private void ShowNotifications()
        {
            foreach (var notification in _notificationService.GetForUser(Context.Current.JMBG))
            {
                ViewUtil.ShowInformation(notification.Display());
                _notificationService.Update(notification);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ExitApp();
        }

        public void ExitApp()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
