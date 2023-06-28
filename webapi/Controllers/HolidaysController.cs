using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;
using webapi.Entities.Dto;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHolidayService _holidayService;

        public HolidaysController(ApplicationDbContext context, IHolidayService holidayService)
        {
            _context = context;
            _holidayService = holidayService;
        }

        // GET: api/Holidays
        [HttpGet("GetHolidayByType/{isLunarDay}")]
        public async Task<ActionResult<IEnumerable<HolidayDto>>> GetHolidays(bool isLunarDay)
        {
            if (_context.Holidays == null)
            {
                return NotFound();
            }
            var holiday = await _holidayService.GetHolidays(isLunarDay);
            holiday = holiday.OrderBy(x => x.Month).ThenBy(x => x.Day).ToList();

            if (holiday == null)
            {
                return NotFound();
            }

            return holiday;
        }

        // GET: api/Holidays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HolidayDto>> GetHoliday(int id)
        {
            if (_context.Holidays == null)
            {
                return NotFound();
            }
            var holiday = await _holidayService.GetHoliday(id);

            if (holiday == null)
            {
                return NotFound();
            }

            return holiday;
        }

        [HttpGet("/ByUrl/{url}")]
        public async Task<ActionResult<HolidayDto>> GetHoliday(string url)
        {
            if (_context.Holidays == null)
            {
                return NotFound();
            }
            var holiday = await _holidayService.GetHoliday(url);

            if (holiday == null)
            {
                return NotFound();
            }

            return holiday;
        }

        //// PUT: api/Holidays/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHoliday(int id, HolidayDto holiday)
        //{
        //    if (id != holiday.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(holiday).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HolidayExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Holidays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HolidayDto>> PostHoliday(CreateHolidayModel req)
        {
            if (_context.Holidays == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Holidays'  is null.");
            }
            var holiday = await _holidayService.CreateHoliday(req);

            return CreatedAtAction("GetHoliday", new { id = holiday.Id }, holiday);
        }

        // POST: api/Holidays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<Entities.Holiday>> PostHolidays(List<CreateHolidayModel> holidays)
        {
            if (_context.Holidays == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Holidays'  is null.");
            }

            await _holidayService.CreateHolidays(holidays);

            return Ok();
        }

        //// DELETE: api/Holidays/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteHoliday(int id)
        //{
        //    if (_context.Holidays == null)
        //    {
        //        return NotFound();
        //    }
        //    var holiday = await _context.Holidays.FindAsync(id);
        //    if (holiday == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Holidays.Remove(holiday);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[ApiExplorerSettings(IgnoreApi = true)]
        //public bool HolidayExists(int id)
        //{
        //    return (_context.Holidays?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        //[ApiExplorerSettings(IgnoreApi = true)]
        //public Entities.Holiday Convert(HolidayDto holiday)
        //{
        //    return new Entities.Holiday
        //    {
        //        HolidayName = holiday.HolidayName,
        //        Description = holiday.Description,
        //        Day = holiday.Day,
        //        Month = holiday.Month,
        //    };
        //}

        //[ApiExplorerSettings(IgnoreApi = true)]
        //public HolidayDto ConvertToDto(Holiday holiday)
        //{
        //    return new HolidayDto
        //    {
        //        HolidayName = holiday.HolidayName,
        //        Description = holiday.Description,
        //        Day = holiday.Day,
        //        Month = holiday.Month,
        //    };
        //}

        //public class HolidayDto
        //{
        //    public int Id { get; set; }
        //    public string HolidayName { get; set; }
        //    public string? Description { get; set; }
        //    public bool IsLunarHoliday { get; set; }
        //    public int Day { get; set; }
        //    public int Month { get; set; }

        //}
    }
}
