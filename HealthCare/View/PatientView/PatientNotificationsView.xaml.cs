using HealthCare;
using HealthCare.Application;
using HealthCare.Application.Common;
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
    /// Interaction logic for PatientNotificationsView.xaml
    /// </summary>
    public partial class PatientNotificationsView : UserControl
    {
        List<UserNotification> notifications;
        List<Prescription> prescriptions;
        private readonly PrescriptionService _prescriptionService;
        private readonly MedicationService _medicationService;
        private readonly PatientService _patientService;
        private readonly UserNotificationService _userNotificationService;
        public PatientNotificationsView()
        {
            _prescriptionService = Injector.GetService<PrescriptionService>(Injector.REGULAR_PRESCRIPTION_S);
            _medicationService = Injector.GetService<MedicationService>();
            _patientService = Injector.GetService<PatientService>();
            _userNotificationService = Injector.GetService<UserNotificationService>();
            InitializeComponent();
            LoadNotifications();
            //DataContext = new PatientNotificationsViewModel(hospital);
            
        }

        public void LoadNotifications()
        {
            NotificationsPanel.Children.Clear();
            DateTime currentTime = DateTime.Now;
            Patient patient = (Patient)Context.Current;
            int notificationHoursThreshold = patient.NotificationHours;
            notifications = _userNotificationService.GetForUser(Context.Current.JMBG);
            prescriptions = _prescriptionService.GetPatientsPrescriptions(patient.JMBG);
            foreach(Prescription prescription in prescriptions)
            {
                foreach (DateTime pillDateTime in prescription.GetPillConsumptionTimes())
                {
                    if(pillDateTime > currentTime)
                    {
                        Medication medication = _medicationService.Get(prescription.MedicationId);
                        string notificationMessage = "Lek: " + medication.Name + "\n"
                                                   + "Instrukcije: " + prescription.Instruction + "\n"
                                                   + "Vreme uzimanja leka: " + pillDateTime.ToString();
                        notifications.Add(new UserNotification(patient.JMBG, pillDateTime, "Popijte tabletu", notificationMessage, false));
                    }
                }
            }

            
            notifications = notifications.OrderBy(x => x.receiveTime).ToList();
            foreach (UserNotification userNotification in notifications)
            {
                DateTime notificationReceiveTime = userNotification.receiveTime;
                if (notificationReceiveTime > currentTime)
                {
                    TimeSpan timeDifference = notificationReceiveTime - currentTime;

                    if (timeDifference.TotalHours < notificationHoursThreshold)
                    {
                        NotificationControl notificationControl = new NotificationControl();
                        notificationControl.NotificationCaption.Text = userNotification.caption;
                        notificationControl.NotificationText.Text = userNotification.text;
                        if (userNotification.isCustom)
                        {
                            SolidColorBrush brush = new SolidColorBrush(Colors.IndianRed);
                            notificationControl.Header.Background = brush;
                        }
                        notificationControl.TxtHoursLeft.Text = ((int)timeDifference.TotalHours).ToString() + " sati preostalo";
                        NotificationsPanel.LastChildFill = true;
                        DockPanel.SetDock(notificationControl, Dock.Top);
                        NotificationsPanel.Children.Add(notificationControl);
                    }
                }
            }
        }

        private void expandButton_Click(object sender, RoutedEventArgs e)
        {
            new NotificationCreationView(this).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hoursText = hoursTextBox.Text;
            if (int.TryParse(hoursText, out int notificationHours))
            {
                if (notificationHours <= 0)
                {
                    ViewUtil.ShowWarning("Broj sati mora biti pozitivan broj!");
                    return;
                }
                else
                {
                    Patient patient = (Patient)Context.Current;
                    patient.NotificationHours = notificationHours;
                    _patientService.Update(patient);
                    LoadNotifications();
                }
            }
            else
            {
                ViewUtil.ShowWarning("Broj sati mora biti broj");
            }
        }
    }
}
