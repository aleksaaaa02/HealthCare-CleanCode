using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.RenovationService;

namespace HealthCare.Context
{
    public class Hospital
    {
        public string Name { get; set; }
        public User? Current { get; set; }

        public Inventory Inventory;
        public RoomService RoomService;
        public NurseService NurseService;
        public OrderService OrderService;
        public DoctorService DoctorService;
        public PatientService PatientService;
        public TransferService TransferService;
        public EquipmentService EquipmentService;
        public AnamnesisService AnamnesisService;
        public SimpleRenovationService SimpleRenovationService;
        public JoiningRenovationService JoiningRenovationService;
        public SplittingRenovationService SplittingRenovationService;
        public NotificationService NotificationService;

        public Hospital() : this("Bolnica") { }

        public Hospital(string name)
        {
            Name = name;
            Current = null;

            RoomService = new RoomService(Global.roomPath);
            Inventory = new Inventory(Global.inventoryPath);
            NurseService = new NurseService(Global.nursePath);
            SimpleRenovationService = new SimpleRenovationService(Global.renovationPath);
            JoiningRenovationService = new JoiningRenovationService(
                Global.joiningRenovationPath, RoomService, Inventory);
            SplittingRenovationService = new SplittingRenovationService(
                Global.splittingRenovationPath, RoomService, Inventory);
            DoctorService = new DoctorService(Global.doctorPath);
            PatientService = new PatientService(Global.patientPath);
            EquipmentService = new EquipmentService(Global.equipmentPath);
            AnamnesisService = new AnamnesisService(Global.anamnesisPath);
            TransferService = new TransferService(Global.transferPath, Inventory);
            NotificationService = new NotificationService(Global.notificationPath);
            OrderService = new OrderService(Global.orderPath, Inventory, RoomService);
        }

        public void LoadAll()
        {
            Schedule.Load(Global.appointmentPath);
            FillAppointmentDetails();

            OrderService.ExecuteAll();
            TransferService.ExecuteAll();
        }

        public void SaveAll()
        {
            Schedule.Save(Global.appointmentPath);
        }

        public UserRole LoginRole(string username, string password)
        {
            if (Global.managerUsername == username)
            {
                if (Global.managerPassword != password)
                    throw new WrongPasswordException();
                return UserRole.Manager;
            }

            var userServices = new IUserService[] { DoctorService, NurseService, PatientService };
            foreach (var service in userServices)
            {
                var user = service.GetByUsername(username);
                if (user is not null) {
                    if (user.Password != password)
                        throw new WrongPasswordException();

                    Current = user;
                    return service.GetRole();
                }
            }
            throw new UsernameNotFoundException();
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
