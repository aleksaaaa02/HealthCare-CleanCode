﻿using HealthCare.Application;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.View.DoctorView
{
    public partial class PatientInformationView : Window
    {
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public PatientInformationView(Patient patient, bool isEdit)
        {
            InitializeComponent();

            DataContext = new PatientInformationViewModel(patient, isEdit);            
        }
    }
}
