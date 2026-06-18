namespace Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.models;
using System.Text.Json;

public class PersistenceInMemory : IPersistence
{
    public List<Doctor>? Doctors { get; set; }
    private List<Speciality>? Specialities { get; set; }
    public PersistenceInMemory()
    {
        Doctors = new List<Doctor>();
        Specialities = new List<Speciality>();
        LoadSpecialities();
    }
    public void AddDoctor(Doctor doctor)
    {
        Doctors.Add(doctor);
    }
    public void DeleteDoctor(Guid idDoctor)
    {
        var doctor = GetDoctor(idDoctor);
        if (doctor != null)
        {
            doctor.IsActive = false;
        }
    }
    public Doctor GetDoctor(Guid idDoctor)
    {
        return Doctors.FirstOrDefault(d => d.Id == idDoctor);
    }
    public List<Doctor> GetAllDoctor()
    {
        return Doctors.Where(d => d.IsActive).ToList();
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return Specialities.FirstOrDefault(s => s.Id == id);
    }

    private void LoadSpecialities()
    {
        var json = File.ReadAllText("specialities.json");
        Specialities = JsonSerializer.Deserialize<List<Speciality>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

}
