using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Enums
{
    public enum AppointmentType
    {
        Consultation = 1,    // استشارة طبية
        FollowUp = 2,        // متابعة حالة
        Surgery = 3,         // عملية جراحية
        Therapy = 4,         // جلسة علاجية (مثل العلاج الطبيعي)
        DiagnosticTest = 5   // فحص تشخيصي (مثل أشعة أو تحاليل)
    }
}
