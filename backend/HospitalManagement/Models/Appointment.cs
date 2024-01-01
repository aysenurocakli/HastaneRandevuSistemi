using System;
namespace HospitalManagement.Models
{
    public class Appointment
    {
        public DateTime AppointmentDay { get; set; }
        public int Id { get; set; }
        public string DoctorName { get; set; }
        // Foreign key and Navigation property for User
        public int UserId { get; set; }
        public User User { get; set; }
        public string City { get; set; } // Şehir
        public string District { get; set; } // İlçe
        public string Hospital { get; set; } // Hastane
        public string Policlinic { get; set; } // Poliklinik
        public DateTime AppointmentTime { get; set; } // Randevu saati
    }
}

 