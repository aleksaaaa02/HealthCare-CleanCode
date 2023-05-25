using HealthCare.Context;
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

namespace HealthCare.View.ManagerView.RenovationView
{
    public partial class SplittingRenovationView : Window
    {
        private readonly Hospital _hospital;
        public SplittingRenovationView(Hospital hospital, int roomId)
        {
            InitializeComponent();
            
            _hospital = hospital;
        }
    }
}
