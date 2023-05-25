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
            services["AnamnesisService"]        = AnamnesisService.GetInstance(new FileRepository<Anamnesis>(Global.anamnesisPath));
            services["AppointmentService"]      = AppointmentService.GetInstance(new FileRepository<Appointment>(Global.appointmentPath));
            services["DoctorService"]           = DoctorService.GetInstance(new FileRepository<Doctor>(Global.doctorPath));
            services["EquipmentService"]        = EquipmentService.GetInstance(new FileRepository<Equipment>(Global.equipmentPath));
            services["EquipmentService"]        = EquipmentService.GetInstance(new FileRepository<Equipment>(Global.equipmentPath));
            services["EquipmentInventory"]      = Inventory.GetInstance(new FileRepository < InventoryItem >(Global.equipmentInventoryPath));
            services["MedicationInventory"]     = Inventory.GetInstance(new FileRepository<InventoryItem>(Global.medicationInventoryPath));
            services["MedicationService"]       = MedicationService.GetInstance(new FileRepository<Medication>(Global.medicationPath));
            services["NotificationService"]     = NotificationService.GetInstance(new FileRepository<Notification>(Global.notificationPath));
            services["NurseService"]            = NurseService.GetInstance(new FileRepository<User>(Global.nursePath));
            services["RoomService"]             = RoomService.GetInstance(new FileRepository<Room>(Global.roomPath));
            services["EquipmentOrderService"]   = OrderService.GetInstance(new FileRepository<OrderItem>(Global.orderPath));
            services["MedicationOrderService"]  = OrderService.GetInstance(new FileRepository<OrderItem>(Global.medicationOrderPath));
            services["PatientService"]          = PatientService.GetInstance(new FileRepository<Patient>(Global.patientPath));
            services["PrescriptionService"]     = PrescriptionService.GetInstance(new FileRepository<Prescription>(Global.prescriptionPath));
            services["SpecialistReferralService"] = SpecialistReferralService.GetInstance(new FileRepository<SpecialistReferral>(Global.specialistReferralPath));
            services["TherapyService"]          = TherapyService.GetInstance(new FileRepository<Therapy>(Global.therapyPath));
            services["TransferService"]         = TransferService.GetInstance(new FileRepository<TransferItem>(Global.transferPath));
            services["TreatmentReferralService"] = TreatmentReferralService.GetInstance(new FileRepository<TreatmentReferral>(Global.treatmentReferralPath));
        }
    }
}
