namespace HealthCare.Context
{
    public static class Global
    {
        private const string dirPath = "../../../Resource/";

        public const string roomPath = dirPath + "rooms.csv";
        public const string orderPath = dirPath + "orders.csv";
        public const string medicationOrderPath = dirPath + "medication_orders.csv";
        public const string nursePath = dirPath + "nurses.csv";
        public const string doctorPath = dirPath + "doctors.csv";
        public const string patientPath = dirPath + "patients.csv";
        public const string transferPath = dirPath + "transfers.csv";
        public const string anamnesisPath = dirPath + "anamneses.csv";
        public const string equipmentPath = dirPath + "equipment.csv";
        public const string patientLogsPath = dirPath + "patient_logs.csv";
        public const string appointmentPath = dirPath + "appointments.csv";
        public const string equipmentInventoryPath = dirPath + "inventory_items.csv";
        public const string medicationInventoryPath = dirPath + "medication_inventory.csv";
        public const string notificationPath = dirPath + "notifications.csv";

        public const string medicationPath = dirPath + "medications.csv";
        public const string prescriptionPath = dirPath + "prescriptions.csv";
        public const string treatmentReferralPath = dirPath + "treatment_referrals.csv";
        public const string specialistReferralPath = dirPath + "specialist_referrals.csv";
        public const string therapyPrescriptionPath = dirPath + "therapy_prescriptions.csv";
        public const string therapyPath = dirPath + "therapy.csv";
        public const string userNotificationPath = dirPath + "user_notifications.csv";
        public const string managerUsername = "admin";
        public const string managerPassword = "admin";

        public const string dateFormat = "dd-MM-yyyy HH:mm:ss";
        public const string timeSpanFormat = "c";
    }
}
