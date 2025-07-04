
using Microsoft.EntityFrameworkCore;
using HorasExtrasAppClean.Models;

namespace HorasExtrasAppClean.Data
{
    public class OvertimeDbContext : DbContext
    {
        public OvertimeDbContext(DbContextOptions<OvertimeDbContext> options)
            : base(options) { }

        public DbSet<OvertimeEntry> OvertimeEntries { get; set; }
        public DbSet<OvertimeRow> OvertimeRows { get; set; }
        public DbSet<OvertimeHour> OvertimeHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OvertimeRow>()
                .HasOne(r => r.Entry)
                .WithMany(e => e.Rows)
                .HasForeignKey(r => r.OvertimeEntryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OvertimeHour>()
                .HasOne(h => h.Row)
                .WithMany(r => r.Hours)
                .HasForeignKey(h => h.OvertimeRowId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class OvertimeEntry
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime WeekDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<OvertimeRow> Rows { get; set; } = new List<OvertimeRow>();
    }

    public class OvertimeRow
    {
        public int Id { get; set; }
        public int OvertimeEntryId { get; set; }
        public OvertimeEntry Entry { get; set; } = null!;
        public string SelectedDays { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? FilePath { get; set; }
        public ICollection<OvertimeHour> Hours { get; set; } = new List<OvertimeHour>();
    }

    public class OvertimeHour
    {
        public int Id { get; set; }
        public int OvertimeRowId { get; set; }
        public OvertimeRow Row { get; set; } = null!;
        public string DayName { get; set; } = null!;
        public double Hours { get; set; }
    }
}
