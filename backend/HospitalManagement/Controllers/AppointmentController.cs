using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using HospitalManagement.Models;

namespace HospitalManagement.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(ApplicationDbContext context, ILogger<AppointmentsController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAppointment(Appointment appointment, string selectedPoliclinic, string doctorName, DateTime appointmentDay)
        {
            // Your logic to create appointment based on the form data goes here
           appointment.Policlinic = selectedPoliclinic;
           appointment.DoctorName = doctorName;
           appointment.AppointmentDay = appointmentDay;
           _context.Appointments.Add(appointment);
           await _context.SaveChangesAsync();
           

            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        // Other methods can be added as needed
    }
}