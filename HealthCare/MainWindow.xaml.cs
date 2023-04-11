using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.DoctorView;
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

            
            string UserName = txtUserName.Text;
            string Password = txtPassword.Password;
            // SKLONI OVO!!
            if (UserName != null && Password != null)
            {
                if (UserName == "mamatvoja123" && Password == "mrs")
                {
                    WelcomeMessage.Text = "sara jo";
                   //FIX THIS


                }
            }

            string username = txtUserName.Text;
            string password = txtPassword.Password;


            try
            {
                switch(_hospital.LoginRole(username, password))
                {
                    case UserRole.Manager:
                        // new View
                        break;
                    case UserRole.Doctor:
                        // new View
                        break;
                    case UserRole.Nurse:
                        // new View
                        break;
                    case UserRole.Patient:
                        // new View
                        break;
                }
            } catch (IncorrectPasswordException e1) {

            } catch (UsernameNotFoundException e2) {

            }
        }
    }
}
