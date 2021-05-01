using Microsoft.EntityFrameworkCore;

namespace NomadDashboardsAPI.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }
    }
}