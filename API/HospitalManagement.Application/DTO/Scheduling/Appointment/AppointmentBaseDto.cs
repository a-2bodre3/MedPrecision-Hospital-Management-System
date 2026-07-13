using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Scheduling.Appointment
{
    public abstract record AppointmentBaseDto
    {
        public DateTime AppointmentDate { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? Notes { get; set; }
    }
}
