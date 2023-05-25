using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;

namespace HealthCare.Context
{
    public class Hospital
    {
        public string Name { get; set; }
        public User? Current { get; set; }

        public Inventory EquipmentInventory;
        public Inventory MedicationInventory;
        public RoomService RoomService;
        public OrderService EquipmentOrderService;
        public OrderService MedicationOrderService;
        public DoctorService DoctorService;
        public PatientService PatientService;
        public TransferService TransferService;
        public EquipmentService EquipmentService;
        public AnamnesisService AnamnesisService;
        public NotificationService NotificationService;
        
        public TreatmentReferralService TreatmentReferralService;
        public SpecialistReferralService SpecialistReferralService;
        public MedicationService MedicationService;
        public PrescriptionService PrescriptionService;
        public TherapyService TherapyService;
        public PrescriptionService TherapyPrescriptionService;
        public Hospital() : this("Bolnica") { }

        public Hospital(string name)
        {
            Name = name;
            Current = null;

            ServiceProvider.BuildServices();

            RoomService = new RoomService(Global.roomPath);
            EquipmentInventory = new Inventory(Global.equipmentInventoryPath);
            MedicationInventory = new Inventory(Global.medicationInventoryPath);
            DoctorService = new DoctorService(Global.doctorPath);
            PatientService = new PatientService(Global.patientPath);
            EquipmentService = new EquipmentService(Global.equipmentPath);
            AnamnesisService = new AnamnesisService(Global.anamnesisPath);
            TransferService = new TransferService(Global.transferPath);
            NotificationService = new NotificationService(Global.notificationPath);
            EquipmentOrderService = new OrderService(Global.orderPath);
            MedicationOrderService = new OrderService(Global.medicationOrderPath);

            TreatmentReferralService = new TreatmentReferralService(Global.treatmentReferralPath);
            SpecialistReferralService = new SpecialistReferralService(Global.specialistReferralPath);
            MedicationService = new MedicationService(Global.medicationPath);
            PrescriptionService = new PrescriptionService(Global.prescriptionPath);
            TherapyService = new TherapyService(Global.therapyPath);
            TherapyPrescriptionService = new PrescriptionService(Global.therapyPrescriptionPath);
        }

        public void LoadAll()
        {
            Schedule.Load(Global.appointmentPath);
            FillAppointmentDetails();

            EquipmentOrderService.ExecuteAll();
            MedicationOrderService.ExecuteAll();
            TransferService.ExecuteAll();
        }

        public void SaveAll()
        {
            Schedule.Save(Global.appointmentPath);
        }

        public Role LoginRole(string username, string password)
        {
            var loginService = (LoginService)ServiceProvider.services["LoginService"];
            return loginService.Login(username, password);
        }

        private void FillAppointmentDetails()
        {
            foreach (Appointment appointment in Schedule.Appointments)
            {
                appointment.Doctor = DoctorService.GetAccount(appointment.Doctor.JMBG);
                appointment.Patient = PatientService.GetAccount(appointment.Patient.JMBG);
            }
        }
    }
}
