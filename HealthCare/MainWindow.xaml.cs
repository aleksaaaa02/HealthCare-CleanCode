
﻿using HealthCare.View.AppointmentView;

﻿using HealthCare.Context;
using HealthCare.Exceptions;

using HealthCare.View.DoctorView;
using HealthCare.View.ManagerView;
using HealthCare.View.PatientView;
using System.Windows;
using HealthCare.View.ReceptionView;
using HealthCare.View.UrgentAppointmentView;

namespace HealthCare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Hospital _hospital;
        public MainWindow()
        {
            InitializeComponent();

            _hospital = new Hospital("Venac");
            _hospital.LoadAll();
        }

        private void btnQuitApp_Click(object sender, RoutedEventArgs e)
        {
            new UrgentView(_hospital).ShowDialog();
           //_hospital.SaveAll();
           //Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Password;

            try
            {
                switch(_hospital.LoginRole(username, password))
                {
                    case UserRole.Manager:
                        new EquipmentOrderView(this, _hospital).Show();
                        break;
                    case UserRole.Doctor:
                        new DoctorMainView(this, _hospital).Show();
                        break;
                    case UserRole.Nurse:
                        new NurseMenu(this, _hospital).Show();
                        break;
                    case UserRole.Patient:
                        AppointmentMainView appointmentMainView = new AppointmentMainView(_hospital);
                        appointmentMainView.Show();
                        break;
                }

                txtUserName.Clear();
                txtPassword.Clear();
                Hide();
            } catch (IncorrectPasswordException _) {
                MessageBox.Show("Pogresna lozinka. Pokusajte ponovo.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            } catch (UsernameNotFoundException _) {
                MessageBox.Show("Nepostojece korisnicko ime.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
