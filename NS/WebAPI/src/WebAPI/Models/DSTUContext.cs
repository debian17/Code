using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class DSTUContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<DeliveryData> DeliveryDatas { get; set; }

        public DSTUContext(DbContextOptions<DSTUContext> options) : base(options) { }
    }
}
