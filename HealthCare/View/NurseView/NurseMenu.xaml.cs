﻿using System.ComponentModel;
using System.Windows;
using HealthCare.View.NurseView.MedicationView;
using HealthCare.View.NurseView.PatientCRUDView;
using HealthCare.View.NurseView.ReceptionView;
using HealthCare.View.NurseView.ReferralView;
using HealthCare.View.NurseView.UrgentAppointmentView;
using HealthCare.View.NurseView.VisitsView;
using HealthCare.View.PatientView;

namespace HealthCare.View.NurseView
{
    public partial class NurseMenu : Window
    {
        private MainWindow _window;

        public NurseMenu(MainWindow window)
        {
            InitializeComponent();
            _window = window;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _window.Show();
        }

        private void mnuCRUD_Click(object sender, RoutedEventArgs e)
        {
            new NurseMainView().ShowDialog();
        }

        private void mnuReception_Click(object sender, RoutedEventArgs e)
        {
            new MainReceptionView().ShowDialog();
        }

        private void mnuUrgent_Click(object sender, RoutedEventArgs e)
        {
            new UrgentView().ShowDialog();
        }

        private void mnuReferral_Click(object sender, RoutedEventArgs e)
        {
            new AllPatientsView().ShowDialog();
        }

        private void mnuOrder_Click(object sender, RoutedEventArgs e)
        {
            new OrderMedicationView().ShowDialog();
        }

        private void mnuVisit_Click(object sender, RoutedEventArgs e)
        {
            new VisitView().ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ChatApp().Show();
        }
    }
}