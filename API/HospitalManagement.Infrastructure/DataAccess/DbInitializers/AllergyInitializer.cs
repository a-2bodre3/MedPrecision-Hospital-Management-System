using HospitalManagement.Domain.Entities.Patients;
using HospitalManagement.Infrastructure.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.DbInitializers
{
    public class AllergyInitializer
    {
        public static async Task SeedAllergies(AppDbContext context)
        {
            if (await context.Allergies.AnyAsync()) return;

            var allergies = new List<Allergy>
      {
         new Allergy
         {
            Name = "حساسية الفول السوداني",
            Description = "تفاعل تحسسي شديد تجاه الفول السوداني قد يسبب صعوبة في التنفس أو صدمة تحسسية خطيرة."
         },
         new Allergy
         {
            Name = "حساسية منتجات الألبان",
            Description = "حساسية تجاه بروتينات الحليب تسبب مشاكل في الجهاز الهضمي أو طفح جلدي."
         },
         new Allergy
         {
            Name = "حساسية الجلوتين",
            Description = "تفاعل مناعي تجاه الجلوتين يؤدي إلى مشاكل في الجهاز الهضمي مثل الانتفاخ والإسهال."
         },
         new Allergy
         {
            Name = "حساسية المأكولات البحرية",
            Description = "حساسية تجاه الأسماك والمحار وقد تسبب ردود فعل تحسسية شديدة وخطيرة."
         },
         new Allergy
         {
            Name = "حساسية البيض",
            Description = "تفاعل تحسسي تجاه البروتينات الموجودة في البيض، خاصة عند الأطفال."
         },
         new Allergy
         {
            Name = "حساسية لسعات النحل",
            Description = "تفاعل تحسسي شديد نتيجة لسعات الحشرات قد يسبب تورم أو صدمة تحسسية."
         },
         new Allergy
         {
            Name = "حساسية البنسلين",
            Description = "تفاعل تحسسي تجاه البنسلين وبعض المضادات الحيوية وقد يكون خطيرًا."
         },
         new Allergy
         {
            Name = "حساسية حبوب اللقاح",
            Description = "حساسية موسمية تسبب العطس والاحتقان ودموع العين."
         },
         new Allergy
         {
            Name = "حساسية وبر الحيوانات",
            Description = "تفاعل تحسسي تجاه شعر أو جلد الحيوانات مثل القطط والكلاب."
         },
         new Allergy
         {
            Name = "حساسية اللاتكس",
            Description = "تفاعل تحسسي تجاه منتجات اللاتكس مثل القفازات الطبية."
         }
      };

            await context.Allergies.AddRangeAsync(allergies);
            await context.SaveChangesAsync();
        }
    }
}
