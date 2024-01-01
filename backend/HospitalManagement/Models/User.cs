using HospitalManagement.Models;

namespace HospitalManagement;

public class User
{
    public DateTime CreatedDate { get; set; }
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; } // ADMIN or PATIENT

    // Navigation property for Appointments
    public ICollection<Appointment> Appointments { get; set; }
}
