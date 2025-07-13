using Microsoft.EntityFrameworkCore;
using HorasExtrasAppClean.Models;

namespace HorasExtrasAppClean.Data
{
    public class OvertimeDbContext : DbContext
    {
        public OvertimeDbContext(DbContextOptions<OvertimeDbContext> options) : base(options) { }
        public DbSet<OvertimeRecord> OvertimeRecords { get; set; } = null!;
    }
}
