using HealthCare.Application.Common;
using HealthCare.Model;
using HealthCare.Model.Renovation;
using HealthCare.Repository;
using HealthCare.Service;
using HealthCare.Service.RenovationService;
using HealthCare.Service.ScheduleService;
using System;
using System.Collections.Generic;

namespace HealthCare.Application
{
    public static class Injector
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>()
        {
            { typeof(SpecialistReferralService), new SpecialistReferralService(
                GetFileRepository<SpecialistReferral> (Paths.SPECIALIST_REFERRALS)) },
            { typeof(UserNotificationService), new UserNotificationService(
                GetFileRepository<UserNotification> (Paths.USER_NOTIFICATIONS_REFERRALS)) },
            { typeof(TreatmentReferralService), new TreatmentReferralService(
                GetFileRepository<TreatmentReferral> (Paths.TREATMENT_REFERRALS)) },
            { typeof(BasicRenovationService), new BasicRenovationService(
                GetFileRepository<RenovationBase> (Paths.BASIC_RENOVATIONS)) },
            { typeof(NotificationService), new NotificationService(
                GetFileRepository<Notification> (Paths.NOTIFICATIONS)) },
            { typeof(AppointmentService), new AppointmentService(
                GetFileRepository<Appointment> (Paths.APPOINTMENTS)) },
            { typeof(MedicationService), new MedicationService(
                GetFileRepository<Medication> (Paths.MEDICATIONS)) },
            { typeof(EquipmentService), new EquipmentService(
                GetFileRepository<Equipment> (Paths.EQUIPMENT)) },
            { typeof(AnamnesisService), new AnamnesisService(
                GetFileRepository<Anamnesis> (Paths.ANAMNESES)) },
            { typeof(TherapyService), new TherapyService(
                GetFileRepository<Therapy> (Paths.THERAPIES)) },
            { typeof(PatientService), new PatientService(
                GetFileRepository<Patient> (Paths.PATIENTS)) },
            { typeof(DoctorService), new DoctorService(
                GetFileRepository<Doctor> (Paths.DOCTORS)) },
            { typeof(LoginService), new LoginService(
                GetFileRepository<Patient> (Paths.PATIENTS),
                GetFileRepository<Doctor> (Paths.DOCTORS),
                GetFileRepository<User> (Paths.NURSES)) },
            { typeof(RoomService), new RoomService(
                GetFileRepository<Room> (Paths.ROOMS)) },
            { typeof(PrescriptionService), new Dictionary<string, object>()
            {
                { REGULAR_PRESCRIPTION_S, new PrescriptionService(
                GetFileRepository<Prescription> (Paths.REGULAR_PRESCRIPTIONS)) },
                { THERAPY_PRESCRIPTION_S, new PrescriptionService(
                GetFileRepository<Prescription> (Paths.THERAPY_PRESCRIPTIONS)) } }
            },
            { typeof(InventoryService), new Dictionary<string, object>()
            {
                { EQUIPMENT_INVENTORY_S, new InventoryService(
                    GetFileRepository<InventoryItem> (Paths.EQUIPMENT_INVENTORY)) },
                { MEDICATION_INVENTORY_S, new InventoryService(
                    GetFileRepository<InventoryItem> (Paths.MEDICATION_INVENTORY)) } }
            }
        };

        static Injector()
        {
            _services[typeof(SplittingRenovationService)] = new SplittingRenovationService(
                GetFileRepository<SplittingRenovation>(Paths.SPLITTING_RENOVATIONS));
            _services[typeof(JoiningRenovationService)] = new JoiningRenovationService(
                GetFileRepository<JoiningRenovation>(Paths.JOINING_RENOVATIONS));

            _services[typeof(PatientSchedule)] = new PatientSchedule();
            _services[typeof(DoctorSchedule)] = new DoctorSchedule();
            _services[typeof(RoomSchedule)] = new RoomSchedule();
            _services[typeof(Schedule)] = new Schedule();

            _services[typeof(TransferService)] = new TransferService(
                GetFileRepository<TransferItem>(Paths.TRANSFERS));
            _services[typeof(OrderService)] = new Dictionary<string, object>()
            {
                { EQUIPMENT_ORDER_S, new OrderService(
                GetFileRepository<OrderItem> (Paths.EQUIPMENT_ORDERS),
                GetService<InventoryService> (EQUIPMENT_INVENTORY_S)) },

                { MEDICATION_ORDER_S, new OrderService(
                GetFileRepository<OrderItem> (Paths.MEDICATION_ORDERS),
                GetService<InventoryService> (MEDICATION_INVENTORY_S)) }
            };
        }

        public static T GetService<T>(string? implKey = null)
        {
            Type type = typeof(T);
            var val = GetDictionaryValue(type);
            if (val is T)
                return (T)val;

            if (implKey is null)
                throw new ArgumentException($"Multiple implementations found for type '{type}'. Specific key is needed");

            var implementations = (Dictionary<string, object>)val;
            if (implementations.ContainsKey(implKey))
                return (T)implementations[implKey];

            throw new ArgumentException($"No implementation found for implementation key '{implKey}'");
        }

        private static object GetDictionaryValue(Type type)
        {
            if (_services.ContainsKey(type))
                return _services[type];
            throw new ArgumentException($"No implementation found for type '{type}'");
        }

        public static FileRepository<T> GetFileRepository<T>(string path) where T : RepositoryItem, new()
        {
            return new FileRepository<T>(path);
        }

        public const string EQUIPMENT_ORDER_S = "EquipmentOrderService";
        public const string MEDICATION_ORDER_S = "MedicationOrderService";
        public const string EQUIPMENT_INVENTORY_S = "EquipmentInventoryService";
        public const string MEDICATION_INVENTORY_S = "MedicationInventoryService";
        public const string REGULAR_PRESCRIPTION_S = "RegularPrescriptionService";
        public const string THERAPY_PRESCRIPTION_S = "TherapyPrescriptionService";
    }
}
