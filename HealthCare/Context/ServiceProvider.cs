using HealthCare.Model;
using HealthCare.Repository;
using HealthCare.Serialize;
using HealthCare.Service;
using System;
using System.Collections.Generic;

namespace HealthCare.Context
{
    public abstract class ServiceProvider
    {

        public static Dictionary<string, object> services = new Dictionary<string, object>();

        public static void BuildServices()
        {
            services["SpecialistReferralService"] = new SpecialistReferralService(new FileRepository<SpecialistReferral>(Global.specialistReferralPath));
            services["TreatmentReferralService"]  = new TreatmentReferralService(new FileRepository<TreatmentReferral>(Global.treatmentReferralPath));
            services["NotificationService"]     = new NotificationService(new FileRepository<Notification>(Global.notificationPath));
            services["TherapyPrescriptionService"] = new PrescriptionService(new FileRepository<Prescription>(Global.therapyPrescriptionPath));
            services["PrescriptionService"]     = new PrescriptionService(new FileRepository<Prescription>(Global.prescriptionPath));
            services["EquipmentInventory"]      = new Inventory(new FileRepository < InventoryItem >(Global.equipmentInventoryPath));
            services["MedicationInventory"]     = new Inventory(new FileRepository<InventoryItem>(Global.medicationInventoryPath));
            services["AppointmentService"]      = new AppointmentService(new FileRepository<Appointment>(Global.appointmentPath));
            services["MedicationService"]       = new MedicationService(new FileRepository<Medication>(Global.medicationPath));
            services["EquipmentService"]        = new EquipmentService(new FileRepository<Equipment>(Global.equipmentPath));
            services["AnamnesisService"]        = new AnamnesisService(new FileRepository<Anamnesis>(Global.anamnesisPath));
            services["TherapyService"]          = new TherapyService(new FileRepository<Therapy>(Global.therapyPath));
            services["PatientService"]          = new PatientService(new FileRepository<Patient>(Global.patientPath));
            services["DoctorService"]           = new DoctorService(new FileRepository<Doctor>(Global.doctorPath));
            services["RoomService"]             = new RoomService(new FileRepository<Room>(Global.roomPath));

            services["MedicationOrderService"]  = new OrderService(new FileRepository<OrderItem>(Global.medicationOrderPath));
            services["EquipmentOrderService"]   = new OrderService(new FileRepository<OrderItem>(Global.orderPath));
            services["TransferService"]         = new TransferService(new FileRepository<TransferItem>(Global.transferPath));

            services["LoginService"] = new LoginService(
                new FileRepository<Patient>(Global.patientPath),
                new FileRepository<Doctor>(Global.doctorPath),
                new FileRepository<User>(Global.nursePath));

            // services["Schedule"] = new Schedule();
        }
    }
}
