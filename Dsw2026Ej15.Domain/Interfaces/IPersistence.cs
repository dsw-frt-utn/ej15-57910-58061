using Dsw2026Ej15.Domain.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task AddDoctor(Doctor doctor);
        Task DeleteDoctor(Guid idDoctor);
        Task<Doctor?> GetDoctorById(Guid idDoctor);
        Task<IEnumerable<Doctor>> GetAllDoctor();
        Task <Speciality?> GetSpecialityById(Guid id);
        Task UpdateDoctor(Doctor doctor);
    }
}
