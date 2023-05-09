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
        public Inventory Inventory;
        public OrderService OrderService;
        public TransferService TransferService;
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
            Inventory = new Inventory(Global.inventoryPath);
            OrderService = new OrderService(Global.orderPath, Inventory, RoomService);
            TransferService = new TransferService(Global.transferPath, Inventory);
            NotificationService = new NotificationService(Global.notificationPath);
        }

        public void LoadAll()
        {
            Schedule.Load(Global.appointmentPath);
            FillAppointmentDetails();

            OrderService.ExecuteOrders();
            TransferService.ExecuteTransfers();
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
                    throw new LoginException();
                return UserRole.Manager;
            }

            User? u = DoctorService.GetByUsername(username);
            if (u is not null)
            {
                if (u.Password != password)
                    throw new LoginException();
                Current = u;
                return UserRole.Doctor;
            }

            u = NurseService.GetByUsername(username);
            if (u is not null)
            {
                if (u.Password != password)
                    throw new LoginException();
                Current = u;
                return UserRole.Nurse;
            }

            u = PatientService.GetByUsername(username);
            if (u is not null)
            {
                if (u.Password != password)
                    throw new LoginException();
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
    }
}
