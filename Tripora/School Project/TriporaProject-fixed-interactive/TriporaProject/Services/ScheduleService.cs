using Microsoft.EntityFrameworkCore;
using TriporaProject.Models;

namespace TriporaProject.Services
{
    public class ScheduleService
    {
        private readonly TriporaDbContext _db;

        public ScheduleService(TriporaDbContext db)
        {
            _db = db;
        }

       
        public async Task<List<Schedule>> SearchByDateAsync(DateTime date)
        {
            var dayStart = date.Date;
            var dayEnd = dayStart.AddDays(1);

            return await _db.Schedules
                .AsNoTracking()
                .Where(s => s.DepartureTime >= dayStart && s.DepartureTime < dayEnd)
                .OrderBy(s => s.DepartureTime)
                .ToListAsync();
        }
    }
}
