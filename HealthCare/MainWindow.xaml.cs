﻿using HealthCare.View.DoctorView;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthCare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnQuitApp_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointmentView makeAppointmentView = new MakeAppointmentView();
            makeAppointmentView.Show();
                
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

        }
    }
}
