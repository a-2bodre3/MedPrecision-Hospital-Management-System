using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedById { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public int? LastModifiedById { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public int? DeletedById { get; set; }
    }
}
