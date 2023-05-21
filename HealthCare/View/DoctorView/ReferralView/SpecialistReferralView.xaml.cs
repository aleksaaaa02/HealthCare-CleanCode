﻿using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Referrals;
using System.Windows;

namespace HealthCare.View.DoctorView.ReferralView
{
    public partial class SpecialistReferralView : Window
    {
        public SpecialistReferralView(Hospital hospital, Patient patient)
        {
            InitializeComponent();
            DataContext = new SpecialistReferralViewModel(hospital, patient);
        }
    }
}
