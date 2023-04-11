
﻿using HealthCare.View.AppointmentView;

﻿using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;

using HealthCare.View.DoctorView;
using HealthCare.View.ManagerView;
using HealthCare.View.PatientView;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            _hospital = new Hospital("Poslednji trzaj");
            _hospital.LoadAll();
        }

        private void btnQuitApp_Click(object sender, RoutedEventArgs e)
        {
            _hospital.SaveAll();
            Close();
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
                        new ManagerMainView(this, _hospital).Show();
                        break;
                    case UserRole.Doctor:
                        new DoctorMainView(_hospital, this).Show();
                        break;
                    case UserRole.Nurse:
                        new NurseMainView(this).Show();
                        break;
                    case UserRole.Patient:
                        AppointmentMainView appointmentMainView = new AppointmentMainView(_hospital);
                        appointmentMainView.Show();
                        break;
                }

                txtUserName.Clear();
                txtPassword.Clear();
                Hide();
            } catch (IncorrectPasswordException e1) {
                MessageBox.Show("Pogresna lozinka. Pokusajte ponovo.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            } catch (UsernameNotFoundException e2) {
                MessageBox.Show("Nepostojece korisnicko ime.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
