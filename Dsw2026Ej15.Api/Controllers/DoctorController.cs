using Dsw2026Ej15.Api.DTOs;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            // 1. Validaciones
            if (string.IsNullOrEmpty(dto.Name))
                throw new ValidationException("Name es requerido");

            if (string.IsNullOrEmpty(dto.LicenseNumber))
                throw new ValidationException("LicenseNumber es requerido");

            var speciality = await _persistence.GetSpecialityById(dto.SpecialityId);
            if (speciality == null)
                throw new ValidationException("La especialidad no existe");

            // 2. Crear el doctor
            var doctor = new Doctor(dto.Name, dto.LicenseNumber, speciality);

            // 3. Guardar
            _persistence.AddDoctor(doctor);

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _persistence.GetAllDoctor();
            return Ok(doctors);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var doctor = await _persistence.GetDoctorById(id);
            if (doctor == null || !doctor.IsActive)
                throw new ValidationException("El médico no existe o no está activo");

            return Ok(new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality.Name
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var doctor = await _persistence.GetDoctorById(id);
            if (doctor == null || !doctor.IsActive)
                throw new ValidationException("El médico no existe o no está activo");

            await _persistence.DeleteDoctor(id);
            await _persistence.UpdateDoctor(doctor);

            return NoContent();
        }

    }
}