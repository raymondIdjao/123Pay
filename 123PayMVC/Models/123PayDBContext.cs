using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _123PayMVC.Models
{
    public class _123PayDBContext : DbContext
    {
        public _123PayDBContext(DbContextOptions<_123PayDBContext> options):base(options)
        {

        }

        public DbSet<tblPaymentRequests> tblPaymentRequests { get; set; }
    }
}
