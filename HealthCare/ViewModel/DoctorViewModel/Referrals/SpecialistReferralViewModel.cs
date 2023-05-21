using HealthCare.Context;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals
{
    public class SpecialistReferralViewModel : ViewModelBase
    {

        private readonly Hospital _hospital;
        private ObservableCollection<DoctorsViewModel> _doctors;



        public SpecialistReferralViewModel()
        {

        }

        public void Update()
        {
            _doctors.Clear();
            foreach (var doctor in _hospital.DoctorService.GetAll())
            {
                _doctors.Add(new DoctorsViewModel(doctor));
            }

        }
    }
}
