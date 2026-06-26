namespace Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.models;
using System.Text.Json;

public class PersistenceInMemory : IPersistence
{
    private List<Doctor> _doctors = [];
    private List<Speciality> _specialities = [];
    public PersistenceInMemory()
    {
        LoadSpecialities();
    }
    public async Task AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }
    public async Task DeleteDoctor(Guid idDoctor)
    {
        var doctor = await GetDoctorById(idDoctor);
        if (doctor != null)
        {
            doctor.desactivate();
        }
    }
    public async Task<Doctor?> GetDoctorById(Guid idDoctor)
    {
        return _doctors.FirstOrDefault(d => d.Id == idDoctor);
    }
    public async Task <IEnumerable<Doctor>> GetAllDoctor()
    {
        return _doctors.Where(d => d.IsActive).ToList();
    }

    public async Task<Speciality?> GetSpecialityById(Guid id)
    {
        return _specialities.FirstOrDefault(s => s.Id == id);
    }
    public async Task UpdateDoctor(Doctor doctor)
    {
        _doctors.Remove(doctor);
        _doctors.Add(doctor);
    }

    private async void LoadSpecialities()
    {
        var json = await File.ReadAllTextAsync("specialities.json");
        _specialities = JsonSerializer.Deserialize<List<Speciality>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

}
