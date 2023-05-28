using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.RenovationService;
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
    public partial class JoiningRenovationView : Window
    {
        private readonly Hospital _hospital;
        private readonly RoomService _roomService;
        private readonly JoiningRenovationService _joiningRenovationService;

        public JoiningRenovationView(Hospital hospital, List<int> roomIds)
        {
            InitializeComponent();

            _hospital = hospital;
            _roomService = hospital.RoomService;
            _joiningRenovationService = new Hospital().JoiningRenovationService;
        }

        private void PopulateComboBox()
        {
            cbType.ItemsSource = Enum.GetValues(typeof(RoomType));
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Renovate(object sender, RoutedEventArgs e)
        {

        }
    }
}
