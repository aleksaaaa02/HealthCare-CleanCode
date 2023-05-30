using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthCare.View.PatientView
{
    public class PatientNotificationsViewModel : INotifyPropertyChanged
    {
        private readonly UserNotificationService _userNotificationService;
        private readonly PrescriptionService _prescriptionService;
        private ObservableCollection<UserNotification> _notifications;
        private ObservableCollection<UserControl> _notificationControls;
        private readonly MedicationService _medicationService;
        private List<Prescription> prescriptions;

        public ObservableCollection<UserNotification> Notifications
        {
            get { return _notifications; }
            set
            {
                _notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }

        public ObservableCollection<UserControl> NotificationControls
        {
            get { return _notificationControls; }
            set
            {
                _notificationControls = value;
                OnPropertyChanged(nameof(NotificationControls));
            }
        }

        public PatientNotificationsViewModel()
        {
            _userNotificationService = Injector.GetService<UserNotificationService>();
            _prescriptionService = Injector.GetService<PrescriptionService>(Injector.REGULAR_PRESCRIPTION_S);
            _medicationService = Injector.GetService<MedicationService>();
            Notifications = new ObservableCollection<UserNotification>();
            NotificationControls = new ObservableCollection<UserControl>();
            LoadNotifications();
        }

        public void LoadNotifications()
        {
            GetNotifications();
            GetNotificationControls();
        }

        public void GetNotifications()
        {
            string currentUserJMBG = Context.Current.JMBG;
            List<UserNotification> userNotifications = _userNotificationService.GetForUser(currentUserJMBG);
            DateTime currentTime = DateTime.Now;
            prescriptions = _prescriptionService.GetPatientsPrescriptions(currentUserJMBG);
            foreach (Prescription prescription in prescriptions)
            {
                foreach (DateTime pillDateTime in prescription.GetPillConsumptionTimes())
                {
                    if (pillDateTime > currentTime)
                    {
                        Medication medication = _medicationService.Get(prescription.MedicationId);
                        string notificationMessage = "Lek: " + medication.Name + "\n"
                                                   + "Instrukcije: " + prescription.Instruction + "\n"
                                                   + "Vreme uzimanja leka: " + pillDateTime.ToString();
                        userNotifications.Add(new UserNotification(currentUserJMBG, pillDateTime, "Popijte tabletu", notificationMessage, false));
                    }
                }
            }
            userNotifications = userNotifications.OrderBy(x => x.receiveTime).ToList();
            Notifications.Clear();
            foreach (var notification in userNotifications)
            {
                Notifications.Add(notification);
            }
        }

        public void GetNotificationControls()
        {
            NotificationControls.Clear();
            foreach (var notification in Notifications)
            {
                var notificationControl = new NotificationControl(notification);
                NotificationControls.Add(notificationControl);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
