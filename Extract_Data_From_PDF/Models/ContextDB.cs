using Microsoft.EntityFrameworkCore;

namespace Extract_Data_From_PDF.Models
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
            
        }

        public DbSet<PdfData> PdfDatas { get; set; }

        public DbSet<ItRequest> ItRequests { get; set; }
    }
}
