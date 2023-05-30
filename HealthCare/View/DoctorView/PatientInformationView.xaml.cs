﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;

namespace HealthCare.View.DoctorView
{
    public partial class PatientInformationView : Window
    {
        public PatientInformationView(Patient patient, bool isEdit)
        {
            InitializeComponent();

            DataContext = new PatientInformationViewModel(patient, isEdit);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}