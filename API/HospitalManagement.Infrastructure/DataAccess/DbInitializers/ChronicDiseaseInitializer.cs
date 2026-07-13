using HospitalManagement.Domain.Entities.Patients;
using HospitalManagement.Infrastructure.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.DbInitializers
{
    public class ChronicDiseaseInitializer
    {
        public static async Task SeedChronicDisease(AppDbContext context)
        {
            if (await context.ChronicDiseases.AnyAsync()) return;

            var chronicDiseases = new List<ChronicDisease>
        {
            new ChronicDisease
            {
                Name = "السكري",
                Description = "مرض مزمن يؤثر على قدرة الجسم في تنظيم مستوى السكر في الدم."
            },
            new ChronicDisease
            {
                Name = "ارتفاع ضغط الدم",
                Description = "حالة مزمنة يرتفع فيها ضغط الدم عن المعدل الطبيعي مما يزيد خطر أمراض القلب."
            },
            new ChronicDisease
            {
                Name = "أمراض القلب المزمنة",
                Description = "مجموعة من الأمراض التي تؤثر على القلب مثل ضعف عضلة القلب أو انسداد الشرايين."
            },
            new ChronicDisease
            {
                Name = "الربو",
                Description = "مرض مزمن يصيب الجهاز التنفسي ويسبب صعوبة في التنفس وضيق في الشعب الهوائية."
            },
            new ChronicDisease
            {
                Name = "أمراض الكلى المزمنة",
                Description = "تدهور تدريجي في وظائف الكلى مما يؤثر على قدرة الجسم على التخلص من السموم."
            },
            new ChronicDisease
            {
                Name = "أمراض الغدة الدرقية",
                Description = "خلل في إفراز هرمونات الغدة الدرقية مما يؤثر على الأيض والطاقة في الجسم."
            },
            new ChronicDisease
            {
                Name = "التهاب المفاصل المزمن",
                Description = "حالة تسبب ألم وتيبس في المفاصل وتؤثر على الحركة بشكل مستمر."
            },
            new ChronicDisease
            {
                Name = "مرض الانسداد الرئوي المزمن",
                Description = "مرض رئوي مزمن يسبب صعوبة في التنفس وغالبًا يرتبط بالتدخين."
            },
            new ChronicDisease
            {
                Name = "فقر الدم المزمن",
                Description = "انخفاض مستمر في عدد كريات الدم الحمراء مما يسبب التعب والضعف العام."
            },
            new ChronicDisease
            {
                Name = "السمنة المفرطة",
                Description = "حالة مزمنة تتمثل في زيادة مفرطة في الوزن تؤثر على الصحة العامة."
            }
        };

            await context.ChronicDiseases.AddRangeAsync(chronicDiseases);
            await context.SaveChangesAsync();
        }
    }
}
