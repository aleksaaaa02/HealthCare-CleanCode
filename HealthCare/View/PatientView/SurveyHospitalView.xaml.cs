using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for SurveyHospitalView.xaml
    /// </summary>
    public partial class SurveyHospitalView : Window
    {
        public ObservableCollection<Survey> SurveySections { get; set; }


        public SurveyHospitalView()
        {
            InitializeComponent();
            SurveySections = new ObservableCollection<Survey>();

            SurveySections.Add(new Survey
            {
                Description = "a",
                SelectedRating = 1,
            });

            SurveySections.Add(new Survey
            {
                Description = "b",
                SelectedRating = 2,
            });

            SurveySections.Add(new Survey
            {
                Description = "c",
                SelectedRating = 3,
            });

            DataContext = this;
        }
    }
}
