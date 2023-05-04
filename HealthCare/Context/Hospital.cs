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

        public RoomService RoomService;
        public NurseService NurseService;
        public DoctorService DoctorService;
        public PatientService PatientService;
        public EquipmentService EquipmentService;
        public Inventory Inventory;
        public OrderService OrderService;

        public Hospital() : this("Bolnica") { }
        public Hospital(string name)
        {
            Name = name;
            Current = null;

            RoomService = new RoomService(Global.roomPath);
            NurseService = new NurseService(Global.nursePath);
            DoctorService = new DoctorService(Global.doctorPath);
            PatientService = new PatientService(Global.patientPath);
            EquipmentService = new EquipmentService(Global.equipmentPath);
            Inventory = new Inventory(Global.inventoryPath);
            OrderService = new OrderService(Global.orderPath);
        }

        public void LoadAll()
        {
            RoomService.Load();
            NurseService.Load();
            DoctorService.Load();
            PatientService.Load();
            EquipmentService.Load();
            Inventory.Load();
            OrderService.Load();
            Schedule.Load(Global.appointmentPath);

            FillAppointmentDetails();
            FillInventoryDetails();
        }

        public void SaveAll()
        {
            RoomService.Save();
            DoctorService.Save();
            PatientService.Save();
            EquipmentService.Save();
            Inventory.Save();
            OrderService.Save();
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

        private void FillInventoryDetails()
        {
            foreach (InventoryItem item in Inventory.Items)
            {
                item.Equipment = EquipmentService.Get(item.Equipment.Name);
                item.Room = RoomService.Get(item.Room.Name);
            }
        }
    }
}
