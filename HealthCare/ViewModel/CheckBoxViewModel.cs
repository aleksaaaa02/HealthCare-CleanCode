using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel
{
    internal class CheckBoxViewModel : ViewModelBase
    {
        private bool _isChecked;
        public bool IsChecked { 
            get => _isChecked; 
            set {  
                _isChecked = value;
                OnPropertyChanged();
            } 
        }

        public CheckBoxViewModel(bool isChecked)
        {
            IsChecked = isChecked;
        }
    }
}
