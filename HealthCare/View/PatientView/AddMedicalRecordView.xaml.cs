using HealthCare.Model;
using HealthCare.Service;
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

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for AddMedicalRecordView.xaml
    /// </summary>
    public partial class AddMedicalRecordView : Window
    {
        private PatientService? patientService;
        private NurseMainView _window;
        public AddMedicalRecordView(NurseMainView window, PatientService patientService)
        {
            _window = window;
            InitializeComponent();
            if (_window.Record != null)
            {
                tbHeight.Text = _window.Record.Height.ToString();
                tbWidth.Text = _window.Record.Weight.ToString();
                rtbMedicalHistory.AppendText(string.Join(",", _window.Record.MedicalHistory));
            }
            else {
                _window.Record = new MedicalRecord();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                MedicalRecord medicalRecord = new MedicalRecord();
                medicalRecord.Height = float.Parse(tbHeight.Text);
                medicalRecord.Weight = float.Parse(tbWidth.Text);
                TextRange textRange = new TextRange(
                    rtbMedicalHistory.Document.ContentStart,
                    rtbMedicalHistory.Document.ContentEnd
                );
                medicalRecord.MedicalHistory = textRange.Text.Trim().Split(",");
                _window.Record = medicalRecord;
                Close();
            }
            else
                ShowErrorMessageBox();
           
        }

        public bool ValidateFields()
        {
            float parsed;
            if (float.TryParse(tbHeight.Text, out parsed) && float.TryParse(tbWidth.Text, out parsed))
                return true;
            return false;
        }

        public void ShowErrorMessageBox()
        {
            string messageBoxText = "Visina i tezina moraju biti brojevi";
            string content = "Greska";
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(messageBoxText, content, button, icon);
        }
    }
}
