﻿using HealthCare.Context;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCare.View.PatientView
{
    class DoctorSortViewModel
    {
        public ObservableCollection<Doctor> Doctors { get; set; }
        public List<Doctor> _doctors;
        public Hospital _hospital;

        public DoctorSortViewModel(Hospital hospital)
        {
            _hospital = hospital;
            Doctors = new ObservableCollection<Doctor>();
            _doctors = hospital.DoctorService.GetAll();
            LoadData(hospital.DoctorService.GetAll());
        }

        public void LoadData(List<Doctor> doctors)
        {
            Doctors.Clear();
            foreach (Doctor doctor in doctors)
            {
                Doctors.Add(doctor);
            }
        }

        public void Filter(string filterProperty)
        {
            IEnumerable<Doctor> query = _doctors.ToList().Where(
             x =>
             x.Name.Contains(filterProperty, StringComparison.OrdinalIgnoreCase) ||
             x.LastName.Contains(filterProperty, StringComparison.OrdinalIgnoreCase) ||
             x.Specialization.Contains(filterProperty, StringComparison.OrdinalIgnoreCase) 
            ).ToList();
            LoadData(query.ToList());
        }

        public void Sort(string sortProperty)
        {
            switch (sortProperty)
            {
                case "Ime":
                    LoadData(Doctors.OrderBy(x => x.Name).ToList());
                    break;
                case "Prezime":
                    LoadData(Doctors.OrderBy(x => x.LastName).ToList());
                    break;
                case "Specijalizacija":
                    LoadData(Doctors.OrderBy(x => x.Specialization).ToList());
                    break;
                case "Prosecna ocena":
                    
                    LoadData(Doctors.OrderBy(x => x.Rating).ToList());
                    break;
                default:
                    MessageBox.Show("How Did We Get Here?", "Achievement Unlocked");
                    break;
            }
        }
    }
}
