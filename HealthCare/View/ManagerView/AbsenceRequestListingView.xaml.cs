using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.ManagerView
{
    /// <summary>
    /// Interaction logic for AbsenceRequestListingView.xaml
    /// </summary>
    public partial class AbsenceRequestListingView : Window
    {
        public AbsenceRequestListingView()
        {
            InitializeComponent();

            DataContext = new AbsenceRequestListingViewModel(this);
        }
    }
}
