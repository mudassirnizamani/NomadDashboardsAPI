using Microsoft.EntityFrameworkCore;
using NomadDashboardsAPI.Models;

namespace NomadDashboardsAPI.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }

        public DbSet<ClientQuestion> ClientQuestionsAnswers { get; set; }
    }
}