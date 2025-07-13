using HorasExtrasAppClean.Models;
using Microsoft.EntityFrameworkCore;

namespace HorasExtrasAppClean.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<OvertimeRecord> OvertimeRecords { get; set; }
    }
}
