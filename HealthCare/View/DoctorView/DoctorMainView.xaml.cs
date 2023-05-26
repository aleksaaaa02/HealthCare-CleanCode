﻿using HealthCare.Context;
using HealthCare.ViewModels.DoctorViewModel;
using System.ComponentModel;
using System.Windows;

namespace HealthCare.View.DoctorView
{
    public partial class DoctorMainView : Window
    {
        private MainWindow _loginWindow;

        public DoctorMainView(MainWindow loginWindow)
        {
            _loginWindow = loginWindow;
            InitializeComponent();
            DataContext = new DoctorMainViewModel(this);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {            
            _loginWindow.Show();
        }
    }
}
