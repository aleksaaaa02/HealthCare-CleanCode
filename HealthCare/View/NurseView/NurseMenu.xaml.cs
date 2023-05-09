﻿using HealthCare.Context;
using HealthCare.View.ReceptionView;
using HealthCare.View.UrgentAppointmentView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for NurseMenu.xaml
    /// </summary>
    public partial class NurseMenu : Window
    {
        private readonly Hospital hospital;
        private MainWindow window;
        public NurseMenu(MainWindow window,Hospital hospital)
        {
            this.window = window;
            this.hospital = hospital;
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            window.Show();
        }

        private void mnuCRUD_Click(object sender, RoutedEventArgs e)
        {
            new NurseMainView(hospital).ShowDialog();
        }

        private void mnuReception_Click(object sender, RoutedEventArgs e)
        {
            new MainReceptionView(hospital).ShowDialog();
        }

        private void mnuUrgent_Click(object sender, RoutedEventArgs e)
        {
            new UrgentView(hospital).ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            window.ExitApp();
        }
    }
}
