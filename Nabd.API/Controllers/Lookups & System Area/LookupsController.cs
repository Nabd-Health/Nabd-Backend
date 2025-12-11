using Microsoft.AspNetCore.Mvc;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LookupsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LookupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ==========================================
        // 1.  (Enums)
        // ==========================================

      
        [HttpGet("governorates")]
        [ResponseCache(Duration = 3600)] 
        public IActionResult GetGovernorates()
        {
            return Ok(GetEnumList<Governorate>());
        }

      
        [HttpGet("specialties")]
        [ResponseCache(Duration = 3600)]
        public IActionResult GetSpecialties()
        {
            return Ok(GetEnumList<MedicalSpecialty>());
        }


        [HttpGet("blood-types")]
        [ResponseCache(Duration = 3600)]
        public IActionResult GetBloodTypes()
        {
            return Ok(GetEnumList<BloodType>());
        }

       
        [HttpGet("genders")]
        [ResponseCache(Duration = 3600)]
        public IActionResult GetGenders()
        {
            return Ok(GetEnumList<Gender>());
        }

     
        [HttpGet("appointment-types")]
        public IActionResult GetAppointmentTypes()
        {
            return Ok(GetEnumList<AppointmentType>());
        }

        // ==========================================
        // 2. 
        // ==========================================

        /// <summary>
        /// البحث في قاعدة بيانات الأدوية (Autofill).
        /// </summary>
        /// <param name="term">جزء من اسم الدواء (مثال: "Panadol")</param>
        [HttpGet("medications/search")]
        public async Task<IActionResult> SearchMedications([FromQuery] string? term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Ok(new List<object>());

            var medications = await _unitOfWork.Medications.SearchAsync(term);

            var result = medications.Select(m => new
            {
                Id = m.Id,
                Name = $"{m.TradeName} ({m.Strength}) - {m.Form}",
                Details = m.ScientificName
            });

            return Ok(result);
        }

        // ==========================================
        // Helper Methods
        // ==========================================


        private List<LookupDto> GetEnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(e => new LookupDto
                       {
                           Id = Convert.ToInt32(e),
                           Name = e.ToString()
                       })
                       .ToList();
        }

        public class LookupDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}