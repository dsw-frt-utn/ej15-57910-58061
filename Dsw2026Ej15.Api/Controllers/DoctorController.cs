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
        public IActionResult Create([FromBody] CreateDoctorDto dto)
        {
            // 1. Validaciones
            if (string.IsNullOrEmpty(dto.Name))
                throw new ValidationException("Name es requerido");

            if (string.IsNullOrEmpty(dto.LicenseNumber))
                throw new ValidationException("LicenseNumber es requerido");

            var speciality = _persistence.GetSpecialityById(dto.SpecialityId);
            if (speciality == null)
                throw new ValidationException("La especialidad no existe");

            // 2. Crear el doctor
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LicenseNumber = dto.LicenseNumber,
                IsActive = true,
                Speciality = speciality
            };

            // 3. Guardar
            _persistence.AddDoctor(doctor);

            return StatusCode(201);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _persistence.GetAllDoctor();
            return Ok(doctors);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var doctor = _persistence.GetDoctor(id);
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
        public IActionResult Delete(Guid id)
        {
            var doctor = _persistence.GetDoctor(id);
            if (doctor == null || !doctor.IsActive)
                throw new ValidationException("El médico no existe o no está activo");

            _persistence.DeleteDoctor(id);

            return NoContent();
        }

    }
}