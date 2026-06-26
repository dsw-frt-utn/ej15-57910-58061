using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }
        public async Task AddDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctor(Guid idDoctor)
        {
            var doctor = await GetDoctorById(idDoctor);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            } 
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctor()
        {
            return _context.Doctors.Where(d => d.IsActive);
        }

        public async Task<Doctor?> GetDoctorById(Guid idDoctor)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == idDoctor && d.IsActive);
        }

        public async Task<Speciality?> GetSpecialityById(Guid id)
        {
            return await _context.Specialities.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task UpdateDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
