using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Infrastructure.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.DbInitializers
{
    public class PermissionInitializer
    {
        public static async Task SeedPermissions(AppDbContext context)
        {
            if (await context.Permissions.AnyAsync()) return;
            var permissions = new List<Permission>
        {
            //--------------------Patient & EMR Module----------------------
            new Permission { Token = "Patient_Create", Description = "Allows registration of new patients into the system.", Module = "Patient & EMR" },
            new Permission { Token = "Patient_Read_Basic", Description = "Allows viewing of basic identity data (Name, Age, Contact info).", Module = "Patient & EMR" },
            new Permission { Token = "Patient_Read_Medical", Description = "Allows viewing sensitive medical histories, allergies, and Electronic Medical Records (EMR).", Module = "Patient & EMR" },
            new Permission { Token = "Patient_Update", Description = "Allows editing patient demographic and contact information.", Module = "Patient & EMR" },
            new Permission { Token = "Patient_Delete", Description = "Soft-deletes or archives a patient profile from the system.", Module = "Patient & EMR" },
            //--------------------Appointment Scheduling Module----------------------
            new Permission { Token = "Appointment_Create", Description = "Allows booking new appointments for patients with doctors.", Module = "Appointment Scheduling" },
            new Permission { Token = "Appointment_Read", Description = "Allows viewing appointment slots, calendars, and schedules.", Module = "Appointment Scheduling" },
            new Permission { Token = "Appointment_Update", Description = "Allows rescheduling or modifying existing appointments.", Module = "Appointment Scheduling" },
            new Permission { Token = "Appointment_Cancel", Description = "Allows cancelling a booked appointment slot.", Module = "Appointment Scheduling" },
            new Permission { Token = "Appointment_CheckIn", Description = "Allows updating status when a patient arrives at the clinic (e.g., \"Waiting\").", Module = "Appointment Scheduling" },
            //--------------------Clinical & Consultation Module----------------------
            new Permission { Token = "Consultation_Start", Description = "Allows initiating a medical examination session with a patient.", Module = "Clinical & Consultation" },
            new Permission { Token = "Prescription_Create", Description = "Allows issuing electronic prescriptions and medical orders.", Module = "Clinical & Consultation" },
            new Permission { Token = "Diagnosis_Add", Description = "Allows adding clinical diagnoses and ICD codes to patient files.", Module = "Clinical & Consultation" },
            new Permission { Token = "Vitals_Record", Description = "Allows recording patient vital signs (Blood pressure, Temperature, Pulse).", Module = "Clinical & Consultation" },
            new Permission { Token = "Medical_Report_Generate", Description = "Allows generating and signing official medical reports or discharge summaries.", Module = "Clinical & Consultation" },
            //--------------------Laboratory & Radiology Module----------------------
            new Permission { Token = "Lab_Request_Create", Description = "Allows doctors to order lab tests or radiology scans for a patient.", Module = "Laboratory & Radiology" },
            new Permission { Token = "Lab_Request_Read", Description = "Allows viewing pending lab and radiology requests.", Module = "Laboratory & Radiology" },
            new Permission { Token = "Lab_Result_Upload", Description = "Allows laboratory technicians to upload test results and images.", Module = "Laboratory & Radiology" },
            new Permission { Token = "Lab_Result_Approve", Description = "Allows lab heads to review and officially approve results before publication.", Module = "Laboratory & Radiology" },
            //--------------------Pharmacy & Inventory Module----------------------
            new Permission { Token = "Pharmacy_Prescription_Read", Description = "Allows pharmacists to view doctor-issued electronic prescriptions.", Module = "Pharmacy & Inventory" },
            new Permission { Token = "Pharmacy_Dispense", Description = "Allows marking medications as dispensed and automatically deducting stock.", Module = "Pharmacy & Inventory" },
            new Permission { Token = "Inventory_Read", Description = "Allows viewing stock levels of medicines, surgical items, and tools.", Module = "Pharmacy & Inventory" },
            new Permission { Token = "Inventory_Update", Description = "Allows adding stock (purchases) or writing off damaged inventory items.", Module = "Pharmacy & Inventory" },
            new Permission { Token = "Inventory_Alert_Config", Description = "Allows setting threshold limits for low-stock automated alerts.", Module = "Pharmacy & Inventory" },
            //--------------------Billing & Finance Module----------------------
            new Permission { Token = "Invoice_Create", Description = "Allows generating invoices for consultations, procedures, labs, or stays.", Module = "Billing & Finance" },
            new Permission { Token = "Invoice_Read", Description = "Allows viewing patient billing history, payment tracking, and outstanding balances.", Module = "Billing & Finance" },
            new Permission { Token = "Invoice_Update", Description = "Allows applying authorized discounts, refunds, or waivers to invoices.", Module = "Billing & Finance" },
            new Permission { Token = "Insurance_Claim_Process", Description = "Allows managing health insurance approvals and claims submission.", Module = "Billing & Finance" },
            new Permission { Token = "Financial_Report_View", Description = "Allows viewing hospital revenue streams, expense sheets, and profit reports.", Module = "Billing & Finance" },
            //--------------------System & User Management Module----------------------
            new Permission { Token = "User_Create", Description = "Allows creating new system users (staff accounts).", Module = "System & User Management" },
            new Permission { Token = "User_Read", Description = "Allows viewing the directory of hospital employees and active users.", Module = "System & User Management" },
            new Permission { Token = "User_Update", Description = "Allows updating staff profiles, resetting passwords, or deactivating accounts.", Module = "System & User Management" },
            new Permission { Token = "Role_Manage", Description = "Allows the creation of user roles and modifying assigned permission matrices.", Module = "System & User Management" },
            new Permission { Token = "Clinic_Setup", Description = "Allows configuring hospital departments, clinics, and doctor shift patterns.", Module = "System & User Management" },
            new Permission { Token = "System_Logs_View", Description = "Allows auditing system tracking logs (security actions, data mutations, logins).", Module = "System & User Management" },
            new Permission { Token = "Branch_Create", Description = "Allows creating new hospital branch", Module = "System & User Management" },
            new Permission { Token = "Branch_Read", Description = "Allows view all hospital branches", Module = "System & User Management" },
            new Permission { Token = "Branch_Update", Description = "Allow update hospital branch", Module = "System & User Management" },
        };

            await context.Permissions.AddRangeAsync(permissions);
            await context.SaveChangesAsync();
        }
    }
}
