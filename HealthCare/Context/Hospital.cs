using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Context
{
    public enum UserRole
    {
        Manager, Doctor, Nurse, Patient
    }

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
        public AnamnesisService AnamnesisService;
        public EquipmentService EquipmentService;
        public NotificationService NotificationService;

        public Hospital() : this("Bolnica") { }
        public Hospital(string name)
        {
            Name = name;
            Current = null;

            RoomService = new RoomService(Global.roomPath);
            Inventory = new Inventory(Global.inventoryPath);
            OrderService = new OrderService(Global.orderPath);
            NurseService = new NurseService(Global.nursePath);
            DoctorService = new DoctorService(Global.doctorPath);
            PatientService = new PatientService(Global.patientPath);
            EquipmentService = new EquipmentService(Global.equipmentPath);
            AnamnesisService = new AnamnesisService(Global.anamnesisPath);
            NotificationService = new NotificationService(Global.notificationPath);
        }

        public void LoadAll()
        {
            Schedule.Load(Global.appointmentPath);

            FillAppointmentDetails();
            // ExecuteEquipmentOrders();
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
                    throw new IncorrectPasswordException();
                return UserRole.Manager;
            }

            User? u = DoctorService.GetByUsername(username);
            if (u is not null)
            {
                if (u.Password != password)
                    throw new IncorrectPasswordException();
                Current = u;
                return UserRole.Doctor;
            }

            u = NurseService.GetByUsername(username);
            if (u is not null)
            {
                if (u.Password != password)
                    throw new IncorrectPasswordException();
                Current = u;
                return UserRole.Nurse;
            }

            u = PatientService.GetByUsername(username);
            if (u is not null)
            {
                if (u.Password != password)
                    throw new IncorrectPasswordException();
                Current = u;
                return UserRole.Patient;
            }
            throw new UsernameNotFoundException();
        }

        private void FillAppointmentDetails()
        {
            foreach(Appointment appointment in Schedule.Appointments)
            {
                appointment.Doctor = DoctorService.GetAccount(appointment.Doctor.JMBG);
                appointment.Patient = PatientService.GetAccount(appointment.Patient.JMBG);
            }
        }
        
        private void ExecuteEquipmentOrders()
        {
            int warehouseId = RoomService.GetWarehouseId();
            foreach (OrderItem item in OrderService.GetAll())
            {
                if (!item.Executed && item.Scheduled >= DateTime.Now)
                    Inventory.RestockInventoryItem(new InventoryItem(0, item.EquipmentId, warehouseId, item.Quantity));
            }
        }
        
    }
}
