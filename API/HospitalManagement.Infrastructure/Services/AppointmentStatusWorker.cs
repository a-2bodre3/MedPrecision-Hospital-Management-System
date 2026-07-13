using HospitalManagement.Domain.Enums;
using HospitalManagement.Infrastructure.DataAccess.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.Services
{
    public class AppointmentStatusWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AppointmentStatusWorker> _logger;
        private readonly TimeSpan _period = TimeSpan.FromMinutes(15);

        public AppointmentStatusWorker(IServiceScopeFactory scopeFactory, ILogger<AppointmentStatusWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("AppointmentStatusWorker is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Checking for expired appointments...");
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        var now = DateTime.UtcNow;
                        var expiredAppointments = await context.Appointments
                            .Where(a => a.AppointmentDate < now && a.Status == AppointmentsStatus.Scheduled).ToListAsync(stoppingToken);

                        if (expiredAppointments.Any())
                        {
                            foreach (var appointment in expiredAppointments)
                            {
                                appointment.Status = AppointmentsStatus.NoShow;
                            }
                            await context.SaveChangesAsync(stoppingToken);
                            _logger.LogInformation("{Count} appointments updated to NoShow.", expiredAppointments.Count);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating appointment statuses.");
                }
                await Task.Delay(_period, stoppingToken);
            }
            _logger.LogInformation("Appointment Status Worker stopped.");
        }

    }
}
