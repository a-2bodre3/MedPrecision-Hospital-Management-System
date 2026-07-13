
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Enums
{
    public enum AppointmentsStatus
    {
        Scheduled = 1,       // تم جدولة الموعد
        Completed = 2,       // تم إتمام الموعد
        Canceled = 3,        // تم إلغاء الموعد
        NoShow = 4,          // لم يحضر المريض
        Rescheduled = 5      // تم إعادة جدولة الموعد
    }
}
