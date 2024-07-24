using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace datacapture.Modal
{
    public class Datacontext:DbContext
    {


        public Datacontext(DbContextOptions<Datacontext> options) : base(options)
        {
        }

        public virtual DbSet<Data> Datas { get; set; }
        public virtual DbSet<ErrorResponse> ErrorResponses { get; set; }
      

        public virtual DbSet<User> Users { get; set; }
        
    }
}
