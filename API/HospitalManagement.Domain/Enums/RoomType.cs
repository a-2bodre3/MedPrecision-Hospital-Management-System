using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Enums
{
    public enum RoomType
    {
        Clinic = 1,          // عيادة خارجية
        Ward = 2,            // عنبر / غرفة إقامة مرضى
        OperationRoom = 3,   // غرفة عمليات
        Office = 4,          // مكتب إداري (محاسبة، إدارة)
        Laboratory = 5       // معمل تحاليل أو أشعة
    }

}
