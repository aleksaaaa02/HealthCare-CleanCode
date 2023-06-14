using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.UserService;

namespace HealthCare.ViewModel.PatientViewModell
{
    class DoctorSortViewModel
    {
        private readonly DoctorService _doctorService;
        public List<Doctor> _doctors;

        public DoctorSortViewModel()
        {
            Doctors = new ObservableCollection<Doctor>();
            _doctorService = Injector.GetService<DoctorService>();
            _doctors = _doctorService.GetAll();
            LoadData(_doctors);
        }

        public ObservableCollection<Doctor> Doctors { get; set; }

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