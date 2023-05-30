﻿using System.Windows;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Examination;

namespace HealthCare.View.DoctorView
{
    public partial class DoctorExamView : Window
    {
        public DoctorExamView(Appointment appointment)
        {
            InitializeComponent();
            DataContext = new DoctorExamViewModel(this, appointment);
        }
    }
}