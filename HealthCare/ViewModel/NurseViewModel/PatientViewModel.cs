﻿using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class PatientViewModel
    {
        public ObservableCollection<Patient> Patients { get; set; }
        private PatientService _patientService;

        public PatientViewModel()
        {
            Patients = new ObservableCollection<Patient>();
            _patientService = (PatientService)ServiceProvider.services["PatientService"];
        }

        public void Update()
        {
            Patients.Clear();
            foreach (var patient in _patientService.GetAll())
                Patients.Add(patient);
        }
    }
}
